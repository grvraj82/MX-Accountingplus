<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/LogOn.Master"
    CodeBehind="ManualLogOn.aspx.cs" Inherits="AccountingPlusDevice.PSPModel.ManualLogOn" %>

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
    <div style="display: inline; width: 300px; left: 30px; z-index: 1; position: absolute"
        id="PageLoadingID" class="InsidePage_BGcolor">
        <table cellpadding="0" cellspacing="0" border="0" width="250" height="150">
            <tr>
                <td align="left" style="padding-left: 5px;" valign="middle">
                    <asp:Image ID="ImagePageLoading" runat="server" />
                </td>
                <td align="left" style="padding-left: 5px;" valign="middle" class="Login_TextFonts">
                    <asp:Label ID="LabelPageLoading" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none" id="PageShowingID">
        <asp:Table ID="TableManualLogOn" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
            <asp:TableRow ID="TableRowLogOnControls" runat="server" Visible="true" Width="100%">
                <asp:TableCell HorizontalAlign="Center" VerticalAlign="top">
                    <asp:Table ID="TableLogOnControls" runat="server" Width="100%" CellPadding="0" CellSpacing="0">
                        <asp:TableRow ID="TableRowMessage" Visible="true">
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                                <table width="90%" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                    height="30">
                                    <tr>
                                        <td width="13%" align="center" valign="middle">
                                            <asp:Image ID="Info" runat="server" />
                                        </td>
                                        <td width="77%" align="left" valign="middle">
                                            <asp:Label ID="LabelManualLogOnMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRowControls">
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top" Style="padding-top: 10px">
                                <asp:Table Width="100%" CellPadding="0" CellSpacing="2" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Top">&nbsp;</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Bottom">
                                            <asp:Image ID="LoginUser" Visible="false" runat="server" />
                                        </asp:TableCell>
                                        <asp:TableCell Width="62%" HorizontalAlign="Left" VerticalAlign="Top">
                                            <asp:Table Width="100%" CellPadding="3" CellSpacing="3" runat="server">
                                                <asp:TableRow ID="TableRowPrintReleaseServer" Visible="false">
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxPrintReleaseServer" MaxLength="15" runat="server" Width="225px"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowUserId">
                                                    <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelUserId" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxUserId" runat="server" CssClass="Inputbox" MaxLength="50"
                                                            Text=""></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowPassword">
                                                    <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelPassword" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxUserPassword" runat="server" CssClass="Inputbox" TextMode="Password"
                                                            MaxLength="20"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowDomain">
                                                    <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelDomain" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle" Wrap="false">
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
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Table Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                    <asp:TableRow>
                                        <asp:TableCell Width="45%" HorizontalAlign="Right" VerticalAlign="Middle">
                                            <div style="width: 60%; margin-left: 0px; margin-right: 0px;">
                                                <asp:LinkButton ID="LinkButtonClear" OnClick="LinkButtonClear_Click" runat="server">
                                                 <a href="javascript:ClearControls()">
                                                    <asp:Table Width="85%" CellPadding="0" CellSpacing="0" Height="38" runat="server">
                                                        <asp:TableRow>
                                                            <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="75%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                                <div class="Login_TextFonts">
                                                                    <asp:Label ID="LabelClear" runat="server" Text=""></asp:Label></div>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                    </a>
                                                </asp:LinkButton>
                                            </div>
                                        </asp:TableCell>
                                        <asp:TableCell Width="6%" HorizontalAlign="left" VerticalAlign="top">
                                        </asp:TableCell>
                                        <asp:TableCell Width="40%" HorizontalAlign="left" VerticalAlign="middle">
                                            <div style="width: 70%">
                                                <asp:LinkButton ID="LinkButtonLogOn" OnClick="LinkButtonLogOn_Click" runat="server">
                                                <a href="javascript:SendData()">
                                                    <asp:Table runat="server" Width="82%" CellPadding="0" CellSpacing="0" Height="38">
                                                        <asp:TableRow>
                                                            <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="74%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                                <div class="Login_TextFonts">
                                                                    <asp:Label ID="LabelLogOnOK" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                    </a>
                                                </asp:LinkButton>
                                            </div>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="TableCommunicator" EnableViewState="false" Visible="false" runat="server"
                        Width="70%" Height="170" HorizontalAlign="Center" CellPadding="3" CellSpacing="3">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="100">
                                <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                                <div style="width: 40%">
                                    <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                        <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" Height="38"
                                            border="0">
                                            <asp:TableRow>
                                                <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                </asp:TableCell>
                                                <asp:TableCell Width="93%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                    <div class="Login_TextFonts">
                                                        <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </asp:TableCell>
                                                <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
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
    </div>
</asp:Content>
