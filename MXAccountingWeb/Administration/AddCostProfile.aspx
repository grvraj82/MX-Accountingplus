<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="AddCostProfile.aspx.cs" Inherits="AccountingPlusWeb.Administration.AddCostProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowPrice();
        Meuselected("Pricing");
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

        function PageLoaded() {
            document.all.PageLoad.style.visibility = "hidden";
        }

        function DeleteDevice() {
            if (IsDeviceSelected()) {
                return confirm('The selected Profile will be deleted ');
            }
            else {
                return false;
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
            var users = thisForm.__DEVICE_ID.length;
            var selectedCount = 0;
            if (thisForm.__DEVICE_ID[refcheckbox - 1].checked) {

                thisForm.__DEVICE_ID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__DEVICE_ID[refcheckbox - 1].checked = true;
            }
            ValidateSelectedCount();
        }
        function IsDeviceSelected() {
            var thisForm = document.forms[0];
            var Device = thisForm.__DEVICE_ID.length;
            var selectedCount = 0;

            if (Device > 0) {
                for (var item = 0; item < Device; item++) {
                    if (thisForm.__DEVICE_ID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__DEVICE_ID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                alert("Please select the profile ");
                return false;
            }
        }

        function UpdateDeviceDetails() {
            if (IsDeviceSelected()) {
                if (GetSeletedCount() > 1) {
                    alert("Please select only one profile to update the details");
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var device = thisForm.__DEVICE_ID.length;
            var selectedCount = 0;
            if (device > 0) {
                for (var item = 0; item < device; item++) {
                    if (thisForm.__DEVICE_ID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__DEVICE_ID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }
    </script>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" style="height: 550px">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg" style="height: 30px">
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelheadCostProfile" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="20px">
                                    </td>
                                    <td>
                                        <asp:Table ID="Table1" runat="server" CellPadding="3" CellSpacing="3">
                                            <asp:TableRow ID="tablerowMain">
                                                <asp:TableCell CssClass="">
                                                    <asp:ImageButton ID="ImageButtonAdd" runat="server" SkinID="AssignMFPsToGroupadd"
                                                        OnClick="ImageButtonAdd_Click"  CausesValidation="False" />
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="MenuSpliter" ID="TableCellEdit">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" SkinID="AssignMFPsToGroupedit"
                                                        OnClick="IbGetUpdateDetails_Click"  CausesValidation="False" />
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="MenuSpliter" ID="TableCellDelete">
                                                    <asp:ImageButton ID="IbDeleteGroup" SkinID="AssignMFPsToGroupdelete" runat="server"
                                                        OnClick="IbDeleteGroup_Click"  CausesValidation="False" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr class="Grid_tr" id="tablerowMainTable" runat="server">
                        <td valign="top" align="center">
                            <asp:Panel ID="PanelMainCostProfile" runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" width="98%">
                                    <tr>
                                        <td height="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Table ID="tblDevice" CellSpacing="1" CellPadding="3" Width="100%" BorderWidth="0"
                                                runat="server" CssClass="Table_bg" SkinID="Grid">
                                                <asp:TableHeaderRow ID="TableHeaderRow1" CssClass="Table_HeaderBG" runat="server">
                                                    <asp:TableHeaderCell ID="TableHeaderCell1" Width="30px" runat="server" HorizontalAlign="Left">
                                                 <input id="chkALL" onclick="ChkandUnchk()" type="checkbox"  />
                                                    </asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCell4" Width="30px" runat="server" HorizontalAlign="Left"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCell2" runat="server" HorizontalAlign="Left"
                                                        CssClass="H_title"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCell3" runat="server" HorizontalAlign="Left"
                                                        CssClass="H_title"></asp:TableHeaderCell>
                                                </asp:TableHeaderRow>
                                            </asp:Table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="Grid_tr">
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
                                           <p class="LabelWarningFont">There are no Cost Profile(s) created.</p>
                                           <p class="LabelLoginFont"></p>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table cellpadding="3" cellspacing="1" id="TableAddCostProfile" runat="server" visible="false">
                                <tr class="Grid_tr" id="TableRowSQLCreditinals" runat="server" visible="true">
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <table align="center" cellpadding="0" cellspacing="0" border="0" class="table_border_org">
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
                                                            <td height="3">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="30">
                                                                &nbsp;&nbsp;<asp:Label ID="LabelMFPGroup" runat="server" Text=""></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td valign="middle" align="left" colspan="2">
                                                                <asp:TextBox ID="TextBoxMFPGroup" runat="server" MaxLength="30" Width="150px" TabIndex="1"></asp:TextBox>
                                                                <asp:Image ID="Imageuser" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                                    padding-left: 5px;" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxMFPGroup"
                                                                    runat="server" ErrorMessage="Required field"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" height="30">
                                                                &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td valign="middle" align="left" colspan="2">
                                                                <asp:CheckBox ID="CheckBoxGroup" runat="server" Checked="true" TabIndex="2" />
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
                                                    <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="" TabIndex="3"
                                                        OnClick="ButtonSave_Click" />
                                                    <asp:Button ID="ButtonUpdate" CssClass="Login_Button" CausesValidation="false" runat="server"
                                                        Text="" TabIndex="4" OnClick="ButtonUpdate_Click" Visible="false" />
                                                    <asp:Button ID="ButtonCancel" CssClass="Cancel_button" CausesValidation="false" runat="server"
                                                        Text="" TabIndex="5" OnClick="ButtonCancel_Click" />
                                                    <asp:Button runat="server" ID="ButtonReset" Text="" TabIndex="6" CssClass="Login_Button"
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
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="white-space: nowrap; height: 293px;" align="right">
                            <br />
                            <asp:Label ID="LblActionMessage" runat="server" Text=""></asp:Label>&nbsp;
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="24px" Style="z-index: 100;
                                left: 444px; position: absolute; top: 294px" Width="175px" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenJobsCount" Value="0" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
