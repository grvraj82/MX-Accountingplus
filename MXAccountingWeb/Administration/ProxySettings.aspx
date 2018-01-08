<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ProxySettings.aspx.cs" Inherits="AccountingPlusWeb.Administration.ProxySettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image3" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="left">
                            <table cellpadding="0" cellspacing="0" width="60%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingProxySettings" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="5%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonSave" SkinID="SettingsImageButtonSave" runat="server"
                                            CausesValidation="true" ImageAlign="Middle" ToolTip="Click here to save/update"
                                            OnClick="ButtonUpdate_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                    </td>
                                    <td width="7%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" SkinID="SettingsImageButtonReset"
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="Click here to reset" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table align="center" cellpadding="0" cellspacing="0" border="0" class="table_border_org"
                                width="50%">
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                            <tr class="Top_menu_bg">
                                                <td width="50%" align="left" valign="middle">
                                                    &nbsp;<asp:Label ID="LabelProxySettings" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                </td>
                                                <td align="right" width="30%" valign="middle">
                                                </td>
                                                <td align="left" width="20%">
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
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td height="35" align="right" width="40%">
                                                    <asp:Label ID="LabelFromAddress" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="DropDownListProxyEnabled" CssClass="Dropdown_CSS" runat="server"
                                                        CausesValidation="true">
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelServerURL" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxServerUrl" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>
                                            </td>
                                            <td align="left">
                                            <asp:Label ID="LabelServerURLExample" runat="server" Text="Server Url e.g: http://127.0.0.1:80" class="f10b"></asp:Label>
                                            </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelDomain" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxDomain" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelUserID" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxUserId" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelPassword" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxPassword" TextMode="Password" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" height="35">
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="240">
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="ButtonUpdate" CssClass="Login_Button" runat="server" Text="" TabIndex="12"
                                                                    OnClick="ButtonUpdate_Click" />
                                                                <asp:Button runat="server" ID="ButtonReset" Text="" CausesValidation="false" CssClass="Login_Button"
                                                                    OnClientClick="this.form.reset();return false;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>                                       
                                       
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorUrlRequired" ControlToValidate="TextBoxServerUrl"
                                            runat="server" ErrorMessage="Server URL cannot be empty." SetFocusOnError="true"
                                            Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorUrlRequired"
                                            ID="ValidatorCalloutExtender3" runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorUrlRequired" runat="server"
                                            ErrorMessage="" ControlToValidate="TextBoxServerUrl" Display="None" SetFocusOnError="True"
                                            ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorUrlRequired"
                                            ID="ValidatorCalloutExtender1" runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                         
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDomain" ControlToValidate="TextBoxDomain"
                                            runat="server" ErrorMessage="Domain Field cannot be empty." SetFocusOnError="true"
                                            Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDomain" ID="ValidatorCalloutExtender2"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidatorDomain" runat="server" ErrorMessage=""
                                            ControlToValidate="TextBoxDomain" Display="None" SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorDomain" ID="ValidatorCalloutExtender4"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
                                         
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" ControlToValidate="TextBoxUserId"
                                            runat="server" ErrorMessage="User Name cannot be empty." SetFocusOnError="true"
                                            Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorUsername" ID="ValidatorCalloutExtender5"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage=""
                                            ControlToValidate="TextBoxUserId" Display="None" SetFocusOnError="True" ValidationExpression="/((?:https?\:\/\/|www\.)(?:[-a-z0-9]+\.)*[-a-z0-9]+.*)/i"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorUrlRequired" ID="ValidatorCalloutExtender6"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
                                         
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" ControlToValidate="TextBoxPassword"
                                            runat="server" ErrorMessage="Password cannot be empty." SetFocusOnError="true"
                                            Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorPassword" ID="ValidatorCalloutExtender7"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage=""
                                            ControlToValidate="TextBoxPassword" Display="None" SetFocusOnError="True" ValidationExpression="/((?:https?\:\/\/|www\.)(?:[-a-z0-9]+\.)*[-a-z0-9]+.*)/i"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorUrlRequired" ID="ValidatorCalloutExtender8"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
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
