<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="FirstTimeUse.aspx.cs" Inherits="AccountingPlusDevice.FirstTimeUse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LogOnControls" runat="server">
    <asp:Table ID="TableFirstTimeUse" runat="server" CellPadding="0" CellSpacing="0"
        Width="100%">
        <asp:TableRow ID="TableFirstTimeUseControls" runat="server" Visible="true">
            <asp:TableCell Width="8%" Height="229">&nbsp;</asp:TableCell>
            <asp:TableCell Width="83%" HorizontalAlign="Left" VerticalAlign="top">
                <asp:Table ID="TableLogOnControls" runat="server" Width="100%" CellPadding="3" CellSpacing="3">
                    <asp:TableRow ID="TableRowMessage" Visible="true">
                        <asp:TableCell Height="45" HorizontalAlign="Center" VerticalAlign="Middle">
                            <table width="80%" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                height="33">
                                <tr>
                                    <td width="6%" align="center" valign="middle">
                                        <asp:Image ID="Info" runat="server" />
                                    </td>
                                    <td width="94%" align="left" valign="middle">
                                        <asp:Label ID="LabelFirstTimeUseMessage" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRowControls">
                        <asp:TableCell Height="130" HorizontalAlign="Left" VerticalAlign="Middle">
                            <asp:Table Width="90%" CellPadding="0" CellSpacing="0" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="30%" HorizontalAlign="Right">
                                        <asp:Image ID="LoginUser" Visible="false" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="70%" HorizontalAlign="Left" VerticalAlign="Top">
                                        <asp:Table ID="Table2" Width="100%" CellPadding="3" CellSpacing="3" runat="server">
                                            <asp:TableRow ID="TableRowFirstTimeUseUserName" CssClass="Normal_FontLabel">
                                                <asp:TableCell Width="40%" HorizontalAlign="right" VerticalAlign="middle">
                                                    <asp:Label ID="LabelUserName" runat="server" Text="" MaxLength="50" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                    <asp:TextBox ID="TextBoxFirstTimeUseUserName" Text="" runat="server" CssClass="UserName_TextBox"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="RowFirstTimeUsePassword" CssClass="Normal_FontLabel">
                                                <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                    <asp:Label ID="LabelPassword" runat="server" Text="" MaxLength="20" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                    <asp:TextBox ID="TextBoxFirstTimeUsePassword" runat="server" TextMode="Password"
                                                        CssClass="UserName_TextBox"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="RowFirstTimeUseDomain" CssClass="Normal_FontLabel">
                                                <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                    <asp:Label ID="LabelDomainName" runat="server" Text="" MaxLength="30" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                    <asp:TextBox ID="TextBoxFirstTimeUseDomain" runat="server" CssClass="UserName_TextBox"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRowButtons">
                        <asp:TableCell Height="20" HorizontalAlign="Left" VerticalAlign="Top">
                            <asp:Table ID="Table1" Width="90%" runat="server" CellPadding="0" CellSpacing="0"
                                border="0">
                                <asp:TableRow>
                                    <asp:TableCell Width="30%" HorizontalAlign="Left" VerticalAlign="Top">&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell Width="25%" HorizontalAlign="Right" VerticalAlign="Middle">
                                        <asp:LinkButton ID="LinkButtonFirstLogOnCancel" OnClick="LinkButtonFirstLogOnCancel_Click"
                                            runat="server">
                                            <asp:Table Width="82%" CellPadding="0" CellSpacing="0" Height="38" runat="server"
                                                border="0">
                                                <asp:TableRow>
                                                    <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="75%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_center">
                                                        <div class="Login_TextFonts">
                                                            <asp:Label ID="LabelCancel" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:LinkButton>
                                    </asp:TableCell>
                                    <asp:TableCell Width="25%" HorizontalAlign="Center" VerticalAlign="middle">
                                        <asp:LinkButton ID="LinkButtonFirstTimeUseOk" OnClick="LinkButtonFirstTimeUseOk_Click"
                                            runat="server">
                                            <asp:Table runat="server" Width="82%" CellPadding="0" CellSpacing="0" Height="38">
                                                <asp:TableRow>
                                                    <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="75%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                        <div class="Login_TextFonts">
                                                            <asp:Label ID="LabelLogOnOK" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:LinkButton>
                                    </asp:TableCell>
                                    <asp:TableCell Width="10%" HorizontalAlign="left" VerticalAlign="top">&nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="TableCommunicator" EnableViewState="false" Visible="false" runat="server"
                    Width="50%" Height="229" HorizontalAlign="Center" CellPadding="3" CellSpacing="3">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="115">
                            <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="115">
                            <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="40%">
                                    <asp:TableRow>
                                        <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                        </asp:TableCell>
                                        <asp:TableCell Width="80%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                            <div class="Login_TextFonts">
                                                <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:TableCell>
                                        <asp:TableCell Width="3%" HorizontalAlign="Right" VerticalAlign="Middle" CssClass="Button_Right">
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
