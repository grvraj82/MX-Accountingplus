<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="DisplayFields.aspx.cs" Inherits="ApplicationRegistration.Settings.DisplayFields" %>
<asp:Content ID="ConfigureFields" ContentPlaceHolderID="PageContentHolder" runat="server">

<br />
<table align="center" cellpadding="0" cellspacing ="0" border="0">
    <tr>
        <td>
        <fieldset>
            <legend>&nbsp;<b>Field Access Definition&nbsp;</b></legend>
            <table cellpadding="4" cellspacing ="0" border="0" align="center">
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                               
                <tr>
                    <td>
                        Category
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListCategories" runat="server" Width="308px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCategories_SelectedIndexChanged">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        Role
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListRoles" runat="server" Width="308px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListRoles_SelectedIndexChanged">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" class="f11b" style="background-color:Silver;text-align:center">Fields</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Table ID="TableFields" CellSpacing="1" CellPadding="3" BorderWidth="0"  runat="server" HorizontalAlign="Center">
                         </asp:Table>        
                    </td>
                </tr>    
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="ButtonUpdate" runat="server" Width="85px" Text="Update" OnClick="ButtonUpdate_Click" />&nbsp;
                        <asp:Button ID="ButtonCancel" runat="server" Width="85px" Text="Cancel" OnClick="ButtonCancel_Click" />&nbsp;
                    </td>
                </tr>

            </table>
           </fieldset>
        </td>
    </tr>
</table>
<br />
</asp:Content>