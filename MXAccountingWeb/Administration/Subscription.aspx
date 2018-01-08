<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="Subscription.aspx.cs" Inherits="AccountingPlusWeb.Administration.Subscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
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
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingSubscription" runat="server" Text="Subscription"></asp:Label>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td align="right" style="padding-right: 10px;">
                                        &nbsp;<asp:ImageButton ID="ImageButtonHelp" ToolTip="Help" runat="server" Visible="false" OnClick="ImageButtonHelp_Click"
                                            SkinID="HelpUsers" />
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
                                                    &nbsp;<asp:Label ID="LabelSubscription" runat="server" Text="Subscription" SkinID="Normal_FontLabel_bold"></asp:Label>
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
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelCustomerPassCode" class="f10b" Text="Token" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxCustomerPasscode" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelCustomerAccessid" class="f10b" Text="Access ID" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxCustomerAccessid" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" height="35">
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="170">
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="Save" OnClick="ButtonSave_Click" />
                                                                <asp:Button runat="server" ID="ButtonReset" Text="Cancel" CausesValidation="false"
                                                                    CssClass="Login_Button" OnClick="ButtonReset_Click" />
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <div id="DivNote" runat="server" visible="true" class="Help_DivBg">
                                <%--<div class="Top_menu_bg Help_HeaderDivFont">
                                    <asp:Label ID="LabelNoteHeader" runat="server" Text="Note"></asp:Label>
                                </div>--%>
                                <table id="TableHelpContent" width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td class="Help_OuterPadd">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
                                                style="height: 185px; line-height: 40px;">
                                                <tr class="Top_menu_bg">
                                                    <td width="50%" align="left" valign="middle">
                                                        &nbsp;<asp:Label ID="LabelNote" runat="server" Text="Note"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="f10b" align="left">
                                                        &nbsp;<asp:Label ID="LabelNumber" runat="server" Text="1."></asp:Label>
                                                        &nbsp;<asp:Label ID="LabelContent1" runat="server" Text="The above information is required to publish MFP usage data like"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="f10b" align="left" style="line-height: 25px; padding-left: 21px;">
                                                        &nbsp;<asp:Label ID="LabelContentCount" runat="server" Text="Click Count"></asp:Label>
                                                        <br />
                                                        &nbsp;<asp:Label ID="LabelPaper" runat="server" Text="Paper Status"></asp:Label>
                                                        <br />
                                                        &nbsp;<asp:Label ID="LabelToner" runat="server" Text="Toner Status"></asp:Label>
                                                        <br />
                                                        &nbsp;<asp:Label ID="LabelContent11" runat="server" Text="to centralised web portal for billing and providing alerts."></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="f10b" align="left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="f10b" align="left">
                                                        &nbsp;<asp:Label ID="LabelNumber2" runat="server" Text="2."></asp:Label>
                                                        &nbsp;<asp:Label ID="LabelContent2" runat="server" Text="Please get in touch with the Dealer to get above information."></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" height="40">
                                                        <asp:Button ID="ButtonClose" runat="server" Text="Close" Visible="false" CssClass="Cancel_button"
                                                            OnClick="ButtonClose_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--<ul class="Help_Ul">
                                        <li>The above information is required to publish MFP usage data like
                                            <ul>
                                                <li>Click Count</li>
                                                <li>Paper Status</li>
                                                <li>Toner Status</li>
                                            </ul>
                                            to centralised web portal for billing and providing alerts. </li>
                                        <li>Please get in touch with the Dealer to get above information. </li>
                                    </ul>--%>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
