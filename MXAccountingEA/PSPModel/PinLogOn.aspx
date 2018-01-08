<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/LogOn.Master"
    CodeBehind="PinLogOn.aspx.cs" Inherits="AccountingPlusEA.PSPModel.PinLogOn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LogOnControls" runat="server">

<script language="javascript" type="text/javascript">
    function SendData() {
        var pin = document.getElementById("ctl00_LogOnControls_TextBoxUserPin").value;

        var authSource = document.getElementById("ctl00_LogOnControls_hfAuthenticationSocurce").value;
        var logOnMode = document.getElementById("ctl00_LogOnControls_hfLogOnMode").value;

        var targetUrl = "PinLogOn.aspx?authSource=" + authSource + "&logOnMode=" + logOnMode + "&pin=" + pin;
        document.forms[0].action = targetUrl
        document.forms[0].submit();
    }

    function ClearControls() {
        var targetUrl = "PinLogOn.aspx";
        document.forms[0].action = targetUrl
        document.getElementById("ctl00_LogOnControls_TextBoxUserPin").value = "";
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
        <asp:Table ID="TablePinLogOn" runat="server" CellPadding="0" CellSpacing="0" Width="90%">
            <asp:TableRow ID="TableRowLogOnControls" runat="server" Visible="true" Width="100%">
                <asp:TableCell HorizontalAlign="Center" VerticalAlign="top">
                    <asp:Table runat="server" ID="TableLogOnControls" Width="100%" CellPadding="0" CellSpacing="0">
                        <asp:TableRow ID="TableRowMessage" Visible="true">
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                                <table width="90%" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                    height="30">
                                    <tr>
                                        <td width="13%" align="center" valign="middle">
                                            <asp:Image ID="Info" runat="server" />
                                        </td>
                                        <td width="77%" align="left" valign="middle">
                                            <asp:Label ID="LabelPinMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRowControls">
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                                <asp:Table ID="Table1" Width="100%" CellPadding="0" CellSpacing="0" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Width="10%" HorizontalAlign="Left" VerticalAlign="Top">&nbsp;</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Bottom">
                                            <asp:Image ID="LoginUser" Visible="false" runat="server" />
                                        </asp:TableCell>
                                        <asp:TableCell Width="62%" HorizontalAlign="Left" VerticalAlign="Top">
                                            <asp:Table Width="100%" Height="100" CellPadding="0" CellSpacing="0" runat="server">
                                                <asp:TableRow ID="TableRowPrintReleaseServer" Visible="false">
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxPrintReleaseServer" MaxLength="15" runat="server" Width="225px"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowUserId">
                                                    <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelPin" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxUserPin" runat="server" CssClass="Inputbox" MaxLength="10"
                                                            Text="" TextMode="Password"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowDomain">
                                                    <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelDomain" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxDomain" runat="server" Text="" CssClass="Inputbox" MaxLength="30"></asp:TextBox>
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
                                        <asp:TableCell Width="15%" HorizontalAlign="left" VerticalAlign="top">&nbsp;
                                        </asp:TableCell>
                                        <asp:TableCell Width="38%" HorizontalAlign="Left" VerticalAlign="Middle">
                                            <asp:LinkButton ID="LinkButtonClear" OnClick="LinkButtonClear_Click" runat="server">
                                             <a href="javascript:ClearControls()">
                                                <asp:Table Width="80%" CellPadding="0" CellSpacing="0" Height="38" runat="server">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="10%" HorizontalAlign="Left" VerticalAlign="Top" CssClass="Button_Left">&nbsp;
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="25%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelClear" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="15%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                </a>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell Width="32%" HorizontalAlign="left" VerticalAlign="top">
                                            <asp:LinkButton ID="LinkButtonLogOn" OnClick="LinkButtonLogOn_Click" runat="server">
                                            <a href="javascript:SendData()">
                                                <asp:Table runat="server" Width="90%" CellPadding="0" CellSpacing="0" Height="38">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="10%" HorizontalAlign="left" VerticalAlign="top" CssClass="Button_Left">&nbsp;
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="35%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelLogOnOK" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="15%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                </a>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell Width="15%" HorizontalAlign="left" VerticalAlign="top">&nbsp;
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
                                        <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" Height="38">
                                            <asp:TableRow>
                                                <asp:TableCell Width="15%" HorizontalAlign="Left" VerticalAlign="Top" CssClass="Button_Left">&nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell Width="45%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                    <div class="Login_TextFonts">
                                                        <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </asp:TableCell>
                                                <asp:TableCell Width="40%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
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
