<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    CodeBehind="ManageGroups.aspx.cs" Inherits="AccountingPlusWeb.Administration.ManageGroups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/SettingsMenu.ascx" TagName="SettingsMenu" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        Meuselected("Groups");
        function DeleteGroups() {
            if (IsGroupSelected()) {
                return confirm(C_GROUP_DELETE_CONFIRMATION);
            }
            else {
                return false;
            }
        }

        function EditGroupDetails() {
            if (IsGroupSelected()) {
                if (GetSeletedCount() > 1) {
                    jNotify(C_SELECT_ONEGROUP)
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function IsGroupSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__GROUPID.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__GROUPID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__GROUPID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                jNotify(C_SELECT_GROUP)
                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__GROUPID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__GROUPID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__GROUPID.checked) {
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
        function Required() {
            if (document.getElementById('ctl00_PageContent_TextBoxGroupName').value == "") {
                jNotify(C_GROUP_NAME_EMPTY)
                return false;

            }
        }
    </script>
    <div id="content" style="display: none;">
    </div>
    <div style="height: 10px;">
        &nbsp;</div>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
        height="550">
        <tr>
            <td width="25%" align="left" valign="top" style="display: none">
                <uc1:SettingsMenu Visible="False" ID="SettingsMenu1" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td valign="top" align="left">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <div id="divEditUsers" runat="server">
                                    <tr>
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
                                                        <asp:Label ID="LabelHeadingGroups" runat="server" Text="Groups"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </div>
                                <div id="divRequired" runat="server" visible="false">
                                    <td height="33" align="left" valign="middle" width="4%" class="Top_menu_bg">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" CausesValidation="False" SkinID="ManageGroupsBackPage" 
                                            Visible="true" OnClick="ImageButtonBack_Click" />
                                    </td>
                                    <td width="1%" class="Top_menu_bg">
                                        <asp:Image ID="Image3" runat="server" SkinID="ManageusersimgSplit" />
                                    </td>
                                    <td align="left" valign="middle" width="4%" class="Top_menu_bg">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" CausesValidation="true" ImageAlign="Middle" SkinID="ManageGroupssave" 
                                            ToolTip="" OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Top_menu_bg">
                                        <asp:Image ID="Image5" runat="server" SkinID="ManageusersimgSplit" />
                                    </td>
                                    <td align="left" valign="middle" width="10%" class="Top_menu_bg">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="ManageGroupsReset"  
                                            ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <td height="33" align="left" valign="middle" class="Top_menu_bg">
                                        <asp:Label ID="LabelAddHeadingGroups" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td height="33" align="right" valign="middle" width="80%" class="Top_menu_bg">
                                        <asp:Image ID="ImageLogOnRequired" runat="server" SkinID="LogonImgRequired" />&nbsp;
                                        <asp:Label ID="LabelRequiredField" SkinID="LabelLogon" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                    </td>
                                </div>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Panel Visible="false" ID="PanelAddGroup" runat="server">
                                <table width="100%" border="0" cellpadding="3" cellspacing="1">
                                    <tr height="2">
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr height="31px">
                                        <td align="right" width="47%">
                                            <asp:Label ID="LabelAddGroup" runat="server" class="f10b" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" align="left">
                                            &nbsp;
                                            <asp:TextBox ID="TextBoxGroupName" runat="server" CssClass="FormTextBox_bg" MaxLength="15"></asp:TextBox>
                                            <asp:Label ID="LabelGroupName" runat="server" CssClass="FormTextBox_bg" Text="" Visible="false"></asp:Label>
                                        </td>
                                        <td valign="middle">
                                            &nbsp;<asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="47%">
                                            <asp:Label ID="LabelGroupActive" runat="server" class="f10b" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" align="left">
                                            &nbsp;
                                            <asp:CheckBox ID="CheckBoxGroupActive" runat="server" />
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
                                            <asp:Button ID="ButtonCancel" runat="server" Text="" CssClass="Cancel_button" CausesValidation="False"
                                                OnClick="ButtonCancel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" height="20">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorGroup" Display="None" runat="server"
                                                ControlToValidate="TextBoxGroupName" ErrorMessage="sdas"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorGroup" ID="ValidatorCalloutExtender1"
                                                runat="server">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                <tr class="Grid_tr">
                                    <td>
                                        <asp:Table EnableViewState="false" ID="TableGroups" CellSpacing="1" CellPadding="3"
                                            Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell CssClass="Grid_topbg1"><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                                <asp:TableHeaderCell Width="30" CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellGroupName" Text="" HorizontalAlign="Left"  CssClass="H_title"
                                                    Width="70%"></asp:TableHeaderCell>
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
