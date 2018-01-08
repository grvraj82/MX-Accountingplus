<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/LogOn.Master"
    CodeBehind="FirstTimeUse.aspx.cs" Inherits="AccountingPlusEA.PSPModel.FirstTimeUse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogOnControls" runat="server">
    <asp:Table ID="TableFirstTimeUse" runat="server" CellPadding="0" CellSpacing="0"
        Width="90%">
        <asp:TableRow ID="TableFirstTimeUseControls" runat="server" Visible="true" Width="100%">
            <asp:TableCell Width="83%" HorizontalAlign="Center" VerticalAlign="top">
                <asp:Table ID="TableLogOnControls" runat="server" Width="100%" CellPadding="3" CellSpacing="3">
                    <asp:TableRow ID="TableRowMessage" Visible="true">
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                height="30">
                                <tr>
                                    <td width="13%" align="center" valign="middle">
                                        <asp:Image ID="Info" runat="server" />
                                    </td>
                                    <td width="77%" align="left" valign="middle">
                                        <asp:Label ID="LabelFirstTimeUseMessage" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRowControls">
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                            <asp:Table Width="100%" CellPadding="3" CellSpacing="3" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="100%" HorizontalAlign="left" VerticalAlign="Top">
                                        <asp:Table Width="100%" CellPadding="0" CellSpacing="2" runat="server">
                                            <asp:TableRow ID="TableRowFirstTimeUseUserName" CssClass="Normal_FontLabel">
                                                <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                    <asp:Label ID="LabelUserName" runat="server" Text="" MaxLength="50" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell Width="65%" HorizontalAlign="Center" VerticalAlign="middle">
                                                    <asp:TextBox ID="TextBoxFirstTimeUseUserName" Text="" runat="server" Width="225px"
                                                        CssClass="Inputbox"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="RowFirstTimeUsePassword" CssClass="Normal_FontLabel">
                                                <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                    <asp:Label ID="LabelPassword" runat="server" Text="" MaxLength="20" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell Width="65%" HorizontalAlign="Center" VerticalAlign="middle">
                                                    <asp:TextBox ID="TextBoxFirstTimeUsePassword" runat="server" TextMode="Password"
                                                        Width="225px" CssClass="Inputbox"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="RowFirstTimeUseDomain" CssClass="Normal_FontLabel">
                                                <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                    <asp:Label ID="LabelDomainName" runat="server" Text="" MaxLength="30" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell Width="65%" HorizontalAlign="Center" VerticalAlign="middle">
                                                    <asp:TextBox ID="TextBoxFirstTimeUseDomain" Text="" runat="server" Width="225px"
                                                        CssClass="Inputbox"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRowButtons">
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                            <asp:Table Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell Width="49%" HorizontalAlign="Right" VerticalAlign="Middle">
                                        <div style="width: 70%; margin-left: 0px; margin-right: 0px;">
                                            <asp:LinkButton ID="LinkButtonFirstLogOnCancel" OnClick="LinkButtonFirstLogOnCancel_Click"
                                                runat="server">
                                                <asp:Table Width="100%" CellPadding="0" CellSpacing="0" Height="38" runat="server">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="10%" HorizontalAlign="Left" VerticalAlign="Top" CssClass="Button_Left">&nbsp;
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="55%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelCancel" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="35%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:LinkButton>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell Width="2%">
                                    </asp:TableCell>
                                    <asp:TableCell Width="49%" HorizontalAlign="Left" VerticalAlign="middle">
                                        <div style="width: 70%; margin-left: 0px; margin-right: 0px;">
                                            <asp:LinkButton ID="LinkButtonFirstTimeUseOk" OnClick="LinkButtonFirstTimeUseOk_Click"
                                                runat="server">
                                                <asp:Table runat="server" Width="100%" CellPadding="0" Height="38" CellSpacing="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="10%" HorizontalAlign="left" VerticalAlign="top" CssClass="Button_Left">&nbsp;
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="55%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelLogOnOK" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="35%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:LinkButton>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="TableCommunicator" EnableViewState="false" Visible="false" runat="server"
                    Width="70%" HorizontalAlign="Center" CellPadding="3" CellSpacing="3">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="100">
                            <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                            <div style="width: 50%;">
                                <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                    <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell Width="15%" HorizontalaAlign="left" VerticalAlign="top" CssClass="Button_Left">&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell Width="35%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:LinkButton>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
