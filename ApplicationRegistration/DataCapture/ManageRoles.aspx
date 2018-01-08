<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true" CodeBehind="ManageRoles.aspx.cs" Inherits="ApplicationRegistration.DataCapture.ManageRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
<asp:Panel ID="PanelManageRole" runat="server">
   <br />
   <table cellpadding="2" cellspacing="3" border="0" align="center" id="UserForm" runat="server">
   <tr>
    <td>
    <fieldset><legend><asp:Label ID="LabelFieldSetTitle" runat="server" Text="Role Information" Font-Bold="True"></asp:Label></legend>
      
        <table cellpadding="1" cellspacing="1" border="0" width="100%">
        <tr>
        <td colspan="2"><asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label></td>
        </tr>
       <tr>
            <td colspan="2" style="text-align:right"><asp:Label ID="LabelRequiredFields" CssClass="f11" Text="" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="white-space:nowrap" width="100">
                <asp:Label ID="LabelRoleName" runat="server" Text="Name"></asp:Label>
                *</td>
            <td style="width: 354px">
                <asp:TextBox ID="TextBoxRoleName" runat="server" Width="328px" MaxLength="50" CssClass="DataCapture"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredFieldName" runat="server" ControlToValidate="TextBoxRoleName"
                    ErrorMessage="Please enter Role Name" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regularExpressionValidatorName" runat="server" ControlToValidate="TextBoxRoleName"
                    Display="Dynamic" ErrorMessage="Please enter valid alphanumeric value" ValidationExpression="^[a-zA-Z0-9 ]+$" SetFocusOnError="True">*</asp:RegularExpressionValidator>
                <asp:HiddenField ID="HiddenRoleID" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelUserID" runat="server" Text="Role ID"></asp:Label>
                *</td>
            <td style="width: 354px">
                <asp:TextBox ID="TextBoxRoleID" runat="server" Width="328px" MaxLength="20" CssClass="DataCapture"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredFieldLabelRoleID" runat="server" ControlToValidate="TextBoxRoleID"
                    ErrorMessage="Please enter valid alphanumeric Role ID" SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxRoleID"
                        Display="Dynamic" ErrorMessage="Please enter alphanumeric Role ID" ValidationExpression="\w*">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelRoleCategory" runat="server" Text="Category"></asp:Label></td>
            <td style="width: 354px">
                <asp:DropDownList ID="DropDownListCategory" runat="server" CssClass="DataCapture" Width="332px">
                    <asp:ListItem>Portal</asp:ListItem>
                    <asp:ListItem>Product</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="LabelRoleDescription" runat="server" Text="Description"></asp:Label></td>
            <td style="width: 354px">
                <asp:TextBox ID="TextBoxRoleDescription" runat="server" MaxLength="500" Width="327px"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td colspan="2"><asp:CheckBox ID="CheckBoxActive" runat="server" CssClass="DataCapture" /><asp:Label ID="LabelEnableAccess" runat="server" Text="Active"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" Width="85px" Visible="False" />
                <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" Width="85px" Visible="False" />&nbsp;
                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" Width="85px" CausesValidation="False" OnClick="ButtonCancel_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
            </td>
        </tr>
       </table>
        
     
        </fieldset>
        </td>
         </tr>
    </table>
    </asp:Panel>

</asp:Content>