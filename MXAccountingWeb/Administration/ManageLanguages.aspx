<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ManageLanguages.aspx.cs" Inherits="PrintRoverWeb.Administration.ManageLanguage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellSettings(), Meuselected("Settings");
        function DeleteDepartments() {
            if (IsDepSelected()) {
                return confirm('All the Details regarding selected Language will be deleted. \n\nDo you want to Continue?');
            }
            else {
                return false;
            }
        }

        function EditDepDetails() {
            if (IsDepSelected()) {
                if (GetSeletedCount() > 1) {
                    jNotify(C_LANUAGE_SELECT_ONLY_ONE)

                    return false;
                }
            }
            else {
                return false;
            }
        }

        function IsDepSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__LANGID.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__LANGID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__LANGID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                jNotify(C_LANGUAGE_SELECT)
                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__LANGID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__LANGID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__LANGID.checked) {
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
            if (document.getElementById("ctl00_PageContent_HiddenFieldisSortingEnable").value == 0) {

                document.getElementById("chkALL").checked = false;

            }
        }
        function togall(refcheckbox) {

            var thisForm = document.forms[0];
            var users = thisForm.__LANGID.length;
            var selectedCount = 0;
            if (thisForm.__LANGID[refcheckbox - 1].checked) {

                thisForm.__LANGID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__LANGID[refcheckbox - 1].checked = true; ;
            }

        }
        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldisSortingEnable").value);

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
                else if (hiddenvalue == "2") {
                    document.getElementById('ctl00_PageContent_ButtonUpdate').focus();
                }
            }
        }

        document.onkeypress = myKeyPressHandler;
        
    </script>
    <div id="content" style="display: none;">
    </div>
    <%--<div style="height: 10px;"> &nbsp;</div>--%>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr height="35">
            <td align="right" valign="top" style="width:1px">
                <asp:Image ID="Image3" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td valign="top" align="left">
                            <div id="divEditUsers" runat="server" style="width: 100%">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr class="Top_menu_bg">
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                                                <tr>
                                                    <td class="HeadingMiddleBg" style="width: 10%">
                                                        <div style="padding: 4px 10px 0px 10px;">
                                                            <asp:Label ID="LabelHeadManageLanguage" runat="server" Text=""></asp:Label></div>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                                    </td>
                                                    <td style="width: 2%">
                                                    </td>
                                                    <td align="left" width="15%" valign="middle" class="HeaderPadding">
                                                        <table cellpadding="1" cellspacing="0" border="0" height="35" width="100%">
                                                            <tr>
                                                                <td align="right" valign="middle" width="15%">
                                                                    <asp:ImageButton ID="IBAdd" ToolTip="" SkinID="PriceMangerAdd" runat="server" OnClick="IBAdd_Click" />
                                                                    <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="ManageLanguagesBackPage"
                                                                        CausesValidation="False" OnClick="ImageButtonBack_Click" Visible="false" />
                                                                </td>
                                                                <td align="center" valign="middle" width="5%">
                                                                    <asp:Image ID="ImageMenuSplitBar" runat="server" SkinID="ManageusersimgSplit" />
                                                                </td>
                                                                <td align="left" valign="middle" width="15%">
                                                                    <asp:ImageButton SkinID="IBEdit" ToolTip="" runat="server" ID="IBEdit" OnClick="IBEdit_Click" />
                                                                    <asp:ImageButton ID="ImageButtonSave" runat="server" Visible="false" CausesValidation="true"
                                                                        ImageAlign="Middle" SkinID="SettingsImageButtonSave" ToolTip="" OnClick="ImageButtonSave_Click" />
                                                                </td>
                                                                <td width="5%" id="tdImageLock" runat="server">
                                                                    <asp:Image ID="Image1" runat="server" SkinID="ManageusersimgSplit" />
                                                                </td>
                                                                <td width="15%" align="left" id="tdImageLockButton" runat="server" valign="middle">
                                                                    <asp:ImageButton runat="server" ID="ImageButtonLock" ToolTip="" SkinID="SettingsImageButtonLock"
                                                                        OnClick="ImageButtonLock_Click" />
                                                                </td>
                                                                <td width="5%" id="tdImageUnLock" runat="server">
                                                                    <asp:Image ID="Image2" runat="server" SkinID="ManageusersimgSplit" />
                                                                </td>
                                                                <td width="15%" align="left" id="tdImageUnLockButton" runat="server" valign="middle">
                                                                    <asp:ImageButton runat="server" ID="ImageButtonUnLock" ToolTip="" SkinID="SettingsImageButtonUnLock"
                                                                        OnClick="ImageButtonUnLock_Click" />
                                                                </td>
                                                                <td width="10%" id="TableCellresetSpilt" runat="server" visible="false">
                                                                    <asp:Image ID="Image5" runat="server" SkinID="ManageusersimgSplit" />
                                                                </td>
                                                                <td width="15%" align="left" id="TableCellReset" runat="server" valign="middle" visible="false">
                                                                    <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="SettingsImageButtonReset"
                                                                        ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="85%" align="left" valign="middle">
                                                        &nbsp;
                                                        <asp:Label ID="LabelHeadingLanguage" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top" class="CenterBG">
                <asp:Panel Visible="false" ID="PanelAddDepartment" runat="server">
                    <table width="98%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2" height="5">
                            </td>
                        </tr>
                        <tr height="31px">
                            <td align="right" width="47%">
                                &nbsp;&nbsp;
                                <asp:Label ID="Labellanguage" runat="server" class="f10b" Text=""></asp:Label>
                            </td>
                            <td width="15%" align="left">
                                &nbsp;
                                <asp:DropDownList ID="DropDownListLanguage" CssClass="Dropdown_CSS" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                        <tr height="31px">
                            <td align="right" width="47%">
                                &nbsp;&nbsp;
                                <asp:Label ID="LabelUIDirection" runat="server" class="f10b" Text=""></asp:Label>
                            </td>
                            <td width="15%" align="left">
                                &nbsp;
                                <asp:DropDownList ID="DropDownListUIDirection" CssClass="Dropdown_CSS" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                        <tr height="31px">
                            <td align="right" width="47%">
                                &nbsp;&nbsp;
                                <asp:Label ID="LabelLanguageActive" runat="server" class="f10b" Text=""></asp:Label>
                            </td>
                            <td width="15%" align="left">
                                &nbsp;
                                <asp:CheckBox ID="CheckBoxLanguageActive" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="5">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr align="center">
                                        <td align="right" width="55%">
                                            <asp:Button ID="ButtonSave" runat="server" Text="" CssClass="Login_Button" OnClick="ButtonSave_Click" />
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="ButtonUpdate" runat="server" Text="" CssClass="Login_Button" OnClick="ButtonUpdate_Click" />
                                        </td>
                                        <td align="left" width="45%">
                                            &nbsp;
                                            <asp:Button ID="ButtonCancel" runat="server" Text="" CssClass="Cancel_button" CausesValidation="False"
                                                OnClick="ButtonCancel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:HiddenField ID="HiddenFieldAddEdit" Value="0" runat="server" />
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
            <td valign="top" align="center" class="CenterBG">
                <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                    <tr class="Grid_tr">
                        <td>
                            <asp:Table EnableViewState="false" ID="TableLanguage" CellSpacing="1" CellPadding="3"
                                Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                    <asp:TableHeaderCell Width="30" HorizontalAlign="Left" CssClass="Grid_topbg1"><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Width="30" CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellLanguageID" Text="" HorizontalAlign="Left"
                                        CssClass="H_title" Width="20%"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellLanguageName" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellEnabled" Text="" HorizontalAlign="Left" CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellLanguageDirection" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenFieldisSortingEnable" Value="0" runat="server" />
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ClientMessages">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
