<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="AccessDenied.aspx.cs" Inherits="AccountingPlusEA.Mfp.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function RedirectTo() {
            location.href = "logon.aspx";
        }

        function timeout() {
            setTimeout("RedirectTo()", 6000);
        }
        onload = timeout();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogOnControls" runat="server">
    <asp:Table ID="Table1" runat="server" Width="100%" Height="150">
        <asp:TableRow HorizontalAlign="Center" VerticalAlign="Top">
            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                <asp:Label ID="LabelDisplayMessage" runat="server" Font-Bold="true" ForeColor="Maroon"
                    Font-Names="Verdana" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow HorizontalAlign="Center" VerticalAlign="Top">
            <asp:TableCell>
                <asp:LinkButton ID="LinkButtonLogOn" OnClick="LinkButtonLogOn_Click" runat="server">
                    <asp:Table ID="Table2" runat="server" Width="100px" CellPadding="0" CellSpacing="0"
                        Height="38" border="0">
                        <asp:TableRow>
                            <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Left">
                            </asp:TableCell>
                            <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                <div class="Login_TextFonts">
                                    <asp:Label ID="LabelLogOnOK" runat="server" Text="Ok"></asp:Label>
                                </div>
                            </asp:TableCell>
                            <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Right">
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:LinkButton>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
