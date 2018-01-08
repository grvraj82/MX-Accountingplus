<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="ManualLogOn.aspx.cs" Inherits="AccountingPlusDevice.Mfp.ManualLogOn"
    Title="Manual LogOn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LogOnControls" runat="server">
<script language="javascript" type="text/javascript">
    function SendData() {
        var uid = document.getElementById("ctl00_LogOnControls_TextBoxUserId").value;
        var pwd = document.getElementById("ctl00_LogOnControls_TextBoxUserPassword").value;
        var domain = "";
        try {
            domain = document.getElementById("ctl00_LogOnControls_TextBoxDomain").value;
        }
        catch (Error) {
            domain = "";
        }
        var authSource = document.getElementById("ctl00_LogOnControls_hfAuthenticationSocurce").value;
        var logOnMode = document.getElementById("ctl00_LogOnControls_hfLogOnMode").value;

        var targetUrl = "ManualLogOn.aspx?authSource=" + authSource + "&logOnMode=" + logOnMode + "&uid=" + uid + "&pwd=" + pwd + "&domain=" + domain;

        document.forms[0].action = targetUrl
        document.forms[0].submit();
    }

    function ClearControls() {
        var targetUrl = "ManualLogOn.aspx";
        document.forms[0].action = targetUrl
        document.getElementById("ctl00_LogOnControls_TextBoxUserId").value = "";
        document.getElementById("ctl00_LogOnControls_TextBoxUserPassword").value = "";
    }
    </script>

    <asp:HiddenField ID="hfAuthenticationSocurce" Value="" runat="server" />
    <asp:HiddenField ID="hfLogOnMode" Value="" runat="server" />
    <div style="display: inline; width: 500px; left: 30px; z-index: 1; position: absolute;"
        id="PageLoadingID">
        <table cellpadding="0" cellspacing="0" border="0" width="300" height="200">
            <tr>
                <td align="left" style="padding-left: 5px;" valign="middle">
                    <asp:Image ID="ImagePageLoading" runat="server" SkinID="ImagePageLoading" />
                </td>
                <td align="left" style="padding-left: 5px;" valign="middle" class="Login_TextFonts">
                    <asp:Label ID="LabelPageLoading" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none" id="PageShowingID">
        <asp:Table ID="TableManualLogOn" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
            <asp:TableRow ID="TableRowLogOnControls" runat="server" Visible="true">
                <asp:TableCell Width="8%">&nbsp;</asp:TableCell>
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
                                            <asp:Label ID="LabelManualLogOnMessage" runat="server" Text=""></asp:Label>
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
                                            <asp:Image ID="LoginUser" SkinID="LoginUser" Visible="false" runat="server" />
                                        </asp:TableCell>
                                        <asp:TableCell Width="70%" HorizontalAlign="Left" VerticalAlign="Top">
                                            <asp:Table Width="100%" CellPadding="3" CellSpacing="3" runat="server">
                                                <asp:TableRow ID="TableRowPrintReleaseServer" Visible="false">
                                                    <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelPrintReleaseServer" runat="server" Text=""></asp:Label>&nbsp;
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                        &nbsp;<asp:TextBox ID="TextBoxPrintReleaseServer" MaxLength="15" runat="server"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowUserId" CssClass="Normal_FontLabel">
                                                    <asp:TableCell Width="50%" HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelUserId" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxUserId" runat="server" CssClass="UserName_TextBox" MaxLength="50"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowPassword" CssClass="Normal_FontLabel">
                                                    <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelPassword" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxUserPassword" runat="server" CssClass="UserName_TextBox"
                                                            TextMode="Password" MaxLength="20"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowDomain" CssClass="Normal_FontLabel">
                                                    <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelDomain" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle" Wrap="false">
                                                        <asp:DropDownList ID="DropDownListDomainList" runat="server" Width="225px" CssClass="UserName_TextBox">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LabelNoDomains" runat="server" Text="" CssClass="Login_title_Font"
                                                            Visible="false"></asp:Label>
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
                                <asp:Table Width="100%" runat="server" CellPadding="0" CellSpacing="0" border="0">
                                    <asp:TableRow>
                                        <asp:TableCell Width="28%" HorizontalAlign="Left" VerticalAlign="Top">&nbsp;
                                        </asp:TableCell>
                                        <asp:TableCell Width="38%" HorizontalAlign="Right" VerticalAlign="Middle">
                                            <asp:LinkButton ID="LinkButtonClear" OnClick="LinkButtonClear_Click" runat="server">
                                            <a href="javascript:ClearControls()">
                                                <asp:Table Width="60%" CellPadding="0" CellSpacing="0" Height="38" runat="server"
                                                    border="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Left">
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelClear" runat="server" Text=""></asp:Label></div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                </a>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell Width="32%" HorizontalAlign="Center" VerticalAlign="middle">
                                            <asp:LinkButton ID="LinkButtonLogOn" OnClick="LinkButtonLogOn_Click" runat="server">
                                            <a href="javascript:SendData()">
                                                <asp:Table runat="server" Width="70%" CellPadding="0" CellSpacing="0" Height="38"
                                                    border="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Left">
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelLogOnOK" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                </a>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell Width="2%" HorizontalAlign="left" VerticalAlign="top">&nbsp;
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="TableCommunicator" EnableViewState="false" Visible="false" runat="server"
                        Width="50%" Height="229" HorizontalAlign="Center" CellPadding="0" CellSpacing="0"
                        border="0">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="115">
                                <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="32%" HorizontalAlign="Center" VerticalAlign="Middle" Height="112">
                                <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                    <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="45%"
                                        border="0">
                                        <asp:TableRow>
                                            <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Left">
                                            </asp:TableCell>
                                            <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Right">
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
                <asp:TableCell Width="9%">&nbsp;</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
