<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="SelfRegistration.aspx.cs" Inherits="AccountingPlusDevice.Browser.SelfRegistration"
    Title="Self Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function Login_ErrorShow() {
            document.getElementById("Error_meesageID").style.display = "inline";
        }
        function Login_ErrorMsgHidden() {
            document.getElementById("Error_meesageID").style.display = "none";
        }

        function userOkClick() {
            try {
                document.getElementById("PanelCommunicator").style.display = "none";
            }
            catch (Error) {
            }
            window.location = "../Mfp/SelfRegistration.aspx";
        }

        function userAction() {
            var bConfirm = window.confirm("User Did not Found. Do you want to Register");
            if (bConfirm == true) {
                window.location = "../Mfp/SelfRegistration.aspx";
            }
            else {
                window.location = "../Mfp/LogOn.aspx";
            }
        }

        function CloseCommunicator() {
            try {
                document.getElementById("PanelCommunicator").style.display = "none";
            }
            catch (Error) {
            }
        }

        function LoadPageImages() {
            pageImage = new Image(100, 25);
            self.setTimeout('CloseCommunicator()', 10000)
        }

        function SubmitData() {
            var cardTypeControl = document.getElementById("HiddenCardType");
            var cardID = document.getElementById("LabelUserId");
            var cardPassword = document.getElementById("TextBoxUserPassword");
            if (cardTypeControl.value == Constants.CARD_TYPE_SECURE_SWIPE) {
                if (cardID.value != "" && cardPassword.value != "") {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LogOnControls" runat="server">
    <asp:HiddenField ID="HiddenCardType" Value="" runat="server" />
    <table width="80%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" valign="top">
                <asp:Table ID="TableSelfRegistrationControls" HorizontalAlign="Center" runat="server"
                    Width="100%" CellPadding="0" Height="100" CellSpacing="0">
                    <asp:TableRow ID="TableRowMessage" Visible="true">
                        <asp:TableCell Height="45" HorizontalAlign="Center" VerticalAlign="Middle">
                            <table width="80%" border="0" cellpadding="0" cellspacing="0" class="info_table"
                                height="33">
                                <tr>
                                    <td width="6%" align="center" valign="middle">
                                        <asp:Image ID="Info" runat="server" />
                                    </td>
                                    <td width="94%" align="left" valign="middle">
                                        <asp:Label ID="LabelSelfRegisterMessage" runat="server" Text=""></asp:Label>
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
                                        <asp:Table Width="100%" CellPadding="3" CellSpacing="3" runat="server">
                                            <asp:TableRow ID="TableRowCard" Visible="false">
                                                <asp:TableCell HorizontalAlign="Right" CssClass="Normal_FontLabel">
                                                    <asp:Label ID="LabelCardId" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left">
                                                    <asp:TextBox ID="TextBoxCardId" runat="server" CssClass="UserName_TextBox" MaxLength="400"
                                                        TextMode="Password">
                                                    </asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRowUserName">
                                                <asp:TableCell HorizontalAlign="Right" CssClass="Normal_FontLabel">
                                                    <asp:Label ID="LabelUserId" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left">
                                                    <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="UserName_TextBox" MaxLength="50">
                                                    </asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRowPassword">
                                                <asp:TableCell HorizontalAlign="Right" CssClass="Normal_FontLabel">
                                                    <asp:Label ID="LabelPassword" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left">
                                                    <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" CssClass="UserName_TextBox"
                                                        MaxLength="20">
                                                    </asp:TextBox>
                                                    <span class="style4"></span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRowDomain">
                                                <asp:TableCell HorizontalAlign="Right" CssClass="Normal_FontLabel">
                                                    <asp:Label ID="LabelDomainName" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left">
                                                    <asp:TextBox ID="TextBoxDomain" runat="server" CssClass="UserName_TextBox" MaxLength="30">
                                                    </asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRowButtons">
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                            <table cellpadding="3" cellspacing="3" width="100%" border="0">
                                <tr>
                                    <td width="4%">
                                    </td>
                                    <td width="48%" align="right" valign="middle">
                                        <asp:LinkButton ID="LinkButtonCancel" Visible="true" OnClick="LinkButtonCancel_Click"
                                            runat="server">
                                            <asp:Table ID="Table1" Width="52%" CellPadding="0" CellSpacing="0" Height="38" runat="server"
                                                border="0">
                                                <asp:TableRow>
                                                    <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                        <div class="Login_TextFonts">
                                                            <asp:Label ID="LabelCancel" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:LinkButton>
                                    </td>
                                    <td width="48%" align="left" valign="middle">
                                        <asp:LinkButton ID="LinkButtonRegister" Visible="true" OnClick="LinkButtonRegister_Click"
                                            runat="server">
                                            <asp:Table ID="Table2" runat="server" Width="52%" CellPadding="0" CellSpacing="0"
                                                border="0" Height="38">
                                                <asp:TableRow>
                                                    <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Left">
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                        <div class="Login_TextFonts">
                                                            <asp:Label ID="LabelRegister" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="3%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="TableFutureLogOnControls" Visible="false" CellPadding="3" CellSpacing="0"
                    runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2">
                            <asp:Label ID="LabelFutureMessage" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>&nbsp;&nbsp;
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Login_title_Font" ColumnSpan="2">
                            <asp:RadioButton AutoPostBack="true" ID="RadioButtonUseWindowsPassword" runat="server"
                                Text="" OnCheckedChanged="RadioButtonUseWindowsPassword_CheckedChanged" Checked="true"
                                CssClass="Login_title_Font" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Login_title_Font" ColumnSpan="2">
                            <asp:RadioButton AutoPostBack="true" ID="RadioButtonUsePin" runat="server" Text=""
                                Checked="false" OnCheckedChanged="RadioButtonUsePin_CheckedChanged" CssClass="Login_title_Font" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="30%" HorizontalAlign="Right">
                            <asp:Label ID="LabelPin" runat="server" Text="" CssClass="Login_title_Font"></asp:Label>:
                        </asp:TableCell>
                        <asp:TableCell Width="70%" HorizontalAlign="Left">
                            <asp:TextBox ID="TextBoxPin" runat="server" MaxLength="10" Enabled="false" TextMode="Password"
                                CssClass="UserName_TextBox"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Width="100%">
                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td align="center" width="50%">
                                        <asp:LinkButton ID="LinkButtonFutureLogOnCancel" Visible="true" OnClick="LinkButtonFutureLogOnCancel_Click"
                                            runat="server">
                                            <asp:Table runat="server" Width="50%" CellPadding="0" CellSpacing="0" Height="38">
                                                <asp:TableRow>
                                                    <asp:TableCell Width="2%" HorizontalAlign="Right" VerticalAlign="top" CssClass="Button_Left">
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="46%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                        <div class="Login_TextFonts">
                                                            <asp:Label ID="LabelFutureLogOnCancel" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="1%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:LinkButton>
                                    </td>
                                    <td align="left" width="50%">
                                        <asp:LinkButton ID="LinkButtonFutureLogOn" Visible="true" OnClick="LinkButtonFutureLogOn_Click"
                                            runat="server">
                                            <asp:Table runat="server" Width="50%" CellPadding="0" CellSpacing="0" Height="38"
                                                border="0">
                                                <asp:TableRow>
                                                    <asp:TableCell Width="2%" HorizontalAlign="Right" VerticalAlign="top" CssClass="Button_Left">
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="46%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                        <div class="Login_TextFonts">
                                                            <asp:Label ID="LabelFutureLogOnOk" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="1%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="TableCommunicator" Visible="false" runat="server" Width="50%" Height="229"
                    HorizontalAlign="Center" CellPadding="0" CellSpacing="0" border="0">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="115">
                            <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                            <asp:LinkButton ID="LinkButtonOk" Visible="true" OnClick="LinkButtonOk_Click" runat="server">
                                <asp:Table runat="server" CellPadding="0" CellSpacing="0" Height="38" Width="40%"
                                    border="0">
                                    <asp:TableRow>
                                        <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                        </asp:TableCell>
                                        <asp:TableCell Width="70%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
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
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Content>
