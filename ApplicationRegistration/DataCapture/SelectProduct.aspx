<%@ Page MasterPageFile="~/AppMaster/LogOn.Master"  Language="C#" AutoEventWireup="true" CodeBehind="SelectProduct.aspx.cs" Inherits="ApplicationRegistration.DataCapture.SelectProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
    &nbsp;&nbsp;

Products: 
    <asp:DropDownList ID="DropDownListProducts" runat="server">
    </asp:DropDownList>
    <asp:Button ID="ButtonSelectProduct" runat="server" Text="Select" OnClick="ButtonSelectProduct_Click" />
</asp:Content>