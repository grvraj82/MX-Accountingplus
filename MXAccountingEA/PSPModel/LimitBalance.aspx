<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LimitBalance.aspx.cs" Inherits="AccountingPlusEA.PSPModel.LimitBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="Browser" content="NetFront" />
    <title>Limit Balance</title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
</head>
<style type="text/css">
     <asp:Literal ID="PageBackground" runat="server"></asp:Literal>
</style>
<body leftmargin="0" topmargin="0" scroll="auto" class="InsideBG" onload="PageShowingEve()">
    <div style="display: inline; width: 500px; left: 30px; z-index: 1; position: absolute"
        id="PageLoadingID" class="InsidePage_BGcolor">
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
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" height="250" id="TablePage" runat="server"
        runat="server" width="102%">
        <tr>
            <td height="50" align="left" valign="top">
                <asp:Table ID="Table1" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0"
                    Height="50" border="0" runat="server">
                    <asp:TableRow>
                        <asp:TableCell Width="65%" CssClass="Inside_TOPTitleFontBold">
                            &nbsp;<asp:Label ID="LabelJobListPageTitle" runat="server" Text=""></asp:Label>
                            <span class="Inside_TOPTitleUserFontBold">
                                <asp:Label ID="LabelCostCenterName" runat="server" Text=""></asp:Label>
                            </span>
                        </asp:TableCell>
                        <asp:TableCell Width="18%" HorizontalAlign="Center">
                            <div id="PanelPrintActive" style="display: inline">
                                <asp:LinkButton ID="LinkButtonCancel" OnClick="LinkButtonCancel_Click" runat="server">
                                    <table cellpadding="0" cellspacing="0" border="0" height="38" width="82%">
                                        <tr>
                                            <td width="2%" align="right" valign="top" class="Button_Left">
                                            </td>
                                            <td width="96%" align="center" valign="middle" class="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelCancel" runat="server" Text=""></asp:Label>
                                                </div>
                                            </td>
                                            <td width="2%" align="left" valign="middle" class="Button_Right">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell ID="TCPrint" Width="17%" HorizontalAlign="Center">
                            <asp:LinkButton ID="LinkButtonPrint" OnClick="ImageButtonPrintNow_Click" runat="server">
                                <table width="82%" border="0" cellpadding="0" cellspacing="0" height="38">
                                    <tr>
                                        <td width="4%" align="left" valign="middle" class="Button_Left">
                                        </td>
                                        <td width="75%" align="center" valign="middle" class="Button_center">
                                            <div class="Login_TextFonts">
                                                <asp:Label ID="LabelPrint" runat="server" Text=""></asp:Label>
                                            </div>
                                        </td>
                                        <td width="3%" align="left" valign="middle" class="Button_Right">
                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td height="2" class="HR_line_New">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <table id="TablePrintJobs" width="100%" border="0" cellpadding="0" cellspacing="0"
                    runat="server">
                    <tr>
                        <td width="89%" align="left" valign="top">
                            <asp:Table ID="TableLimits" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                <asp:TableHeaderRow CssClass="Title_bar_bg" Height="30">
                                    <asp:TableHeaderCell ID="HeaderCellSlNo" Wrap="false" Width="10"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell7" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                    <asp:TableHeaderCell ID="HeaderCellJobType" Wrap="false" Style="font-size: small;"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell1" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellLimits" Wrap="false" Style="font-size: small;"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell3" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                    <asp:TableHeaderCell ID="TableHeaderCellLimitsAvailable" Wrap="false" Style="font-size: small;"></asp:TableHeaderCell>
                                    <asp:TableCell ID="TableCell5" Width="2" runat="server" class="Vr_Line_Insidepage"
                                        RowSpan="26"></asp:TableCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Height="2" ColumnSpan="12"
                                        CssClass="HR_line_New">
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    <script language="javascript" type="text/javascript">
        function PageShowingEve() {
            setTimeout(PageShowing(), 50000);
        }
        function PageShowing() {
            document.getElementById("PageLoadingID").style.display = "none";
            document.getElementById("PageShowingID").style.display = "inline";
        }
    
    </script>
</body>
</html>
