<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="MyPermissionsandLimits.aspx.cs" Inherits="AccountingPlusWeb.Administration.MyPermissionsandLimits" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellUsers();
        //fnPermissionsLimits();
        Meuselected("UserID");        
    </script>
    <table border="0" cellpadding="0" cellspacing="0" align="center" width="100%">
        <tr align="right">
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image3" SkinID="HeadingLeft" runat="server" />
            </td>
            <td height="33" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg">
                        <td class="HeadingMiddleBg" width="8%">
                            <div style="padding: 4px 10px 0px 10px;">
                                <asp:Label ID="LabelHeadingMyPermissionsandLimits" runat="server" Text="My Permissions and Limits"></asp:Label>
                            </div>
                        </td>
                        <td width="1px">
                            <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelLimitsOn" runat="server" Font-Bold="false" Text="Limits On :"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                        <td width="9%" align="left" style="white-space: nowrap">
                            <asp:DropDownList ID="DropDownListLimitsOn" CssClass="FormDropDown_Small" runat="server"
                                AutoPostBack="True" Visible="true" OnSelectedIndexChanged="DropDownListLimitsOn_SelectedIndexChanged">
                                <%--<asp:ListItem Text="" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="" Value="0"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="Menu_split" width="1%" style="display: none">
                        </td>
                        <td width="62%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 2px">
            <td>
            </td>
            <td class="CenterBG" align="center" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top" align="center" class="CenterBG" style="height: 550px">
                <asp:HiddenField ID="HdnJobTypesCount" Value="0" runat="server" />
                <table align="center" cellpadding="0" width="98%" cellspacing="0" border="0">
                    <tr>
                        <td width="60%" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr class="Grid_tr">
                                    <td valign="top" width="95%">
                                        <asp:Table ID="TableLimits" runat="server" CellPadding="3" CellSpacing="0" BorderWidth="0" Width="100%"
                                            CssClass="Table_bg" SkinID="Grid">
                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell ID="HeaderCellSlNo" Wrap="false" CssClass="H_title" Text="S no"
                                                    Width="10"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="HeaderCellJobType" Wrap="false" CssClass="H_title" Text="Job Type"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellPermissions" CssClass="H_title" Text="Permissions"
                                                    Wrap="false"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellLimits" CssClass="H_title" Wrap="false" Text="Limits"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellJobUsed" CssClass="H_title" Wrap="false"
                                                    Text="Job Used"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellAlertLimit" CssClass="H_title" Wrap="false"
                                                    Text="Alert Limit"></asp:TableHeaderCell>                                                
                                                <asp:TableHeaderCell ID="TableHeaderCellOverDraft" Wrap="false" CssClass="H_title" Text="Over Draft"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellTotalLimits" Wrap="false" CssClass="H_title" Text="Total Limits"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellLimitsAvailable" CssClass="H_title" Wrap="false"
                                                    Text="Available Limits"></asp:TableHeaderCell>
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
