<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ApplicationRegistration.DataCapture.ChangePassword" %>
<asp:Content ID="ChangePasswordContent" ContentPlaceHolderID="PageContentHolder" runat="server">
<br />
<table cellpadding="2" cellspacing="3" border="0" align="center" id="UserForm" runat="server">
   <tr>
    <td>
    <fieldset><legend><asp:Label ID="LabelFieldSetTitle" runat="server" Text="Change Password" Font-Bold="True"></asp:Label></legend>
      
        <table cellpadding="1" cellspacing="1" border="0" width="100%">
       <tr>
            <td colspan="2" style="text-align:right"><asp:Label ID="LabelRequiredFields" CssClass="f11" Text="" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="height: 23px">
                <asp:Label ID="LabelUserId" runat="server" Text="User Id"></asp:Label></td>
            <td style="width: 354px; height: 23px;">
                <asp:DropDownList ID="DropDownListUsers" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="white-space:nowrap" width="100">
                <asp:Label ID="LabelOldPassword" runat="server" Text="Old Password"></asp:Label>
                *</td>
            <td style="width: 354px">
                <asp:TextBox ID="TextBoxOldPassword" runat="server" Width="328px" MaxLength="50" CssClass="DataCapture" TextMode="Password"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 22px">
                <asp:Label ID="LabelNewPassword" runat="server" Text="New Password"></asp:Label>
                *</td>
            <td style="width: 354px; height: 22px;">
                <asp:TextBox ID="TextBoxNewPassword" runat="server" Width="328px" MaxLength="20" CssClass="DataCapture" TextMode="Password"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="height: 23px">
                <asp:Label ID="LabelConfirmNewPassword" runat="server" Text="Confirm Password"></asp:Label>
                *</td>
            <td style="width: 354px; height: 23px;">
                <asp:TextBox ID="TextBoxConfirmPassword" runat="server" CssClass="DataCapture" MaxLength="20"
                    Width="328px" TextMode="Password"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;
                <asp:Button ID="ButtonChange" runat="server" Text="Change Password" Width="125px" OnClick="ButtonChange_Click" />&nbsp;
                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" Width="125px" CausesValidation="False" OnClick="ButtonCancel_Click" />&nbsp;
            </td>
        </tr>
       </table>
        &nbsp;<br />
        <br />
        </fieldset>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldPassword" runat="server"
            ControlToValidate="TextBoxOldPassword" Display="None" ErrorMessage="Please enter old password"
            SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;<br />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPassword" runat="server"
            ControlToValidate="TextBoxNewPassword" Display="None" ErrorMessage="Please enter new password"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidationCannotBeSame" runat="server" ControlToCompare="TextBoxOldPassword"
            ControlToValidate="TextBoxNewPassword" Display="None" ErrorMessage="Old and New Password can not be same"
            Operator="NotEqual"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidatorConfirmPassword" runat="server" ControlToCompare="TextBoxConfirmPassword"
            ControlToValidate="TextBoxNewPassword" Display="None" ErrorMessage="New password is not matching with confirm password"></asp:CompareValidator></td>
         </tr>
    </table>
</asp:Content>