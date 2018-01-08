<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintJobs.aspx.cs" Inherits="AccountingPlusDevice.PSPModel.PrintJobs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Print Jobs::</title>
    <meta name="Browser" content="NetFront" />

    <script language="javascript" type="text/javascript">
        function JobFinished()
        {
            location.href = "../PSPModel/JobList.aspx?ID=Status";
        }
    
    </script>

</head>
<body leftmargin="0" topmargin="0" scroll="NO" class="InsidePage_BGcolor">
    <form id="form1" runat="server">
    <table width="440" border="0" cellpadding="0" cellspacing="0" height="225">
        <tr>
            <td height="35" align="left" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" height="35">
                    <tr>
                        <td width="60%" class="Inside_TOPTitleFontBold">
                            &nbsp;
                            <asp:Label ID="LabelPrintingProgress" runat="server" Text=""></asp:Label>
                        </td>
                        <td width="8%">
                            &nbsp;
                        </td>
                        <td width="12%" align="center">
                            &nbsp;
                        </td>
                        <td width="11%" align="center">
                            &nbsp;
                        </td>
                        <td width="9%" align="center">
                            <asp:ImageButton ID="ImageButtonClose" runat="server" SkinID="MFPPrintjobsClose"
                                OnClick="LinkButtonClose_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" height="2" class="HR_line">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" height="216">
                <asp:Table ID="TableStatus" HorizontalAlign="Center" BorderWidth="0" Width="100%"
                    runat="server" Height="220">
                    <asp:TableRow>
                        <asp:TableCell Width="100%">
                            <asp:Literal ID="LiteralPrintAPI" Text="" runat="server"></asp:Literal>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Visible="false">
                        <asp:TableCell HorizontalAlign="Center">
                            <table>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="LabelResponseMessage" CssClass="Inside_TOPTitleFontBold" runat="server"
                                            Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div style="width: 40%">
                                            <asp:LinkButton ID="LinkButtonOK" Visible="true" runat="server" OnClick="LinkButtonOK_Click">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" height="38">
                                                    <tr>
                                                        <td width="15%" align="left" valign="top" class="Button_Left">
                                                            &nbsp;
                                                        </td>
                                                        <td width="50%" align="center" valign="middle" class="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td width="35%" align="left" valign="middle" class="Button_Right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="HR_line" Height="2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
