<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true" CodeBehind="AssignProductstoRedist.aspx.cs" Inherits="ApplicationRegistration.DataCapture.AssignProductstoRedist" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
<br />
<table cellpadding="2" cellspacing="2" border="0" width="98%" align="center">
<tr>
    <td colspan="2" class="f11b">Assign Products to Redistributors</td>
</tr>

<tr>
    <td align="left">Redistributors</td>
    <td align="left">
        <asp:DropDownList ID="DropDownListRedist" runat="server" Width="303px" 
            AutoPostBack="True"  CssClass="DataCapture" onselectedindexchanged="DropDownListRedist_SelectedIndexChanged">
        </asp:DropDownList></td>
</tr>

</table>
<br />
<div style="overflow:auto;width:895px;height:420px">
    <table style="width:98%">
        <tr>
            <td>
                &nbsp;<asp:Button ID="ButtonOk" runat="server" Text="OK" OnClick="ButtonOk_Click" Width="62px" />&nbsp;
                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" Width="62px" /></td>
        </tr>
        <tr>
            <td><asp:Table ID="TableProducts" runat="server" CellSpacing="1" CellPadding="3" GridLines="Both" HorizontalAlign="center"  Width="98%" BorderWidth="0" BackColor="Silver">
                <asp:TableHeaderRow ID="trUserGridColumnHeader" runat="server" BackColor="#EFEFEF">
                    <asp:TableHeaderCell Width="20px" ID="tdItemSelection" runat="server"></asp:TableHeaderCell>
                    <asp:TableHeaderCell  runat="server" Wrap="false" HorizontalAlign="Center">ID</asp:TableHeaderCell>      
                    <asp:TableHeaderCell  runat="server" Wrap="false" HorizontalAlign="Center">Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell  runat="server" Wrap="false" HorizontalAlign="Center">Version</asp:TableHeaderCell>
                    <asp:TableHeaderCell  runat="server" Wrap="false" HorizontalAlign="Center">Build</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
   </table>
    </div>
</asp:Content>
