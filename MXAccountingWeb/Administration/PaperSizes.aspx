<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="PaperSizes.aspx.cs" Inherits="AccountingPlusWeb.Administration.PaperSizes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="SC" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowPrice();
        Meuselected("Pricing");
        function DeletePaperSizes() {
            if (IsDepSelected()) {
                return confirm(C_PAPAERSIZE_DELETE_CONFIRMATION);
            }
            else {
                return false;
            }
        }

        function EditDepDetails() {
            if (IsDepSelected()) {
                if (GetSeletedCount() > 1) {
                    jNotify(C_SELECT_ONEPAPERSIZE)
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
                jNotify(C_SELECT_PAPERSIZE)
                return false;
            }
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

            if (document.getElementById("ctl00_PageContent_HiddenJobsCount").value == 0) {

                document.getElementById("chkALL").checked = false;

            }
        }
        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenJobsCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;

            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
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

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenJobsCount").value);
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
            var users = thisForm.__DEPID.length;
            var selectedCount = 0;
            if (thisForm.__DEPID[refcheckbox - 1].checked) {

                thisForm.__DEPID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__DEPID[refcheckbox - 1].checked = true; ;
            }
            ValidateSelectedCount();
        }
        function Required() {
            if (document.getElementById('ctl00_PageContent_TextBoxPaperSizeName').value == "") {
                jNotify(C_PAPAERSIZE_NAME_EMPTY)
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
                    var description = document.getElementById('ctl00_PageContent_TextBoxPaperSizeName').value;
                    if (description == "" || description == null) {
                        document.getElementById('ctl00_PageContent_TextBoxPaperSizeName').focus();
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
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image1" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg">
                        <td valign="top" align="left">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" width="10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingPaperSizes" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image2" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="1%">
                                    </td>
                                    <td valign="top" width="88%">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" class="HeaderPadding">
                                            <tr>
                                                <td align="left" width="30%" valign="middle">
                                                    <asp:Table ID="Table1" runat="server" CellPadding="3" CellSpacing="3">
                                                        <asp:TableRow>
                                                            <asp:TableCell>
                                                                <asp:ImageButton SkinID="IBAdd" CausesValidation="False" ID="IBAdd" ToolTip="" runat="server"
                                                                    OnClick="IBAdd_Click" />
                                                            </asp:TableCell>
                                                            <asp:TableCell CssClass="MenuSpliter" ID="TableCellEdit">
                                                                <asp:ImageButton SkinID="IBEdit" ToolTip="" runat="server" ID="IBEdit" OnClick="IBEdit_Click"
                                                                    CausesValidation="false" />
                                                            </asp:TableCell>
                                                            <asp:TableCell CssClass="MenuSpliter" ID="TableCellDelete">
                                                                <asp:ImageButton SkinID="IBDelete" ToolTip="" ID="IBDelete" runat="server" OnClick="IBDelete_Click"
                                                                    CausesValidation="false" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                                <td width="70%" align="left" valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table cellpadding="3" cellspacing="1" border="0" id="TablePapersize" runat="server"
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
                                                                <asp:Label ID="LabelPaperSizeCategory" runat="server" class="f10b" Text=""></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td valign="middle" align="left" colspan="2">
                                                                <asp:TextBox ID="TextBoxPaperSizeCategory" runat="server" CssClass="FormTextBox_bg"
                                                                    MaxLength="15" TabIndex="1"></asp:TextBox>
                                                                <asp:Label ID="Label2" runat="server" CssClass="FormTextBox_bg" Text="" Visible="false"></asp:Label>
                                                                <asp:Image ID="Imageuser" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                                    padding-left: 5px;" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCostCenter" ControlToValidate="TextBoxPaperSizeCategory"
                                                                    runat="server" ErrorMessage="Required field"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="30">
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="LabelAddPaperSizes" runat="server" class="f10b" Text=""></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td valign="middle" align="left" colspan="2">
                                                                <asp:TextBox ID="TextBoxPaperSizeName" runat="server" CssClass="FormTextBox_bg" MaxLength="15" TabIndex="2"></asp:TextBox>
                                                                <asp:Label ID="LabelPaperSizeName" runat="server" CssClass="FormTextBox_bg" Text=""
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="30">
                                                                &nbsp;&nbsp;<asp:Label ID="LabelPaperSizeActive" runat="server" class="f10b" Text=""></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td valign="middle" align="left" colspan="2">
                                                                <asp:CheckBox ID="CheckBoxPaperSizeActive" runat="server" TabIndex="3" />
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
                                                    <asp:Button runat="server" ID="ButtonReset" Text="" TabIndex="7" CssClass="Login_Button"
                                                        OnClientClick="this.form.reset();return false;" />
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
                            <asp:HiddenField ID="HiddenJobsCount" Value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <asp:Panel ID="PanelMainPaperSizes" runat="server">
                                <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor"
                                    runat="server" id="TablePaperSizesComp">
                                    <tr class="Grid_tr">
                                        <td>
                                            <asp:Table EnableViewState="false" ID="TablePapersizes" CellSpacing="1" CellPadding="3"
                                                Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                    <asp:TableHeaderCell HorizontalAlign="Left" CssClass="Grid_topbg1"><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell Width="30" CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCellPaperSizeName" Text="" HorizontalAlign="Left"
                                                        CssClass="H_title" Width="20%" Wrap="false"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCellPaperCategory" Text="" HorizontalAlign="Left"
                                                        CssClass="H_title" Width="20%" Wrap="false"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCellisEnabled" Text="" HorizontalAlign="Left"
                                                        CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                                </asp:TableHeaderRow>
                                            </asp:Table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
                                           <p class="LabelWarningFont">There are no Paper Sizes created.</p>
                                           <p class="LabelLoginFont"></p>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxPaperSizeCategory"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator2" ID="ValidatorCalloutExtender1"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextBoxPaperSizeName"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender Enabled="true" TargetControlID="RequiredFieldValidator3"
                                ID="ValidatorCalloutExtender2" runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenFieldAddEdit" Value="0" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
