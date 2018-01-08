<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="OtherConfigurations.aspx.cs" Inherits="ApplicationRegistration.AppConfiguration.OtherConfigurations" %>
<asp:Content ID="AppOtherConfigurations" ContentPlaceHolderID="PageContentHolder" runat="server">

<br />
<table align="center" cellpadding="0" cellspacing ="0" border="0">
    <tr>
        <td style="height: 216px">
        <fieldset>
            <legend>&nbsp;<b>Other Configurations&nbsp;</b></legend>
            <table cellpadding="4" cellspacing ="0" border="0" align="center">
                                
                <tr>
                    <td colspan="2">&nbsp;<asp:Label ID="LabelActionMessage" runat="server" CssClass="f11b"></asp:Label></td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Label ID="LabelOnscreenMaxRecords" runat="server" Text="Maximum records in reports for the option 'On Screen'"></asp:Label>
                        *</td>
                    <td style="width: 149px">
                        <asp:TextBox ID="TextBoxOnscreenMaxRecords" runat="server" MaxLength="6"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td><asp:Label ID="LabelCsvMaxRecords" runat="server" Text="Maximum records in reports for the option 'CSV'"></asp:Label>
                        *</td>

                    <td style="width: 149px">
                        <asp:TextBox ID="TextBoxCsvMaxRecords" runat="server" MaxLength="6"></asp:TextBox></td>
                </tr>
                
                                
                <tr>
                    <td><asp:Label ID="LabelSmtpServer" runat="server" Text="SMTP Server"></asp:Label>
                        *</td>

                    <td style="width: 149px">
                        <asp:TextBox ID="TextBoxSmtpServer" runat="server" MaxLength="100"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" Width="70px" />&nbsp;<asp:Button ID="ButtonCancel" CausesValidation="false" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" Width="70px" /></td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorOnscreen" runat="server" ControlToValidate="TextBoxOnscreenMaxRecords"
                Display="None" ErrorMessage="Please enter value for [Maximum records in reports for option: On Screen]"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCsv" runat="server" ControlToValidate="TextBoxCsvMaxRecords"
                Display="None" ErrorMessage="Please enter value for [Maximum records in reports for option: CSV]"
                SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSmtp" runat="server" ControlToValidate="TextBoxSmtpServer"
                Display="None" ErrorMessage="Please enter value for [SMTP server]" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidatorOnscreen" runat="server" ControlToValidate="TextBoxOnscreenMaxRecords"
                Display="None" ErrorMessage="Please enter value between 1 to 5000 for [Maximum records in reports for option: On Screen]"
                MaximumValue="5000" MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
            <asp:RangeValidator ID="RangeValidatorCsv" runat="server" ControlToValidate="TextBoxCsvMaxRecords"
                Display="None" ErrorMessage="Please enter value between 1 to 25000 for [Maximum records in reports for option: CSV]"
                MaximumValue="25000" MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator><br />
            &nbsp;
            <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
           </fieldset>
        </td>
    </tr>
</table>
<br />


</asp:Content>