<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="LicenseErrorpage.aspx.cs" Inherits="AccountingPlusDevice.Mfp.LicenseErrorpage"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LogOnControls" runat="server">
    <asp:Table runat="server" Width="100%"  Height="150">
        <asp:TableRow HorizontalAlign="Center" VerticalAlign="Top">
            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                <asp:Label ID="LabelDeployLicense" runat="server" Font-Bold="true"
                                ForeColor="Maroon" Font-Names="Verdana" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
