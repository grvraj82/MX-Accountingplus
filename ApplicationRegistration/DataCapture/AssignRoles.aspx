<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true" CodeBehind="AssignRoles.aspx.cs" Inherits="ApplicationRegistration.DataCapture.AssignRoles" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
<br />
<table cellpadding="2" cellspacing="2" border="0" width="98%" align="center">
<tr>
    <td colspan="2" class="f11b">Assign Roles</td>
</tr>

<tr>
    <td align="left">Role</td>
    <td align="left">
        <asp:DropDownList ID="DropDownListRoles" runat="server" Width="303px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListRoles_SelectedIndexChanged" CssClass="DataCapture">
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
            <td><asp:Table ID="TableUsers" runat="server" CellSpacing="1" CellPadding="3" GridLines="Both" HorizontalAlign="center"  Width="98%" BorderWidth="0" BackColor="Silver">
                <asp:TableHeaderRow ID="trUserGridColumnHeader" runat="server" BackColor="#EFEFEF">
                    <asp:TableHeaderCell Width="20px" ID="tdItemSelection" runat="server"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="tdUserId" runat="server" Wrap="false" HorizontalAlign="Center">ID</asp:TableHeaderCell>      
                    <asp:TableHeaderCell ID="tdUserName" runat="server" Wrap="false" HorizontalAlign="Center">Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="tdUserCompany" runat="server" Wrap="false" HorizontalAlign="Center">Company</asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="tdUserEmail" runat="server" Wrap="false" HorizontalAlign="Center">Email</asp:TableHeaderCell>
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
