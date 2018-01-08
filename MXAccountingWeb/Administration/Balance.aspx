<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    Inherits="AccountingPlusWeb.Administration.Balance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
    <link href="../Notify/lightbox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script type="text/javascript">
        DisplayTabs();
        Meuselected("ctl00_trBalance");

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
    <script src="../Notify/jquery.min.js" type="text/javascript"></script>
    <script src="../Notify/lightbox.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                                    <asp:Label ID="LabelHeadUserManagement" runat="server" Text="Balance"></asp:Label></div>
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
                                                            <asp:Label ID="LabelUserSource" runat="server" Text="UserSource" SkinID="TotalResource"></asp:Label>
                                                            :
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="tabelCellDropDownUserSource" align="center" valign="middle" runat="server"
                                                            Visible="true" CssClass="NoWrap">
                                                            <asp:DropDownList ID="DropDownListUserSource" CssClass="FormDropDown_Small" runat="server"
                                                                AutoPostBack="true" OnSelectedIndexChanged="DropDownListUserSource_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCellLabelUsers" align="center" valign="middle" runat="server"
                                                            CssClass="NoWrap" Visible="false">
                                                            <asp:Label ID="Label1" runat="server" Text="User" SkinID="TotalResource"></asp:Label>
                                                            :
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell3" align="center" valign="middle" runat="server" CssClass="NoWrap"
                                                            Visible="false">
                                                            <asp:DropDownList ID="DropDownListUsers" CssClass="FormDropDown_Small" runat="server"
                                                                AutoPostBack="true" OnSelectedIndexChanged="DropDownListUsers_SelectedIndexChanged">
                                                                <%--     <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="DropDownListUsers">
                                                                </cc1:ListSearchExtender>--%>
                                                            </asp:DropDownList>
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="MenuSpliter">
                                                            <asp:ImageButton ID="ImageButtonAdd" ToolTip="Add Balance" runat="server" CausesValidation="False"
                                                                SkinID="ManageusersimgAddusers" OnClick="ImageButtonAdd_Click" />
                                                        </asp:TableCell>
                                                         <asp:TableCell CssClass="MenuSpliter">
                                                             <asp:ImageButton ID="ImageButtonImport" ToolTip="Import Top-Up Cards" SkinID="ManageUserCards"
                                                                runat="server" CausesValidation="False" OnClick="ImageButtonImport_Click" />
                                                          
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="margin-top: 0">
                                <td colspan="2" valign="top" align="center" style="margin-top: 0">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr id="trAdd" runat="server" visible="false" align="center">
                                            <td align="center" valign="top">
                                                <table width="90%" class="table_border_org" cellpadding="0" cellspacing="0" border="0">
                                                    <tr class="Top_menu_bg">
                                                        <td class="f10b" height="35" colspan="2" align="left">
                                                            &nbsp;
                                                            <asp:Label ID="LabelJobRetention" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="47%" height="35">
                                                            <asp:Label ID="LabelDays" runat="server" Text="Recharge Amount/Unit" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="TextBoxAmount" runat="server" MaxLength="8"></asp:TextBox>
                                                        <%--    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                                ID="Image3" />--%>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxAmount"
                                                                ValidationExpression="^([0-9-_'.,/ ]*)$" ErrorMessage="Only Numerics allowed"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="47%" height="35">
                                                            <asp:Label ID="Label4" runat="server" Text="Recharge ID" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="TextBoxRecharge" runat="server" MaxLength="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="47%" height="35">
                                                            <asp:Label ID="LabelTime" runat="server" Text="Remarks" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="TextBoxRemarks" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="display: none">
                                                        <td align="right" width="47%" height="35">
                                                            <asp:Label ID="Label3" runat="server" Text="Apply for all users" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:CheckBox ID="CheckBoxBalance" Checked="false" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr valign="middle" align="center">
                                                        <td colspan="2">
                                                            <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="Save" OnClick="ButtonSave_Click" />
                                                            <asp:Button ID="ButtonCancel" CssClass="Cancel_button" CausesValidation="false" runat="server"
                                                                Text="Close" OnClick="ButtonCancel_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr height="5px">
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <%--    ----------------------------------New changes comes in this space holder-------------------------------------------------------------------   --%>
                                        <tr>
                                            <td valign="top">
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
                                                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                            <tr>
                                                                                                <td style="height: 15px">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table cellpadding="0" cellspacing="0" width="250px">
                                                                                                        <tr style="background-color: White; height: 25px">
                                                                                                            <td>
                                                                                                                <asp:TextBox BorderWidth="0" ToolTip="" CssClass="SearchTextBox" OnTextChanged="SearchTextBox_OnTextChanged"
                                                                                                                    AutoPostBack="true" ID="TextBoxCostProfileSearch" Text="*" runat="server" Width="100%"></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:ImageButton ID="ImageButtonSearchCostProfile" OnClick="ImageButtonSearchCostProfile_Click"
                                                                                                                    runat="server" SkinID="SearchList" />
                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxCostProfileSearch"
                                                                                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" ServicePath="~/WebServices/ContextSearch.asmx"
                                                                                                                    CompletionInterval="1000" ServiceMethod="GetUserNames" CompletionListCssClass="autocomplete_completionListElement"
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
                                                                                    <td width="99%" valign="top">
                                                                                        <asp:Table ID="TableMainData" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0"
                                                                                            runat="server">
                                                                                            <asp:TableRow>
                                                                                                <asp:TableCell VerticalAlign="Top">
                                                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                        <tr>
                                                                                                            <td style="width: 99%; padding-right: 2px" valign="middle" height="40" align="right">
                                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                    <tr>
                                                                                                                        <td align="left">
                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" height="33" width="100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0">
                                                                                                                                            <asp:TableRow>
                                                                                                                                                <asp:TableCell ID="TableCell20" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                                                                                                                    VerticalAlign="Middle" CssClass="NoWrap">
                                                                                                                                                    <asp:Label ID="LabelFromDate" runat="server" Text="From Date" SkinID="TotalResource"></asp:Label>
                                                                                                                                                    :&nbsp;&nbsp;
                                                                                                                                                </asp:TableCell>
                                                                                                                                                <asp:TableCell ID="TableCell21" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                                                                                                                    VerticalAlign="Middle" CssClass="NoWrap">
                                                                                                                                                    <asp:DropDownList ID="DropDownListFromMonth" runat="server">
                                                                                                                                                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <asp:DropDownList ID="DropDownListFromDate" runat="server">
                                                                                                                                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <asp:DropDownList ID="DropDownListFromYear" runat="server">
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <%-- <asp:TextBox ID="TextBoxFromDate" Width="80px" CssClass="Normal_FontLabel" runat="server"></asp:TextBox>--%>
                                                                                                                                                    <%--<asp:CompareValidator ID="cmpStartDate" runat="server" ControlToValidate="TextBoxFromDate"
                                            ErrorMessage="" Operator="LessThanEqual"  Display="None"></asp:CompareValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="TopLeft"
                                            runat="server" TargetControlID="cmpStartDate">
                                        </cc1:ValidatorCalloutExtender>--%>
                                                                                                                                                    <%-- <cc1:CalendarExtender ID="CalendarExtenderFrom" runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxFromDate">
                                        </cc1:CalendarExtender>--%>
                                                                                                                                                </asp:TableCell>
                                                                                                                                                <asp:TableCell ID="TableCell22" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                                                                                                                    VerticalAlign="Middle" CssClass="NoWrap">
                                                                                                                                                    &nbsp;&nbsp;<asp:Label ID="LabelToDate" runat="server" Text="To Date" SkinID="TotalResource"></asp:Label>
                                                                                                                                                    :&nbsp;&nbsp;
                                                                                                                                                </asp:TableCell>
                                                                                                                                                <asp:TableCell ID="TableCell23" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                                                                                                                    VerticalAlign="Middle" CssClass="NoWrap">
                                                                                                                                                    <asp:DropDownList ID="DropDownListToMonth" runat="server">
                                                                                                                                                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <asp:DropDownList ID="DropDownListToDate" runat="server">
                                                                                                                                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <asp:DropDownList ID="DropDownListToYear" runat="server">
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <%-- <asp:TextBox ID="TextBoxToDate" Width="80px" CssClass="Normal_FontLabel" runat="server"></asp:TextBox>--%>
                                                                                                                                                    <%-- <asp:CompareValidator ID="CompareValidatorToDate" runat="server" ControlToValidate="TextBoxToDate"
                                            ErrorMessage="End date cannot be greater than today's date" Operator="LessThanEqual"
                                             Display="None"></asp:CompareValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" PopupPosition="TopLeft"
                                            TargetControlID="CompareValidatorToDate" runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
                                                                                                                                                    <%--<cc1:CalendarExtender ID="CalendarExtenderTo" runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxToDate">
                                        </cc1:CalendarExtender>--%>
                                                                                                                                                </asp:TableCell>
                                                                                                                                                <asp:TableCell ID="TableCell24" align="center" valign="middle" runat="server" Visible="true"
                                                                                                                                                    HorizontalAlign="Left" CssClass="NoWrap">
                                                                                                                                                    &nbsp;&nbsp;<asp:Button ID="ButtonGo" CssClass="Login_Button" runat="server" Text="Submit"
                                                                                                                                                        ToolTip="" OnClick="ButtonGo_Click" />
                                                                                                                                                </asp:TableCell>
                                                                                                                                            </asp:TableRow>
                                                                                                                                        </asp:Table>
                                                                                                                                    </td>
                                                                                                                                    <td width="50%" align="right" valign="middle" class="HeaderPaddingR">
                                                                                                                                        <asp:UpdatePanel runat="server" ID="PaginationPanel">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:Table ID="Table31" runat="server" CellPadding="2" CellSpacing="0">
                                                                                                                                                    <asp:TableRow>
                                                                                                                                                        <asp:TableCell ID="TableCell6" align="center" valign="middle" runat="server" Visible="true"
                                                                                                                                                            CssClass="NoWrap">
                                                                              
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                </asp:Table>
                                                                                                                                            </ContentTemplate>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="Label2" runat="server" Text="Current balance Amount/Unit : "></asp:Label><asp:Label
                                                                                                                                ID="LabelBalanceAmount" CssClass="LabelLoginFont" runat="server" Text=""></asp:Label>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
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
                                                                                                                <asp:Table EnableViewState="false" ID="TableUsers" SkinID="Grid" CellSpacing="0"
                                                                                                                    CellPadding="3" Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg"
                                                                                                                    border="0">
                                                                                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                                                        <asp:TableHeaderCell ID="TableHeaderCell1" Wrap="false" Width="30px" CssClass="H_title">
                                                                                                                        </asp:TableHeaderCell>
                                                                                                                        <asp:TableHeaderCell ID="TableHeaderCellDate" Wrap="false" CssClass="H_title">
                                                                                                                            <asp:Label ID="Labeldate" runat="server" Text="Date"></asp:Label>
                                                                                                                        </asp:TableHeaderCell>
                                                                                                                        <asp:TableHeaderCell ID="TableHeaderCellRemarks" Wrap="false" CssClass="H_title">
                                                                                                                            <asp:Label ID="LabelRemarks" runat="server" Text="Remarks"></asp:Label>
                                                                                                                        </asp:TableHeaderCell>
                                                                                                                        <asp:TableHeaderCell ID="TableHeaderCellJobLogId" Wrap="false" CssClass="H_title">
                                                                                                                            <asp:Label ID="LabelLogID" runat="server" Text="Reference No"></asp:Label>
                                                                                                                        </asp:TableHeaderCell>
                                                                                                                        <asp:TableHeaderCell ID="TableHeaderCellUserName" Width="70px" Wrap="false" CssClass="H_title">
                                                                                                                            <asp:Label ID="LabelUserName" runat="server" Text="Debit"></asp:Label>
                                                                                                                        </asp:TableHeaderCell>
                                                                                                                        <asp:TableHeaderCell ID="TableHeaderCellAmount" Width="70px" Wrap="false" CssClass="H_title">
                                                                                                                            <asp:Label ID="LabelAmount" runat="server" Text="Credit"></asp:Label>
                                                                                                                        </asp:TableHeaderCell>
                                                                                                                        <asp:TableHeaderCell ID="TableHeaderCellJOBTYPE" Wrap="false" CssClass="H_title">
                                                                                                                            <asp:Label ID="LabelJobType" runat="server" Text="Closing Balance"></asp:Label>
                                                                                                                        </asp:TableHeaderCell>
                                                                                                                    </asp:TableHeaderRow>
                                                                                                                </asp:Table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:TableCell>
                                                                                                <asp:TableCell Visible="false" VerticalAlign="Top" CssClass="Grid_tr">
                                                                                                    <asp:Table ID="TableFilter" Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                                                                                        <asp:TableRow>
                                                                                                            <asp:TableCell HorizontalAlign="Left">
                                                                                                                
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
                                        <%--  -------------------------------------------------------------------------------------------------------------------------------------------    --%>
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
            <asp:HiddenField ID="HiddenUserID" Value="0" runat="server" />
            <asp:HiddenField ID="HiddenFieldisSortingEnable" Value="true" runat="server" />
            <asp:HiddenField ID="HiddenFieldUserSource" Value="DB" runat="server" />
            <asp:HiddenField ID="HiddenFieldAdminUserAccId" Value="" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ImageButtonAdd" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
