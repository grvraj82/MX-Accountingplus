<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Blank.Master" AutoEventWireup="true"
    CodeBehind="ForgetPassword.aspx.cs" Inherits="AccountingPlusWeb.Administration.ForgetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <table id="resetpassword" border="0" cellspacing="3" width="50%" class="table_border_org">
        <tr>
            <td colspan="2">
            <div id="divStaus" runat="server" visible="false">
                <asp:Label ID="LabelStatus" Font-Bold="true" runat="server" Text=""></asp:Label><br />
                <asp:LinkButton ID="LinkButtonLogin" runat="server" 
                    onclick="LinkButtonLogin_Click">Click here to login</asp:LinkButton>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Table_HeaderBG">
                Reset Password
            </td>
        </tr>
        <tr>
            <td class="LoginFont" width="30%" align="right">
                <asp:Label ID="LabelUserId" runat="server" Text="User Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxUserId" CssClass="TextBox_BG" Width="220px" MaxLength="30"
                    AccessKey="u" Text="" runat="server" TabIndex="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="center">
                <asp:Button ID="ButtonReset" runat="server" CssClass="Login_Button" Text="Reset"
                    OnClick="ButtonReset_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
