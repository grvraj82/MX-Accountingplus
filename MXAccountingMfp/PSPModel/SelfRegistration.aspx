<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="SelfRegistration.aspx.cs" Inherits="AccountingPlusDevice.PSPModel.SelfRegistration" %>

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
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="info_table"
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
                        <asp:TableCell Height="100" HorizontalAlign="Center" VerticalAlign="Top">
                            <asp:Table Width="100%" CellPadding="3" CellSpacing="3" runat="server">
                                <asp:TableRow ID="TableRowCard" Visible="false">
                                    <asp:TableCell HorizontalAlign="Right" CssClass="Normal_FontLabel">
                                        <asp:Label ID="LabelCardId" runat="server" Text="Card ID" CssClass="Login_title_Font"></asp:Label>:
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="TextBoxCardId" runat="server" Width="225px" MaxLength="400" CssClass="Inputbox"
                                            TextMode="Password"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowUserName">
                                    <asp:TableCell HorizontalAlign="Right" CssClass="Normal_FontLabel">
                                        <asp:Label ID="LabelUserId" runat="server" Text="User name" CssClass="Login_title_Font"></asp:Label>:
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="TextBoxUserName" runat="server" Width="225px" MaxLength="50" Text=""
                                            CssClass="Inputbox"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowPassword">
                                    <asp:TableCell HorizontalAlign="Right" CssClass="Normal_FontLabel">
                                        <asp:Label ID="LabelPassword" runat="server" Text="Password" CssClass="Login_title_Font"></asp:Label>:
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Width="225px"
                                            MaxLength="20" CssClass="Inputbox"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowDomain">
                                    <asp:TableCell HorizontalAlign="Right" CssClass="Normal_FontLabel">
                                        <asp:Label ID="LabelDomainName" runat="server" Text="Domain" CssClass="Login_title_Font"></asp:Label>:
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="TextBoxDomain" runat="server" Width="225px" MaxLength="30" Text="Enter Domain name"
                                            CssClass="Inputbox"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <table cellpadding="3" cellspacing="3" width="100%">
                                            <tr>
                                                <td width="50%" align="right" valign="middle">
                                                    <div style="width: 75%;">
                                                        <asp:LinkButton ID="LinkButtonCancel" Visible="true" OnClick="LinkButtonCancel_Click"
                                                            runat="server">
                                                            <asp:Table Width="100%" CellPadding="0" CellSpacing="0" runat="server" Height="38">
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="15" HorizontalAlign="Left" VerticalAlign="Top" CssClass="Button_Left">&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                                        <div class="Login_TextFonts">
                                                                            <asp:Label ID="LabelCancel" runat="server" Text="Cancel"></asp:Label>
                                                                        </div>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell Width="35%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </asp:LinkButton>
                                                    </div>
                                                </td>
                                                <td width="2%">
                                                </td>
                                                <td align="left" width="49%">
                                                    <div style="width: 75%;">
                                                        <asp:LinkButton ID="LinkButtonRegister" Visible="true" OnClick="LinkButtonRegister_Click"
                                                            runat="server">
                                                            <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" Height="38">
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="15%" HorizontalAlign="left" VerticalAlign="top" CssClass="Button_Left">&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                                        <div class="Login_TextFonts">
                                                                            <asp:Label ID="LabelRegister" runat="server" Text="Register"></asp:Label>
                                                                        </div>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell Width="35%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </asp:LinkButton>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="TableFutureLogOnControls" Visible="false" CellPadding="3" CellSpacing="3"
                    runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2">
                            <asp:Label ID="LabelFutureMessage" runat="server" Text="Future Login" CssClass="Login_title_Font"></asp:Label>&nbsp;&nbsp;
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
                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" Width="50%">
                            <asp:Label ID="LabelPin" runat="server" Text="Pin" CssClass="Login_title_Font"></asp:Label>:
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="50%">
                            <asp:TextBox ID="TextBoxPin" runat="server" MaxLength="10" Enabled="false" Width="225px"
                                CssClass="Inputbox" TextMode="Password"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Width="100%">
                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                            <table cellpadding="3" cellspacing="3" width="100%">
                                <tr>
                                    <td align="right" width="49%">
                                        <div style="width: 90%">
                                            <asp:LinkButton ID="LinkButtonFutureLogOnCancel" Visible="true" OnClick="LinkButtonFutureLogOnCancel_Click"
                                                runat="server">
                                                <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" Height="38">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="20%" HorizontalAlign="left" VerticalAlign="top" CssClass="Button_Left">&nbsp;
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelFutureLogOnCancel" runat="server" Text="Cancel"></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="30%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td align="left" width="49%">
                                        <div style="width: 90%">
                                            <asp:LinkButton ID="LinkButtonFutureLogOn" Visible="true" OnClick="LinkButtonFutureLogOn_Click"
                                                runat="server">
                                                <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" Height="38">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="20%" HorizontalAlign="left" VerticalAlign="top" CssClass="Button_Left">&nbsp;
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="40%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelFutureLogOnOk" runat="server" Text="Ok"></asp:Label>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="40%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="TableCommunicator" EnableViewState="false" Visible="false" runat="server"
                    Width="70%" Height="170" HorizontalAlign="Center" CellPadding="3" CellSpacing="3">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="100" Width="100%">
                            <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                            <div style="width: 50%">
                                <asp:LinkButton ID="LinkButtonOk" Visible="true" OnClick="LinkButtonOk_Click" runat="server">
                                    <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" Height="38">
                                        <asp:TableRow>
                                            <asp:TableCell Width="15%" HorizontalAlign="left" VerticalAlign="top" CssClass="Button_Left">&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelOK" runat="server" Text="Ok"></asp:Label>
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell Width="35%" HorizontalAlign="left" VerticalAlign="middle" CssClass="Button_Right">
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:LinkButton>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Content>
