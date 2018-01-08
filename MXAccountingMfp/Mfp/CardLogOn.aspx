<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="CardLogOn.aspx.cs" Inherits="AccountingPlusDevice.Mfp.CardLogOn"
    Title="Card LogOn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function RaisePostBackEvent(controlID) {
            var cardIDControl = document.getElementById(controlID);
            var cardID = cardIDControl.value;
            var functionToBeCalled = "MoniterCardIDChange('" + cardID + "','" + controlID + "')";
            setTimeout(functionToBeCalled, 2000);
        }

        function MoniterCardIDChange(oldCardID, controlID) {
            var cardIDControl = document.getElementById(controlID);
            var cardID = cardIDControl.value;
            if (cardID != "" && (cardID == oldCardID)) {
                document.forms[0].submit();
            }
            else {
                return false;
            }
        }

        function RedirectToManualLogin() {
            location.href = "../Mfp/ManualLogOn.aspx";
        }

        function ClearControls() {
            var targetUrl = "CardLogOn.aspx";
            document.forms[0].action = targetUrl
            document.getElementById("ctl00_LogOnControls_TextBoxCardId").value = "";
            document.getElementById("ctl00_LogOnControls_TextBoxUserPassword").value = "";
        }

        function SendData() {
            var cardid = document.getElementById("ctl00_LogOnControls_TextBoxCardId").value;
            var pwd = "";
            try {
                pwd = document.getElementById("ctl00_LogOnControls_TextBoxUserPassword").value;
            }
            catch (Error) {
                pwd = "";
            }
            var authSource = document.getElementById("ctl00_LogOnControls_hfAuthenticationSocurce").value;
            var logOnMode = document.getElementById("ctl00_LogOnControls_hfLogOnMode").value;
            var cardType = document.getElementById("ctl00_LogOnControls_HfCardType").value;

            var targetUrl = "CardLogOn.aspx?authSource=" + authSource + "&logOnMode=" + logOnMode + "&cardId=" + cardid + "&pwd=" + pwd + "&cardType=" + cardType;

            document.forms[0].action = targetUrl
            document.forms[0].submit();
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LogOnControls" runat="server">
    <asp:HiddenField ID="hfAuthenticationSocurce" Value="" runat="server" />
    <asp:HiddenField ID="hfLogOnMode" Value="" runat="server" />
    <asp:HiddenField ID="HfCardType" Value="" runat="server" />
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
        <asp:Table ID="TableCardLogOn" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
            <asp:TableRow ID="TableRowControls" runat="server" Visible="true">
                <asp:TableCell Width="8%" Height="229">&nbsp;</asp:TableCell>
                <asp:TableCell Width="83%" HorizontalAlign="Left" VerticalAlign="top">
                    <asp:Table runat="server" ID="TableLogOn" Width="95%" CellPadding="3" CellSpacing="3">
                        <asp:TableRow ID="TableRowMessage" Visible="true">
                            <asp:TableCell Height="45" HorizontalAlign="Center" VerticalAlign="Middle">
                                <table width="80%" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                    height="33" style="margin-top: 10px;">
                                    <tr>
                                        <td width="6%" align="center" valign="middle">
                                            <asp:Image ID="Info" runat="server" />
                                        </td>
                                        <td width="94%" align="left" valign="middle">
                                            <asp:Label ID="LabelCardLogOnMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRowLogOnControls">
                            <asp:TableCell Height="130" HorizontalAlign="left" VerticalAlign="Middle">
                                <asp:Table Width="90%" CellPadding="0" CellSpacing="0" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Width="20%" HorizontalAlign="Right">
                                            <asp:Image ID="ImageSwipeGO" Visible="false" runat="server" />
                                        </asp:TableCell>
                                        <asp:TableCell Width="80%" HorizontalAlign="Left" VerticalAlign="top">
                                            <asp:Table Width="100%" CellPadding="3" CellSpacing="3" runat="server">
                                                <asp:TableRow ID="TableRowPrintReleaseServer" Visible="false">
                                                    <asp:TableCell Width="35%" HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelPrintReleaseServer" runat="server" Text=""></asp:Label>&nbsp;
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxPrintReleaseServer" MaxLength="15" runat="server" Width="225px"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowCardId" CssClass="Normal_FontLabel">
                                                    <asp:TableCell Width="50%" HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelCardId" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="65%" HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxCardId" runat="server" CssClass="UserName_TextBox" MaxLength="400"
                                                            TextMode="Password"></asp:TextBox>
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
                                                <asp:TableRow ID="TableRowDomain" Visible="false" CssClass="Normal_FontLabel">
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
                        <asp:TableRow ID="TableLogOnButtons">
                            <asp:TableCell Height="20" HorizontalAlign="Center" VerticalAlign="top">
                                <asp:Table Width="90%" runat="server" CellPadding="0" CellSpacing="0" border="0">
                                    <asp:TableRow>
                                        <asp:TableCell ID="TableCellEmptySpace">&nbsp;
                                        </asp:TableCell>
                                        <asp:TableCell Width="30%" HorizontalAlign="Right" VerticalAlign="middle">
                                            <asp:LinkButton ID="LinkButtonManualLogOn" OnClick="LinkButtonManualLogOn_Click"
                                                runat="server">
                                                <a href="javascript:RedirectToManualLogin()">
                                                <asp:Table Width="82%" CellPadding="0" CellSpacing="0" Height="38" runat="server"
                                                    border="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelManualLogOn" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                </a>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellCelar" Width="25%" HorizontalAlign="Right" VerticalAlign="Middle">
                                            <asp:LinkButton ID="LinkButtonClear" OnClick="LinkButtonClear_Click" runat="server">
                                            <a href="javascript:ClearControls()">
                                                <asp:Table runat="server" Width="82%" CellPadding="0" CellSpacing="0" Height="38"
                                                    border="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="4%" HorizontalAlign="Right" VerticalAlign="Middle" CssClass="Button_Left">
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelClear" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                </a>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell Width="25%" HorizontalAlign="Right" VerticalAlign="Middle">
                                            <asp:LinkButton ID="LinkButtonLogOn" OnClick="LinkButtonLogOn_Click" runat="server">
                                            <a href="javascript:SendData()">
                                                <asp:Table runat="server" Width="82%" CellPadding="0" CellSpacing="0" Height="38"
                                                    border="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
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
                                        </asp:TableCell>
                                        <asp:TableCell Width="4%"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="TableCommunicator" EnableViewState="false" Visible="false" runat="server"
                        border="0" Width="70%" Height="229" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="115">
                                <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="115">
                                <asp:Table CellPadding="0" CellSpacing="0" runat="server" Width="80%" border="0">
                                    <asp:TableRow HorizontalAlign="Center">
                                        <asp:TableCell ID="TableCellButtonNo" HorizontalAlign="Right" Width="26%">
                                            <asp:LinkButton ID="LinkButtonNo" OnClick="LinkButtonNo_Click" runat="server">
                                                <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="50%">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="4%" HorizontalAlign="Right" VerticalAlign="Middle" CssClass="Button_Left">
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="70%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelNo" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellButtonOk" HorizontalAlign="center" Width="36%">
                                            <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                                <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="36%"
                                                    border="0">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:LinkButton>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellButtonRegister" HorizontalAlign="Center" Width="28%">
                                            <asp:LinkButton ID="LinkButtonRegister" OnClick="LinkButtonRegister_Click" runat="server">
                                                <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="50%">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="3%" HorizontalAlign="left" VerticalAlign="Middle" CssClass="Button_Left">
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="45%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelRegister" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="2%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
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
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <asp:Literal ID="LiteralSubmitButton" Text="" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">
        var cardType = "<%=cardType %>";
        if (cardType == "Swipe and Go") {
            self.setInterval("RaisePostBackEvent('<%=TextBoxCardId.ClientID %>')", 5000)
        }
    </script>
</asp:Content>
