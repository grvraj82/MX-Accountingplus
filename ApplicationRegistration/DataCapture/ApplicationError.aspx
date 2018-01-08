<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/LogOn.Master" CodeBehind="ApplicationError.aspx.cs" Inherits="ApplicationRegistration.DataCapture.ApplicationError" %>
<asp:Content ID="ContentApplicationError" ContentPlaceHolderID="PageContentHolder" runat="server">
<br /><br />
<table align="center" width="75%" cellpadding="4" cellspacing="3">


<tr>
    <td colspan="2" align="center"><img src="../AppImages/Error.jpg" alt="Error" /></td>
</tr>
<tr>
    <td valign="top" class="f12b" style="white-space:nowrap">Error Message</td>
    <td valign="top">
        <asp:Label ID="LabelErrorMessage" CssClass="f11b" runat="server" Text="Label" ForeColor="#C00000"></asp:Label></td>
</tr>
<tr>
    <td valign="top" class="f12b" style="white-space:nowrap">Error Source</td>
    <td valign="top">
        <asp:Label ID="LabelErrorSource" CssClass="f11b" runat="server" Text="Label" ForeColor="Maroon"></asp:Label></td>
</tr>
<tr>
    <td valign="top" class="f12b" style="white-space:nowrap">Trace</td>
    <td valign="top">
        <asp:Label ID="LabelStackTrace" CssClass="f11b" runat="server" Text="Label" ForeColor="Maroon"></asp:Label></td>
</tr>
<tr>
    <td valign="top" class="f12b" style="white-space:nowrap">Action</td>
    <td valign="top" class="f12b">None</td>
</tr>
<tr>
    <td valign="top" class="f12b" style="white-space:nowrap">Suggestion</td>
    <td valign="top" class="f12b">Please contact Registration and Activation Web Administrator</td>
</tr>
</table>


</asp:Content>