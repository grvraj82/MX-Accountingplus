<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    Inherits="AdministrationManageUsers" CodeBehind="ManageUsers.aspx.cs" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/SortMenu.ascx" TagName="AlphabetSort" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
    <link href="../Notify/lightbox.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Blue/AppStyle/sweetalert.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PageContent" ID="PC" runat="server">
    <script src="../JavaScript/sweetalert.min.js" type="text/javascript"></script>
    <script src="../Notify/jquery.min.js" type="text/javascript"></script>
    <script src="../Notify/lightbox.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        fnShowCellUsers();
        Meuselected("UserID");
        function DeleteUsers() {
            if (IsUnlockDeleteUserSelected()) {
                var confirmflag = confirm(C_DELETE_CONFIRMATION);
                if (!confirmflag) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }

        function UnlockUsers() {
            //if (IsUnlockDeleteUserSelected()) {
            if (IsUserSelected()) {
            }
            else {
                return false;
            }
        }

        function IsUserSelected() {
            try {
                var thisForm = document.forms[0];
                var users = thisForm.__USERID.length;
                var selectedCount = 0;

                if (users > 0) {
                    for (var item = 0; item < users; item++) {
                        if (thisForm.__USERID[item].checked) {
                            selectedCount++

                            return true;
                        }
                    }
                }
                else {
                    if (thisForm.__USERID.checked) {
                        selectedCount++
                        return true;
                    }
                }
                if (selectedCount == 0) {
                    jNotify(C_SELECT_ONE_USER)
                    return false;
                }
            }
            catch (Error) {
                jNotify(C_SELECT_ONE_USER)
                return false;
            }
        }

        function UpdateUserDetails() {
            if (IsUserSelected()) {
                if (GetSeletedCount() > 1) {
                    for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                    jNotify(C_SELECT_ONLY_ONE_USER)
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function openpopup() {
            var w = "900";
            var h = "500";
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWindow = window.open("../Administration/ImportADUsers.aspx", "List", 'toolbar=no, location=no, status=no, menubar=no, scrollbars=no, resizable=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }

        function openCSVFileUploadpopup() {
            var w = "900";
            var h = "500";
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 1);
            var targetWindow = window.open("../Administration/ManageDBUploadUsers.aspx", "List", 'toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__USERID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__USERID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__USERID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;

        }

        function ChkandUnchk() {
            var userSource = document.getElementById("ctl00_PageContent_HiddenFieldUserSource").value;
            if (document.getElementById('chkALL').checked) {

                var adminAccid = document.getElementById("ctl00_PageContent_HiddenFieldAdminUserAccId").value;
                var guestAccid = document.getElementById("ctl00_PageContent_HiddenFieldGuestAccId").value;

                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].id == adminAccid && document.getElementById('aspnetForm').elements[i].type == 'checkbox' && userSource == "DB" || document.getElementById('aspnetForm').elements[i].id == guestAccid && userSource == "DB" && document.getElementById('aspnetForm').elements[i].type == 'checkbox') {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                    else if (document.getElementById('aspnetForm').elements[i].type == 'checkbox') {
                        document.getElementById('aspnetForm').elements[i].checked = true;

                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    // alert(document.getElementById('aspnetForm').elements[i].value);
                    document.getElementById('aspnetForm').elements[i].checked = false;
                }
            }
            if (document.getElementById("ctl00_PageContent_HiddenFieldisSortingEnable").value == 'false') {
                document.getElementById("chkALL").checked = false;
            }
        }

        function togall(refcheckbox) {

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenUsersCount").value);
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
            var users = thisForm.__USERID.length;
            var selectedCount = 0;
            var userSource = document.getElementById("ctl00_PageContent_HiddenFieldUserSource").value;

            //            if (userSource == "DB") {
            if (thisForm.__USERID[refcheckbox - 1].checked) {

                thisForm.__USERID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__USERID[refcheckbox - 1].checked = true;
            }
            //            }
            //            else {
            //                if (thisForm.__USERID[refcheckbox-1].checked) {

            //                    thisForm.__USERID[refcheckbox-1].checked = false;
            //                }
            //                else {
            //                    thisForm.__USERID[refcheckbox-1].checked = true;
            //                }

            //            }
            ValidateSelectedCount();
        }

        function connectTo(sortOn, sortMode) {
            if (document.getElementById("ctl00_PageContent_HiddenFieldisSortingEnable").value == 'true') {
                var navigationUrl = "ManageUsers.aspx?sortOn=" + sortOn + "&sortMode=" + sortMode;
                location.href = navigationUrl;
            }
            else {
                return false;
            }
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenUsersCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }

        function IsUnlockDeleteUserSelected() {
            try {
                var thisForm = document.forms[0];
                var users = thisForm.__USERID.length;
                var selectedCount = 0;

                if (users > 0) {
                    for (var item = 0; item < users; item++) {
                        if (thisForm.__USERID[item].checked) {
                            selectedCount++
                            return true;
                        }
                    }
                }
                else {
                    if (thisForm.__USERID.checked) {
                        selectedCount++
                        return true;
                    }
                }

                if (selectedCount == 0) {
                    jNotify(C_USER_DELETE_UNLOCK)
                    return false;
                }

            }
            catch (Error) {
                jNotify(C_USER_DELETE_UNLOCK)
                return false;
            }
        }
        function EnableRadioButton() {

            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
            document.getElementById('ctl00_PageContent_RadioButtonAll').checked = true;
            try {
                var thisForm = document.forms[0];
                var users = thisForm.__USERID.length;
                var selectedCount = 0;

                if (users > 0) {
                    for (var item = 0; item < users; item++) {
                        if (thisForm.__USERID[item].checked) {
                            selectedCount++
                            document.getElementById('ctl00_PageContent_RadioButtonSelected').checked = true;
                            return true;
                        }
                    }
                }
                else {
                    if (thisForm.__USERID.checked) {
                        selectedCount++

                        return true;
                    }
                }
            }
            catch (Error) {

                return false;
            }

        }
        function SendMailconfirm() {
            if (PinUserSelected()) {
                var confirmflag = confirm('Are you sure,you want to distribute?');
                if (!confirmflag) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }

        function PinUserSelected() {
            try {
                var thisForm = document.forms[0];
                var users = thisForm.__USERID.length;
                var selectedCount = 0;

                if (users > 0) {
                    for (var item = 0; item < users; item++) {
                        if (thisForm.__USERID[item].checked) {
                            selectedCount++
                            return true;
                        }
                    }
                }
                else {
                    if (thisForm.__USERID.checked) {
                        selectedCount++
                        return true;
                    }
                }

                if (selectedCount == 0) {
                    if (document.getElementById('ctl00_PageContent_RadioButtonSelected').checked) {
                        jNotify(C_USER_DELETE_UNLOCK)
                        return false;
                    }
                    else if (document.getElementById('ctl00_PageContent_RadioButtonAll').checked) {
                        return true;
                    }
                    else if (document.getElementById('ctl00_PageContent_RadioButtonLastSend').checked) {
                        return true;
                    }

                }

            }
            catch (Error) {
                jNotify(C_USER_DELETE_UNLOCK)
                return false;
            }
        }

        function AllowNumeric() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else
                return false;
        }
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="Panel">
        <ContentTemplate>
            <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
                <tr>
                    <td align="right" valign="top" style="width: 1px">
                        <asp:Image ID="Image1" SkinID="HeadingLeft" runat="server" />
                    </td>
                    <td valign="top" class="CenterBG">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr class="Top_menu_bg">
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="HeadingMiddleBg">
                                                <div style="padding: 4px 10px 0px 10px;">
                                                    <asp:Label ID="LabelHeadUserManagement" runat="server" Text=""></asp:Label></div>
                                            </td>
                                            <td>
                                                <asp:Image ID="Image2" SkinID="HeadingRight" runat="server" />
                                            </td>
                                            <td width="5px">
                                            </td>
                                            <td valign="top" class="HeaderPaddingL">
                                                <asp:Table ID="Table1" runat="server" CellPadding="3" CellSpacing="3">
                                                    <asp:TableRow>
                                                        <asp:TableCell ID="tabelCellLabelUserSource" align="center" valign="middle" runat="server"
                                                            Visible="true" CssClass="NoWrap">
                                                            <asp:Label ID="LabelUserSource" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                            :
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="tabelCellDropDownUserSource" align="center" valign="middle" runat="server"
                                                            Visible="true" CssClass="NoWrap">
                                                            <asp:DropDownList ID="DropDownListUserSource" CssClass="FormDropDown_Small" runat="server"
                                                                AutoPostBack="true" OnSelectedIndexChanged="DropDownListUserSource_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </asp:TableCell>
                                                        <asp:TableCell runat="server" ID="aspImageUploadCsv" Visible="false" HorizontalAlign="Left"
                                                            VerticalAlign="Top">
                                                            <asp:ImageButton ID="ImportCSVData" ToolTip="" runat="server" SkinID="ManageusersimgUploadCsv"
                                                                OnClick="ImageUploadCsv_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="aspImageSyncLdap" runat="server">
                                                            <asp:ImageButton ID="ImageSyncLdap" ToolTip="" runat="server" SkinID="ManageusersimgImageButtonSyncLdap"
                                                                OnClick="ImageButtonSyncLdap_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell runat="server" ID="aspImageButtonAdd" CssClass="MenuSpliter" Visible="false"
                                                            HorizontalAlign="Left" VerticalAlign="Top">
                                                            <asp:ImageButton ID="ImageButtonAdd" SkinID="ManageusersimgAddusers" runat="server"
                                                                OnClick="ImageButtonAdd_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" ID="TableCellEdit" Visible="false">
                                                            <asp:ImageButton runat="server" ID="ImageButtonEdit" ToolTip="" SkinID="ManageusersimgEditusers"
                                                                OnClick="ImageButtonEdit_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" ID="TableCellLock" Visible="false">
                                                            <asp:ImageButton runat="server" ID="ImageButtonLock" ToolTip="" SkinID="ManageUsersLockUser"
                                                                OnClick="ImageButtonLock_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" ID="TableCellReset" Visible="false">
                                                            <asp:ImageButton runat="server" ID="ImageButtonReset" ToolTip="" SkinID="ManageusersimgResetusers"
                                                                OnClick="ImageButtonReset_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" ID="TableCellDelete" Visible="false">
                                                            <asp:ImageButton ID="ImageButtonDelete" ToolTip="" SkinID="ManageusersimgImageButtonDelete"
                                                                runat="server" OnClick="ImageButtonDelete_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" ID="TablecellSetting" runat="server" Visible="false">
                                                            <asp:ImageButton ID="ImageButtonSetting" ToolTip="" runat="server" CausesValidation="False"
                                                                SkinID="ManageUsersSettings" OnClick="ImageButtonSetting_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" Visible="false" ID="TableCellAssignUsersToCostCenter">
                                                            <asp:ImageButton ID="ImageButtonAssignToGroup" ToolTip="Assign Users To Cost Centers"
                                                                SkinID="ManageUsersAssignUserstoCostCenter" runat="server" CausesValidation="False"
                                                                OnClick="ImageButtonAssignToGroup_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" Visible="false" ID="TableCellAssignUserGroupToDeviceGroup">
                                                            <asp:ImageButton ID="ImageButtonAssignUserGroupsToDeviceGroups" ToolTip="Assign Cost Centers to MFP Groups"
                                                                SkinID="ManageUsersCostCentertoMFP" runat="server" CausesValidation="False" OnClick="ImageButtonAssignUserGroupsToDeviceGroups_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" Visible="false" ID="TableCellManageCardIds">
                                                            <asp:ImageButton ID="ImageButtonManageADCardIds" ToolTip="" SkinID="ManageUserCards"
                                                                runat="server" CausesValidation="False" OnClick="ImageButtonManageADCardIds_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter">
                                                            <asp:ImageButton ID="ImageButtonRefresh" ToolTip="" runat="server" CausesValidation="False"
                                                                SkinID="ManageUsersRefresh" OnClick="ImageButtonRefresh_Click" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" Visible="true" ID="TableCell2">
                                                            <asp:ImageButton ID="ImageButtonGeneratePin" ImageUrl="~/App_Themes/Blue/Images/GeneratePin.png"
                                                                runat="server" CausesValidation="False" OnClick="ImageButtonGeneratePin_Click"
                                                                ToolTip="Generate Pin" />
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" Visible="true" ID="TableCell3">
                                                            <a href="javascript:void(0)" onclick="EnableRadioButton();">
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Blue/Images/mail.png"
                                                                    ToolTip="Distribute Pin Numbers" /></a>
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter" ID="TableCell4" Visible="false">
                                                            <asp:ImageButton ID="ImageButtonUpdateGroups" SkinID="ManageUsersRefresh" runat="server"
                                                                CausesValidation="False" OnClick="ImageButtonUpdateGroups_Click" ToolTip="Update User Memeber Of" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" height="5px">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td valign="top" align="center" colspan="2">
                                    <uc1:AlphabetSort ID="SortMenuUserControl" runat="server" />
                                </td>
                            </tr>
                            <tr style="margin-top: 0">
                                <td colspan="2" valign="top" align="center" style="margin-top: 0">
                                    <table width="98%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <table width="100%" id="tableSearchandPaging" runat="server" border="0" visible="false"
                                                    cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="5%" style="padding: 0 10px 0 0;" id="TableCellDomain" runat="server">
                                                            <asp:DropDownList ID="DropDownListDomainList" runat="server" CssClass="Dropdown_CSS"
                                                                AutoPostBack="true" Width="183px" OnSelectedIndexChanged="DropDownListDomainList_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" width="200px" style="background-color: White;">
                                                                <tr style="background-color: White; height: 10px;">
                                                                    <td>
                                                                        <asp:TextBox BorderWidth="0" ToolTip="" CssClass="SearchTextBox" OnTextChanged="SearchTextBox_OnTextChanged"
                                                                            Text="*" MaxLength="50" AutoPostBack="true" ID="TextBoxSearch" runat="server"
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButtonGo" runat="server" SkinID="SearchList" OnClick="ImageButtonGo_Click" />
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxSearch"
                                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" CompletionInterval="1000"
                                                                            ServiceMethod="GetUserSearchList" CompletionListCssClass="autocomplete_completionListElement"
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
                                                        <td>
                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 95%">
                                                                    </td>
                                                                    <td style="width: 5%">
                                                                        <asp:Table ID="TableUsersPaging" runat="server" CellPadding="3" CellSpacing="0">
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
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="Grid_tr">
                                            <td valign="top" width="95%">
                                                <asp:Table EnableViewState="false" ID="TableUsers" SkinID="Grid" CellSpacing="0"
                                                    CellPadding="3" Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg"
                                                    border="0">
                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                        <asp:TableHeaderCell HorizontalAlign="Left" Width="30" CssClass="Grid_topbg1"><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell Width="30" CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellLogOnName" Wrap="false" CssClass="H_title">
                                                            <asp:Label ID="LabelLogOnName" runat="server" Text=""></asp:Label>
                                                        </asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellUserName" Wrap="false" CssClass="H_title">
                                                            <asp:Label ID="LabelUserName" runat="server" Text=""></asp:Label>
                                                        </asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellAuthenticationServer" Wrap="false" CssClass="H_title">
                                                            <asp:Label ID="LabelAuthenticationServer" runat="server" Text=""></asp:Label>
                                                        </asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellEmail" Wrap="false" CssClass="H_title">
                                                            <asp:Label ID="LabelEmailId" runat="server" Text=""></asp:Label>
                                                        </asp:TableHeaderCell>
                                                        <%--  <asp:TableHeaderCell ID="TableHeaderCellDepartment"  Wrap="false" CssClass="Grid_topbg1">
                                                            <table border="0" width="100%">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="LabelDepartment" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableHeaderCell>--%>
                                                        <asp:TableHeaderCell ID="TableHeaderCellEnableLogin" Wrap="false" CssClass="H_title">
                                                            <asp:Label ID="LabelEnableLogin" runat="server" Text=""></asp:Label>
                                                        </asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCell1IsAdministrator" Wrap="false" CssClass="H_title">
                                                            <asp:Label ID="LabelUserRole" runat="server" Text=""></asp:Label>
                                                        </asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellUserPin" Wrap="false" Visible="false" CssClass="H_title">
                                                            <asp:Label ID="LabelUserPin" runat="server" Text="User Pin"></asp:Label>
                                                        </asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
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
                                           <p  class="LabelLoginFont"> </p>
                                           <p class="LabelWarningFont">There are no User(s) created.</p>
                                           <p class="LabelLoginFont"></p>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="5" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenUsersCount" Value="0" runat="server" />
            <asp:HiddenField ID="HiddenFieldisSortingEnable" Value="true" runat="server" />
            <asp:HiddenField ID="HiddenFieldUserSource" Value="DB" runat="server" />
            <asp:HiddenField ID="HiddenFieldAdminUserAccId" Value="" runat="server" />
            <asp:HiddenField ID="HiddenFieldGuestAccId" Value="" runat="server" />
            <div id="light" style="width: 800px; height: 400px; margin-left: 25%" class="white_content">
                <table cellpadding="3" cellspacing="0" border="0" width="100%">
                    <tr width="100%">
                        <td align="right">
                            <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">
                                <img src="../App_Themes/Black/Images/Clear.png" border="0" /></a>
                        </td>
                    </tr>
                </table>
                <table cellpadding="8" cellspacing="0" border="0" cssclass="Table_bg" style="vertical-align: middle;
                    margin-left: 17%; margin-top: 5%; border: 1px solid #CCC; width: 500px" class="Grid_tr">
                    <tr class="Table_HeaderBG">
                        <td style="font-size: medium">
                            Distribute Pin numbers through user email(s).
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonAll" Text="All Users" runat="server" GroupName="send" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonSelected" Text="Selected Users" runat="server" GroupName="send" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="RadioButtonLastSend" Text="Send email to user where the pin number changed in last"
                                            runat="server" GroupName="send" /> &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxDuration" runat="server" Width="35px" Text="1" onkeypress="javascript:return AllowNumeric()" MaxLength="5"></asp:TextBox> &nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListDurattion" runat="server">
                                            <asp:ListItem Text="Hour" Value="Hour"></asp:ListItem>
                                            <asp:ListItem Text="Day" Value="Day"></asp:ListItem>
                                            <asp:ListItem Text="Month" Value="Month"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="ButtonSend" runat="server" Text="Send" SkinID="AddUsersImageButtonBack"
                                OnClick="ButtonSend_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td height="10px">
                        </td>
                    </tr>
                </table>
            </div>
            <div id="fade" class="black_overlay">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
