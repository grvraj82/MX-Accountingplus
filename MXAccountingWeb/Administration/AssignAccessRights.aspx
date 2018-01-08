﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="AssignAccessRights.aspx.cs" Inherits="AccountingPlusWeb.Administration.AssignAccessRights" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellUsers();
        Meuselected("UserID");


        function ChkandUnchk() {
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__SelectedData') {
                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__SelectedData') {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                }
            }
            if (document.getElementById("ctl00_PageContent_HiddenMainUserCount").value == 0) {

                document.getElementById("chkALL").checked = false;

            }
        }



        function togall(refcheckbox) {

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenMainUserCount").value);
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
            var users = thisForm.__SelectedData.length;
            var selectedCount = 0;
            if (thisForm.__SelectedData[refcheckbox].checked) {

                thisForm.__SelectedData[refcheckbox].checked = false;
            }
            else {
                thisForm.__SelectedData[refcheckbox].checked = true;
            }
            ValidateSelectedCount();
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenMainUserCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;

            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }

        }
        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__SelectedData.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__SelectedData[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__SelectedData.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }


        function selectAssignListToDelete() {



        }
        function selectAccessDetailsToAdd() {

        }



        function ChkandUnchkList() {
            if (document.getElementById('CheckboxListAll').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    //                    if (document.getElementById('aspnetForm').elements[i].value != 'admin') {


                    if (document.getElementById('aspnetForm').elements[i].name == '__ISCOSTCENTERSELECTED') {

                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                    //}

                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__ISCOSTCENTERSELECTED') {

                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }

                }
            }

        }

        function togalllist(refcheckbox) {

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldUserListCount").value);
            if (totalRecords == "1") {
                if (document.getElementById("CheckboxListAll").checked == true) {
                    document.getElementById("CheckboxListAll").checked = false
                }
                else {
                    document.getElementById("CheckboxListAll").checked = true
                }
                ChkandUnchkList();
                return;
            }

            var thisForm = document.forms[0];
            var users = thisForm.__ISCOSTCENTERSELECTED.length;
            var selectedCount = 0;
            if (thisForm.__ISCOSTCENTERSELECTED[refcheckbox].checked) {

                thisForm.__ISCOSTCENTERSELECTED[refcheckbox].checked = false;
            }
            else {
                thisForm.__ISCOSTCENTERSELECTED[refcheckbox].checked = true;
            }
            ValidateSelectedCountList();
        }
        function ValidateSelectedCountList() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldUserListCount").value);

            if (totalRecords == GetSeletedCountList()) {

                var checkBoxAll = document.getElementById("CheckboxListAll").checked = true;

            }
            else {
                var checkBoxAll = document.getElementById("CheckboxListAll").checked = false;
            }

        }
        function GetSeletedCountList() {
            var thisForm = document.forms[0];
            var users = thisForm.__ISCOSTCENTERSELECTED.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__ISCOSTCENTERSELECTED[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__ISCOSTCENTERSELECTED.checked) {
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="right" valign="top" style="width: 1px">
                        <asp:Image ID="Image1" SkinID="HeadingLeft" runat="server" />
                    </td>
                    <td align="left" valign="top" class="CenterBG">
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr class="Top_menu_bg">
                                <td valign="top">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="HeadingMiddleBg">
                                                <div style="padding: 4px 10px 0px 10px;">
                                                    <asp:Label ID="LabelHeadAccessRights" runat="server" Text=""></asp:Label></div>
                                            </td>
                                            <td>
                                                <asp:Image ID="Image2" SkinID="HeadingRight" runat="server" />
                                            </td>
                                            <td width="5px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td height="33" align="left" valign="middle" width="15%" class="HeaderPadding">
                                    <table cellpadding="2" cellspacing="2" width="100%" border="0">
                                        <tr>
                                            <td class="f10b" height="35" align="left" style="white-space: nowrap">
                                                &nbsp;<asp:Label ID="LabelAssign" SkinID="TotalResource" runat="server" Text=""></asp:Label>:&nbsp;
                                            </td>
                                            <td valign="middle" align="left" style="white-space: nowrap">
                                                <asp:DropDownList ID="DropDownListAssignOn" runat="server" CssClass="FormDropDown_Small"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownListAssignOn_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="f10b" height="35" align="left" style="white-space: nowrap">
                                                <asp:Label ID="LabelTo" SkinID="TotalResource" runat="server" Text=""></asp:Label>:&nbsp;
                                            </td>
                                            <td valign="middle" align="left" style="white-space: nowrap">
                                                <asp:DropDownList ID="DropDownListAssignTo" runat="server" CssClass="FormDropDown_Small"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownListAssignTo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="85%">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="CenterBG" valign="top">
                        <asp:Panel ID="PanelMainData" runat="server">
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr style="height: 500px">
                                    <td valign="top">
                                        <table align="center" cellpadding="0" cellspacing="0" border="0" class="" width="98%">
                                            <tr>
                                                <td valign="top">
                                                    <table cellpadding="2" cellspacing="0" border="0" width="98%">
                                                        <tr align="center">
                                                            <td valign="top">
                                                                <!-- Search Control MFP/MFP Groups-->
                                                                <table cellpadding="0" cellspacing="0" width="250px">
                                                                    <tr>
                                                                        <td colspan="2" style="height: 5px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="background-color: White; height: 10px">
                                                                        <td>
                                                                            <asp:TextBox BorderWidth="0" Text="*" AutoPostBack="true" OnTextChanged="TextBoxGroupSearch_OnTextChanged"
                                                                                ToolTip="Enter first few characters of 'Group Name' and click on Search icon"
                                                                                CssClass="SearchTextBox" ID="TextBoxGroupSearch" runat="server" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:ImageButton ID="ImageButtonSearchMFP" OnClick="ImageButtonSearchMFP_Click" runat="server"
                                                                                SkinID="SearchList" />
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderMFPSearch" runat="server" TargetControlID="TextBoxGroupSearch"
                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" CompletionInterval="1000"
                                                                                ServicePath="~/WebServices/ContextSearch.asmx" ServiceMethod="GetMFPGroupForSearch"
                                                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                            </cc1:AutoCompleteExtender>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:ImageButton ID="ImageButtonCancelMFPSearch" runat="server" SkinID="CancelSearch"
                                                                                OnClick="ImageButtonCancelMFPSearch_Click" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="height: 1px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" valign="top" class="Grid_tr">
                                                                            <asp:Table ID="TableMFPGroups" EnableViewState="false" CssClass="Table_bg" CellSpacing="1"
                                                                                CellPadding="3" Width="100%" BorderWidth="0" SkinID="Grid" runat="server">
                                                                            </asp:Table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 90%" valign="top">
                                                                <asp:Table ID="TableMainData" Width="100%" BorderWidth="0" runat="server">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell VerticalAlign="Top">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td width="95%">
                                                                                        <table cellpadding="3" cellspacing="3">
                                                                                            <tr>
                                                                                                <td style="white-space: nowrap">
                                                                                                   <asp:Label ID="LabelListofMFPorMFPGroups" runat="server" Text="" SkinID="TotalResource"></asp:Label> 
                                                                                                </td>
                                                                                                <td>
                                                                                                    :
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="LabelSelectedGroupName" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                                                                    <asp:HiddenField ID="HiddenFieldSelectedGroup" Value="" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table cellpadding="3" cellspacing="3">
                                                                                            <tr>
                                                                                                <td align="right" style="white-space: nowrap">
                                                                                                    <asp:Table ID="Table1" runat="server" CellPadding="2" CellSpacing="0">
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
                                                                        <asp:TableCell Visible="false" HorizontalAlign="Left" VerticalAlign="Top">
                                                                            <table cellpadding="3" cellspacing="3">
                                                                                <tr>
                                                                                    <td style="width: 40px">
                                                                                    </td>
                                                                                    <td>
                                                                                        <table cellpadding="0" cellspacing="0" style="width: 250px">
                                                                                            <tr style="background-color: White">
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TextBoxSearch" AutoPostBack="true" OnTextChanged="TextBoxSearch_OnTextChanged"
                                                                                                        CssClass="SearchTextBox" Text="*" Width="100%" BorderWidth="0" runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="ImageButtonGo" SkinID="SearchList" runat="server" OnClick="ImageButtonGo_Click" />
                                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxSearch"
                                                                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" CompletionInterval="1000"
                                                                                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                                    </cc1:AutoCompleteExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="ImageButtonCancelSearch" runat="server" SkinID="CancelSearch"
                                                                                                        OnClick="ImageButtonCancelSearch_Click" />
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
                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                                                                <tr class="Grid_tr">
                                                                                    <td>
                                                                                        <asp:Table EnableViewState="false" ID="TableUsers" CellSpacing="1" CellPadding="3"
                                                                                            Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid"
                                                                                            border="0">
                                                                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                                <asp:TableHeaderCell Width="30" HorizontalAlign="Center">
                                                                                                    <input id="chkALL" onclick="ChkandUnchk()" type="checkbox" />
                                                                                                </asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell ID="TableHeaderCellName" Wrap="false" HorizontalAlign="Left"
                                                                                                    CssClass="H_title"> 
                                                                                                </asp:TableHeaderCell>
                                                                                                <%-- <asp:TableHeaderCell ID="TableHeaderCellUserName" Wrap="false" HorizontalAlign="Left"  CssClass="H_title"> 
                                                                                            </asp:TableHeaderCell>--%>
                                                                                                <asp:TableHeaderCell ID="TableHeaderCellUserSource" Text="" Wrap="false" HorizontalAlign="Left"
                                                                                                    CssClass="H_title"> 
                                                                                                </asp:TableHeaderCell>
                                                                                            </asp:TableHeaderRow>
                                                                                        </asp:Table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Visible="false" VerticalAlign="Top">
                                                                            <asp:Table ID="TableFilter" Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell RowSpan="3" Width="40" VerticalAlign="Top">
                                                                                        <table cellpadding="3" cellspacing="3">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="ImageButtonMoveLeft" SkinID="MoveLeft" ToolTip=""
                                                                                                        runat="server" OnClick="ImageButtonMoveLeft_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="ImageButtonMoveRight" SkinID="MoveRight" ToolTip=""
                                                                                                        runat="server" OnClick="ImageButtonMoveRight_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                             <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="ImageButtonMoveToAll" SkinID="MoveLeft" Visible="false" ToolTip="Assign to all"
                                                                                                        runat="server" OnClick="ImageButtonMoveToAll_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                                        <table width="75%" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Menu runat="server" ForeColor="Black" ID="Menu1" SkinID="NavigationMenu" Orientation="Horizontal"
                                                                                                        DynamicHorizontalOffset="2" StaticSubMenuIndent="20px" border="0" Width="100%"
                                                                                                        OnMenuItemClick="Menu1_MenuItemClick" Visible="false">
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
                                                                                        <asp:Table ID="Table2" runat="server" CellPadding="2" CellSpacing="0">
                                                                                            <asp:TableRow>
                                                                                                <asp:TableCell>
                                                                                                    <asp:Label ID="LabelPageSize1" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                                                                </asp:TableCell>
                                                                                                <asp:TableCell>
                                                                                                    <asp:DropDownList ID="DropDownPageSize1" CssClass="Normal_FontLabel" runat="server"
                                                                                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownPageSize1_SelectedIndexChanged">
                                                                                                        <asp:ListItem Selected="true">50</asp:ListItem>
                                                                                                        <asp:ListItem>100</asp:ListItem>
                                                                                                        <asp:ListItem>200</asp:ListItem>
                                                                                                        <asp:ListItem>500</asp:ListItem>
                                                                                                        <asp:ListItem>1000</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </asp:TableCell>
                                                                                                <asp:TableCell>
                                                                                                    <asp:Label ID="LabelPage1" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                                                                </asp:TableCell>
                                                                                                <asp:TableCell>
                                                                                                    <asp:DropDownList ID="DropDownCurrentPage1" runat="server" AutoPostBack="true" CssClass="Normal_FontLabel"
                                                                                                        OnSelectedIndexChanged="DropDownCurrentPage1_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </asp:TableCell>
                                                                                                <asp:TableCell>
                                                                                                    <asp:Label ID="LabelTotalRecordsTitle1" runat="server" SkinID="TotalResource" Text=""></asp:Label>:
                                                                                                </asp:TableCell>
                                                                                                <asp:TableCell>
                                                                                                    <asp:Label ID="LabelTotalRecordsValue1" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                                                                </asp:TableCell>
                                                                                            </asp:TableRow>
                                                                                        </asp:Table>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow CssClass="Grid_tr">
                                                                                    <asp:TableCell>
                                                                                        <asp:Table EnableViewState="false" ID="TableUserData" CellSpacing="1" CellPadding="3"
                                                                                            Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                                                        </asp:Table>
                                                                                        <asp:Table EnableViewState="false" ID="TableCostCenerData" CellSpacing="1" CellPadding="3"
                                                                                            Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
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
                                                            <td colspan="2" height="10">
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
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Table ID="TableWarningMessage" Visible="false" CellSpacing="1" CellPadding="3"
                            Width="50%" runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
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
            <asp:HiddenField ID="HiddenMainUserCount" Value="0" runat="server" />
            <asp:HiddenField ID="HiddenFieldUserListCount" Value="0" runat="server" />
             <asp:HiddenField ID="HiddenFieldGroupscount" Value="0" runat="server" />
              <asp:HiddenField ID="HiddenFieldAllDeviceList" Value="0" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
