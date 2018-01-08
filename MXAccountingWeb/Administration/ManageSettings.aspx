<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    Inherits="AdministrationManageSettings" CodeBehind="ManageSettings.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <link rel="Stylesheet" href="../AppStyle/ApplicationStyle.css" />
    <script type="text/javascript" language="javascript">

        fnShowCellSettings();
        Meuselected("Settings");

        function HidenSetting() {
            document.getElementById("ShowConfig").style.display = "none";
            document.getElementById("HidenSettingConfig").innerHTML = "<a href=\"#\" onclick=\"return ShowSetting();\">Show Setting</a>";


        }
        function ShowSetting() {
            document.getElementById("ShowConfig").style.display = "inline";
            document.getElementById("HidenSettingConfig").innerHTML = "<a href=\"#\" onclick=\"return HidenSetting();\">Hide Setting </a>";
        }


        function taust(asi1, col1)
        { asi1.bgColor = col1; }

        function myKeyPressHandler() {
            if (event.keyCode == 13) {
                document.getElementById('ctl00_PageContent_ButtonUpdate').focus();
            }
        }

        document.onkeypress = myKeyPressHandler;
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image3" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="left">
                            <table cellpadding="0" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" style="width: 10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingGeneralPage" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td style="width: 2%">
                                    </td>
                                    <td width="7%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" SkinID="SettingsImageButtonSave"
                                            CausesValidation="true" ImageAlign="Middle" ToolTip="" OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                    </td>
                                    <td width="7%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" SkinID="SettingsImageButtonReset"
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <%--<td width="1%" class="Menu_split"></td>--%>
                                    <td width="85%" align="left" valign="middle">
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
                    <tr>
                        <td valign="top" align="center">
                            <table width="70%" class="table_border_org" cellpadding="0" cellspacing="0" border="0">
                                <tr class="Top_menu_bg">
                                    <td class="f10b" height="35" colspan="2" align="left">
                                        &nbsp;
                                        <asp:Label ID="LabelJobRetention" runat="server" SkinID="LabelLogon" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Table EnableViewState="false" ID="Table_GeneralSettings" CellSpacing="3" CellPadding="3"
                                            Width="82%" BorderWidth="0" runat="server">
                                        </asp:Table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" height="50">
                                        <asp:Button ID="ButtonUpdate" Visible="True" runat="server" CssClass="Login_Button"
                                            Text="" OnClick="ButtonUpdate_Click" />
                                        <asp:Button runat="server" ID="ButtonReset" Text="" CssClass="Login_Button"
                                            OnClientClick="this.form.reset();return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <span style="padding-left: 125px;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="HiddenField_SelectedValues" runat="server" />
                            <asp:HiddenField ID="HiddenFieldCategoryType" runat="server" />
                            <asp:HiddenField ID="HiddenFieldSettingType" Value="0" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
