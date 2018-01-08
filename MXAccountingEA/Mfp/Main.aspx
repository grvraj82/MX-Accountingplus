<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs" Inherits="AccountingPlusEA.Mfp.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.Button_Scan
{
    background-color:Red;
	background: url( '../Images/scan.png' );
	background-repeat: repeat-x;
	background-repeat-y: 0%;
	text-decoration: none;
}

.Button_Print
{
    background-color:Red;
	background: url( '../Images/Print.png' );
	background-repeat: repeat-x;
	background-repeat-y: 0%;
	text-decoration: none;
}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogOnControls" runat="server">
    <div>
        <table >
            <tr>
                <td style="font-size:20px">
                    Please choose your option
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <table cellpadding=" 0" cellspacing="0" border="0" width="80%">
                <tr>
                <td align="left" style="padding-left:100px" >
                    <asp:LinkButton ID="LinkButtonGuest" runat="server" OnClick="LinkButtonGuest_Click">
                        <asp:Table ID="Table2" runat="server" CellPadding="0" CellSpacing="0" width="134" >
                            <asp:TableRow >
                                <asp:TableCell  HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_Scan" Height="134" width="89">
                                    <div >
                                       <%-- <asp:Label ID="Label2" runat="server" Text="MFP Home"></asp:Label>--%>
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:LinkButton>
                </td>

                <td align="right" style="padding-right:100px">
                    <asp:LinkButton ID="LinkButtonLogin" runat="server" OnClick="LinkButtonLogin_Click">
                        <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0"  width="134" >
                            <asp:TableRow CssClass="Login_TextFonts">
                                <asp:TableCell  HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_Print" Height="134" width="89">
                                    <div class="Login_TextFonts">
                                        <%--<asp:Label ID="Label3" runat="server" Text="Confidential Print"></asp:Label>--%>
                                    </div>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
