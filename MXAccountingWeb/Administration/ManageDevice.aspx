<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    Inherits="AdministrationManageDevices" CodeBehind="ManageDevice.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageContent" ID="PC" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellMFPs();
        Meuselected("Device");
        function DeleteMfps() {
            if (IsDeleteMfpSelected()) {
                return confirm(C_DEVICE_DELETE_CONFIRM);
            }
            else {
                return false;
            }
        }

        function IsMfpSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__MfpID.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__MfpID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__MfpID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                jNotify(C_SELECT_ONE_MFP)
                return false;
            }
        }

        function UpdateMfpDetails() {
            if (IsMfpSelected()) {
                if (GetSeletedCount() > 1) {
                }
            }
            else {
                return false;
            }
        }

        function RegisterMfp() {
            if (IsMfpSelected()) {
                if (GetSeletedCount() > 1) {
                }
            }
            else {
                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__MfpID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__MfpID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__MfpID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }

        function UpdatemfpDetails() {
            if (IsUserSelected()) {
                if (GetSeletedCount() > 1) {
                    for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                    jNotify(C_SELECT_ONE_MFP)

                    return false;
                }
            }
            else {
                return false;
            }
        }

        function IsUserSelected() {

            try {
                var thisForm = document.forms[0];
                var users = thisForm.__MfpID.length;
                var selectedCount = 0;

                if (users > 0) {
                    for (var item = 0; item < users; item++) {
                        if (thisForm.__MfpID[item].checked) {
                            selectedCount++
                            return true;
                        }
                    }
                }
                else {
                    if (thisForm.__MfpID.checked) {
                        selectedCount++
                        return true;
                    }
                }

                if (selectedCount == 0) {
                    jNotify(C_SELECT_ONE_MFP)
                    return false;
                }

            }
            catch (Error) {
                jNotify(C_SELECT_ONE_MFP)
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
            if (document.getElementById("ctl00_PageContent_HiddenFieldDeviceCount").value == 0) {
                document.getElementById("chkALL").checked = false;
            }
        }

        function togall(refcheckbox) {

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldDeviceCount").value);
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
            var users = thisForm.__MfpID.length;
            var selectedCount = 0;
            if (thisForm.__MfpID[refcheckbox - 1].checked) {
                thisForm.__MfpID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__MfpID[refcheckbox - 1].checked = true; ;
            }
            ValidateSelectedCount();
        }

        function CopyToClipboard(stringTobeCopied) {
            if (window.clipboardData && clipboardData.setData) {
                clipboardData.setData("Text", stringTobeCopied);
            }
            else {

            }
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldDeviceCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }

        function IsDeleteMfpSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__MfpID.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__MfpID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__MfpID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                jNotify(C_DEVICE_DELETE)
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="TimerRefresh" runat="server" Interval="60000" OnTick="TimerRefresh_Tick">
            </asp:Timer>
            <div id="content" style="display: none;">
            </div>
            <div id="divScript" runat="server" style="display: none;">
            </div>
            <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="500">
                <tr height="33">
                    <td align="right" valign="top" style="width: 1px">
                        <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
                    </td>
                    <td align="left" valign="top" class="CenterBG">
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" height="33">
                            <tr class="Top_menu_bg">
                                <td class="HeadingMiddleBg" style="width: 10%">
                                    <div style="padding: 4px 10px 0px 10px;">
                                        <asp:Label ID="LabelHeadDeviceManagement" runat="server" Text=""></asp:Label></div>
                                </td>
                                <td>
                                    <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                </td>
                                <td style="width: 2%">
                                </td>
                                <td align="left" valign="middle" width="88%" class="HeaderPaddingL">
                                    <asp:Table ID="Table1" runat="server" CellPadding="3" CellSpacing="0">
                                        <asp:TableRow>
                                            <asp:TableCell runat="server" ID="aspImageButtonNewDevice" VerticalAlign="Top">
                                                <asp:ImageButton SkinID="ManageMfpSimgadd" ID="ImageButtonNewDevice" ToolTip="New Device"
                                                    runat="server" OnClick="ButtonNewDevice_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" ID="aspImageButtonUpdateDetails" runat="server"
                                                VerticalAlign="Top">
                                                <asp:ImageButton SkinID="ManageMfpSimgedit" ToolTip="Edit" runat="server" ID="ImageButtonUpdateDetails"
                                                    OnClick="ButtonUpdateDetails_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" ID="TableCellLock" runat="server" VerticalAlign="Top" Visible="false">
                                                <asp:ImageButton runat="server" ID="ImageButtonLock" ToolTip="" SkinID="ManageDeviceDisableDevice"
                                                    OnClick="ImageButtonLock_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" ID="TableCellReset" runat="server" VerticalAlign="Top" Visible="false">
                                                <asp:ImageButton runat="server" ID="ImageButtonReset" ToolTip="" SkinID="ManageDeviceEnableDevice"
                                                    OnClick="ImageButtonReset_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" runat="server" ID="aspImageButtonDelete" VerticalAlign="Top">
                                                <asp:ImageButton SkinID="ManageMfpSimgDelete" ToolTip="Delete" ID="ImageButtonDelete"
                                                    runat="server" OnClick="ButtonDelete_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" runat="server" ID="aspImageButtonDiscover"
                                                VerticalAlign="Top">
                                                <asp:ImageButton SkinID="ManageMfpSimgDiscover" ToolTip="Discover" ID="ImageButtonDiscover"
                                                    runat="server" OnClick="ButtonDiscover_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" ID="TableCell10" align="center" runat="server"
                                                Visible="true" HorizontalAlign="Left" VerticalAlign="Top">
                                                <asp:ImageButton ID="ImageButtonCardSettings" ToolTip="" runat="server" CausesValidation="False"
                                                    SkinID="ManageDeviceSettings" OnClick="ImageButtonSetting_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" runat="server" ID="TableCellRegister" VerticalAlign="Top">
                                                <asp:ImageButton ID="ImageButtonRegister" SkinID="ManageDeviceRegister" ToolTip=""
                                                    runat="server" OnClick="ImageButtonRegister_Click" OnClientClick="return RegisterMfp()" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" runat="server" ID="TableCell8" VerticalAlign="Top"
                                                Style="display: none">
                                                <asp:ImageButton ID="ImageButtonFleetReports" SkinID="ManageDevicePrinterWithChart"
                                                    ToolTip="Device Fleet Reports" runat="server" OnClick="ImageImageButtonFleetReports_Click"
                                                    OnClientClick="return UpdatemfpDetails()" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" runat="server" ID="TableCell12" VerticalAlign="Top"
                                                Visible="false">
                                                <asp:ImageButton ID="ImageButtonAssignToGroups" SkinID="ManageDeviceMFPGroup" ToolTip=""
                                                    runat="server" OnClick="ImageButtonAssignToGroups_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" runat="server" ID="TableCell13" VerticalAlign="Top"
                                                Visible="true">
                                                <asp:ImageButton ID="ImageButtonURLs" SkinID="ManageDeviceMFPURLs" ToolTip="" runat="server"
                                                    OnClick="ImageButtonURLs_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="MenuSpliter" runat="server" ID="aspImageButtonRefresh" VerticalAlign="Top">
                                                <asp:ImageButton ID="ImageButtonRefresh" SkinID="ManageMfpSimgRefresh" ToolTip=""
                                                    runat="server" OnClick="ImageButtonRefresh_Click" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td valign="top" align="center" class="CenterBG">
                        <table width="98%" align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelURLs" Visible="false" runat="server">
                                        <table align="center" width="60%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                            <tr class="Grid_tr">
                                                <td>
                                                    <asp:Table ID="Table2" CellSpacing="1" CellPadding="3" Width="100%" BorderWidth="0"
                                                        runat="server" CssClass="Table_bg" SkinID="Grid">
                                                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                            <asp:TableHeaderCell ID="TableHeaderCellApplicationName" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellApplicationUrl" HorizontalAlign="Left" CssClass="H_title"
                                                                ColumnSpan="2" Wrap="false"></asp:TableHeaderCell>
                                                        </asp:TableHeaderRow>
                                                        <asp:TableRow CssClass="GridRow">
                                                            <asp:TableCell ID="TableCellEAC" HorizontalAlign="Left" Font-Bold="true" ColumnSpan="3"
                                                                Wrap="false">
                                                                <asp:Label ID="LabelRequired" runat="server" Text="Label"></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="GridRow">
                                                            <asp:TableCell ID="TableCellEAUI" CssClass="AddressText" align="Left" Wrap="false"></asp:TableCell>
                                                            <asp:TableCell align="Left" Wrap="false">
                                                                <asp:Label ID="LabelEAUI" runat="server" Text=""></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="GridRow">
                                                            <asp:TableCell ID="TableCellEAWS" CssClass="AddressText" align="Left" Wrap="false"></asp:TableCell>
                                                            <asp:TableCell align="Left" Wrap="false">
                                                                <asp:Label ID="LabelEAWebService" runat="server" Text=""></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="GridRow">
                                                            <asp:TableCell ID="TableCellPR" HorizontalAlign="Left" Font-Bold="true" ColumnSpan="3"
                                                                Wrap="false"></asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="GridRow">
                                                            <asp:TableCell ID="TableCellPRUI" CssClass="AddressText" align="Left" Wrap="false"></asp:TableCell>
                                                            <asp:TableCell align="Left" Wrap="false">
                                                                <asp:Label ID="LabelPrintReleaseUI" runat="server" Text=""></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="GridRow">
                                                            <asp:TableCell ColumnSpan="2" ID="TableCell14" CssClass="AddressText" align="center"
                                                                Wrap="false">
                                                                <asp:Button ID="ButtonURLCancel" runat="server" CssClass="Cancel_button" Text=""
                                                                    OnClick="ButtonURLCancel_Click" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center">
                                    <asp:Panel ID="PanelMainDevices" Visible="true" runat="server">
                                        <table width="98%" border="0" cellpadding="0" cellspacing="0" class="">
                                            <tr>
                                                <td colspan="2" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" width="250px" style="background-color: White;">
                                                        <tr style="background-color: White; height: 10px;">
                                                            <td>
                                                                <asp:TextBox BorderWidth="0" AutoPostBack="true" OnTextChanged="TextBoxSearch_OnTextChanged"
                                                                    ToolTip="" CssClass="SearchTextBox" Text="*" ID="TextBoxSearch" runat="server"
                                                                    Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButtonGo" runat="server" SkinID="SearchList" OnClick="ImageButtonGo_Click" />
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxSearch"
                                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" CompletionInterval="1000"
                                                                    ServiceMethod="GetMFPHostNameForSearch" ServicePath="~/WebServices/ContextSearch.asmx"
                                                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButtonCancelSearch" runat="server" ToolTip="" SkinID="CancelSearch"
                                                                    OnClick="ImageButtonCancelSearch_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right" style="white-space: nowrap">
                                                    <asp:Table ID="Table3" runat="server" CellPadding="2" CellSpacing="0">
                                                        <asp:TableRow>
                                                            <asp:TableCell>
                                                                <asp:Label ID="LabelPageSize" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:DropDownList ID="DropDownPageSize" CssClass="Normal_FontLabel" runat="server"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownPageSize_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="true">50</asp:ListItem>
                                                                    <asp:ListItem>100</asp:ListItem>
                                                                    <asp:ListItem>200</asp:ListItem>
                                                                    <asp:ListItem>500</asp:ListItem>
                                                                    <asp:ListItem>1000</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:Label ID="LabelPage" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:DropDownList ID="DropDownCurrentPage" runat="server" AutoPostBack="true" CssClass="Normal_FontLabel"
                                                                    OnSelectedIndexChanged="DropDownCurrentPage_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:Label ID="LabelTotalRecordsTitle" runat="server" SkinID="TotalResource" Text=""></asp:Label>:
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:Label ID="LabelTotalRecordsValue" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 1px">
                                                </td>
                                            </tr>
                                            <tr class="Grid_tr">
                                                <td colspan="2">
                                                    <asp:Table EnableViewState="false" ID="TableDevices" CellSpacing="0" CellPadding="0"
                                                        Width="100%" BorderWidth="0" runat="server" SkinID="Grid" CssClass="Table_bg">
                                                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                            <asp:TableHeaderCell HorizontalAlign="Left" CssClass="Grid_topbg1"><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell Width="30" CssClass="Grid_topbg1" Wrap="false">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellHostName" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false" Text=""></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellIPAddress" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                                  <asp:TableHeaderCell ID="TableHeaderCellModelName" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false">Model Name</asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCell1Location" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellSerialNumber" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellLogOnMode" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellisServerRegistered" HorizontalAlign="Left"
                                                                CssClass="H_title" Wrap="false" Text="Is MFP Registered"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellEAMActive" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellCardReaderType" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellUseSso" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellLockDomain" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellDeviceActive" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellDeviceStatus" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false" Text="MFP Status"></asp:TableHeaderCell>
                                                        </asp:TableHeaderRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td height="5">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Table ID="TableWarningMessage" Visible="false" CellSpacing="1" CellPadding="3"
                                        Width="50%" runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
                                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                            <asp:TableHeaderCell ID="TableHeaderCellDivName" CssClass="LabelWarningFont" ColumnSpan="2"
                                                HorizontalAlign="Left"></asp:TableHeaderCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableRow CssClass="GridRow">
                                            <asp:TableCell ID="TableCellWarningImage" HorizontalAlign="Center" Width="20%">
                                                <asp:Image ID="ImageWarning" runat="server" SkinID="PermessionsAndLimitsCritical" />
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell15" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                                           <p class="LabelLoginFont"> </p>
                                           <p class="LabelWarningFont">There are no Device(s) created.</p>
                                           <p class="LabelLoginFont"></p>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="HiddenFieldDeviceCount" Value="0" runat="server" />
                        <asp:HiddenField ID="HiddenFieldCheckedAll" Value="0" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnDecline" OkControlID="buttonOK" CancelControlID="btnDecline" PopupControlID="panelconfirmation">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="panelconfirmation" runat="server">
        <table width="500" cellpadding="6" cellspacing="0" border="0" bgcolor="white">
            <tr>
                <td>
                    <b>Tables and Content</b>
                </td>
          </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="buttonOK" runat="server"Text=" OK " OnClick="buttonOK_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnDecline" runat="server" CssClass="btn" Text=" Cancel " />
                   
                </td>
            </tr>
        </table>
    </asp:Panel>--%>
</asp:Content>
