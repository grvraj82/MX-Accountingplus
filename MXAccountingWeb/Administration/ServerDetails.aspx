<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ServerDetails.aspx.cs" Inherits="AccountingPlusWeb.Administration.ServerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
        function AllowNumeric() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else
                return false;
        }


        function HidenSetting() {
            document.getElementById("ShowConfig").style.display = "none";
            document.getElementById("HidenSettingConfig").innerHTML = "<a href=\"#\" onclick=\"return ShowSetting();\">Show Setting</a>";
        }
        function ShowSetting() {
            document.getElementById("ShowConfig").style.display = "inline";
            document.getElementById("HidenSettingConfig").innerHTML = "<a href=\"#\" onclick=\"return HidenSetting();\">Hide Setting </a>";
        }
    </script>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg" align="left">
                        <td height="35" align="left">
                            <table cellpadding="0" cellspacing="0" border="0" align="left" width="100%">
                                <tr>
                                    <td class="HeadingMiddleBg" width="10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingServerDetails" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image7" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="3%" style="display: none">
                                    </td>
                                    <td width="15%">
                                    </td>
                                    <td width="85%" align="left" valign="middle">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table width="80%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                <tr class="Grid_tr">
                                    <td>
                                        <asp:Table ID="TableServerDetails" CellSpacing="1" CellPadding="1" Width="100%" BorderWidth="0"
                                            runat="server" CssClass="Table_bg" SkinID="Grid">
                                            <asp:TableHeaderRow Width="100%" Height="30" CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell Width="3%" HorizontalAlign="Left"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell Width="37%" ID="TableHeaderCellEnglish" Text="" HorizontalAlign="Left"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell Width="60%" ID="TableHeaderCellSelctedMessage" Text="Details"
                                                    HorizontalAlign="Left"></asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
