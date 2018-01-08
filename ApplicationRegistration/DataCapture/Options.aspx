<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true" CodeBehind="Options.aspx.cs" Inherits="ApplicationRegistration.DataCapture.Options" %>
<asp:Content ID="OptionsIndex" ContentPlaceHolderID="PageContentHolder" runat="server">
<br />
<table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td class="" height="25" style="height:25">&nbsp;</td>
    </tr>
    <tr>
        <td>
             <table width="90%" cellpadding="2" cellspacing="2" align="center">
                <tr>
                    <td style="height: 20px">
                    <a href="ChangePassword.aspx"></a>
                        <asp:LinkButton ID="LinkButtonChangePassword" runat="server" CssClass="f11b" OnClick="LinkButtonChangePassword_Click">Change Password</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="height: 15px">
                        <asp:LinkButton ID="LinkButtonManageProfile" runat="server" CssClass="f11b" OnClick="LinkButtonManageProfile_Click">Manage Profile</asp:LinkButton></td>
                </tr>
               
            </table>
        </td>
    </tr>
    <tr>
        <td class=""  height="25" style="height:25">&nbsp;</td>
    </tr>
</table>

</asp:Content>