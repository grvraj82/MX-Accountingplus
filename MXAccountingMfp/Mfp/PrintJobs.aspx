<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintJobs.aspx.cs" Inherits="AccountingPlusDevice.Browser.PrintJobs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Print Jobs::</title>
    <meta name="Browser" content="NetFront" />

    <script language="javascript" type="text/javascript">
        function JobFinished()
        {
            location.href = "JobList.aspx?ID=Status";
        }
    
    </script>

</head>
<body leftmargin="0" topmargin="0" scroll="NO" class="InsidePage_BGcolor" onload="PageShowing();">
    <div style="display: inline; width: 500px; left: 30px; z-index: 1; position: absolute"
        id="PageLoadingID" class="InsidePage_BGcolor">
        <table cellpadding="0" cellspacing="0" border="0" width="300" height="200">
            <tr>
                <td align="left" style="padding-left: 5px;" valign="middle">
                    <asp:Image ID="ImagePageLoading" runat="server" SkinID="ImagePageLoading" />
                </td>
                <td align="left" style="padding-left: 5px;" valign="middle" class="Login_TextFonts">
                    <asp:Label ID="LabelPageLoading" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none" id="PageShowingID">
        <form id="form1" runat="server">
        <table width="<%=pageWidth%>" border="0" cellpadding="0" cellspacing="0" height="<%=pageHeight%>">
            <tr>
                <td height="50" align="left" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" height="50">
                        <tr>
                            <td width="45%" class="Inside_TOPTitleFontBold">
                                &nbsp;
                                <asp:Label ID="LabelPrintingProgress" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="3%">
                                &nbsp;
                            </td>
                            <td width="18%">
                                &nbsp;
                            </td>
                            <td width="17%">
                            </td>
                            <td width="15%" align="center" valign="middle">
                                <asp:LinkButton ID="LinkButtonClose" Visible="false" runat="server" OnClick="LinkButtonClose_Click">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" height="38">
                                        <tr>
                                            <td width="15%" align="left" valign="top" class="Button_Left">
                                                &nbsp;
                                            </td>
                                            <td width="55%" align="left" valign="middle" class="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelClose" runat="server" Text=""></asp:Label>
                                                </div>
                                            </td>
                                            <td width="30%" align="left" valign="middle" class="Button_Right">
                                                <asp:Image ID="Image1" runat="server" SkinID="CloseIMG" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                            </td>
                            <td width="2%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="2" class="HR_line">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <asp:Table ID="TableStatus" HorizontalAlign="Center" BorderWidth="0" Width="100%"
                        runat="server">
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
                                            <br />
                                            <br />
                                            <asp:LinkButton ID="LinkButtonOK" Visible="true" runat="server" OnClick="LinkButtonOK_Click">
                                                <table width="100" border="0" cellpadding="0" cellspacing="0" height="38">
                                                    <tr>
                                                        <td width="15%" align="left" valign="top" class="Button_Left">
                                                            &nbsp;
                                                        </td>
                                                        <td width="70%" align="left" valign="middle" class="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td width="25%" align="left" valign="middle" class="Button_Right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:LinkButton>
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
    </div>

    <script language="javascript" type="text/javascript">             
    function PageShowing()
    {
        document.getElementById("PageLoadingID").style.display="none";
        document.getElementById("PageShowingID").style.display="inline";
        setInterval("LoadNopPage()", 1000);
    }
    
    function LoadNopPage()
    {
        document.getElementById("NopFrame").src = "../Nop.aspx";
    }
    
    </script>

</body>
</html>
