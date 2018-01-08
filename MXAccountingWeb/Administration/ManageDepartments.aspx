<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ManageDepartments.aspx.cs" Inherits="PrintRoverWeb.Administration.ManageDepartments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
        function DeleteDepartments() {
            if (IsDepSelected()) {
                return confirm(C_DEPARTMENT_DELETE_CONFIRMATION);
            }
            else {
                return false;
            }
        }

        function EditDepDetails() {
            if (IsDepSelected()) {
                if (GetSeletedCount() > 1) {
                    jNotify(C_SELECT_ONEDEPARTMENT)
                    
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function IsDepSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__DEPID.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__DEPID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__DEPID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                jNotify(C_SELECT_DEPARTMENT)
                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__DEPID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__DEPID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__DEPID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
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
        }
        function togall(refcheckbox) {

            var thisForm = document.forms[0];
            var users = thisForm.__DEPID.length;
            var selectedCount = 0;
            if (thisForm.__DEPID[refcheckbox - 1].checked) {

                thisForm.__DEPID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__DEPID[refcheckbox - 1].checked = true; ;
            }

        }
        function Required() {
            if (document.getElementById('ctl00_PageContent_TextBoxDepartmentName').value == "") {
                jNotify(C_DEPARTMENT_NAME_EMPTY)
               
                return false;

            }
            else if (document.getElementById('ctl00_PageContent_TextBoxDescription').value == "") {
                jNotify(C_DESCRIPTION_EMPTY)
                
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
    <div style="height: 10px;">
        &nbsp;</div>
    <table width="98%" align="center" border="0" cellpadding="0" cellspacing="0" height="550" class="table_border_org">
        <tr>
            <td width="72%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                    <tr>
                        <td valign="top" align="left">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <div id="divEditUsers" runat="server">
                                    <tr class="Top_menu_bg">
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                                                <tr width="100%">
                                                    <td align="left" width="10%" valign="middle">
                                                        <table cellpadding="2" cellspacing="0" border="0" height="33" width="70%">
                                                            <tr>
                                                                <td align="center" valign="middle" width="10%">
                                                                    <asp:ImageButton SkinID="IBAdd" ID="IBAdd" ToolTip="" runat="server" OnClick="IBAdd_Click" />
                                                                </td>
                                                                <td align="center" valign="middle" width="15%">
                                                                    <asp:Image ID="ImageMenuSplitBar" runat="server" SkinID="ManageusersimgSplit" />
                                                                </td>
                                                                <td align="center" valign="middle" width="15%">
                                                                    <asp:ImageButton SkinID="IBEdit" ToolTip="" runat="server" ID="IBEdit" OnClick="IBEdit_Click" />
                                                                </td>
                                                                <td align="center" valign="middle" width="15%">
                                                                    <asp:Image ID="ImageMenuSplit" runat="server" SkinID="ManageusersimgSplit" />
                                                                </td>
                                                                <td align="center" valign="middle" width="15%">
                                                                    <asp:ImageButton SkinID="IBDelete" ToolTip="" ID="IBDelete" runat="server" OnClick="IBDelete_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="70%" align="left" valign="middle">
                                                        <asp:Label ID="LabelHeadingDepartments" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </div>
                                <div id="divRequired" runat="server" visible="false">
                                    <td height="33" align="left" valign="middle" width="4%" class="Top_menu_bg">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" CausesValidation="False" SkinID="ManageDepartmentsBackPage" 
                                            OnClick="ImageButtonBack_Click" Visible="true" />
                                    </td>
                                    <td class="Menu_split">
                                    </td>
                                    <td align="left" valign="middle" width="4%" class="Top_menu_bg">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" CausesValidation="true" ImageAlign="Middle"
                                             SkinID="ManageDepartmentssave" ToolTip="" OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td class="Menu_split">
                                    </td>
                                    <td align="left" valign="middle" width="10%" class="Top_menu_bg">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="ManageDepartmentsReset" 
                                            ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <td height="33" align="left" valign="middle" class="Top_menu_bg">
                                        <asp:Label ID="LabelAddHeadingDepartments" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td height="33" align="right" valign="middle" width="80%" class="Top_menu_bg">
                                        <asp:Image ID="ImageLogOnRequired" runat="server" SkinID="LogonImgRequired" />&nbsp;
                                        <asp:Label ID="LabelRequiredField" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                        &nbsp;&nbsp;
                                    </td>
                                </div>
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
                            <asp:Panel Visible="false" ID="PanelAddDepartment" runat="server">
                                <table width="98%" border="0" cellpadding="0" cellspacing="0">
                                    <tr height="31px">
                                        <td align="right" width="47%">
                                            <asp:Label ID="LabelAddDepartment" runat="server" class="f10b" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" align="left">
                                            &nbsp;
                                            <asp:TextBox ID="TextBoxDepartmentName" runat="server" CssClass="FormTextBox_bg"
                                                MaxLength="15"></asp:TextBox>
                                            <asp:Label ID="LabelDepartmentName" runat="server" CssClass="FormTextBox_bg" Text=""
                                                Visible="false"></asp:Label>
                                        </td>
                                        <td valign="middle">
                                            &nbsp;<asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" />
                                        </td>
                                    </tr>
                                    <tr height="31px">
                                        <td align="right" width="47%">
                                            <asp:Label ID="LabelDescription" runat="server" class="f10b" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" align="left">
                                            &nbsp;
                                            <asp:TextBox ID="TextBoxDescription" runat="server" CssClass="FormTextBox_bg" MaxLength="200"></asp:TextBox>
                                        </td>
                                        <td valign="middle">
                                            &nbsp;<asp:Image ID="Image2" runat="server" SkinID="LogonImgRequired" />
                                            <td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="47%">
                                            <asp:Label ID="LabelDepartmentActive" runat="server" class="f10b" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" align="left">
                                            &nbsp;
                                            <asp:CheckBox ID="CheckBoxDepartmentActive" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" height="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center" style="padding-left: 35px;">
                                            <asp:Button ID="ButtonSave" runat="server" Text="" CssClass="Login_Button" OnClick="ButtonSave_Click" />
                                            <asp:Button ID="ButtonUpdate" runat="server" Text="" CssClass="Login_Button" OnClick="ButtonUpdate_Click" />
                                            <asp:Button ID="ButtonCancel" runat="server" Text="" CssClass="Cancel_button" OnClick="ButtonCancel_Click"
                                                CausesValidation="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" height="20">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDepartment" Display="None"
                                                runat="server" ControlToValidate="TextBoxDepartmentName" ErrorMessage="sdas"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" Display="None"
                                                runat="server" ControlToValidate="TextBoxDescription" ErrorMessage="dsds" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDepartment"
                                                ID="ValidatorCalloutExtender1" runat="server">
                                            </cc1:ValidatorCalloutExtender>
                                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDescription"
                                                ID="ValidatorCalloutExtender2" runat="server">
                                            </cc1:ValidatorCalloutExtender>
                                            <asp:HiddenField ID="HiddenFieldAddEdit" Value="0" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                <tr class="Grid_tr">
                                    <td>
                                        <asp:Table EnableViewState="false" ID="TableDepartments" CellSpacing="1" CellPadding="3"
                                            Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell HorizontalAlign="Left"  ><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                                <asp:TableHeaderCell Width="30" HorizontalAlign="Left"  ></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellDepartmentName" Text="" HorizontalAlign="Left"  CssClass="H_title"
                                                    Width="20%"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellDescription" Text="" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellisEnabled" Text="" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
