<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master"
    CodeBehind="ManageSerialKeys.aspx.cs" Inherits="ApplicationRegistration.DataCapture.ManageSerialKeys" %>

<asp:Content ID="ManageSerialKeysContent" runat="server" ContentPlaceHolderID="PageContentHolder">
    <div>
        <asp:Panel ID="PanelManageSerialKeys" runat="server">
            <br />
            <table cellpadding="2" cellspacing="3" border="0" align="center" id="ManageSerialKeysForm"
                runat="server">
                <tr>
                    <td>
                        <fieldset>
                            <legend>&nbsp;<asp:Label ID="LabelGroupTitle" runat="server" Text="Serial Number Information"
                                Font-Bold="True"></asp:Label>&nbsp;</legend>
                            <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Label ID="LabelRequiredField" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="Label3" runat="server" Text="Redistributor"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:DropDownList ID="DropDownListRedistributor" runat="server" Width="330px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="LabelLicense" runat="server" Text="Number of License"></asp:Label>
                                        *
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="TextBoxLicense" runat="server" Width="50px" MaxLength="50" CssClass="DataCapture"></asp:TextBox>
                                        &nbsp;&nbsp;<asp:Button ID="ButtonGenerateSerial" runat="server" Text="Generate SerialKey"
                                            OnClick="ButtonGenerateSerial_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="LabelSerialKey" runat="server" Text="Serial Number"></asp:Label>
                                        *
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="TextBoxSerialKey" runat="server" Width="328px" MaxLength="50" CssClass="DataCapture"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requiredFieldSerialKey" runat="server" ControlToValidate="TextBoxSerialKey"
                                            ErrorMessage="Please enter Serial Number" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regularExpressionValidatorName" runat="server"
                                            ControlToValidate="TextBoxSerialKey" Display="None" ErrorMessage="Please enter valid Serial Number"
                                            ValidationExpression="^[a-zA-Z0-9-]+$" SetFocusOnError="True">*</asp:RegularExpressionValidator>
                                        <asp:HiddenField ID="HiddenKeyId" runat="server" />
                                        <asp:HiddenField ID="HiddenProductId" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelOrganization" runat="server" Text="Company Name"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="TextBoxOrganization" runat="server" Width="328px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelAddressLine1" runat="server" Text="Address Line1"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="TextBoxAddress1" runat="server" Width="328px" MaxLength="250" CssClass="DataCapture"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelAddressLine2" runat="server" Text="Address Line2"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap;">
                                        <asp:TextBox ID="TextBoxAddress2" runat="server" Width="328px" MaxLength="250" CssClass="DataCapture"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelCity" runat="server" Text="City"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="TextBoxCity" runat="server" Width="328px" MaxLength="50" CssClass="DataCapture"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelCountry" runat="server" Text="Country"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:DropDownList ID="DropDownListCountry" runat="server" Width="330px" AutoPostBack="True"
                                            OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged" CssClass="DataCapture">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelState" runat="server" Text="State"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:DropDownList ID="DropDownListState" runat="server" Width="335px" CssClass="DataCapture">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextBoxState" runat="server" Width="328px" MaxLength="100" CssClass="DataCapture"
                                            Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelZipCode" runat="server" Text="Postal Code"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="TextBoxZipCode" runat="server" Width="328px" MaxLength="10" CssClass="DataCapture"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 27px">
                                        <asp:Label ID="Label1" runat="server" Text="Telephone Number"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap; height: 27px;">
                                        <asp:TextBox ID="TextBoxPhone" runat="server" MaxLength="15" Width="328px" CssClass="DataCapture"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Email"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="TextBoxEmail" runat="server" MaxLength="50" Width="328px" CssClass="DataCapture"></asp:TextBox><asp:RegularExpressionValidator
                                            ID="regExpEmail" runat="server" ControlToValidate="TextBoxEmail" Display="None"
                                            ErrorMessage="Please enter Valid Emial ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            SetFocusOnError="True"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelInternetURL" runat="server" Text="Internet URL"></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="TextBoxWebUrl" runat="server" CssClass="DataCapture" MaxLength="250"
                                            Width="328px"></asp:TextBox><asp:RegularExpressionValidator ID="regExpWebUrl" runat="server"
                                                ControlToValidate="TextBoxWebUrl" Display="None" ErrorMessage="Please enter Valid Internet URL"
                                                SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="CheckBoxAllowRegistration" runat="server" />&nbsp;
                                        <asp:Label ID="LabelAllowRegistration" runat="server" Text="Registration Allowed"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="white-space: nowrap">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click"
                                            Width="85px" Visible="False" />
                                        <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" Width="85px"
                                            Visible="False" />&nbsp;
                                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" Width="85px" CausesValidation="False"
                                            OnClick="ButtonCancel_Click" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </fieldset>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
