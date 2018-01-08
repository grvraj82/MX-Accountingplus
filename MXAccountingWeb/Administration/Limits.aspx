<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="Limits.aspx.cs" Inherits="AccountingPlusWeb.Administration.Limits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellUsers();
        Meuselected("UserID");

        function SetValue(isChecked, controlName) {
            var controlObject = eval("document.forms[0]." + controlName);
            if (isChecked) {
                controlObject.value = 1;
            }
            else {
                controlObject.value = 0;
            }
        }

        function ChangeOverDraft(isChecked) {
            var jobTypesCount = document.getElementsByName('ctl00$PageContent$HdnJobTypesCount');
            var count = 11;
            for (var i = 1; i < 11; i++) {
                var destControlObject = eval("document.forms[0].__ALLOWEDOVERDRAFT_" + i);
                if (isChecked) {
                    destControlObject.disabled = false;
                }
                else {
                    destControlObject.disabled = true;
                }
            }
        }

        function funNumber() {
            if (event.keyCode >= 48 && event.keyCode <= 57)
            { return true; }
            else if (event.keyCode >= 45 && event.keyCode <= 46) {
                event.returnValue = false;
                return false;
            }
            else {
                event.returnValue = false;
                return false;
            }
        }

        function SetUnlimitedValue(isUnlimitedChecked, destControlName, dbLimitControlName) {

            var destControlObject = eval("document.forms[0]." + destControlName);
            var dbLimitControlObject = eval("document.forms[0]." + dbLimitControlName);

            if (isUnlimitedChecked) {
                destControlObject.value = document.forms[0].infinityValue.value;
                destControlObject.disabled = true;
            }
            else {
                if (dbLimitControlObject.value == "2147483647") // int.MaxValue
                {
                    destControlObject.value = document.forms[0].infinityValue.value;
                }
                else {
                    destControlObject.value = dbLimitControlObject.value;
                }
                destControlObject.disabled = false;
            }
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" align="center" width="100%" class="table_border_org"
        height="550">
        <tr class="Top_menu_bg" height="33px" align="center">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td width="8%" class="f12b" align="left">
                            &nbsp;<asp:Label ID="LabelLimitsOn" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                            :
                        </td>
                        <td width="9%" align="left">
                            <asp:DropDownList ID="DropDownListLimitsOn" CssClass="FormDropDown_Small" runat="server"
                                AutoPostBack="True" OnTextChanged="DropDownListLimitsOn_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="Menu_split" width="1%">
                        </td>
                        <td class="f12b" width="9%" align="left" runat="server" id="TableCellLabelGroup">
                            &nbsp;<asp:Label ID="lblGroups" SkinID="TotalResource" runat="server" Text=""></asp:Label>:
                        </td>
                        <td width="10%" align="left" runat="server" id="TableCelldrpGroups">
                            <asp:DropDownList ID="drpGroups" CssClass="FormDropDown_Small" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="drpGroups_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="f12b" width="5%" align="left" runat="server" id="TableCellLabelUser" visible="false">
                            &nbsp;<asp:Label ID="LabelUser" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                            :
                        </td>
                        <td width="10%" align="left" runat="server" id="TableCelldrpUsers" visible="false">
                            <asp:DropDownList ID="DropDownListUsers" CssClass="FormDropDown_Small" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="DropDownListUsers_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="Menu_split" width="1%">
                        </td>
                        <td class="f12b" width="1%" align="left">
                            &nbsp;<asp:CheckBox ID="CheckBoxAllowOverDraft" runat="server" onclick="javascript:ChangeOverDraft(this.checked)" />
                        </td>
                        <td width="12%" align="left">
                            <asp:Label ID="LabelAllowedOD" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="left" width="1%" style="display: none">
                            <asp:ImageButton ID="ImageButtonAutoRefill" ToolTip="" runat="server" SkinID="LimitsSettings"
                                OnClick="ImageButtonAutoRefill_Click" />
                        </td>
                        <td width="33%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="2">
            <td class="CenterBG">
                &nbsp;
            </td>
        </tr>
        <tr class="CenterBG">
            <td valign="top">
                <asp:HiddenField ID="HdnJobTypesCount" Value="0" runat="server" />
                <table align="center" cellpadding="0" width="98%" cellspacing="0" border="0">
                    <tr class="Grid_tr">
                        <td class="TableGridColor">
                            <asp:Table ID="tblLimits" Width="100%" CellSpacing="1" BorderWidth="0" CellPadding="0"
                                runat="server" CssClass="Table_bg" SkinID="Grid">
                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                    <asp:TableHeaderCell CssClass="RowHeader" Width="30"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellJobType" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellJobUsed" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellPageLimit" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellAllowedLimit" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellOverDraft" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnUpdate" runat="server" Text="" CssClass="Login_Button" OnClick="BtnUpdate_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            <asp:Label ID="LabelLimitsSetToAuto" SkinID="TotalResource" runat="server" Style="color: Red"
                                Text="Permissions and Limits are set to Automatic Mode. If you want to set the permissions Manually, please change the setting to Manual Mode in Auto Refill page."
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="2">
            <td class="CenterBG">
                &nbsp;
            </td>
        </tr>
    </table>
    <input type="hidden" value="&infin;" name="infinityValue" />
</asp:Content>
