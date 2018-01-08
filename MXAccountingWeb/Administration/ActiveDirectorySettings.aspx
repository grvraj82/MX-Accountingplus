<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ActiveDirectorySettings.aspx.cs" Inherits="AccountingPlusWeb.Administration.ActiveDirectorySettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
        function DeleteDomain() {
            if (IsDomainSelected()) {
                return confirm(C_DOMAIN_CONFIRMATION);
            }
            else {
                return false;
            }
        }

        function EditCostCenterDetails() {
            if (IsDomainSelected()) {
                if (GetSeletedCount() > 1) {
                    jNotify('C_SELECT_ONEDOMAIN')

                    return false;
                }
            }
            else {
                return false;
            }
        }

        function IsDomainSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__ADNAME.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__ADNAME[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__ADNAME.checked) {
                    selectedCount++
                    return true;
                }
            }
            if (selectedCount == 0) {
                jNotify(C_SELECT_DOMAIN)
                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__ADNAME.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__ADNAME[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__ADNAME.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }

        function ChkandUnchk() {

            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].value != '1') {
                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = false;
                }
            }
            if (document.getElementById("ctl00_PageContent_HiddenFieldDomainCount").value == 0) {
                document.getElementById("chkALL").checked = false;
            }
        }

        function togall(refcheckbox) {

            var thisForm = document.forms[0];
            var users = thisForm.__ADNAME.length;
            var selectedCount = 0;
            if (thisForm.__ADNAME[refcheckbox - 1].checked) {

                thisForm.__ADNAME[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__ADNAME[refcheckbox - 1].checked = true; ;
            }
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldDomainCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }

        function myKeyPressHandler() {
            if (event.keyCode == 13) {
                var hiddenvalue = document.getElementById('ctl00_PageContent_HiddenFieldAddEdit').value;
                if (hiddenvalue == "1") {
                    document.getElementById('ctl00_PageContent_ButtonSave').focus();
                }
                if (hiddenvalue == "2") {
                    var description = document.getElementById('ctl00_PageContent_TextBoxDescription').value;
                    if (description == "" || description == null) {
                        document.getElementById('ctl00_PageContent_TextBoxDescription').focus();
                        return false;
                    }
                    else {
                        document.getElementById('ctl00_PageContent_ButtonUpdate').focus();
                    }
                }
            }
        }

        function AllowNumeric() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else
                return false;
        }

        function myKeyPressHandler() {
            if (event.keyCode == 13) {
                document.getElementById('ctl00_PageContent_ButtonUpdate').focus();
            }
        }

        document.onkeypress = myKeyPressHandler;
    </script>
    <div id="content" style="display: none;">
    </div>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550"
        class="table_border_org">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image4" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td valign="top" align="left">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr class="Top_menu_bg">
                                    <%-- <td valign="top" class="Top_menu_bg HeaderPadding">--%>
                                    <td class="HeadingMiddleBg" width="10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelActiveDirectorySettings" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image7" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td valign="top" style="width: 88%">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td align="left" width="10%" valign="middle">
                                                    <asp:Table runat="server" ID="TableMenuBar" CellPadding="3" CellSpacing="3">
                                                        <asp:TableRow>
                                                            <asp:TableCell>
                                                                <asp:ImageButton SkinID="IBAdd" ID="IBAdd" ToolTip="" runat="server" OnClick="IBAdd_Click"
                                                                    CausesValidation="false" />
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="tdIBEdit" CssClass="MenuSpliter">
                                                                <asp:ImageButton SkinID="IBEdit" ToolTip="" runat="server" ID="IBEdit" OnClick="IBEdit_Click"
                                                                    CausesValidation="false" />
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="tdIBDelete" CssClass="MenuSpliter">
                                                                <asp:ImageButton SkinID="IBDelete" ToolTip="" ID="IBDelete" runat="server" OnClick="IBDelete_Click"
                                                                    CausesValidation="false" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                                <td width="90%">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table width="98%" border="0" cellpadding="0" cellspacing="0" class="">
                                <tr id="tablerowMainTable" runat="server">
                                    <td align="right" style="display: none">
                                        <asp:Table ID="Table1" runat="server" CellPadding="3" CellSpacing="0">
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
                                </tr>
                                <tr>
                                    <td style="height: 1px">
                                    </td>
                                </tr>
                                <tr class="Grid_tr">
                                    <td id="tablecellMainTable" runat="server">
                                        <asp:Table EnableViewState="false" ID="TableActiveDirectory" CellSpacing="1" CellPadding="3"
                                            Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell HorizontalAlign="Left"><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                                <asp:TableHeaderCell Width="30" HorizontalAlign="Left"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellDomainName" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title" Width="20%" Wrap="false"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellDomainAlias" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title" Width="20%" Wrap="false"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellUserName" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellPassword" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellPort" Text="" HorizontalAlign="Left" CssClass="H_title"
                                                    Wrap="false"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellFullNameAttribute" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="3" cellspacing="1" border="0" id="TableAddADGroup" runat="server"
                                visible="false" width="60%">
                                <tr class="Grid_tr" id="TableRowSQLCreditinals" runat="server" visible="true">
                                    <td>
                                        <table width="70%" class="table_border_org" cellpadding="0" cellspacing="0" border="0">
                                            <tr class="Top_menu_bg">
                                                <td class="f10b" height="35" colspan="2" align="left">
                                                    &nbsp;
                                                    <asp:Label ID="LabelActiveDirectoryHeding" runat="server" SkinID="LabelLogon" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" width="100%" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="right" width="45%" height="35">
                                                                <asp:Label ID="LabelDomainController" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left" width="55%" height="35">
                                                                <asp:TextBox ID="TextBoxDomainController" runat="server" MaxLength="50" CssClass="FormTextBox_bg"
                                                                    TabIndex="1"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="35" style="white-space: nowrap">
                                                                <asp:Label ID="LabelDomainName" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left" style="white-space: nowrap">
                                                                <asp:TextBox ID="TextBoxDomainName" runat="server" MaxLength="50" CssClass="FormTextBox_bg"
                                                                    TabIndex="2"></asp:TextBox>
                                                                <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                                    ID="ImageDomainName" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="35" style="white-space: nowrap">
                                                                <asp:Label ID="LabelDomainAlias" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left" style="white-space: nowrap">
                                                                <asp:TextBox ID="TextBoxDomainAlias" runat="server" MaxLength="50" CssClass="FormTextBox_bg"
                                                                    TabIndex="2"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="35" style="white-space: nowrap">
                                                                <asp:Label ID="LabelUserName" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left" style="white-space: nowrap">
                                                                <asp:TextBox ID="TextBoxUserName" runat="server" MaxLength="100" CssClass="FormTextBox_bg"
                                                                    TabIndex="3"></asp:TextBox>
                                                                <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                                    ID="ImageUserName" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="35" style="white-space: nowrap">
                                                                <asp:Label ID="LabelPassword" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left" style="white-space: nowrap">
                                                                <asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server" MaxLength="50"
                                                                    CssClass="FormTextBox_bg" TabIndex="4"></asp:TextBox>
                                                                <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                                    ID="ImagePassword" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="35" style="white-space: nowrap">
                                                                <asp:Label ID="LabelPort" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left" style="white-space: nowrap">
                                                                <asp:TextBox ID="TextBoxPort" runat="server" MaxLength="5" CssClass="FormTextBox_bg"
                                                                    TabIndex="5" onkeypress="javascript:return AllowNumeric()"></asp:TextBox>
                                                                <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                                    ID="ImagePort" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="35">
                                                                <asp:Label ID="LabelFullName" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left" style="white-space: nowrap">
                                                                <asp:DropDownList ID="DropDownListFullName" CssClass="Dropdown_CSS" runat="server"
                                                                    TabIndex="6">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" width="100%" colspan="2" style="white-space: nowrap">
                                                                <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="" TabIndex="11"
                                                                    OnClick="ButtonSave_Click" />
                                                                <asp:Button ID="ButtonUpdate" runat="server" CssClass="Login_Button" OnClick="ButtonUpdate_Click"
                                                                    Text="" Visible="True" TabIndex="7" />
                                                                <asp:Button ID="ButtonTest" runat="server" CssClass="Login_Button" Text="" Visible="True"
                                                                    OnClick="ButtonTest_Click" CausesValidation="false" TabIndex="8" />
                                                                <asp:Button ID="ButtonCancel" runat="server" CssClass="Cancel_button" Text="" Visible="True"
                                                                    OnClick="ButtonCancel_Click" CausesValidation="false" TabIndex="9" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="5">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" align="center">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDomainName" ControlToValidate="TextBoxDomainName"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDomainName"
                                ID="ValidatorCalloutExtenderDomainName" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" ControlToValidate="TextBoxUserName"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorUserName" ID="ValidatorCalloutExtenderUserName"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" ControlToValidate="TextBoxPassword"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorPassword" ID="ValidatorCalloutExtenderPassword"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPort" ControlToValidate="TextBoxPort"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorPort" ID="ValidatorCalloutExtenderPort"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenFieldAddEdit" Value="0" runat="server" />
                <asp:HiddenField ID="HiddenFieldDomainCount" Value="0" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
