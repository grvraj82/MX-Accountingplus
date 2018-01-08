<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="OsaICCardLogon.aspx.cs" Inherits="AccountingPlusEA.Mfp.OsaICCardLogi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogOnControls" runat="server">
    <div style="display: inline; width: 500px; left: 30px; z-index: 1; position: absolute;"
        id="PageLoadingID">
        <table cellpadding="0" cellspacing="0" border="0" width="300" height="200">
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
        <table cellpadding="0" cellspacing="0" border="0" width="80%">
            <tr>
                <td width="40%">
                    <asp:Image ID="ImageSwipeCard" runat="server" />
                </td>
                <td>
                    <asp:Label ID="LabelSwipeCard" runat="server" Font-Bold="true" Font-Size="Large"
                        Text="Swipe your card to Login"></asp:Label>
                </td>
            </tr>
            <tr align="center" style="height: 20px; width: 100px;">
                <td>
                </td>
                <td align="left">
                    <%--<asp:Button ID="ButtonManualLogon" runat="server" Text="Manual Login" OnClick="ButtonManualLogon_Click" />--%>
                    <asp:LinkButton ID="LinkButtonManualLogon" runat="server" OnClick="LinkButtonManualLogon_Click">
                        <asp:Table ID="Table9" runat="server" CellPadding="0" CellSpacing="0" Height="38"
                            Width="50%" border="0">
                            <asp:TableRow>
                                <asp:TableCell Width="10%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                </asp:TableCell>
                                <asp:TableCell Width="80%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                    <div class="Login_TextFonts">
                                        <asp:Label ID="LabelManualLogon" runat="server" Text="Manual Login"></asp:Label>
                                    </div>
                                </asp:TableCell>
                                <asp:TableCell Width="10%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <iframe id="reloader" src="refresh.aspx?did=<%=deviceIPAddr%>" style="display: none;">
        </iframe>
    </div>
    <script type="text/javascript">
        var reloadInterval = 60000;
        function init() {
            setTimeout('reload()', reloadInterval);
        }
        // window.onload = init
        function reload() {
            var iframe = document.getElementById('reloader');
            if (!iframe) return false;
            iframe.src = iframe.src;
            setTimeout('reload()', reloadInterval);
            document.getElementById("PageLoadingID").style.display = "none";
            document.getElementById("PageShowingID").style.display = "inline";
        }
        onunload = init();
        //onload = init();
    </script>
</asp:Content>
