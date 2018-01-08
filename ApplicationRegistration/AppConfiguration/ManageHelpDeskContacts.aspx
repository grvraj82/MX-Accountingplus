<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true" CodeBehind="ManageHelpDeskContacts.aspx.cs" Inherits="ApplicationRegistration.AppConfiguration.ManageHelpDeskContacts" %>

<asp:Content ID="ManageHelpDeskContent" ContentPlaceHolderID="PageContentHolder" runat="server">

<table cellpadding="2" cellspacing="2" border="0" align="center">
<tr>
    <td>
        <fieldset>
        <legend class="f11b">Contact Information&nbsp;</legend>

        <table cellpadding="2" cellspacing="2" border="0" width="100%">
        <tr>
            <td style="text-align:right" colspan="2">
                <asp:HiddenField ID="HiddenFieldRecordId" runat="server" />
                <asp:Label ID="LabelRequiredFields" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelContactCentre" runat="server" Text="Contact Centre"></asp:Label> *</td>
            <td>
                <asp:TextBox ID="TextBoxContactCentre" runat="server" MaxLength="50" Width="224px"></asp:TextBox>
                <asp:HiddenField ID="HiddenFieldDbValue" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelContactNumber" runat="server" Text="Contact Number"></asp:Label> *</td>
            <td>
                <asp:TextBox ID="TextBoxContactNumber" runat="server" MaxLength="150" Width="225px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelContactAddress" runat="server" Text="Contact Address"></asp:Label></td>
            <td>
                <asp:TextBox ID="TextBoxContactAddress" Rows="5" runat="server" MaxLength="50" TextMode="MultiLine" Width="226px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" Visible="False" Width="70px" />
                <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" Visible="False" Width="70px" />&nbsp;<asp:Button ID="ButtonCancel"
                    runat="server" Text="Cancel" OnClick="ButtonCancel_Click" Width="70px" CausesValidation="False" EnableViewState="False" /></td>
        </tr>
        </table>
        </fieldset>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorContactNumber" runat="server" ControlToValidate="TextBoxContactNumber"
            Display="None" ErrorMessage="Please enter  Contact Centre Number" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorContactCentre" runat="server"
            ControlToValidate="TextBoxContactCentre" Display="None" ErrorMessage="Please enter  Contact Centre name"
            SetFocusOnError="True"></asp:RequiredFieldValidator></td>
</tr>
</table>
</asp:Content>
