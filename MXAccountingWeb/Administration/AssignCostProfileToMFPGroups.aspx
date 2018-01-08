<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="AssignCostProfileToMFPGroups.aspx.cs" Inherits="AccountingPlusWeb.Administration.AssignCostProfileToMFPGroups" %>

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
        function IsUserGroupSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__MFPGROUPID.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__MFPGROUPID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__MFPGROUPID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {

                var MFPon = document.getElementById('ctl00_PageContent_HiddenFieldMFPOn').value;
                if (MFPon == "MFP") {
                    showDialog('Warning', C_SELECT_ONE_MFP, 'warning', 5)
                    return false;
                }
                else {
                    showDialog('Warning', 'C_SELECT_ONE_MFPGROUP', 'warning', 5)
                    return false;
                }
            }
        }

        function ChkandUnchk() {
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__MFPGROUPID') {
                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__MFPGROUPID') {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                }
            }
        }
        function togall(refcheckbox) {

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldCostProfile").value);
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
            var users = thisForm.__MFPGROUPID.length;
            var selectedCount = 0;
            if (thisForm.__MFPGROUPID[refcheckbox].checked) {

                thisForm.__MFPGROUPID[refcheckbox].checked = false;
            }
            else {
                thisForm.__MFPGROUPID[refcheckbox].checked = true;
            }
            ValidateSelectedCount();
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldCostProfile").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }
        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__MFPGROUPID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__MFPGROUPID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__MFPGROUPID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }

        function ChkandUnchkMFPGroupsList() {
            if (document.getElementById('chkALLMFPGroupList').checked) {
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
        function togallList(refcheckbox) {

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldCostProfileList").value);
            if (totalRecords == "1") {
                if (document.getElementById("chkALLMFPGroupList").checked == true) {
                    document.getElementById("chkALLMFPGroupList").checked = false
                }
                else {
                    document.getElementById("chkALLMFPGroupList").checked = true
                }
                ChkandUnchkMFPGroupsList();
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

        function ValidateSelectedCountList() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldCostProfileList").value);

            if (totalRecords == GetSeletedCountList()) {
                var checkBoxAll = document.getElementById("chkALLMFPGroupList").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALLMFPGroupList").checked = false;
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
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height: 500px">
        <tr>
            <td align="right" valign="top" style="height: 500px">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" valign="top" style="width: 1px">
                            <asp:Image ID="Image1" SkinID="HeadingLeft" runat="server" />
                        </td>
                        <td height="25" align="left" valign="top" class="CenterBG">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="Top_menu_bg">
                                <tr>
                                    <td valign="top">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td class="HeadingMiddleBg">
                                                    <div style="padding: 4px 10px 0px 10px;">
                                                        <asp:Label ID="LabelHeadAssignCostProfileToMFPGroup" runat="server" Text=""></asp:Label></div>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image2" SkinID="HeadingRight" runat="server" />
                                                </td>
                                                <td width="5px">
                                                </td>
                                                <td height="33" align="left" valign="middle" class="HeaderPadding">
                                                    <table cellpadding="3" cellspacing="3" border="0">
                                                        <tr>
                                                            <td class="f10b TextWrapping" align="left">
                                                                <asp:Label ID="PageTitle" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                            </td>
                                                            <td valign="middle" align="left">
                                                                <asp:DropDownList ID="DropDownListDevicesGroups" AutoPostBack="true" CssClass="FormDropDown_Small"
                                                                    OnSelectedIndexChanged="DropDownListDevicesGroups_SelectedIndexChanged" runat="server">
                                                                </asp:DropDownList>
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
                    <tr>
                        <td>
                        </td>
                        <td valign="top" class="CenterBG">
                            <asp:Panel CssClass="CenterBG" ID="PanelMainData" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                    <tr style="height: 500px">
                                        <td valign="top">
                                            <table align="center" cellpadding="0" cellspacing="0" border="0" class="" width="100%">
                                                <tr>
                                                    <td colspan="2" valign="top" align="center">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="98%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="98%">
                                                                        <tr>
                                                                            <td style="height: 15px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0" width="250px">
                                                                                    <tr style="background-color: White; height: 25px">
                                                                                        <td>
                                                                                            <asp:TextBox BorderWidth="0" ToolTip=""
                                                                                                CssClass="SearchTextBox" OnTextChanged="SearchTextBox_OnTextChanged" AutoPostBack="true"
                                                                                                ID="TextBoxCostProfileSearch" Text="*" runat="server" Width="100%"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonSearchCostProfile" OnClick="ImageButtonSearchCostProfile_Click"
                                                                                                runat="server" SkinID="SearchList" />
                                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxCostProfileSearch"
                                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" ServicePath="~/WebServices/ContextSearch.asmx"
                                                                                                CompletionInterval="1000" ServiceMethod="GetCostProfilesForSearch" CompletionListCssClass="autocomplete_completionListElement"
                                                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                            </cc1:AutoCompleteExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonCancelSearch" runat="server" SkinID="CancelSearch"
                                                                                                OnClick="ImageButtonCancelSearch_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 1px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" class="Grid_tr">
                                                                                <asp:HiddenField ID="HiddenFieldSelectedCostProfile" Value="" runat="server" />
                                                                                <asp:Table ID="TableCostProfiles" SkinID="Grid" CssClass="Table_bg" CellSpacing="0"
                                                                                    CellPadding="3" Width="100%" BorderWidth="0" runat="server">
                                                                                </asp:Table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td width="1%" valign="top">
                                                                    &nbsp;
                                                                </td>
                                                                <td width="90%" valign="top">
                                                                    <asp:Table ID="TableMainData" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0"
                                                                        runat="server">
                                                                        <asp:TableRow>
                                                                            <asp:TableCell VerticalAlign="Top">
                                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                    <tr>
                                                                                        <td style="width: 98%" valign="middle" height="40">
                                                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="LabelFilterTitle" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="LabelSelectedCostProfile" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td>
                                                                                            <table cellpadding="3" cellspacing="3" width="98%" border="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonAddItem" Visible="true" ToolTip=""
                                                                                                            SkinID="AddItem" runat="server" OnClick="ImageButtonAddItem_Click" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonRemoveItem" Visible="true" ToolTip=""
                                                                                                            SkinID="RemoveItem" runat="server" OnClick="ImageButtonRemoveItem_Click" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="20" VerticalAlign="Top" HorizontalAlign="Left">
                                                                                <table cellpadding="3" cellspacing="3">
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Visible="false" HorizontalAlign="Left" VerticalAlign="Bottom">
                                                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tr style="background-color: White; height: 25px">
                                                                                                    <td valign="bottom">
                                                                                                        <asp:TextBox ID="TextBoxSearch" OnTextChanged="TextBoxSearch_OnTextChanged" CssClass="SearchTextBox"
                                                                                                            AutoPostBack="true" Text="*" Width="100%" BorderWidth="0" runat="server"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonSearch" SkinID="SearchList" runat="server" OnClick="ImageButtonSearch_Click" />
                                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderMFPSearch" runat="server" TargetControlID="TextBoxSearch"
                                                                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" ServicePath="~/WebServices/ContextSearch.asmx"
                                                                                                            CompletionInterval="1000" ServiceMethod="GetMFPHostNameForSearch" CompletionListCssClass="autocomplete_completionListElement"
                                                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                                        </cc1:AutoCompleteExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonCancelDevice" runat="server" SkinID="CancelSearch"
                                                                                                            OnClick="ImageButtonCancelDevice_Click" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td align="center">
                                                                                            <asp:ImageButton ID="ImageButtonAddToList" Visible="false" ToolTip="Add selected MFPs/Groups to Cost Profile"
                                                                                                SkinID="AddToList" runat="server" OnClick="ImageButtonAddToList_Click" />
                                                                                        </td>
                                                                                        <td align="center">
                                                                                            <asp:ImageButton ID="ImageButtonCancelAction" Visible="false" ToolTip="" SkinID="CancelAction"
                                                                                                runat="server" OnClick="ImageButtonCancelAction_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                        <asp:TableRow>
                                                                            <asp:TableCell VerticalAlign="Top">
                                                                                <table width="99%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                                                                    <tr class="Grid_tr">
                                                                                        <td>
                                                                                            <asp:Table EnableViewState="false" ID="TableMFPGroups" CellSpacing="1" CellPadding="3"
                                                                                                Width="100%" BorderWidth="0" SkinID="Grid" runat="server" CssClass="Table_bg">
                                                                                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                                    <asp:TableHeaderCell Width="30" HorizontalAlign="Left">
                                                                            <input id="chkALL" onclick="ChkandUnchk()" type="checkbox" />
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell Wrap="false" Width="30" HorizontalAlign="Left">
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellGroupName" Wrap="false" Visible="false" Text=""
                                                                                                        runat="server" HorizontalAlign="Left" CssClass="H_title"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellHostName" Wrap="false" Visible="false" Text=""
                                                                                                        runat="server" HorizontalAlign="Left" CssClass="H_title"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellIPAddress" Wrap="false" Visible="false" Text=""
                                                                                                        runat="server" HorizontalAlign="Left" CssClass="H_title"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellLocation" Wrap="false" Visible="false" Text=""
                                                                                                        runat="server" HorizontalAlign="Left" CssClass="H_title"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellModel" Wrap="false" Visible="false" Text=""
                                                                                                        runat="server" HorizontalAlign="Left" CssClass="H_title"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                </asp:TableHeaderRow>
                                                                                            </asp:Table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="20" VerticalAlign="Top" Visible="false" ID="TableCellAddRemoveItems"
                                                                                HorizontalAlign="Left">
                                                                                <table cellpadding="3" cellspacing="3">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonMoveLeft" OnClick="ImageButtonAddToList_Click" SkinID="MoveLeft"
                                                                                                ToolTip="" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonMoveRight" OnClick="ImageButtonRemoveItem_Click"
                                                                                                SkinID="MoveRight" ToolTip="" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Visible="false" VerticalAlign="Top" CssClass="Grid_tr">
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
                                                                                        <asp:TableCell>
                                                                                            <asp:Table EnableViewState="false" ID="TableSearchResults" CellSpacing="1" CellPadding="3"
                                                                                                Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                                                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                                    <asp:TableHeaderCell Width="30px" HorizontalAlign="Left">
                                                                                    <input id="chkALLMFPGroupList" onclick="ChkandUnchkMFPGroupsList()" type="checkbox" />
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell Width="30px" Wrap="false" HorizontalAlign="Left">
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellSearchGroupName" Wrap="false" Visible="false"
                                                                                                        Text="MFP Group" runat="server" HorizontalAlign="Left" CssClass="H_title"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellSearchHostName" Wrap="false" HorizontalAlign="Left"
                                                                                                        CssClass="H_title" Text="Host Name"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellSearchIPAddress" Wrap="false" Text="IP Address"
                                                                                                        HorizontalAlign="Left" CssClass="H_title"> 
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
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="10">
                                                    </td>
                                                </tr>
                                                <tr height="2">
                                                    <td>
                                                        <asp:HiddenField ID="HiddenFieldMFPOn" runat="server" Value="0" />
                                                        <asp:HiddenField ID="HiddenFieldCostProfile" runat="server" Value="0" />
                                                        <asp:HiddenField ID="HiddenFieldCostProfileList" runat="server" Value="0" />
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
                        <td class="CenterBG" align="center">
                            <asp:Table ID="TableWarningMessage" Visible="false" CellSpacing="1" CellPadding="3"
                                Width="100%" runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
                                <asp:TableRow>
                                    <asp:TableCell Height="500" HorizontalAlign="Center" VerticalAlign="Top">
                                        <asp:Table ID="WarningTable" Width="50%" runat="server" CellSpacing="0" CellPadding="3">
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
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
