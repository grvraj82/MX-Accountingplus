<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ImportLdapUsers.aspx.cs" Inherits="PrintRoverWeb.Administration.ImportLdapUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="PC" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="../JavaScript/jsUpdateProgress.js"></script>
    <link href="../App_Themes/Blue/AppStyle/ApplicationStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .st_sno
        {
            width: 40px;
        }
        
        .st_chkbox
        {
            width: 40px;
        }
        
        .st_grpname
        {
            width: 80%;
        }
        
        .is_shared
        {
            width: 40px;
        }
        
        .table_border_org
        {
            border-right: #007CA5 1px solid !important;
            border-top: #007CA5 1px solid;
            border-left: #007CA5 1px solid;
            border-bottom: #007CA5 1px solid;
        }
        
        
        .underline:hover
        {
            text-decoration: underline;
        }
    </style>
    <script language="javascript" type="text/javascript">
        fnShowCellUsers();
        Meuselected("UserID");
        var ModalProgress = '<%= ModalProgress.ClientID %>';

        function DeleteUsers() {
            if (IsUserSelected()) {
                return confirm('Selected user(s) will be deleted. \n\nDo you want to Continue?');
            }
            else {
                return false;
            }
        }

        function IsUserSelected() {
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
                showDialog('Warning', C_SELECT_ONE_USER, 'warning', 5)
                return false;
            }
        }

        function UpdateUserDetails() {
            if (IsUserSelected()) {
                if (GetSeletedCount() > 1) {
                    showDialog('Warning', C_SELECT_ONLY_ONE_USER, 'warning', 5)
                    return false;
                }
            }
            else {
                return false;
            }
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
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__USERID') {
                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__USERID') {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                }
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
            DisplaySelectedCount();
        }



        function DisplaySelectedCount() {
            var selectedCount = 0;
            //parseInt(document.getElementById("ctl00_PageContent_HiddenFieldSelectedUsersCount").value);
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenUsersCount").value);

            for (var i = 0; i < totalRecords; i++) {
                if (document.getElementById("__USERID_" + i + "").checked) {
                    selectedCount++;
                }
                else {
                    selectedCount--;
                }
            }
            document.getElementById("ctl00_PageContent_HiddenFieldSelectedUsersCount").value = selectedCount + 1;
            document.getElementById("ctl00_PageContent_LinkButtonImportSelectedUsers").Text = "Import selected Users (" + selectedCount + 1 + ")";
        }

        window.onerror = null;
        var bName = navigator.appName;
        var bVer = parseInt(navigator.appVersion);
        var NS4 = (bName == "Netscape" && bVer >= 4);
        var IE4 = (bName == "Microsoft Internet Explorer" && bVer >= 4);
        var NS3 = (bName == "Netscape" && bVer < 4);
        var IE3 = (bName == "Microsoft Internet Explorer" && bVer < 4);
        var blink_speed = 500;
        var i = 0;

        if (NS4 || IE4) {
            if (navigator.appName == "Netscape") {
                layerStyleRef = "layer.";
                layerRef = "document.layers";
                styleSwitch = "";
            } else {
                layerStyleRef = "layer.style.";
                layerRef = "document.all";
                styleSwitch = ".style";
            }
        }


        function Blink(layerName) {
            if (NS4 || IE4) {
                if (i % 2 == 0) {
                    eval(layerRef + '["' + layerName + '"]' + styleSwitch + '.visibility="visible"');
                }
                else {
                    eval(layerRef + '["' + layerName + '"]' + styleSwitch + '.visibility="hidden"');
                }
            }
            if (i < 1) {
                i++;
            }
            else {
                i--
            }
            setTimeout("Blink('" + layerName + "')", blink_speed);
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

            if (thisForm.__USERID[refcheckbox - 1].checked) {

                thisForm.__USERID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__USERID[refcheckbox - 1].checked = true;
            }

            ValidateSelectedCount();
        }


        //  group chk & uncheck
        function ChkandUnchkGroup() {
            if (document.getElementById('__GROUPALLCHKBOXID_').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__ISEXISTSID') {
                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__ISEXISTSID') {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                }
            }
        }
        //  End group chk & uncheck

        function togallGroup(refcheckbox) {
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenGroupUserCount").value);
            if (totalRecords == "1") {
                if (document.getElementById("__GROUPALLCHKBOXID_").checked == true) {
                    document.getElementById("__GROUPALLCHKBOXID_").checked = false
                }
                else {
                    document.getElementById("__GROUPALLCHKBOXID_").checked = true
                }
                ChkandUnchkGroup();
                return;
            }
            var thisForm = document.forms[0];

            var users = thisForm.__ISEXISTSID.length;
            var selectedCount = 0;

            if (thisForm.__ISEXISTSID[refcheckbox - 1].checked) {

                thisForm.__ISEXISTSID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__ISEXISTSID[refcheckbox - 1].checked = true;
            }

            ValidateSelectedCountGroup();
        }



        function ValidateSelectedCountGroup() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenGroupUserCount").value);

            if (totalRecords == GetSeletedCountGroup()) {
                var checkBoxGroupAll = document.getElementById("__GROUPALLCHKBOXID_").checked = true;
            }
            else {
                var checkBoxGroupAll = document.getElementById("__GROUPALLCHKBOXID_").checked = false;
            }
            DisplaySelectedCountGroup();
        }
        function GetSeletedCountGroup() {
            var thisForm = document.forms[0];
            var users = thisForm.__ISEXISTSID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__ISEXISTSID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__ISEXISTSID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }

        function DisplaySelectedCountGroup() {
            //            var selectedCount = 0;
            //            var currentPageRecordsCount = parseInt(document.getElementById("ctl00_PageContent_HiddenGroupUserCount").value);
            //            var totalRecourdscount = document.getElementById("ctl00_PageContent_HiddenFieldSelectedGroupUserCount").value);
            //            var Start = parseInt(totalRecourdscount.split(',')[0]);
            //            var total = parseInt(totalRecourdscount.split(',')[1]);
            //             //for (var i = 1; i <= currentPageRecordsCount; i++) {
            //            for (var i = Start; i <= total; i++) {
            //                try {
            //                    if (document.getElementById("__ISEXISTSID_" + i + "").checked) {
            //                        selectedCount++;
            //                    }
            //                    else {
            //                        selectedCount--;
            //                    }
            //                }
            //                catch (Error) {
            //                }
            //            }           
            //            document.getElementById("ctl00_PageContent_HiddenFieldSelectedGroupUserCount").Text = "Import selected Group Users (" + selectedCount + 1 + ")";
            //            document.getElementById("ctl00_PageContent_HiddenGroupUserCount").value = "Import All (" + currentPageRecordsCount + ") Group";
        }

       

    </script>
    <div id="content" style="display: none;">
    </div>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="top" class="CenterBG">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td id="tdActionMessage" colspan="3">
                            <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" colspan="3">
                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg"
                                height="33">
                                <tr>
                                    <td class="HeadingMiddleBg" style="width: 10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingImportLDAP" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td align="left" valign="middle" width="88%">
                                        <table cellpadding="0" cellspacing="0" border="0" height="33" width="100%">
                                            <tr>
                                                <td align="center" valign="middle" width="3%">
                                                    <asp:ImageButton ID="ImageButtonBack" runat="server" OnClick="ImageButtonBack_Click"
                                                        SkinID="ImportLdapUsersBackPage" />
                                                </td>
                                                <td width="1%" class="Menu_split">
                                                </td>
                                                <td align="center" valign="middle" width="3%">
                                                    <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="ImportLdapUsersReset"
                                                        ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                                </td>
                                                <td colspan="2" align="left" valign="middle" width="80%">
                                                    <asp:Label ID="LabelHeadingLdapUsers" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                </td>
                                                <td width="1%" class="Menu_split" style="display: none;">
                                                </td>
                                                <td width="3%" valign="middle" align="center" style="display: none;">
                                                    <asp:ImageButton ID="ImageButtonSyncLdap" SkinID="ManageusersimgAddusers" runat="server"
                                                        OnClick="ImageButtonSyncLdap_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr height="2px">
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="100%" colspan="3">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td valign="top" width="50%" align="center">
                                        <table width="98%" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
                                            height="160px">
                                            <tr height="33">
                                                <td colspan="3" align="center" valign="top" style="margin-right: 10px;">
                                                    <asp:Table ID="Table2" runat="server" CellPadding="0" CellSpacing="0" border="0"
                                                        Width="100%" CssClass="Top_menu_bg" Height="33">
                                                        <asp:TableRow>
                                                            <asp:TableCell>
                                                                <asp:Label ID="LabelFilter" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                            <tr height="31px">
                                                <td align="right" height="31px" width="30%" class="NoWrap">
                                                    <asp:Label ID="LabelDomainName" runat="server" class="f10b"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="30%" class="NoWrap">
                                                    &nbsp;
                                                    <asp:DropDownList ID="DropDownListDomainList" runat="server" CssClass="Dropdown_CSS"
                                                        AutoPostBack="true" Width="183px" OnSelectedIndexChanged="DropDownListDomainList_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="40%" class="NoWrap">
                                                </td>
                                            </tr>
                                            <tr height="31px">
                                                <td align="right" height="31px" width="30%" class="NoWrap">
                                                    <asp:Label ID="LabelGroups" runat="server" class="f10b"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="30%" class="NoWrap">
                                                    &nbsp;
                                                    <asp:DropDownList ID="DropDownGroups" runat="server" CssClass="Dropdown_CSS" AutoPostBack="false"
                                                        Width="183px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="40%" class="NoWrap">
                                                </td>
                                            </tr>
                                            <tr height="31px">
                                                <td align="right" height="31px" width="30%" class="NoWrap">
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="LabelFilterBy" runat="server" class="f10b" Text=""></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="30%" class="NoWrap">
                                                    &nbsp;
                                                    <asp:DropDownList ID="DropDownListFilterBy" CssClass="Dropdown_CSS" Width="183px"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="40%" align="left" class="NoWrap">
                                                    =
                                                    <asp:TextBox ID="TextBoxFilterText" Width="130px" runat="server" CssClass="FormTextBox_bg"
                                                        MaxLength="50" ToolTip="Enter first few Characters to of the selected filter by."></asp:TextBox>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td colspan="3" height="20">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" height="8px">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td height="1px">
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" border="0" width="98%" class="table_border_org">
                                            <tr height="33">
                                                <td colspan="3" height="5" align="center">
                                                    <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0" border="0"
                                                        Width="100%" CssClass="Top_menu_bg" Height="33">
                                                        <asp:TableRow>
                                                            <asp:TableCell>
                                                                <asp:Label ID="LabelColumnMapping" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="31px" align="right" width="30%" class="NoWrap">
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="LabelPinNumber" runat="server" class="f10b" Text=""></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="35%" class="NoWrap">
                                                    &nbsp;
                                                    <asp:DropDownList ID="DropDownListPinNumberMap" CssClass="Dropdown_CSS" Width="183px"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:CheckBox ID="CheckBoxPinNumberMap" runat="server" />
                                                </td>
                                                <td valign="middle" width="1%" class="NoWrap">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="31px" width="30%" class="NoWrap">
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="LabelCardID" runat="server" class="f10b" Text=""></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="35%" class="NoWrap">
                                                    &nbsp;
                                                    <asp:DropDownList ID="DropDownListCardIDMap" CssClass="Dropdown_CSS" Width="183px"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:CheckBox ID="CheckBoxCardIDMap" runat="server" />
                                                </td>
                                                <td valign="middle" width="34%" class="NoWrap">
                                                 <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Login_Button" OnClick="ButtonSave_Click"  />
                                                </td>
                                            </tr>
                                             <tr height="33">
                                                <td colspan="3" height="5" align="center">
                                                    <asp:Table ID="Table6" runat="server" CellPadding="0" CellSpacing="0" border="0"
                                                        Width="100%" CssClass="Top_menu_bg" Height="33">
                                                        <asp:TableRow>
                                                            <asp:TableCell>
                                                                <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Grouping"></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                            <tr height="5px"></tr>
                                              <tr>

                                                <td align="right"  class="NoWrap" >
                                                    <asp:Label ID="Label4" runat="server" Text="Import Department as CostCenter " class="f10b" ToolTip="When this option is selected, at the time of importing user(s), Department will be imported as CostCenter(s)"></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;<asp:CheckBox ID="CheckBoxDepartment" runat="server" ToolTip="When this option is selected, at the time of importing user(s), Department will be imported as CostCenter(s)"/>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" height="5">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" height="20">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="center">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr align="center">
                                                            <td align="right" width="50%">
                                                                <asp:Button ID="ButtonGo" runat="server" Text="" CssClass="Login_Button" OnClick="ButtonGo_Click" />
                                                            </td>
                                                            <td align="left" width="50%">
                                                                &nbsp;
                                                                <asp:Button ID="ButtonCancel" runat="server" Text="" CssClass="Cancel_button" CausesValidation="False"
                                                                    PostBackUrl="~/Administration/ManageUsers.aspx" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" height="5">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" width="50%">
                                        <table width="98%" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
                                            height="160px">
                                            <tr height="33">
                                                <td colspan="3" align="center" valign="top">
                                                    <asp:Table ID="Table4" runat="server" CellPadding="0" CellSpacing="0" border="0"
                                                        Width="100%" CssClass="Top_menu_bg" Height="33">
                                                        <asp:TableRow>
                                                            <asp:TableCell>
                                                                <asp:Label ID="LabelADSettings" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                            <tr height="31px">
                                                <td align="right" width="50%" class="NoWrap">
                                                    <asp:Label ID="LabelLdapDomainName" runat="server" Text="" class="f10b"></asp:Label>
                                                    :
                                                </td>
                                                <td align="left" width="50%" class="NoWrap">
                                                    <asp:Label ID="LabelDomainNameText" runat="server" Text="" class="f10b"></asp:Label>
                                                    <div id="prem_hint" style="position: relative; left: 0; visibility: hidden" class="prem_hint">
                                                        <font color="#FF0000"><b>
                                                            <asp:Label ID="LabelNotSet" runat="server" Text=""></asp:Label></b></font>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr height="31px">
                                                <td align="right" width="50%" class="NoWrap">
                                                    <asp:Label ID="LabelDomainAdministrator" runat="server" Text="" class="f10b"></asp:Label>
                                                    :
                                                </td>
                                                <td align="left" width="50%" class="NoWrap">
                                                    <asp:Label ID="LabelAdministratorName" runat="server" Text="" class="f10b"></asp:Label>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td align="right" width="50%" height="5" class="NoWrap">
                                                    <asp:Label ID="LabelClickToConfigure" runat="server" Text="Click Here to Configure"
                                                        class="f10b"></asp:Label>
                                                </td>
                                                <td align="left" width="50%" class="NoWrap">
                                                    &nbsp; &nbsp;
                                                    <asp:ImageButton ID="ImageButtonSettings" SkinID="ImportLdapUsersSettings" runat="server"
                                                        PostBackUrl="ActiveDirectorySettings.aspx?id=import" ToolTip="Configure Active Directory/Domain settings" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" height="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" height="5">
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="98%" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
                                            height="160px" style="display: none">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelDisplayMessage" runat="server" Text="" class="f10b"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
            </tr>
            <tr height="2px">
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <asp:Panel ID="ShowGroup" Visible="True" runat="server">
                <tr height="2px">
                    <td colspan="3" align="center">
                        <asp:Table ID="Table5" runat="server" CellPadding="0" CellSpacing="0" border="0"
                            Width="100%" CssClass="Top_menu_bg" Height="33">
                            <asp:TableRow>
                                <asp:TableCell Width="65%">
                                    <asp:Label ID="LabelGroupHeader" Text="" Font-Bold="true" runat="server"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </td>
                </tr>
                <tr height="2px">
                    <td colspan="3" align="left">
                        <asp:Menu ID="menuGroupTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab"
                            StaticSelectedStyle-CssClass="selectedTab" Orientation="Horizontal" OnMenuItemClick="menuGroupTabs_MenuItemClick"
                            runat="server">
                            <Items>
                                <asp:MenuItem Text="User(s)" Value="User" Selected="true" />
                                <asp:MenuItem Text="Group(s)" Value="Group" />
                            </Items>
                        </asp:Menu>
                    </td>
                </tr>
                <%--   pagination group--%>
                <tr height="2px">
                    <td colspan="3" align="right">
                        <asp:Table ID="TableHeaderLink" Visible="false" runat="server" CellPadding="0" CellSpacing="0"
                            border="0" Width="100%" CssClass="Top_menu_bg" Height="33">
                            <asp:TableRow>
                                <asp:TableCell Width="65%">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                &nbsp; &nbsp;<asp:LinkButton ID="LinkButtonALLGroupUser" CssClass="Grid_tr underline"
                                                    runat="server" OnClick="LinkButtonALLGroupUser_Click">Import All Group(s)</asp:LinkButton>
                                            </td>
                                            <td>
                                                &nbsp; &nbsp;<asp:LinkButton ID="LinkButtonSelectedGroupUser" CssClass="Grid_tr underline"
                                                    runat="server" OnClick="LinkButtonSelectedGroupUser_Click">Import Selected Group(s)</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <asp:HiddenField ID="HiddenGroupUsersCount" Value="0" runat="server" />
                        <asp:HiddenField ID="HiddenFieldSelectedGroupUsersCount" Value="0" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Panel runat="server" ID="PageSizeGroup" Visible="false">
                            <table cellpadding="2" align="right" cellspacing="2" border="0" width="100%">
                                <tr>
                                    <td width="60%" class="NoWrap" align="right">
                                        <asp:Label ID="Label1" runat="server" Text="Page Size" SkinID="TotalResource"></asp:Label>
                                        <asp:DropDownList ID="DropDownPageSizeGroup" CssClass="Normal_FontLabel" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="DropDownPageSizeGroup_SelectedIndexChanged">
                                            <asp:ListItem Selected="true">50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>500</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%" class="NoWrap" align="right">
                                        <asp:Label ID="Label2" runat="server" Text="Page" SkinID="TotalResource"></asp:Label>
                                        <asp:DropDownList ID="DropDownCurrentPageGroup" runat="server" AutoPostBack="true"
                                            CssClass="Normal_FontLabel" OnSelectedIndexChanged="DropDownCurrentPageGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label3" runat="server" SkinID="TotalResource" Text="Total Records : "></asp:Label>
                                        <asp:Label ID="LabelTotalGroupRecords" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <%-- end pagination group--%>
                <%-- Group table--%>
                <tr class="Grid_tr">
                    <td>
                        <asp:Table ID="TableGroup" CssClass="Grid" Width="100%" runat="server" CellPadding="0"
                            CellSpacing="0">
                            <asp:TableHeaderRow ID="TableHeaderRowTitle" runat="server">
                                <asp:TableHeaderCell Width="30" CssClass="H_title">
                                   <%--   <input id="ChkAllGroup" onclick="ChkandUnchkGroup()" type="checkbox" />--%>
                                </asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                        <asp:HiddenField ID="HiddenGroupUserCount" Value="0" runat="server" />
                        <asp:HiddenField ID="HiddenFieldSelectedGroupUserCount" Value="0" runat="server" />
                        <%--  end  Group table--%>
                        <%--user table--%>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <asp:Panel ID="PanelUsersList" Visible="false" runat="server">
                                <tr height="2px">
                                    <td colspan="3" align="right">
                                        <asp:UpdatePanel runat="server" ID="PaginationPanel">
                                            <ContentTemplate>
                                                <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0" border="0"
                                                    Width="100%" CssClass="Top_menu_bg" Height="33">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="65%">
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp; &nbsp;<asp:LinkButton ID="LinkButtonImportAllUsers" runat="server" OnClick="LinkButtonImportAllUsers_Click">Import All AD User(s)</asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp; &nbsp;<asp:LinkButton ID="LinkButtonImportSelectedUsers" runat="server" OnClick="LinkButtonImportSelectedUsers_Click">Import All AD User(s)</asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;&nbsp;<asp:Label ID="LabelImportUsersAs" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;&nbsp;<asp:DropDownList ID="DropDownListImportingUserRole" CssClass="FormDropDown_Small"
                                                                            runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                <asp:HiddenField ID="HiddenUsersCount" Value="0" runat="server" />
                                                <asp:HiddenField ID="HiddenFieldSelectedUsersCount" Value="0" runat="server" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="DropDownPageSize" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="DropDownCurrentPage" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="60%">
                                    </td>
                                    <td width="40%" align="right">
                                        <table cellpadding="2" cellspacing="2" border="0" width="60%">
                                            <tr>
                                                <td width="10%" class="NoWrap">
                                                    <asp:Label ID="LabelPageSize" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                </td>
                                                <td width="10%" class="NoWrap">
                                                    <asp:DropDownList ID="DropDownPageSize" CssClass="Normal_FontLabel" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownPageSize_SelectedIndexChanged">
                                                        <asp:ListItem Selected="true">50</asp:ListItem>
                                                        <asp:ListItem>100</asp:ListItem>
                                                        <asp:ListItem>200</asp:ListItem>
                                                        <asp:ListItem>500</asp:ListItem>
                                                        <asp:ListItem>1000</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="10%" class="NoWrap">
                                                    <asp:Label ID="LabelPage" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                </td>
                                                <td width="10%" class="NoWrap">
                                                    <asp:DropDownList ID="DropDownCurrentPage" runat="server" AutoPostBack="true" CssClass="Normal_FontLabel"
                                                        OnSelectedIndexChanged="DropDownCurrentPage_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="10%" class="NoWrap">
                                                    <asp:Label ID="LabelTotalRecordsTitle" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                    :
                                                </td>
                                                <td width="10%">
                                                    <asp:Label ID="LabelTotalRecordsValue" runat="server" SkinID="TotalResource" Text=""></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2'" align="center" valign="middle">
                                        <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
                                            <asp:UpdateProgress ID="updateProgress" DisplayAfter="0" runat="server">
                                                <ProgressTemplate>
                                                    <div style="position: relative; vertical-align: middle; text-align: center;">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td width="20%" valign="middle">
                                                                    <asp:Image ID="Image1" SkinID="ImportLdapUsersloading" runat="server" Style="vertical-align: middle" />
                                                                </td>
                                                                <td width="80%" align="left" valign="middle">
                                                                    <asp:Label ID="LabelPageLoading" runat="server" Text="Retrieving Active Directory User information. <br> Please Wait ..."
                                                                        class="f10b"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </asp:Panel>
                                        <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
                                            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
                                        <asp:UpdatePanel ID="UpdatePanelRefresh" runat="server">
                                            <ContentTemplate>
                                                <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                                    <tr class="Grid_tr">
                                                        <td>
                                                            <asp:Table EnableViewState="false" ID="TableUsers" CellSpacing="1" CellPadding="3"
                                                                Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                    <asp:TableHeaderCell Width="30" CssClass="H_title">
                                                        <input id="chkALL" onclick="ChkandUnchk()" type="checkbox" />
                                                                    </asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell Width="30">
                                                                    </asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellLogOnName" Wrap="false" HorizontalAlign="Left"
                                                                        CssClass="H_title"> 
                                                                    </asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellUserName" Wrap="false" HorizontalAlign="Left"
                                                                        CssClass="H_title"> 
                                                                    </asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellEmail" Wrap="false" HorizontalAlign="Left"
                                                                        CssClass="H_title"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellDepartment" HorizontalAlign="Left" Wrap="false"
                                                                        CssClass="H_title"></asp:TableHeaderCell>
                                                                </asp:TableHeaderRow>
                                                            </asp:Table>
                                                            <asp:Panel ID="DemoPanel" Visible="false" runat="server" Width="100%" Height="300px">
                                                                <asp:Label ID="LabelMessage" runat="server" Text="Label"></asp:Label>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <%-- <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ButtonGo" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownPageSize" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownCurrentPage" EventName="SelectedIndexChanged" />
                                </Triggers>--%>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="5">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Table ID="TableWarningMessage" Visible="true" CellSpacing="1" CellPadding="3"
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
                                           <p class="LabelWarningFont">Click on "Go" button to retrieve Active Directory user(s) .</p>
                                           <p class="LabelLoginFont"></p>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                        <%--end user table--%>
                    </td>
                </tr>
            </asp:Panel>
            </table> </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">        Blink('prem_hint');</script>
</asp:Content>
