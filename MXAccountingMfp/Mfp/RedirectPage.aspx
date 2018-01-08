<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="RedirectPage.aspx.cs" Inherits="PrintReleaseEA.Mfp.RedirectPage"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        
        function RedirectTo() 
        {
            location.href = "RedirectPage.aspx?ID=redirect";
        }
        
        function IsJobFinished() 
        {
            setTimeout("RedirectTo()",6000);
        }
        onload=IsJobFinished();
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogOnControls" runat="server">
    <table width="<%=pageWidth%>" border="0" cellpadding="0" cellspacing="0" height="<%=pageHeight%>">
        <tr>
            <td align="center" valign="top">
                <asp:Table runat="server" ID="TableLogOn" BorderWidth="1" CellPadding="3"
                    CellSpacing="3" Width="70%">
                    <asp:TableRow runat="server">
                        <asp:TableCell ColumnSpan="3" HorizontalAlign="Center" Height="75">
                            <asp:Label ID="LabelDisplayMessage" runat="server" Text="asdsa" Font-Bold="true" ForeColor="Maroon"
                                Font-Names="Verdana"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Content>
