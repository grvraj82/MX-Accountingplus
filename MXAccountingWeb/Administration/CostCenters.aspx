<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="CostCenters.aspx.cs" Inherits="AccountingPlusWeb.Administration.CostCenters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowPrice();
        Meuselected("Pricing");

        function DeleteCostCenters() {
            if (IsDepSelected()) {
                return confirm(C_COSTCENTER_CONFIRMATION);
            }
            else {
                return false;
            }
        }

        function EditCostCenterDetails() {
            if (IsDepSelected()) {
                if (GetSeletedCount() > 1) {
                    jNotify(C_SELECT_ONECOSTCENTER)

                    return false;
                }
            }
            else {
                return false;
            }
        }


        function disablekeys() {
            var keyc = event.keyCode;          
            if (event.keyCode == 39 || event.keyCode == 44) {
                event.keyCode = 0;
                //alert('These keys are disabled');
                event.returnValue = false;
            }
        }

        


        function IsDepSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__COSTCENTERID.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__COSTCENTERID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__COSTCENTERID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                jNotify(C_SELECT_COSTCENTER)

                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__COSTCENTERID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__COSTCENTERID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__COSTCENTERID.checked) {
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
            if (document.getElementById("ctl00_PageContent_HiddenFieldDeviceCount").value == 0) {
                document.getElementById("chkALL").checked = false;
            }
        }
        function togall(refcheckbox) {

            var thisForm = document.forms[0];
            var users = thisForm.__COSTCENTERID.length;
            var selectedCount = 0;
            if (thisForm.__COSTCENTERID[refcheckbox - 1].checked) {

                thisForm.__COSTCENTERID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__COSTCENTERID[refcheckbox - 1].checked = true; ;
            }
            ValidateSelectedCount();
        }
        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldDeviceCount").value);

            if (totalRecords == parseInt(GetSeletedCount() + 1)) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }
        function Required() {
            if (document.getElementById('ctl00_PageContent_TextBoxDepartmentName').value == "") {
                jNotify(C_COSTCENTER_NAME_EMPTY)

                return false;

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

        document.onkeypress = myKeyPressHandler;
    </script>
    <div id="content" style="display: none;">
    </div>
    <%--<div style="height:10px;">&nbsp;</div>--%>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td valign="top" align="left">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr class="Top_menu_bg">
                                    <td class="HeadingMiddleBg" style="width: 10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadCostCenters" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td valign="top" style="width: 88%">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td align="left" width="10%" valign="middle">
                                                    <table cellpadding="2" cellspacing="2" border="0">
                                                        <tr id="tablerowMain" runat="server">
                                                            <td align="center" valign="middle" width="10%">
                                                                <asp:ImageButton SkinID="IBAdd" ID="IBAdd" ToolTip="" runat="server" OnClick="IBAdd_Click"
                                                                    CausesValidation="false" />
                                                            </td>
                                                            <td align="center" class="MenuSpliter" valign="middle" width="15%">
                                                                <asp:ImageButton SkinID="IBEdit" ToolTip="" runat="server" ID="IBEdit" OnClick="IBEdit_Click"
                                                                    CausesValidation="false" />
                                                            </td>
                                                            <td align="center" class="MenuSpliter" valign="middle" width="15%">
                                                                <asp:ImageButton SkinID="IBDelete" ToolTip="" ID="IBDelete" runat="server" OnClick="IBDelete_Click"
                                                                    CausesValidation="false" />
                                                            </td>
                                                            <td align="left" valign="middle" width="15%" style="display: none">
                                                                <asp:Label ID="LabelHeadingAddCostCenter" runat="server" SkinID="TotalResource" Text="Add Cost Center(s)">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                    <tr>
                        <td valign="top" align="center">
                            <asp:Panel ID="PanelMainData" Visible="true" runat="server">
                                <table width="98%" border="0" cellpadding="0" cellspacing="0" class="">
                                    <tr id="tablerowMainTable" runat="server">
                                        <td align="right">
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
                                        <td id="tablecellMainTable" height="500" valign="top" runat="server">
                                            <asp:Table EnableViewState="false" ID="TableCostCenters" CellSpacing="1" CellPadding="3"
                                                Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                    <asp:TableHeaderCell HorizontalAlign="Left"><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell Width="30" HorizontalAlign="Left"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCellCostCenterName" Text="" HorizontalAlign="Left"
                                                        CssClass="H_title" Width="20%" Wrap="false"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCellIsEnabled" Text="" HorizontalAlign="Left"
                                                        CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCellIsShared" Text="" HorizontalAlign="Left"
                                                        CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                                </asp:TableHeaderRow>
                                            </asp:Table>
                                        </td>
                                    </tr>
                                </table>
                                <table cellpadding="3" cellspacing="1" border="0" id="TableAddGroups" runat="server"
                                    visible="false">
                                    <tr class="Grid_tr" id="TableRowSQLCreditinals" runat="server" visible="true">
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                <tr>
                                                    <td>
                                                        <table align="center" cellpadding="0" cellspacing="0" border="0" class="table_border_org"
                                                            width="100%">
                                                            <tr>
                                                                <td colspan="3" align="left" valign="top">
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
                                                                <td align="right" height="30" width="40%">
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="LabelAddCostCenter" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left" colspan="2">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="TextBoxCostCenterName" runat="server" MaxLength="30" TabIndex="1"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Image ID="Imageuser" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                                                    padding-left: 5px;" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCostCenter" ControlToValidate="TextBoxCostCenterName"
                                                                                    runat="server" ErrorMessage="Required field"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none">
                                                                <td align="right" height="30">
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="LabelDescription" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left" colspan="2">
                                                                    <asp:TextBox ID="TextBoxDescription" runat="server" MaxLength="200"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" height="30">
                                                                    &nbsp;&nbsp;<asp:Label ID="LabelCostCenterActive" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left" colspan="2">
                                                                    <asp:CheckBox ID="CheckBoxCostCenterActive" runat="server" TabIndex="2" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" height="30">
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="LabelIsCostCenterShared" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left" colspan="2">
                                                                    <asp:CheckBox ID="CheckBoxIsCostCenterShared" runat="server" TabIndex="3" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center" id="TableRowGroup" runat="server" visible="true">
                                        <td colspan="4" height="35">
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td height="10">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2" style="white-space: nowrap;">
                                                        <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="" TabIndex="4"
                                                            OnClick="ButtonSave_Click" />
                                                        <asp:Button ID="ButtonUpdate" CssClass="Login_Button" CausesValidation="false" runat="server"
                                                            Text="" TabIndex="5" OnClick="ButtonUpdate_Click" Visible="false" />
                                                        <asp:Button ID="ButtonCancel" CssClass="Cancel_button" CausesValidation="false" runat="server"
                                                            Text="" TabIndex="6" OnClick="ButtonCancel_Click" />
                                                        <asp:Button runat="server" ID="ButtonReset" TabIndex="7" Text="" CssClass="Login_Button"
                                                            OnClientClick="this.form.reset();return false;" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="10">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="5">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Table ID="TableWarningMessage" Visible="false" CellSpacing="1" CellPadding="3"
                    Width="50%" runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                        <asp:TableHeaderCell ID="TableHeaderCellDivName" CssClass="LabelWarningFont" ColumnSpan="2"
                            HorizontalAlign="Left">Warning</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow Height="500" CssClass="GridRow">
                        <asp:TableCell ID="TableCellWarningImage" HorizontalAlign="Center" Width="20%">
                            <asp:Image ID="ImageWarning" runat="server" SkinID="PermessionsAndLimitsCritical" />
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell1" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                                           <p  class="LabelLoginFont"> </p>
                                           <p class="LabelWarningFont">There are no Cost Centers created.</p>
                                           <p class="LabelLoginFont"></p>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenFieldDeviceCount" Value="0" runat="server" />
    <asp:HiddenField ID="HiddenFieldAddEdit" Value="0" runat="server" />
</asp:Content>
