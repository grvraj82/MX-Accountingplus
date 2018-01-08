<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/InnerPage.Master"
    CodeBehind="DiscoverDevices.aspx.cs" Inherits="PrintRoverWeb.Administration.DiscoverDevices" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="PageContent" ID="DiscoverPC" runat="server">
    <script language="javascript" type="text/javascript">

        fnShowCellMFPs();
        Meuselected("Device");

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelDeviceDiscovery" Visible="true" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" border="0" class="table_border_org"
            height="550">
            <tr class="Top_menu_bg">
                <td align="left" valign="middle" style="padding-top: 3px; padding-left: 5px;" height="35">
                    <asp:Label ID="LabelDeviceDiscovery" runat="server" Text=""></asp:Label>
                    <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="DiscoverDevicesBackPage"
                        CausesValidation="False" ImageAlign="Middle" OnClick="ImageButtonBack_Click" />
                </td>
            </tr>
            <tr height="2px">
                <td colspan="2" class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="CenterBG" valign="top">
                    <table cellpadding="0" cellspacing="0" border="0" align="center" class="table_border_org"
                        width="60%">
                        <tr>
                            <td colspan="2" align="left" height="35" valign="middle">
                                <asp:RadioButton SkinID="RadioFontLabel" ID="RadioButtonDevicesInSubnet" Checked="true"
                                    GroupName="DiscoverDevices" Text="" runat="server" AutoPostBack="True" OnCheckedChanged="RadioButtonDevicesInSubnet_CheckedChanged" />
                            </td>
                        </tr>
                     
                        <tr>
                            <td align="left" width="33%" height="35" valign="middle">
                                <asp:RadioButton SkinID="RadioFontLabel" ID="RadioButtonDeviceByIP" GroupName="DiscoverDevices"
                                    Text="" runat="server" OnCheckedChanged="RadioButtonDeviceByIP_CheckedChanged"
                                    AutoPostBack="True" />
                            </td>
                            <td align="left" width="67%">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <span style="padding-left: 41px;"></span>
                                            <asp:TextBox ID="TextBoxIPAddress" runat="server" MaxLength="100"></asp:TextBox>
                                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage=""
                                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                                ControlToValidate="TextBoxIPAddress" Display="None"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RegularExpressionValidator1"
                                                runat="server">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                     
                        <tr>
                            <td align="left">
                                <asp:RadioButton SkinID="RadioFontLabel" ID="RadioButtonByIPRange" GroupName="DiscoverDevices"
                                    Text="" runat="server" OnCheckedChanged="RadioButtonByIPRange_CheckedChanged"
                                    AutoPostBack="True" />
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="LabelStartIP" SkinID="NormalFontLabel_bold" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBoxStartIP" runat="server" MaxLength="15"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage=""
                                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                                ControlToValidate="TextBoxStartIP" Display="None"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RegularExpressionValidator2"
                                                runat="server">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="LabelEndIP" SkinID="NormalFontLabel_bold" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBoxEndIP" runat="server" MaxLength="15"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage=""
                                                ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                                ControlToValidate="TextBoxEndIP" Display="None"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RegularExpressionValidator3"
                                                runat="server">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                           <tr>
                            <td colspan="3" height="20">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" height="50" valign="middle">
                                <asp:Button ID="ButtonDiscoverDevices" runat="server" CssClass="Login_Button" OnClick="ButtonDiscoverDevices_Click"
                                    Text="" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="ButtonCancelDiscovery" runat="server" CausesValidation="false" CssClass="Cancel_button"
                                    OnClick="ButtonCancelDiscovery_Click" Text="" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
