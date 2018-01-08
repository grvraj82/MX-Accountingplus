<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="AssignUsersToGroups.aspx.cs" Inherits="AccountingPlusWeb.Administration.AssignUsersToGroups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowPrice();
        Meuselected("Pricing");

          function IsUserSelected() {
            try {
                var thisForm = document.forms[0];
                var users = thisForm.__SelectedUsers.length;
                var selectedCount = 0;

                if (users > 0) {
                    for (var item = 0; item < users; item++) {
                        if (thisForm.__SelectedUsers[item].checked) {
                            selectedCount++
                            return true;
                        }
                    }
                }
                else {
                    if (thisForm.__SelectedUsers.checked) {
                        selectedCount++
                        return true;
                    }
                }

                if (selectedCount == 0) {
                    //showDialog('WARNING', 'Select one user', 'warning', 5)
                    jNotify(C_SELECT_ONE_USER)
                    return false;
                }

            }
            catch (Error) {
                //showDialog('WARNING', 'Select one user', 'warning', 5)
                jNotify(C_SELECT_ONE_USER)
                return false;
            }
        }        

        function DeleteGroups() {
            if (IsUserSelected() > 1) {
                jNotify(C_SELECT_ONEGROUP)
                return false;
            }
        }

        function ChkandUnchk() {
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__SelectedUsers') {
                        if (document.getElementById('aspnetForm').elements[i].value != 'admin') {
                            document.getElementById('aspnetForm').elements[i].checked = true;
                        }
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__SelectedUsers') {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                }
            }
            ValidateSelectedCount();
        }
        function togall(refcheckbox) {
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldSelectedCostCenterUsersCount").value);
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
            var users = thisForm.__SelectedUsers.length;
            var selectedCount = 0;
            if (thisForm.__SelectedUsers[refcheckbox].checked) {

                thisForm.__SelectedUsers[refcheckbox].checked = false;
            }
            else {
                thisForm.__SelectedUsers[refcheckbox].checked = true;
            }
            ValidateSelectedCount();
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldSelectedCostCenterUsersCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__SelectedUsers.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__SelectedUsers[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__SelectedUsers.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
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
        function togallList(refcheckbox) {
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldSelectedUsersCountList").value);
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
            ValidateSelectedListCount();
        }
        function ValidateSelectedListCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldSelectedUsersCountList").value);

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
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td height="25" align="left" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadAssignUserGroups" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="20px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td height="33" align="left" valign="middle" class="HeaderPadding" style="display: none">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="f10b TextWrapping" height="35" align="right">
                                        <asp:Label ID="LabelGroups" runat="server" SkinID="TotalResource" Text=""></asp:Label>
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
            <td class="CenterBG" valign="top" style="height:500">
                <asp:Panel ID="PanelMainData" runat="server">
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr style="height:500px">
                            <td valign="top">
                                <table align="center" cellpadding="0" cellspacing="0" border="0" class="" width="98%">
                                    <tr>
                                        <td colspan="2" valign="top">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr align="center">
                                                    <td valign="top">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="98%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="height: 10px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0" width="250px">
                                                                                    <tr style="background-color: White;">
                                                                                        <td>
                                                                                            <asp:TextBox BorderWidth="0"  Text="*" MaxLength="50" AutoPostBack="true" OnTextChanged="TextBoxSearchCostCenter_OnTextChanged"
                                                                                                CssClass="SearchTextBox" ID="TextBoxSearchCostCenter" runat="server" Width="100%"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButtonSearchCostCenter" runat="server" SkinID="SearchList"
                                                                                                OnClick="ImageButtonSearchCostCenter_Click" />
                                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxSearchCostCenter"
                                                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" CompletionInterval="1000"
                                                                                                ServiceMethod="GetCostCenters" ServicePath="~/WebServices/ContextSearch.asmx"
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
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 1px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="Grid_tr">
                                                                                <asp:HiddenField ID="HiddenFieldSelectedGroup" Value="" runat="server" />
                                                                                <asp:Table ID="TableCostCenters" SkinID="Grid" CssClass="Table_bg" CellSpacing="1"
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
                                                                    <asp:Table ID="TableMainData" Width="100%" BorderWidth="0" runat="server">
                                                                        <asp:TableRow>
                                                                            <asp:TableCell VerticalAlign="Top">
                                                                               
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Visible="false" HorizontalAlign="Left" VerticalAlign="Top">
                                                                            
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                        <asp:TableRow>
                                                                            <asp:TableCell VerticalAlign="Top">
                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table style="width: 100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table cellpadding="3" cellspacing="3">
                                                                                                            <tr>
                                                                                                                <td style="white-space: nowrap">
                                                                                                                <asp:Label ID="LabelListfUsersbelongstoCostCenter"  runat="server" Text=""></asp:Label>
                                                                                                                 
                                                                                                                </td>
                                                                                                                <td align="center">
                                                                                                                    :
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="LabelSelectedCostCenter" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td align="right" style="white-space: nowrap">
                                                                                                        <asp:Table ID="Table1" runat="server">
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
                                                                                                                    <asp:Label ID="LabelTotalRecordsTitle" runat="server" SkinID="TotalResource" Text=""></asp:Label>:</asp:TableCell>
                                                                                                                <asp:TableCell>
                                                                                                                    <asp:Label ID="LabelTotalRecordsValue" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                                                                                </asp:TableCell>
                                                                                                            </asp:TableRow>
                                                                                                        </asp:Table>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonAddItem" Visible="true" 
                                                                                                            SkinID="AddItem" runat="server" OnClick="ImageButtonAddItem_Click" />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonRemoveItem" Visible="true" 
                                                                                                            SkinID="RemoveItem" runat="server" OnClick="ImageButtonRemoveItem_Click" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr class="Grid_tr">
                                                                                        <td>
                                                                                            <asp:Table EnableViewState="false" ID="TableUsers" CellSpacing="1" CellPadding="3"
                                                                                                Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                                                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                                    <asp:TableHeaderCell Width="30" HorizontalAlign="Center">
                                                                                                <input id="chkALL" onclick="ChkandUnchk()" type="checkbox" />
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellName" Wrap="false" HorizontalAlign="Left"
                                                                                                        CssClass="H_title"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellUserName" Wrap="false" HorizontalAlign="Left"
                                                                                                        CssClass="H_title"> 
                                                                                                    </asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellEmail" Wrap="false" HorizontalAlign="Left"
                                                                                                        CssClass="H_title"></asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell ID="TableHeaderCellUserSource" Wrap="false" HorizontalAlign="Left"
                                                                                                        CssClass="H_title"></asp:TableHeaderCell>
                                                                                                </asp:TableHeaderRow>
                                                                                            </asp:Table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell id="TablecellUserdata" Visible="false" VerticalAlign="Top">
                                                                                <table  width="100%" cellpadding="2" cellspacing="2">
                                                                                    <tr>
                                                                                        <td style="width: 1px">
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                      
                                                                                            <table  cellpadding="0" cellspacing="0" width="250px">
                                                                                                <tr style="background-color: White;">
                                                                                                    <td>
                                                                                                        <asp:TextBox BorderWidth="0" MaxLength="50" Text="*" AutoPostBack="true" OnTextChanged="TextBoxUserSearch_OnTextChanged"
                                                                                                            CssClass="SearchTextBox" ID="TextBoxUserSearch" runat="server" Width="100%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonUserSearch" runat="server" SkinID="SearchList" OnClick="ImageButtonUserSearch_Click" />
                                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TextBoxUserSearch"
                                                                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" CompletionInterval="1000"
                                                                                                            ServiceMethod="GetUserNames" ServicePath="~/WebServices/ContextSearch.asmx" CompletionListCssClass="autocomplete_completionListElement"
                                                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                                        </cc1:AutoCompleteExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButton2" runat="server" SkinID="CancelSearch" OnClick="ImageButtonCancelUserSearch_Click" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                           
                                                                                        </td>
                                                                                        <td style="width: 1px">
                                                                                            <asp:ImageButton ID="ImageButtonCancelAction" Visible="false"  SkinID="CancelAction"
                                                                                                runat="server" OnClick="ImageButtonCancelAction_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr class="Grid_tr">
                                                                                        <td valign="top">
                                                                                            <table cellpadding="3" cellspacing="3">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonMoveLeft" SkinID="MoveLeft" 
                                                                                                            runat="server" OnClick="ImageButtonMoveLeft_Click" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButtonMoveRight" SkinID="MoveRight"
                                                                                                            runat="server" OnClick="ImageButtonMoveRight_Click" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td valign="top" colspan="2">
                                                                                            <asp:Table ID="TableFilter" Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                                                                                <asp:TableRow>
                                                                                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                                                                                        <asp:Table ID="Table3" runat="server" CellPadding="2" CellSpacing="0">
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
                                                                                                <asp:TableRow>
                                                                                                    <asp:TableCell>
                                                                                                        <asp:Table EnableViewState="false" ID="TableUserData" CellSpacing="1" CellPadding="3"
                                                                                                            Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                                                                        </asp:Table>
                                                                                                    </asp:TableCell>
                                                                                                </asp:TableRow>
                                                                                            </asp:Table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                    </asp:Table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="10px">
                                                        <asp:HiddenField ID="HiddenFieldSelectedCostCenterUsersCount" Value="0" runat="server" />
                                                        <asp:HiddenField ID="HiddenFieldSelectedUsersCountList" Value="0" runat="server" />
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
</asp:Content>
