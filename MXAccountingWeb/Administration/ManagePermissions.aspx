
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ManagePermissions.aspx.cs" Inherits="AccountingPlusWeb.Administration.ManagePermissions" %>

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
    
    </script>
    <table border="0" cellpadding="0" width="100%" cellspacing="0" align="center" class="table_border_org"
        height="550">
        <tr class="Top_menu_bg" height="33px" align="center">
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                   <tr>
                        <td width="11%" class="f12b" align="left">
                            <asp:Label ID="LabelPermissionsOn" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                            :
                        </td>
                        <td width="8%" align="left">
                            <asp:DropDownList ID="DropDownListPermissionsOn" CssClass="FormDropDown_Small" runat="server" AutoPostBack="True"
                                OnTextChanged="DropDownListPermissionsOn_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="Menu_split" width="1%">
                        </td>
                        <td class="f12b" width="9%" align="left" runat="server" id="TableCellLabelGroup">
                            <asp:Label ID="lblGroups" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                            :
                        </td>
                        <td width="10%" align="left" runat="server" id="TableCelldrpGroups">
                            <asp:DropDownList ID="drpGroups" CssClass="FormDropDown_Small" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="drpGroups_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="f12b" width="5%" align="left" runat="server" id="TableCellLabelUser"
                            visible="false">
                            <asp:Label ID="LabelUser" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                            :
                        </td>
                        <td width="10%" align="left" runat="server" id="TableCelldrpUsers" visible="false">
                            <asp:DropDownList ID="DropDownListUsers" CssClass="FormDropDown_Small" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownListUsers_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="1%">
                            <asp:ImageButton ID="ImageButtonAutoRefill" ToolTip="" runat="server" SkinID="ManagePermissionsSettings"
                                Visible="false"  OnClick="ImageButtonAutoRefill_Click" />
                        </td>
                        <td width="45%">
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
        <tr>
            <td valign="top" class="CenterBG">
                <asp:HiddenField ID="HdnJobTypesCount" Value="0" runat="server" />
                <table align="center" cellpadding="0" width="98%" cellspacing="0" border="0" class="TableGridColor">
                    <tr class="Grid_tr">
                        <td>
                            <asp:Table ID="tblPermissions" Width="100%" CellSpacing="1" BorderWidth="0" CellPadding="3"
                                runat="server" CssClass="Table_bg" SkinID="Grid">
                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                    <asp:TableHeaderCell CssClass="RowHeader" Width="30px"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell  CssClass="H_title" ID="TableHeaderCellJobType" HorizontalAlign="Left"
                                        Wrap="false"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell  CssClass="H_title" ID="TableHeaderCellJobPermission" HorizontalAlign="Left"
                                        Wrap="false"></asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnUpdate" runat="server" Text="" CssClass="Login_Button" OnClick="BtnUpdate_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center" valign="middle">
                          <asp:Label ID="LabelLimitsSetToAuto" SkinID="TotalResource" runat="server" style="color:Red"  Text="Permissions and Limits are set to Automatic Mode. If you want to set the permissions Manually, please change the setting to Manual Mode in Auto Refill page." Visible="false"></asp:Label>  
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
</asp:Content>
