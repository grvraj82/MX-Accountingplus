<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="BackupRestore.aspx.cs" Inherits="AccountingPlusWeb.Administration.BackupRestore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
    <style type="text/css">
        .DatabaseTable
        {
            text-align: right;
            padding-right: 5px;
        }
        .DataBaseRight
        {
            text-align: left;
            padding-left: 5px;
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
        function togall(refcheckbox) {

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenJobsCount").value);
            if (totalRecords == "1") {
                if (document.getElementById("chkALL").checked == true) {
                    document.getElementById("chkALL").checked = false
                }
                else {
                    document.getElementById("chkALL").checked = true
                }
                ChkandUnchk();
                return;
            }

            var thisForm = document.forms[0];
            var users = thisForm.__BACKUP.length;
            var selectedCount = 0;
            if (thisForm.__BACKUP[refcheckbox].checked) {

                thisForm.__BACKUP[refcheckbox].checked = false;
            }
            else {
                thisForm.__BACKUP[refcheckbox].checked = true;
            }
            ValidateSelectedCount();
        }
        function DeleteJobs() {
            if (GetSeletedCount() > 1) {
                jNotify('Select only one Backup')

                return false;
            }
            if (IsJobSelected()) {
                return confirm(C_DELETE_CONFIRM);
            }
            else {
                return false;
            }
        }

        function RestoreJobs() {
            if (GetSeletedCount() > 1) {
                jNotify('Select only one Backup')

                return false;
            }
            if (IsJobSelected()) {

            }
            else {
                return false;
            }
        }

        function IsJobSelected() {
            var thisForm = document.forms[0];
            var jobs = thisForm.__BACKUP.length;
            var selectedCount = 0;

            if (jobs > 0) {
                for (var item = 0; item < jobs; item++) {
                    if (thisForm.__BACKUP[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__BACKUP.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {

                jNotify(C_SELECT_ONE_BACKUP)

                return false;
            }

        }

        function ChkandUnchk() {

            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = true;
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = false;
                }
            }
            if (document.getElementById("ctl00_PageContent_HiddenJobsCount").value == 0) {

                document.getElementById("chkALL").checked = false;

            }
        }




        function GetSeletedCount() {

            var thisForm = document.forms[0];
            var users = thisForm.__BACKUP.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__BACKUP[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__BACKUP.checked) {
                    selectedCount++
                }
            }
            return selectedCount;

        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenJobsCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;

            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }

        }

        function Help() {
            document.getElementById('HelpBox').style.display = "inline";
        }
        function HelpClose() {
            document.getElementById('HelpBox').style.display = "none";
        }
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image6" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" height="33" width="100%" border="0">
                    <tr class="Top_menu_bg">
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadBackupRestore" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image7" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="5px">
                                    </td>
                                    <td valign="middle" class="HeaderPadding">
                                        <asp:Table ID="Table1" runat="server" CellPadding="1" CellSpacing="0">
                                            <asp:TableRow>
                                                <asp:TableCell ID="TableCellDelete" CssClass="MenuSpliter" align="left" runat="server"
                                                    Visible="true" HorizontalAlign="Left" VerticalAlign="Middle">
                                                    <asp:ImageButton ID="ImageButtonDelete" ToolTip="" CausesValidation="False" SkinID="SettingsImageButtonDelete"
                                                        runat="server" OnClick="ImageButtonDelete_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellBackUp" CssClass="MenuSpliter" align="center" runat="server"
                                                    Visible="true" HorizontalAlign="Left" VerticalAlign="Top">
                                                    <asp:ImageButton ID="ImageButtonBackup" ToolTip="" runat="server" CausesValidation="False"
                                                        SkinID="SettingsImageButtonBackup" OnClick="ImageButtonBackup_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellRestore" CssClass="MenuSpliter" align="center" runat="server"
                                                    Visible="true" HorizontalAlign="Left" VerticalAlign="Top">
                                                    <asp:ImageButton ID="ImageButtonRestore" SkinID="SettingsImageButtonRestore" ToolTip=""
                                                        runat="server" CausesValidation="False" OnClick="ImageButtonRestore_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellButtonHelp" CssClass="MenuSpliter" align="right" runat="server"
                                                    Visible="true" HorizontalAlign="Left" VerticalAlign="Top">
                                                    <asp:Image ID="ImageHelp" SkinID="SettingsImageButtonHelp" ToolTip="" runat="server"
                                                        CausesValidation="False" onclick="Help()" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellRefersh" CssClass="MenuSpliter" align="center" runat="server"
                                                    Visible="true" HorizontalAlign="Left" VerticalAlign="Top">
                                                    <asp:ImageButton ID="ImageButtonRefresh" SkinID="SettingsImageButtonRefresh" ToolTip=""
                                                        runat="server" CausesValidation="False" OnClick="ImageButtonRefresh_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellBackUpRestore" CssClass="MenuSpliter" align="center"
                                                    runat="server" Visible="false" HorizontalAlign="Left" VerticalAlign="Middle">
                                                    &nbsp;
                                                    <asp:Label ID="LabelHeadingBackupandRestore" runat="server" SkinID="TotalResource"
                                                        Text=""></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="TableRowSQLCreditinals" runat="server" visible="false">
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <table align="center" cellpadding="0" cellspacing="0" border="0" class="table_border_org"
                                            width="50%">
                                            <tr class="Grid_tr">
                                                <td colspan="2" align="left" valign="top">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                        <tr class="Top_menu_bg">
                                                            <td width="60%" align="left" valign="middle">
                                                                &nbsp;<asp:Label ID="LabelAlert" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                            </td>
                                                            <td align="right" width="10%" valign="middle">
                                                                <asp:Image ID="Image3" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                            </td>
                                                            <td align="left" width="30%">
                                                                <asp:Label ID="LabelRequiredField" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="3">
                                                </td>
                                            </tr>
                                            <tr class="Grid_tr">
                                                <td align="right" height="30">
                                                    <asp:Label ID="LabelFileNAmeText" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td valign="middle" align="left" colspan="2">
                                                    <asp:Label ID="LabelFileName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr class="Grid_tr">
                                                <td align="right" height="30">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td valign="middle" align="left" colspan="2">
                                                    <asp:Label ID="LabelDate" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="Grid_tr">
                                                <td colspan="2" align="left" valign="top">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                        <tr class="Top_menu_bg">
                                                            <td width="60%" align="left" valign="middle">
                                                                &nbsp;<asp:Label ID="Label3" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="Grid_tr">
                                                <td align="right" height="30">
                                                    <asp:Label ID="LabelUser" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td valign="middle" align="left" colspan="2">
                                                    <asp:TextBox ID="TextBoxUser" runat="server" MaxLength="30" Width="150px"></asp:TextBox>
                                                    <asp:Image ID="Imageuser" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                        padding-left: 5px;" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxUser"
                                                        runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr class="Grid_tr">
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelPass" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                                <td valign="middle" align="left">
                                                    <asp:TextBox ID="TextBoxPass" runat="server" MaxLength="30" Width="150px" TextMode="Password"></asp:TextBox>
                                                    <asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                        padding-left: 5px;" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxPass"
                                                        runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr align="center" id="TableRowSQLCreditinalsButtons" runat="server" visible="false">
                        <td colspan="4" height="35">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td height="10">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="ButtonRestoreOK" runat="server" CausesValidation="true" Text="" CssClass="Login_Button"
                                            OnClick="ImageRestoreOk_Click" />
                                        <asp:Button ID="ButtonRestoreCancel" runat="server" CausesValidation="false" Text=""
                                            CssClass="Login_Button" OnClick="ButtonRestoreCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="10">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="Grid_tr" id="TablerowBackupName" runat="server" visible="false">
                        <td valign="top">
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <table align="center" cellpadding="0" cellspacing="0" border="0" class="table_border_org"
                                            width="50%">
                                            <tr>
                                                <td colspan="2" align="left" valign="top">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                        <tr class="Top_menu_bg">
                                                            <td width="60%" align="left" valign="middle">
                                                                &nbsp;<asp:Label ID="Label1" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                            </td>
                                                            <td align="right" width="10%" valign="middle">
                                                                <asp:Image ID="Image2" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                            </td>
                                                            <td align="left" width="30%">
                                                                <asp:Label ID="Label2" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="3" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="30">
                                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td valign="middle" align="left">
                                                    <asp:TextBox ID="TextBoxFileName" runat="server" MaxLength="80" ViewStateMode="Disabled"
                                                        Width="150px"></asp:TextBox><asp:Label ID="LabelFileNameAppend" runat="server" Text=""></asp:Label>
                                                    <asp:Image ID="Image4" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                        padding-left: 5px;" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextBoxFileName"
                                                        runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="30">
                                                    <asp:Label ID="LabelDestination" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td valign="middle" align="left">
                                                    <asp:TextBox ID="TextBoxDestination" runat="server" MaxLength="80" ViewStateMode="Disabled"
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="30">
                                                    <asp:Label ID="LabelExample" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td valign="middle" align="left">
                                                    <asp:Label ID="LabelExamplePath" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="30">
                                                    <asp:Label ID="LabelNote" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td valign="middle" align="left">
                                                    <asp:Label ID="LabelNoteDes" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr align="center" id="TablerowBackupNameButton" runat="server" visible="false">
                        <td colspan="4" height="35">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td height="10">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" Text="" CausesValidation="true" CssClass="Login_Button"
                                            OnClick="ImageBackupOk_Click" />
                                        <asp:Button ID="Button2" runat="server" CausesValidation="false" Text="" CssClass="Login_Button"
                                            OnClick="ButtonBackupCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="10">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        <br />
                            <asp:Table EnableViewState="false" ID="TableBackUpDetails" CellPadding="3" Width="50%" CellSpacing="1"
                                BorderWidth="0" runat="server" CssClass="Table_bg " SkinID="Grid">
                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                    <asp:TableHeaderCell Wrap="false" CssClass="H_title" HorizontalAlign="Right">Sql Server</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCell1" Text="" HorizontalAlign="Left"
                                        CssClass="H_title" Width="10%">Details</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow Height="25" CssClass="GridRow  Grid_tr">
                                    <asp:TableCell Width="50%" Font-Bold="true" CssClass="DatabaseTable">
                                    Database Name
                                    </asp:TableCell>
                                    <asp:TableCell Width="50%" CssClass="DataBaseRight">
                                        <asp:Label ID="LabelDatabaseName" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="25" CssClass="GridRow  Grid_tr">
                                    <asp:TableCell Font-Bold="true" CssClass="DatabaseTable">
                                    Version
                                    </asp:TableCell>
                                    <asp:TableCell CssClass="DataBaseRight">
                                        <asp:Label ID="LabelVersion" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="25" CssClass="GridRow  Grid_tr">
                                    <asp:TableCell Font-Bold="true" CssClass="DatabaseTable">
                                     Edition
                                    </asp:TableCell>
                                    <asp:TableCell CssClass="DataBaseRight">
                                        <asp:Label ID="LabelEdition" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="25" CssClass="GridRow  Grid_tr">
                                    <asp:TableCell Font-Bold="true" CssClass="DatabaseTable">
                                     Size limit 
                                    </asp:TableCell>
                                    <asp:TableCell CssClass="DataBaseRight">
                                        <asp:Label ID="LabelOriginalSize" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="25" CssClass="GridRow  Grid_tr">
                                    <asp:TableCell Font-Bold="true" CssClass="DatabaseTable">
                                     Current size 
                                    </asp:TableCell>
                                    <asp:TableCell CssClass="DataBaseRight">
                                        <asp:Label ID="LabelCurrentSize" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                            <asp:Table ID="TableWarningMessage" Visible="false" CellSpacing="1" CellPadding="3"
                                Width="50%" runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                    <asp:TableHeaderCell ID="TableHeaderCellDivName" CssClass="LabelWarningFont" ColumnSpan="2"
                                        HorizontalAlign="Left">
                                        <asp:Label ID="LabelWarning" runat="server" Text=""></asp:Label>
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow CssClass="GridRow">
                                    <asp:TableCell ID="TableCellWarningImage" HorizontalAlign="Center" Width="20%">
                                        <asp:Image ID="ImageWarning" runat="server" SkinID="PermessionsAndLimitsCritical" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell1" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                                        <p class="LabelLoginFont">
                                        </p>
                                        <asp:Label ID="LabelWarningMessage" runat="server" Text=""></asp:Label>
                                        <%--<p class="LabelWarningFont"></p>--%>
                                        <p class="LabelLoginFont">
                                        </p>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
        <asp:Panel ID="PanelMainBackUpandRestore" runat="server">
            <tr style="height: 550px">
                <td>
                </td>
                <td valign="top" class="CenterBG" align="center">
                    <table id="TableSummary" width="98%" align="center" border="0" runat="server" cellpadding="0"
                        cellspacing="0" class="TableGridColor">
                        <tr class="Grid_tr">
                            <td align="center">
                                <asp:Table EnableViewState="false" ID="TableBackupSummary" CellPadding="3" Width="100%"
                                    CellSpacing="1" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                        <asp:TableHeaderCell Wrap="false" Width="30px" HorizontalAlign="Left">
                                                    <input id="chkALL" onclick="ChkandUnchk()" type="checkbox"  />
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" Width="30px"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellUser" Text="" HorizontalAlign="Left"
                                            CssClass="H_title" Width="10%"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellSize" Text="" HorizontalAlign="Left"
                                            CssClass="H_title" Width="15%"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellJobName" Text="" HorizontalAlign="Left"
                                            CssClass="H_title"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellType" Text="" HorizontalAlign="Left"
                                            CssClass="H_title"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellServerName" Text="" HorizontalAlign="Left"
                                            CssClass="H_title"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellFileName" HorizontalAlign="Left"
                                            CssClass="H_title"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell ID="TableHeaderCellPath" HorizontalAlign="Left" CssClass="H_title"></asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                </asp:Table>
                                <asp:Label ID="LabelNoJobs" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                <asp:HiddenField ID="HiddenJobsCount" Value="0" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
    </table>
    <div id="HelpBox" style="display: none; width: 425px; height: 160px; position: fixed;
        padding: 0px; left: 35%; top: 25%; z-index: 1">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" id="tblPopup">
            <tr>
                <td height="25" class="AboutHeader">
                    <table width="100%">
                        <tr width="100%">
                            <td width="95%">
                                <asp:Label ID="LabelAboutHeader" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="95%" align="right">
                                <asp:Image ID="ImageInformation" SkinID="InnerPageInformation" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" CssClass="AddressText" Text="" Style="padding-left: 5px;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelNote1" runat="server" CssClass="AddressText" Text="" Style="padding-left: 5px;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 3px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelNote2" runat="server" CssClass="AddressText" Text="" Style="padding-left: 5px;"></asp:Label>
                    <asp:Label ID="LabelpathInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 3px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelNote3" runat="server" CssClass="AddressText" Text="" Style="padding-left: 5px;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 3px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelNote4" runat="server" CssClass="AddressText" Text="" Style="padding-left: 5px;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 3px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" CssClass="AddressText" Text="" Style="padding-left: 5px;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 3px">
                </td>
            </tr>
            <tr>
                <td class="footer">
                    <a href="#" onclick="HelpClose()">
                        <asp:Image ID="ImageOk" SkinID="InnerPageOk1" runat="server" />
                    </a>
                </td>
            </tr>
        </table>
    </div>
    <div id="RestoreConfirm" style="width: 425px; height: 160px; position: fixed; padding: 0px;
        left: 35%; top: 25%; z-index: 1" runat="server" visible="false">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" id="tblPopupConfirm">
            <tr>
                <td height="25" class="AboutHeader">
                    <table width="100%">
                        <tr width="100%">
                            <td width="95%">
                                <asp:Label ID="Label8" runat="server" CssClass="LabelAboutHeader" Text=""></asp:Label>
                            </td>
                            <td width="95%" align="right">
                                <asp:Image ID="Image5" SkinID="InnerPageInformation" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" CssClass="AddressText" Text="" Style="padding-left: 5px;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                    <asp:Label ID="Label9" runat="server" CssClass="AddressText" Text="" Style="padding-left: 5px;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Buttonokconfirm" CausesValidation="false" runat="server" Text=""
                        OnClick="Buttonokconfirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="ButtonCancelConfirm" CausesValidation="false" runat="server" Text=""
                        OnClick="ButtonCancelConfirm_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
