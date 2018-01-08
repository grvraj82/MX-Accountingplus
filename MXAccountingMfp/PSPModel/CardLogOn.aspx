<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardLogOn.aspx.cs" MasterPageFile="~/MasterPages/LogOn.Master"
    Inherits="AccountingPlusDevice.PSPModel.CardLogOn" %>

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
    <div style="display: inline; width: 300px; left: 30px; z-index: 1; position: absolute;"
        id="PageLoadingID">
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
        <asp:Table ID="TableCardLogOn" runat="server" CellPadding="0" CellSpacing="0" Width="90%">
            <asp:TableRow ID="TableRowControls" runat="server" Visible="true" Width="100%">
                <asp:TableCell HorizontalAlign="Center" VerticalAlign="top">
                    <asp:Table runat="server" ID="TableLogOn" Width="100%" CellPadding="0" CellSpacing="0">
                        <asp:TableRow ID="TableRowMessage" Visible="true">
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                                <table width="90%" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                    height="30">
                                    <tr>
                                        <td width="13%" align="center" valign="middle">
                                            <asp:Image ID="Info" runat="server" />
                                        </td>
                                        <td width="77%" align="left" valign="middle">
                                            <asp:Label ID="LabelCardLogOnMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRowLogOnControls">
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                                <asp:Table Width="100%" CellPadding="0" CellSpacing="0" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Top">&nbsp;</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Bottom">
                                            <div class="Swipe_IMG">
                                                <asp:Image ID="ImageSwipeGO" Visible="false" runat="server" />
                                            </div>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                            <asp:Table Width="100%" Height="100" CellPadding="3" CellSpacing="3" runat="server">
                                                <asp:TableRow ID="TableRowPrintReleaseServer" Visible="false">
                                                    <asp:TableCell HorizontalAlign="left" ColumnSpan="2" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxPrintReleaseServer" MaxLength="15" runat="server" Width="225px"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRowCardId">
                                                    <asp:TableCell HorizontalAlign="right" VerticalAlign="middle">
                                                        <asp:Label ID="LabelCardId" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="left" VerticalAlign="middle">
                                                        <asp:TextBox ID="TextBoxCardId" runat="server" CssClass="Inputbox" MaxLength="400"
                                                            Text="" TextMode="Password"></asp:TextBox>
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
                        <asp:TableRow ID="TableLogOnButtons">
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Table Width="100%" runat="server" CellPadding="0" CellSpacing="0" border="0">
                                    <asp:TableRow>
                                        <asp:TableCell ID="TableCellFront">
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellButtonManualLogOn" Width="38%" HorizontalAlign="Center"
                                            VerticalAlign="Middle">
                                            <div style="width: 80%" align="center">
                                                <asp:LinkButton ID="LinkButtonManualLogOn" OnClick="LinkButtonManualLogOn_Click"
                                                    runat="server">
                                                     <a href="javascript:RedirectToManualLogin()">
                                                    <asp:Table Width="100%" CellPadding="0" CellSpacing="0" Height="38" runat="server">
                                                        <asp:TableRow>
                                                            <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="92%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                                <div class="Login_TextFonts">
                                                                    <asp:Label ID="LabelManualLogOn" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                    </a>
                                                </asp:LinkButton>
                                            </div>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCell1" Visible="false" Width="1%">
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellButtonClear" Width="30%" HorizontalAlign="center" VerticalAlign="middle">
                                            <div style="width: 80%">
                                                <asp:LinkButton ID="LinkButtonClear" OnClick="LinkButtonClear_Click" runat="server">
                                                <a href="javascript:ClearControls()">
                                                    <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" Height="38">
                                                        <asp:TableRow>
                                                            <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                                <div class="Login_TextFonts">
                                                                    <asp:Label ID="LabelClear" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                    </a>
                                                </asp:LinkButton>
                                            </div>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellMiddle" Visible="false" Width="1%">
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellLogOn" Width="30%" HorizontalAlign="Center" VerticalAlign="top">
                                            <div style="width: 80%">
                                                <asp:LinkButton ID="LinkButtonLogOn" OnClick="LinkButtonLogOn_Click" runat="server">
                                                <a href="javascript:SendData()">
                                                    <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" Height="38">
                                                        <asp:TableRow>
                                                            <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                                <div class="Login_TextFonts">
                                                                    <asp:Label ID="LabelLogOnOK" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                    </a>
                                                </asp:LinkButton>
                                            </div>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCellLast">
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="TableCommunicator" EnableViewState="false" Visible="false" runat="server"
                        Width="100%" Height="170" HorizontalAlign="Center" CellPadding="3" CellSpacing="3">
                        <asp:TableRow Width="100%">
                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="5" VerticalAlign="Middle">
                                <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow Width="100%">
                            <asp:TableCell Width="5%">
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="30%">
                                <asp:LinkButton ID="LinkButtonNo" OnClick="LinkButtonNo_Click" runat="server">
                                    <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell Width="15%" HorizontalAlign="Left" VerticalAlign="Top" CssClass="Button_Left">&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell Width="55%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelNo" runat="server" Text=""></asp:Label>
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell Width="5%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:LinkButton>
                            </asp:TableCell>
                            <asp:TableCell ID="TableCellOK" HorizontalAlign="center" Width="30%">
                                <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                    <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell Width="15%" HorizontalAlign="Left" VerticalAlign="Top" CssClass="Button_Left">&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell Width="35%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:LinkButton>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Width="30%">
                                <asp:LinkButton ID="LinkButtonRegister" OnClick="LinkButtonRegister_Click" runat="server">
                                    <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell Width="15%" HorizontalAlign="Left" VerticalAlign="Top" CssClass="Button_Left">&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelRegister" runat="server" Text=""></asp:Label>
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell Width="35%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:LinkButton>
                            </asp:TableCell>
                            <asp:TableCell Width="5%">
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Literal ID="LiteralSubmitButton" Text="" runat="server"></asp:Literal>
    </div>
    <script language="javascript" type="text/javascript">
        var cardType = "<%=cardType %>";
        if (cardType == "Swipe and Go") {
            self.setInterval("RaisePostBackEvent('<%=TextBoxCardId.ClientID %>')", 5000)
        }
    </script>
</asp:Content>
