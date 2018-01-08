<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiniStatement.aspx.cs"
    Inherits="AccountingPlusEA.Mfp.MiniStatement" %>

<html>
<head runat="server">
    <title></title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
    <asp:Literal ID="PageBackground" runat="server"></asp:Literal>
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
            <td valign="middle" class="Height_RechargeHeader PaddingLeft_Bal PaddingTop_RechHeaderText">
                <asp:Label ID="LabelMiniStatement" CssClass="WelcomeFont" runat="server" Text="Mini Statement"></asp:Label>
            </td>
            <td align="right" class="PaddingTop_RechHeaderText PaddingRight_Bal">
                <asp:Label ID="LabelBalanceAmount" CssClass="WelcomeFont" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="PaddingLeft_BackBtn">
                <table cellpadding="0" cellspacing="0" border="0" align="left">
                    <tr>
                        <td>
                        </td>
                        <td valign="middle" class="">
                            <asp:LinkButton ID="LinkButtonEmail" runat="server" Text="Email" OnClick="ButtonEmail_OnClick"
                                CssClass="Bal_Btn" Width="65px"></asp:LinkButton>
                        </td>
                        <td valign="middle" class="PaddingLeft_20">
                            <asp:LinkButton ID="LinkButtonBack" runat="server" Text="Back" OnClick="ButtonBack_OnClick"
                                CssClass="BalCancel_Btn" Width="65px"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="PaddingRight_Bal PaddingTop_10">
                <table cellpadding="0" cellspacing="0" border="0" align="right" class="BorderColorOuter_Grid">
                    <tr>
                        <td>
                        </td>
                        <td width="100%" align="left">
                            <asp:Table ID="TableMiniStatement" runat="server" CellPadding="0" CellSpacing="0"
                                BorderWidth="1" BorderColor="#ccd6e2" Width="100%">
                                <asp:TableHeaderRow CssClass="Title_bar_bg Vr_Line_Insidepage" Height="42">
                                    <asp:TableHeaderCell ID="HeaderCellSlNo" Wrap="false" Width="10" Text="S.No"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell7" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                    <asp:TableHeaderCell ID="HeaderCellRemarks" Wrap="false" Text="Remarks"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell1" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellJobLogID" Wrap="false" Text="Reference No"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell2" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                    <%--<asp:TableHeaderCell ID="TableHeaderCellUserName" Wrap="false" Text="User Name"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell6" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>--%>
                                    <asp:TableHeaderCell ID="TableHeaderCell1" Wrap="false" Text="Debit"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell3" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                    <asp:TableHeaderCell ID="TableHeaderCell2" Wrap="false" Text="Credit"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell4" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                    <asp:TableHeaderCell ID="TableHeaderCell3" Wrap="false" Text="Closing Balance"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell5" runat="server" class="" Width="2" RowSpan="26"></asp:TableCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <%--<td valign="middle" colspan="2" class="BtnsOuter_Height PaddingLeft_BackBtn">
                <asp:LinkButton ID="ButtonBack" runat="server" Text="Back" OnClick="ButtonBack_OnClick"
                    CssClass="BalCancel_Btn" Width="65px"></asp:LinkButton>
            </td>--%>
        </tr>
    </table>
    </form>
</body>
</html>
