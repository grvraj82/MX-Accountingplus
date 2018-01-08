<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ActiveDirectorySyncSettings.aspx.cs" Inherits="AccountingPlusWeb.Administration.ActiveDirectorySyncSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td height="25" align="left" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadADSync" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="20px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td height="33" align="left" valign="middle" width="25%" class="HeaderPadding" style="display: none">
                            <table cellpadding="1" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td width="0%" align="center" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonBack" SkinID="SettingsImageButtonBack" Visible="false"
                                            runat="server" ImageAlign="Middle" ToolTip="" OnClick="ImageButtonBack_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                    </td>
                                    <td width="4%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" SkinID="SettingsImageButtonSave"
                                            CausesValidation="true" ImageAlign="Middle" ToolTip="Click here to save/update"
                                            OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                    </td>
                                    <td width="4%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" SkinID="SettingsImageButtonReset"
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="Click here to reset" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <td width="100%" align="left" valign="middle">
                                        <asp:Label ID="LabelADSyncTitle" runat="server" SkinID="TotalResource" Text="" CssClass="f10b"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="2">
                &nbsp;
            </td>
            <td height="2" class="CenterBG">
                &nbsp;
            </td>
        </tr>
        <tr class="Grid_tr">
            <td>
            </td>
            <td class="CenterBG" valign="top" height="500" align="center">
                <table cellpadding="0" cellspacing="0" width="80%" border="0" class="table_border_org">
                    <tr>
                        <td align="center" valign="top">
                            <asp:Table ID="TableSyncOptions" runat="server" Width="100%" CellPadding="0" CellSpacing="0"
                                border="0">
                                <asp:TableRow CssClass="Top_menu_bg">
                                    <asp:TableCell CssClass="f10b" Height="35" ColumnSpan="2" HorizontalAlign="Left">
                                        &nbsp;
                                        <asp:Label ID="LabelADSyncSettings" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="2">
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>
                                  <asp:TableRow>
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="LabelADSyncEnable" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:CheckBox ID="CheckBoxEnableADSync" AutoPostBack="true" runat="server" OnCheckedChanged="CheckBoxEnableADSync_OnCheckedChanged" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                  <asp:TableRow>
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="Label1" runat="server" Text="Sync Users" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:CheckBox ID="CheckBoxSyncUsers" AutoPostBack="true" runat="server"  />
                                    </asp:TableCell>
                                </asp:TableRow>
                                  <asp:TableRow>
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="Label2" runat="server" Text="Sync CostCenter" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:CheckBox ID="CheckBoxSyncCostCenter" AutoPostBack="true" runat="server"  />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowSyncOn">
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="LabelSyncOn" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListSyncOn" runat="server" AutoPostBack="true" CssClass="Dropdown_CSS"
                                            OnSelectedIndexChanged="DropDownListSyncOn_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowSyncTime" Visible="false">
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="LabelReSyncTime" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Wrap="false">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td width="12%">
                                                    <asp:DropDownList ID="DropDownListHour" runat="server">
                                                    </asp:DropDownList>
                                                    :
                                                </td>
                                                <td width="12%">
                                                    <asp:DropDownList ID="DropDownListMinute" runat="server">
                                                    </asp:DropDownList>
                                                    :
                                                </td>
                                                <td width="12%">
                                                    <asp:DropDownList ID="DropDownListMeridian" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="64%">
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowSyncWeek" Visible="false">
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35">
                                        <asp:Label ID="LabelSyncWeek" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListSyncWeek" runat="server" CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowReSyncDate">
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35">
                                        <asp:Label ID="LabelReSyncDate" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListSyncDate" runat="server">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                               
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td width="50%" align="right">
                                        <asp:Button ID="ButtonUpdate" runat="server" CssClass="Login_Button" OnClick="ButtonUpdate_Click"
                                            Text="" Visible="True" />
                                    </td>
                                    <td align="right" width="11%">
                                        <asp:Button runat="server" ID="ButtonReset" Text="" CssClass="Login_Button" OnClientClick="this.form.reset();return false;" />
                                    </td>
                                    <td align="left" width="5%">
                                        <asp:Button ID="ButtonCancel" runat="server" CssClass="Cancel_button" Text="" Visible="false"
                                            OnClick="ButtonCancel_Click" CausesValidation="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonSyncNow" runat="server" CssClass="Cancel_button" Text="Sync Now"
                                            Visible="false" OnClick="ButtonSyncNow_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
