<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Balance.aspx.cs" Inherits="AccountingPlusEA.Mfp.Balance" %>

<html>
<head runat="server">
    <meta name="Browser" content="NetFront" />
    <title></title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
    <asp:Literal ID="PageBackground" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">
        function ShowMessage(value) {
            if (value == "") {
                document.getElementById("DivMessage").style.display = "inline";
            }
            else {
                document.getElementById("DivMessage").style.display = "none";
            }
        }

        function Close() {
            document.getElementById("DivMessage").style.display = "none";
        }
    </script>
</head>
<body class="Bal_Main_Bg">
    <form id="formBalance" runat="server" visible="true">
    <div align="center" style="display: none;" id="DivMessage" class="ErrorMsg_OuterDiv">
     <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center" class="ErrorMsg_Bg">
            <tr>
                <td align="left" valign="middle">
                    <asp:Label ID="LabelErrorMessage" CssClass="FontErrorMessage" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" valign="middle" class="Login_TextFonts" width="5%">
                    <img src="../App_Themes/Blue/Wide-VGA/Images/Error_close.png" onclick="Close()" />
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0" align="center">
        <tr>
            <td colspan="2" valign="middle" class="Height_RechargeHeader PaddingLeft_Bal PaddingTop_RechHeaderText">
                <asp:Label ID="LabelRecharge" CssClass="WelcomeFont" runat="server" Text="Recharge"></asp:Label>
            </td>
        </tr>
        <tr><td class="PaddingTop_25"></td></tr>
        <tr>
            <td width="40%" align="right">
                <asp:Label ID="LabelTopup" runat="server" Text="Top Up Card" CssClass="Bal_Font">
                </asp:Label>&nbsp;
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxRechargeID" runat="server" CssClass="Bal_TextBox" ></asp:TextBox>
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TextBoxRechargeID"
                    ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*$" Display="Static" EnableClientScript="true" ErrorMessage="Please enter numbers only"
                    runat="server" />--%>
            </td>
        </tr>
        <tr>
            <td width="40%">
            </td>
            <td valign="middle" class="BtnsOuter_Height">
                <asp:LinkButton ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CssClass="BalCancel_Btn" Width="65px"></asp:LinkButton>
                <asp:LinkButton ID="ButtonOk" runat="server" Text="OK" OnClick="ButtonOk_OnClick" CssClass="Bal_Btn" Width="65px"></asp:LinkButton>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
