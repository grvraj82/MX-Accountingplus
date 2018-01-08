<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="PinLoginNumPad.aspx.cs" Inherits="AccountingPlusEA.Mfp.PinLoginNumPad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font-family: Verdana, Geneva, sans-serif;
            background-image: url(../images/bg.png);
            background-position: left top;
            background-repeat: no-repeat;
            height: 393px;
            width: 800px;
        }
        
        .numberBg
        {
            position: fixed;
            left: 5px;
            bottom: 5px;
            width: 225px;
            background-color: #000;
            border: 2px solid #425eaf;
        }
        .operationNumberBg
        {
            background-color: #171717;
        }
        .button1Bg
        {
            background-image: url(../images/button1Bg.png);
            background-repeat: no-repeat;
            background-position: center;
            width: 45px;
            height: 35px;
            text-align: center;
            color: #FFF;
        }
        
        .button2Bg
        {
            background-image: url(../images/button2Bg.png);
            background-repeat: no-repeat;
            background-position: center;
            width: 98px;
            height: 35px;
            text-align: center;
            color: #FFF;
        }
        
        .close
        {
            background-image: url(../images/close.png);
            background-position: right top;
            background-repeat: no-repeat;
            width: 45px;
            height: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogOnControls" runat="server">
    <script language="javascript" type="text/javascript">
        function SendData() {
            document.sound(0);
            var pin = document.getElementById("ctl00_LogOnControls_TextBoxPin").value;

            var authSource = document.getElementById("ctl00_LogOnControls_hfAuthenticationSocurce").value;
            var logOnMode = document.getElementById("ctl00_LogOnControls_hfLogOnMode").value;

            var targetUrl = "PinLoginNumPad.aspx?authSource=" + authSource + "&logOnMode=" + logOnMode + "&pin=" + pin;
            document.forms[0].action = targetUrl
            document.forms[0].submit();
        }

        function ClearControls() {
            var targetUrl = "PinLoginNumPad.aspx";
            document.forms[0].action = targetUrl
            document.getElementById("ctl00_LogOnControls_LabelUserPin").value = "";
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
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="left" VerticalAlign="middle">
                                <table width="65%" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                    height="33">
                                    <tr>
                                        <td width="10%" align="center" valign="middle">
                                            <asp:Image ID="Info" runat="server" />
                                        </td>
                                        <td width="90%" align="left" valign="middle">
                                            <asp:Label ID="LabelPinMessage" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRowControls">
                            <asp:TableCell Height="135" HorizontalAlign="left" VerticalAlign="Top">
                                <asp:Table ID="Table1" Width="90%" CellPadding="0" CellSpacing="0" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Width="30%" HorizontalAlign="Right" Visible="false">
                                            <asp:Image ID="LoginUser" Visible="false" runat="server" />
                                        </asp:TableCell>
                                        <asp:TableCell Width="70%" HorizontalAlign="left" VerticalAlign="Top">
                                            <asp:Table ID="Table2" Width="100%" CellPadding="3" CellSpacing="3" runat="server">
                                                <asp:TableRow ID="TableRowUserId" CssClass="Normal_FontLabel">
                                                    <asp:TableCell Width="15%" HorizontalAlign="right" VerticalAlign="Middle" CssClass="PinNumValign">
                                                        <asp:Label ID="LabelPin" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="85%" HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:Label ID="LabelUserPin" runat="server" Text=""></asp:Label>
                                                        <div style="display: none">
                                                            <asp:TextBox ID="TextBoxPin" runat="server"></asp:TextBox>
                                                        </div>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowPrintReleaseServer" Visible="false">
                                                    <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelPrintReleaseServer" runat="server" Text=""></asp:Label>&nbsp;
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxPrintReleaseServer" MaxLength="15" runat="server" CssClass="UserName_TextBox"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowDomain" CssClass="Normal_FontLabel">
                                                    <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelDomain" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
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
                        <asp:TableRow ID="TableRowButtons" Visible="false">
                            <asp:TableCell Height="20" HorizontalAlign="left" VerticalAlign="top">
                                <asp:Table ID="Table3" Width="100%" runat="server" CellPadding="0" CellSpacing="0">
                                    <asp:TableRow>
                                        <asp:TableCell Width="28%" Height="48" HorizontalAlign="left" VerticalAlign="top">&nbsp;
                                        </asp:TableCell>
                                        <asp:TableCell Width="38%" HorizontalAlign="right" VerticalAlign="middle">
                                            <asp:LinkButton ID="LinkButtonClear" OnClick="LinkButtonClear_Click" runat="server">
                                                <a href="javascript:ClearControls()">
                                                    <asp:Table ID="Table4" Width="50%" CellPadding="0" CellSpacing="0" Height="38" runat="server">
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
                                        <asp:TableCell Width="32%" HorizontalAlign="Center" VerticalAlign="middle">
                                            <asp:LinkButton ID="LinkButtonLogOn" OnClick="LinkButtonLogOn_Click" runat="server">
                                                <a href="javascript:SendData()">
                                                    <asp:Table ID="Table5" runat="server" Width="60%" CellPadding="0" CellSpacing="0"
                                                        Height="38">
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
                                    <asp:Table ID="Table6" runat="server" CellPadding="0" CellSpacing="0" Height="38"
                                        Width="40%">
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
        <iframe id="reloader" src="refresh.aspx?did=<%=deviceIPAddr1%>" style="display: none;">
        </iframe>
    </div>
    <div class="NumberAlign" style="display: inline;" id="divNumPad" runat="server">
        <table height="220" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td width="10">
                </td>
                <td valign="top">
                    <table cellpadding="0" cellspacing="0" border="0" class="NumberTabsOuterBg">
                        <tr>
                            <td class="NumberBtnBg" onclick="number_write(1);">
                                1
                            </td>
                            <td class="NumberBtnBg" onclick="number_write(2);">
                                2
                            </td>
                            <td class="NumberBtnBg" onclick="number_write(3);">
                                3
                            </td>
                        </tr>
                        <tr>
                            <td class="HeightDiff">
                            </td>
                        </tr>
                        <tr>
                            <td class="NumberBtnBg" onclick="number_write(4);">
                                4
                            </td>
                            <td class="NumberBtnBg" onclick="number_write(5);">
                                5
                            </td>
                            <td class="NumberBtnBg" onclick="number_write(6);">
                                6
                            </td>
                        </tr>
                        <tr>
                            <td class="HeightDiff">
                            </td>
                        </tr>
                        <tr>
                            <td class="NumberBtnBg" onclick="number_write(7);">
                                7
                            </td>
                            <td class="NumberBtnBg" onclick="number_write(8);">
                                8
                            </td>
                            <td class="NumberBtnBg" onclick="number_write(9);">
                                9
                            </td>
                        </tr>
                        <tr>
                            <td class="HeightDiff">
                            </td>
                        </tr>
                        <tr>
                            <td class="NumberBtnBg" onclick="number_c()">
                                C
                            </td>
                            <td class="NumberBtnBg" onclick="number_write(0);">
                                0
                            </td>
                            <td class="NumberBtnBg GoFont" onclick="SendData()">
                                GO
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        function number_write(x) {

            document.sound(0);
            var num = document.getElementById("ctl00_LogOnControls_TextBoxPin").value;
            var len = document.getElementById("ctl00_LogOnControls_TextBoxPin").value.length;
            if (num == undefined || num == null) {
                num = 0;
            }
            var valueNew = ((num * 10) + x);
            
            zeroPad(valueNew, len)
            processFormData();


        }
        function number_clear() {

            document.getElementById("ctl00_LogOnControls_LabelUserPin").innerText = "";
            document.getElementById("ctl00_LogOnControls_TextBoxPin").value = "";
        }


        function number_c() {
            document.sound(0);
            var control = document.getElementById("ctl00_LogOnControls_TextBoxPin");
            var num = control.value;
            if (num.length == 0) {
                control.value = '';
            }
            else if (num.length == 1) {
                control.value = '';
            }
            else {
                var num1 = num % 10;
                num -= num1;
                num /= 10;
                control.value = num;
            }
            processFormData();
        }

        function processFormData() {
            var textControl = document.getElementById("ctl00_LogOnControls_TextBoxPin").value;
            var totalTextChar = textControl.length;
            var strvalue = '';

            for (i = 0; i < totalTextChar; i++) {

                strvalue += '*';
                if (totalTextChar == 1) {
                    strvalue += '';
                }

            }

            document.getElementById("ctl00_LogOnControls_LabelUserPin").innerText = strvalue;
        }
        function zeroPad(num, count) {
            var numZeropad = num + '';
            while (numZeropad.length <= count) {
                numZeropad = ("0" + numZeropad).toString();
            }
           
            document.getElementById("ctl00_LogOnControls_TextBoxPin").value = numZeropad;
           
        }
      
    </script>
    <script type="text/javascript">

        var reloadInterval = 5000;
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
