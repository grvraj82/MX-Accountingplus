<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="AssignMFPsToGroups.aspx.cs" Inherits="AccountingPlusWeb.Administration.AssignMFPsToGroups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellMFPs();
        Meuselected("Device");

        function DeleteUsers() {

            var confirmflag = confirm('Selected MFP group will be deleted.\n Do you want to continue?');
            if (!confirmflag) {
                return false;
            }
            else {
                return true;
            }
        }

        function IsDeviceSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__MfpIP.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__MfpIP[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__MfpIP.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                showDialog('Warning', C_SELECT_ONE_USER, 'warning', 5)
                return false;
            }
        }



        function togall(refcheckbox) {
            debugger;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldMFPCount").value);
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
            var users = thisForm.__MfpIP.length;
            var selectedCount = 0;
            if (thisForm.__MfpIP[refcheckbox].checked) {

                thisForm.__MfpIP[refcheckbox].checked = false;
            }
            else {
                thisForm.__MfpIP[refcheckbox].checked = true;
            }
            ValidateSelectedCount();
        }
        function ChkandUnchk() {
            debugger;
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__MfpIP') {
                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__MfpIP') {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                }
            }
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldMFPCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }
        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__MfpIP.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__MfpIP[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__MfpIP.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }

        function togallIP(refcheckbox) {
            debugger;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldMFPListCount").value);
            if (totalRecords == "1") {
                if (document.getElementById("chkALLMFPList").checked == true) {
                    document.getElementById("chkALLMFPList").checked = false
                }
                else {
                    document.getElementById("chkALLMFPList").checked = true
                }
                ChkandUnchkMFPList();
                return;
            }

            var thisForm = document.forms[0];
            var users = thisForm.__SearchMfpIP.length;
            var selectedCount = 0;
            if (thisForm.__SearchMfpIP[refcheckbox].checked) {

                thisForm.__SearchMfpIP[refcheckbox].checked = false;
            }
            else {
                thisForm.__SearchMfpIP[refcheckbox].checked = true;
            }
            ValidateSelectedCountList();
        }



        function ChkandUnchkMFPList() {
            debugger;
            if (document.getElementById('chkALLMFPList').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__SearchMfpIP') {
                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__SearchMfpIP') {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                }
            }
        }


        function ValidateSelectedCountList() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldMFPListCount").value);

            if (totalRecords == GetSeletedCountList()) {
                var checkBoxAll = document.getElementById("chkALLMFPList").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALLMFPList").checked = false;
            }
        }
        function GetSeletedCountList() {
            var thisForm = document.forms[0];
            var users = thisForm.__SearchMfpIP.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__SearchMfpIP[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__SearchMfpIP.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }

               
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="500">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image1" SkinID="HeadingLeft" runat="server" />
            </td>
            <td height="33" align="left" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                    <tr>
                        <td valign="top">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadAssignMFPToGroups" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image2" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="5px">
                                    </td>
                                    <td align="left" valign="middle" class="HeaderPadding" style="display: none">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td class="f10b TextWrapping" height="35" valign="middle" align="left">
                                                    <asp:Label ID="LabelDeviceGroups" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 500">
            <td height="5">
            </td>
            <td class="CenterBG" valign="top">
                <asp:Panel CssClass="CenterBG" ID="PanelMainData" runat="server">
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr style="height: 500px">
                            <td valign="top">
                                <table align="center" cellpadding="0" cellspacing="0" border="0" class="" width="98%">
                                    <tr>
                                        <td colspan="2" valign="top">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                <tr align="center">
                                                    <td valign="top">
                                                        <asp:Table ID="TableMainData" Width="98%" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                                            runat="server">
                                                            <asp:TableRow>
                                                                <asp:TableCell VerticalAlign="Top">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="250">
                                                                                <table cellpadding="0" cellspacing="0" width="250px">
                                                                                    <tr>
                                                                                        <td colspan="2" style="height: 10px">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="background-color: White; height: 10px">
                                                                                        <td>
                                                                                            <asp:TextBox BorderWidth="0" OnTextChanged="SearchTextBox_OnTextChanged" ToolTip=""
                                                                                                CssClass="SearchTextBox" ID="TextBoxGroupSearch" AutoPostBack="true" Text="*"
                                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonGo" OnClick="ImageButtonGo_Click" runat="server"
                                                                                                SkinID="SearchList" />
                                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxGroupSearch"
                                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" CompletionInterval="1000"
                                                                                                ServiceMethod="GetMFPGroupForSearch" CompletionListCssClass="autocomplete_completionListElement"
                                                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                            </cc1:AutoCompleteExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonCancelSearch" runat="server" SkinID="CancelSearch"
                                                                                                OnClick="ImageButtonCancelSearch_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="3" style="height: 1px">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td valign="top" style="width: 5px; white-space: normal">
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                            <td width="95%" valign="middle">
                                                                                <table cellpadding="3" cellspacing="3">
                                                                                    <tr>
                                                                                        <td style="white-space: nowrap">
                                                                                            <asp:Label ID="LabelListofMFPandMFPGroups" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            :
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="LabelSelectedGroupName" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="1%">
                                                                                <table cellpadding="3" cellspacing="3">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonAddItem" Visible="true" ToolTip="" SkinID="AddItem"
                                                                                                runat="server" OnClick="ImageButtonAddItem_Click" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonRemoveItem" Visible="false" ToolTip="" SkinID="RemoveItem"
                                                                                                runat="server" OnClick="ImageButtonRemoveItem_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:TableCell>
                                                                <asp:TableCell Visible="false" HorizontalAlign="Left" VerticalAlign="Top">
                                                                    <table cellpadding="3" cellspacing="3">
                                                                        <tr>
                                                                            <td style="width: 30px">
                                                                            </td>
                                                                            <td>
                                                                                <table cellpadding="3" cellspacing="0" width="250px">
                                                                                    <tr style="background-color: White">
                                                                                        <td>
                                                                                            <asp:TextBox ID="TextBoxSearch" Text="*" AutoPostBack="true" OnTextChanged="TextBoxSearch_OnTextChanged"
                                                                                                Width="100%" BorderWidth="0" ToolTip=""
                                                                                                runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonSearch" SkinID="SearchList" runat="server" OnClick="ImageButtonSearch_Click" />
                                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TextBoxSearch"
                                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" CompletionInterval="1000"
                                                                                                ServiceMethod="GetMFPHostNameForSearch" CompletionListCssClass="autocomplete_completionListElement"
                                                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                            </cc1:AutoCompleteExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonClearDevices" runat="server" SkinID="CancelSearch"
                                                                                                OnClick="ImageButtonClearDevices_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButtonCancelAction" Visible="false" ToolTip="" SkinID="CancelAction"
                                                                                    runat="server" OnClick="ImageButtonCancelAction_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell VerticalAlign="Top">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td valign="top" class="Grid_tr">
                                                                                <asp:HiddenField ID="HiddenFieldSelectedGroup" Value="" runat="server" />
                                                                                <asp:Table ID="TableMFPGroups" SkinID="Grid" CssClass="Table_bg" CellSpacing="1"
                                                                                    CellPadding="3" Width="250px" BorderWidth="0" runat="server">
                                                                                </asp:Table>
                                                                            </td>
                                                                            <td valign="top" style="width: 5px; white-space: normal">
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                            <td valign="top" style="width: 98%" class="Grid_tr">
                                                                                <asp:Table EnableViewState="false" CssClass="Table_bg" ID="TableDevices" CellSpacing="1"
                                                                                    CellPadding="3" Width="100%" BorderWidth="0" SkinID="Grid" runat="server">
                                                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                        <asp:TableHeaderCell Width="30" HorizontalAlign="Left">
                                                                                    <input id="chkALL1" onclick="ChkandUnchk1()" type="checkbox" />
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell Wrap="false" HorizontalAlign="Left">
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ID="TableHeaderCellHostName" Wrap="false" HorizontalAlign="Left"
                                                                                            CssClass="H_title" Text=""> 
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ID="TableHeaderCellIPAddress" Wrap="false" HorizontalAlign="Left"
                                                                                            CssClass="H_title"> 
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ID="TableHeaderCellModel" Wrap="false" HorizontalAlign="Left"
                                                                                            CssClass="H_title"> 
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ID="TableHeaderCell1Location" Wrap="false" HorizontalAlign="Left"
                                                                                            CssClass="H_title"></asp:TableHeaderCell>
                                                                                    </asp:TableHeaderRow>
                                                                                </asp:Table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:TableCell>
                                                                <asp:TableCell Visible="false" VerticalAlign="Top">
                                                                    <asp:Table ID="TableFilter" Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                                                        <asp:TableRow>
                                                                            <asp:TableCell HorizontalAlign="Left">
                                                                                <table width="75%" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Menu Visible="false" runat="server" ForeColor="Black" ID="Menu1" SkinID="NavigationMenu"
                                                                                                Orientation="Horizontal" DynamicHorizontalOffset="2" StaticSubMenuIndent="20px"
                                                                                                border="0" Width="100%" OnMenuItemClick="Menu1_MenuItemClick">
                                                                                                <Items>
                                                                                                    <asp:MenuItem Text="" Value="-"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="*" Value="*" Selected="true"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="A" Value="A"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="B" Value="B"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="C" Value="C"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="D" Value="D"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="E" Value="E"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="F" Value="F"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="G" Value="G"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="H" Value="H"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="I" Value="I"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="J" Value="J"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="K" Value="K"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="L" Value="L"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="M" Value="M"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="N" Value="N"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="O" Value="O"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="P" Value="P"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="Q" Value="Q"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="R" Value="R"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="S" Value="S"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="T" Value="T"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="U" Value="U"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="V" Value="V"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="W" Value="W"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="X" Value="X"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="Y" Value="Y"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="Z" Value="Z"></asp:MenuItem>
                                                                                                    <asp:MenuItem Text="[0-9]" Value="[0123456789]"></asp:MenuItem>
                                                                                                </Items>
                                                                                            </asp:Menu>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                        <asp:TableRow>
                                                                            <asp:TableCell Width="20" VerticalAlign="Top" HorizontalAlign="Left">
                                                                                <table cellpadding="3" cellspacing="3">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonMoveLeft" SkinID="MoveLeft" ToolTip="" runat="server"
                                                                                                OnClick="ImageButtonMoveLeft_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonMoveRight" SkinID="MoveRight" ToolTip="" runat="server"
                                                                                                OnClick="ImageButtonMoveRight_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell VerticalAlign="Top" CssClass="Grid_tr">
                                                                                <asp:Table EnableViewState="false" ID="TableSearchResults" CellSpacing="1" CellPadding="3"
                                                                                    Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                        <asp:TableHeaderCell HorizontalAlign="Left">
                                                                                    <input id="chkALL"  onclick="ChkandUnchk()" type="checkbox"  />
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell Wrap="false" HorizontalAlign="Left">
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ID="TableHeaderCell2" Wrap="false" HorizontalAlign="Left" CssClass="H_title"
                                                                                            Text=""> 
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ID="TableHeaderCellSearchIPAddress" Wrap="false" HorizontalAlign="Left"
                                                                                            CssClass="H_title"> 
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell Visible="false" ID="TableHeaderCellSearchModel" Wrap="false"
                                                                                            HorizontalAlign="Left" CssClass="H_title"> 
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell Visible="false" ID="TableHeaderCellSearchLocation" Wrap="false"
                                                                                            HorizontalAlign="Left" CssClass="H_title"></asp:TableHeaderCell>
                                                                                    </asp:TableHeaderRow>
                                                                                </asp:Table>
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                    </asp:Table>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="10">
                                                        <asp:HiddenField ID="HiddenFieldMFPCount" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldMFPListCount" Value="0" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="center" valign="top" class="CenterBG">
                <asp:Table ID="TableWarningMessage" Visible="false" CellSpacing="0" CellPadding="3"
                    Width="50%" runat="server" border="0" SkinID="Grid">
                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                        <asp:TableHeaderCell ID="TableHeaderCellDivName" CssClass="LabelWarningFont" ColumnSpan="2"
                            HorizontalAlign="Left">Warning</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow CssClass="GridRow">
                        <asp:TableCell ID="TableCellWarningImage" HorizontalAlign="Center" Width="20%">
                            <asp:Image ID="ImageWarning" runat="server" SkinID="PermessionsAndLimitsCritical" />
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell1" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                            <p class="LabelLoginFont">
                            </p>
                            <p class="LabelWarningFont">
                                <asp:Label ID="LabelWarningMessage" runat="server" Text="LabeWarningMessage"></asp:Label></p>
                            <p class="LabelLoginFont">
                            </p>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Content>
