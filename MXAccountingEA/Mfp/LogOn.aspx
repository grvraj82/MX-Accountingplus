<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="LogOn.aspx.cs" Inherits="AccountingPlusEA.Browser.LogOn" Title="" %>

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

        function LoadPageImages() 
        {
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
    <table width="<%=pageWidth%>" border="0" cellpadding="0" cellspacing="0" height="<%=pageHeight%>">
        <tr>
            <td align="center" valign="top">
                <asp:Table runat="server" ID="TableLogOn" BorderWidth="1" Visible="true" CellPadding="3"
                    CellSpacing="3" Width="70%" border="0" style="margin-top:50px">
                    <asp:TableRow ID="TableDisplayRegisterDevice" runat="server" Visible="false">
                        <asp:TableCell ColumnSpan="3" HorizontalAlign="Center" Height="75">
                            <asp:Label ID="LabelDisplayRegisterMessage" runat="server" Text="" Font-Bold="true"
                                ForeColor="Maroon" Font-Names="Verdana"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
    <asp:Literal ID="LiteralSubmitButton" Text="" runat="server"></asp:Literal>
</asp:Content>
