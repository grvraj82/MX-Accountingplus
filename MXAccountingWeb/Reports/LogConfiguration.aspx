<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="LogConfiguration.aspx.cs" Inherits="AccountingPlusWeb.Reports.LogConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
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
        function taust(asi1, col1) {

            asi1.bgColor = col1;
        }

        function AllowNumeric() {

            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else

                return false;
        }
        function IsUserSelected() {


            var thisForm = document.forms[0];
            var users = thisForm.__SelectedUsers.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__SelectedUsers[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__SelectedUsers.checked) {
                    selectedCount++
                    return true;
                }
            }

        }

        function myKeyPressHandler() {

            if (event.keyCode == 13) {
                document.getElementById('ctl00_PageContent_ButtonUpdate').focus();
            }
        }

        document.onkeypress = myKeyPressHandler;

    </script>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image4" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="left">
                            <table cellpadding="0" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" width="10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingJobConfig" runat="server" Text="Log Configuration"></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image7" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td id="Tablecellback" runat="server" width="8%" visible="false" align="left" valign="middle">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="SettingsImageButtonBack"
                                            CausesValidation="False" ToolTip="" OnClick="ImageButtonBack_Click" />
                                    </td>
                                    <td width="85%" align="left" valign="middle">
                                        &nbsp;
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
                        <td align="center" valign="top">
                            <table width="90%" class="table_border_org" cellpadding="0" cellspacing="0" border="0">
                                <tr class="Top_menu_bg">
                                    <td class="f10b" height="35" colspan="2" align="left">
                                        &nbsp;
                                        <asp:Label ID="LabelGenerateReports" SkinID="LabelLogon" runat="server" Text="Audit Log Configuration"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="5">
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td colspan="2">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor"
                                            align="center">
                                            <tr class="Grid_tr">
                                                <td align="center">
                                                    <asp:Table EnableViewState="false" ID="TableUsers" CellSpacing="1" CellPadding="3"
                                                        Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                            <asp:TableHeaderCell ID="TableHeaderCellSN" Wrap="false" Width="30px" CssClass="Grid_topbg1"> 
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellJobName" Wrap="false" HorizontalAlign="Left"
                                                                CssClass="H_title" Text="Log Type"> 
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellJobNameStatus" Wrap="false" HorizontalAlign="Left"
                                                                CssClass="H_title" Text="Is Enabled?"></asp:TableHeaderCell>
                                                        </asp:TableHeaderRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr height="10px">
                                    <td colspan="2" align="center">
                                        <table width="50%">
                                            <tr>
                                                <td width="10%">
                                                    &nbsp;
                                                </td>
                                                <td align="right" width="50%">
                                                    <asp:Button ID="ButtonUpdate" runat="server" CssClass="Login_Button" OnClick="ButtonUpdate_Click"
                                                        Text="Update" Visible="True" />
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="ButtonCancel" runat="server" CssClass="Cancel_button" Text="Cancel"
                                                        Visible="false" OnClick="ButtonCancel_Click" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
    </table>
</asp:Content>
