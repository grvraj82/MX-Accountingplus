<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SortMenu.ascx.cs" Inherits="AccountingPlusWeb.UserControls.SortMenu" %>
<asp:Menu runat="server" ForeColor="Black" ID="Menu1" SkinID="NavigationMenu" Orientation="Horizontal"
    DynamicHorizontalOffset="2" OnMenuItemClick="Menu1_MenuItemClick" StaticSubMenuIndent="20px"
    border="0" Width="100%">
    <Items>
        <asp:MenuItem Text="" Value="-"></asp:MenuItem>
        <asp:MenuItem Text="*" Value="ALL" Selected="true"></asp:MenuItem>
        <asp:MenuItem Text="A" Value="A"></asp:MenuItem>
        <asp:MenuItem Text="B" Value="B"></asp:MenuItem>
        <asp:MenuItem Text="C" Value="C"></asp:MenuItem>
        <asp:MenuItem Text="D" Value="D"></asp:MenuItem>
        <asp:MenuItem Text="E" Value="E"></asp:MenuItem>
        <asp:MenuItem Text="F" Value="F"></asp:MenuItem>
        <asp:MenuItem Text="G" Value="G"></asp:MenuItem>
        <asp:MenuItem Text="H" Value="H"></asp:MenuItem>
        <asp:MenuItem Text="I" Value="I"></asp:MenuItem>
        <asp:MenuItem Text="J" Value="J"></asp:MenuItem>
        <asp:MenuItem Text="K" Value="K"></asp:MenuItem>
        <asp:MenuItem Text="L" Value="L"></asp:MenuItem>
        <asp:MenuItem Text="M" Value="M"></asp:MenuItem>
        <asp:MenuItem Text="N" Value="N"></asp:MenuItem>
        <asp:MenuItem Text="O" Value="O"></asp:MenuItem>
        <asp:MenuItem Text="P" Value="P"></asp:MenuItem>
        <asp:MenuItem Text="Q" Value="Q"></asp:MenuItem>
        <asp:MenuItem Text="R" Value="R"></asp:MenuItem>
        <asp:MenuItem Text="S" Value="S"></asp:MenuItem>
        <asp:MenuItem Text="T" Value="T"></asp:MenuItem>
        <asp:MenuItem Text="U" Value="U"></asp:MenuItem>
        <asp:MenuItem Text="V" Value="V"></asp:MenuItem>
        <asp:MenuItem Text="W" Value="W"></asp:MenuItem>
        <asp:MenuItem Text="X" Value="X"></asp:MenuItem>
        <asp:MenuItem Text="Y" Value="Y"></asp:MenuItem>
        <asp:MenuItem Text="Z" Value="Z"></asp:MenuItem>
    </Items>
</asp:Menu>
