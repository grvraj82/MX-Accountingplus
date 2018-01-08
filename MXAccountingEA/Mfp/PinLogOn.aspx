<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="PinLogOn.aspx.cs" Inherits="AccountingPlusEA.Mfp.PinLogOn" Title="Pin LogOn" %>

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

        function TabSelect() {
          
            document.getElementById("ADdiv").setAttribute("class", "Log_SelectedTab_Card");
            document.getElementById("Dbdiv").setAttribute("class", "Log_UnselectedTab_Card");
            document.getElementById("ctl00_LogOnControls_hfAuthenticationSocurce").value = "AD";
        }
        function TabSelectDB() {
            document.getElementById("ADdiv").setAttribute("class", "Log_UnselectedTab_Card");
            document.getElementById("Dbdiv").setAttribute("class", "Log_SelectedTab_Card");
            document.getElementById("ctl00_LogOnControls_hfAuthenticationSocurce").value = "DB";
        }
     

    </script>
    <asp:HiddenField ID="hfAuthenticationSocurce" Value="" runat="server" />
    <asp:HiddenField ID="hfLogOnMode" Value="" runat="server" />

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
        <asp:Table ID="TablePinLogOn" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
            <asp:TableRow ID="TableRowLogOnControls" runat="server" Visible="true">
                <asp:TableCell Width="8%" Height="229">&nbsp;</asp:TableCell>
                <asp:TableCell Width="83%" HorizontalAlign="Left" VerticalAlign="top">
                    <asp:Table runat="server" ID="TableLogOnControls" Width="100%" CellPadding="3" CellSpacing="3">
                        <asp:TableRow ID="TableRowMessage" Visible="true">
                            <asp:TableCell ID="LoginInfo" ColumnSpan="2" HorizontalAlign="Center" VerticalAlign="middle" CssClass="info_table_Padd">
                                <table width="94%" align="center" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                    height="33">
                                    <tr>
                                        <td width="6%" align="center" valign="middle">
                                            <asp:Image ID="Info" runat="server" />
                                        </td>
                                        <td width="94%" align="left" valign="middle">
                                            <asp:Label ID="LabelPinMessage" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRowControls">
                            <asp:TableCell HorizontalAlign="left" VerticalAlign="Middle">
                                <asp:Table Width="97%" CellPadding="0" CellSpacing="0" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Width="30%" HorizontalAlign="Right">
                                            <asp:Image ID="LoginUser" Visible="false" runat="server" CssClass="MarginPinLoginImg_1" />
                                        </asp:TableCell>
                                        <asp:TableCell Width="70%" HorizontalAlign="Right" VerticalAlign="Middle">
                                            <asp:Table ID="Table11" CellPadding="0" CellSpacing="0" runat="server">
                                                <asp:TableRow>
                                                    <asp:TableCell  CssClass="TabsHeight_Pinlogon" >
                                                        <asp:Table ID="TableTabs" CellPadding="0" CellSpacing="0" runat="server">
                                                            <asp:TableRow>
                                                                <asp:TableCell  HorizontalAlign="Center" VerticalAlign="Middle" >
                                                                    <div onclick="TabSelect()" id="ADdiv" class="Log_SelectedTab_Card">Active Directory</div>
                                                                </asp:TableCell>
                                                                <asp:TableCell CssClass="Tabs_Space">&nbsp;</asp:TableCell>
                                                                <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <div onclick="TabSelectDB()" id="Dbdiv"  class="Log_UnselectedTab_Card">Database</div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="TabsBody_Bg_Width">
                                                        <asp:Table ID="TableReleaseServer" Width="100%" HorizontalAlign="Right" CellPadding="3" CellSpacing="3" runat="server"
                                                            CssClass="TabsBody_Bg">
                                                            <asp:TableRow ID="TableRowPrintReleaseServer" Visible="false">
                                                                <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                                    <asp:Label ID="LabelPrintReleaseServer" runat="server" Text=""></asp:Label>&nbsp;
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                                    <asp:TextBox ID="TextBoxPrintReleaseServer" MaxLength="15" runat="server" CssClass="UserName_TextBox"></asp:TextBox>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="TableRowUserId" CssClass="Normal_FontLabel">
                                                                <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                                    <asp:Label ID="LabelPin" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                                    <asp:TextBox ID="TextBoxUserPin" runat="server" CssClass="UserName_TextBox" MaxLength="10"
                                                                        TextMode="Password" istyle="4"></asp:TextBox>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="TableRowDomain" CssClass="Normal_FontLabel">
                                                                <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                                    <asp:Label ID="LabelDomain" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                                    <asp:TextBox ID="TextBoxDomain" runat="server" Text="" CssClass="UserName_TextBox"
                                                                        MaxLength="30"></asp:TextBox>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRowButtons">
                            <asp:TableCell Height="20" HorizontalAlign="left" VerticalAlign="top">
                                <asp:Table Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                    <asp:TableRow>
                                        <asp:TableCell Width="28%" Height="48" HorizontalAlign="left" VerticalAlign="top">&nbsp;
                                        </asp:TableCell>
                                        <asp:TableCell Width="25%" HorizontalAlign="right" VerticalAlign="middle">
                                            <asp:LinkButton ID="LinkButtonClear" OnClick="LinkButtonClear_Click" runat="server">
                                                <a href="javascript:ClearControls()">
                                                    <asp:Table Width="50%" CellPadding="0" CellSpacing="0" Height="38" runat="server">
                                                        <asp:TableRow>
                                                            <asp:TableCell Width="4%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Left">
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="75%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                                <div class="Login_TextFonts">
                                                                    <asp:Label ID="LabelClear" runat="server" Text=""></asp:Label></div>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="3%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </a>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell Width="19%" HorizontalAlign="Center" VerticalAlign="middle">
                                            <asp:LinkButton ID="LinkButtonLogOn" OnClick="LinkButtonLogOn_Click" runat="server">
                                                <a href="javascript:SendData()">
                                                    <asp:Table runat="server" Width="60%" CellPadding="0" CellSpacing="0" Height="38">
                                                        <asp:TableRow>
                                                            <asp:TableCell Width="4%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Left">
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="75%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                                <div class="Login_TextFonts">
                                                                    <asp:Label ID="LabelLogOnOK" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="3%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
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
                                            <asp:TableCell Width="4%" HorizontalAlign="left" VerticalAlign="top" CssClass="Button_Left">
                                            </asp:TableCell>
                                            <asp:TableCell Width="75%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell Width="3%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
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
        <iframe id="reloader" src="refresh.aspx?did=<%=deviceIPAddr%>" style="display: none;">
        </iframe>
    </div>
    <script type="text/javascript">
        var reloadInterval = 60000;
        function init() {
            setTimeout('reload()', reloadInterval);
        }

        function reload() {
            var iframe = document.getElementById('reloader');
            if (!iframe) return false;
            iframe.src = iframe.src;
            setTimeout('reload()', reloadInterval);
            document.getElementById("PageLoadingID").style.display = "none";
            document.getElementById("PageShowingID").style.display = "inline";
        }
        onunload = init();
          
    </script>
</asp:Content>
