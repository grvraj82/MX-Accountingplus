<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="ManageProducts.aspx.cs" Inherits="ApplicationRegistration.DataCapture.ManageProducts" %>
    <asp:Content ID="Products" runat="server" ContentPlaceHolderID="PageContentHolder">
    <script language="javascript" type="text/javascript">
    
    function isValidLogofile()
    {
        //thisForm = document.forms[0];
        //fileType = document.forms[0]
        alert("Validating Image file");
        return true;
    }
    
    function onkeyPress(e)
    {
        var key = window.event ? e.keyCode : e.which;
        if (key == 13)
        e.cancelBubble = true;
        e.returnValue = false;
        return false;
    }

    </script>
    <br />
      <table width="100%" cellpadding="0" cellspacing="0" border="0">
              
        <tr>
            <td align="center">
               <div style="overflow:auto;width:895px;height:420px" id="PanelProductDetails">
                <table cellpadding="0" cellspacing="0" border="0">
                     <tr>
                        <td width="100%">
                               <table cellpadding="2" cellspacing="3" border="0" align="center">
                               <tr>
                                    <td align="left">
                                    <fieldset><legend><asp:Label ID="LabelGroupName" runat="server" Text="Product Information" Font-Bold="True"></asp:Label>&nbsp;</legend>
                                    <table cellpadding="3" cellspacing="1" border="0" width="100%">
                                    <tr>
                                        <td colspan="2"><asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align:right"><asp:Label ID="LabelRequiredFields" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="white-space:nowrap">
                                            <asp:Label ID="LabelProductName" runat="server" Text="Product Name"></asp:Label>
                                            *</td>
                                        <td>
                                            <asp:TextBox ID="TextBoxProductName" runat="server" Width="328px" MaxLength="50" CssClass="DataCapture"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredFieldProductName" runat="server" ControlToValidate="TextBoxProductName"
                                                ErrorMessage="Please enter Valid Product Name" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="HiddenProductID" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelProductID" runat="server" Text="Product ID"></asp:Label>
                                            *</td>
                                        <td>
                                            <asp:TextBox ID="TextBoxProductCode" runat="server" Width="328px" MaxLength="30" CssClass="DataCapture"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredFieldProductID" runat="server" ControlToValidate="TextBoxProductCode"
                                                ErrorMessage="Please enter valid Product ID" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelProductVersion" runat="server" Text="Version"></asp:Label>
                                            *</td>
                                        <td>
                                            <asp:TextBox ID="TextBoxProductVersion" runat="server" Width="328px" MaxLength="20" CssClass="DataCapture"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredFieldProductVersion" runat="server" ControlToValidate="TextBoxProductVersion"
                                                Display="None" ErrorMessage="Please enter valid Product Version" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelProductBuild" runat="server" Text="Build Number"></asp:Label>
                                            *</td>
                                        <td>
                                            <asp:TextBox ID="TextBoxProductBuild" runat="server" Width="328px" MaxLength="20" CssClass="DataCapture"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredFieldProductBuild" runat="server" ControlToValidate="TextBoxProductBuild"
                                                Display="None" ErrorMessage="Please enter valid Product Build Number" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelProductFamily" runat="server" Text="Family"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TextBoxProductFamily" runat="server" Width="328px" MaxLength="100" CssClass="DataCapture"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelProductCompany" runat="server" Text="Company"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DropDownListProductCompany" runat="server" Width="331px" CssClass="DataCapture">
                                            </asp:DropDownList>&nbsp;
                                            <asp:RequiredFieldValidator ID="requiredFieldProductCompany" runat="server" ControlToValidate="DropDownListProductCompany"
                                                ErrorMessage="Please select the company">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="LabelProductLogo" runat="server" Text="Logo"></asp:Label></td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadProductLogo" runat="server" Width="332px" CssClass="DataCapture" />
                                            <asp:CustomValidator ID="CustomValidatorProductLogo" runat="server" ClientValidationFunction="isValidLogofile()"
                                                ControlToValidate="FileUploadProductLogo" Display="None" ErrorMessage="Please select valid image file (*.jpg, *.gif)"
                                                SetFocusOnError="True" Visible="False"></asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        <asp:CheckBox ID="CheckBoxProductActive" runat="server" CssClass="DataCapture" />
                                        <asp:Label ID="LabelProductActive" runat="server" Text="Allow Registration"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" Width="85px" Visible="False" />
                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" Width="85px" Visible="False" />&nbsp;
                                            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" Width="85px" CausesValidation="False" OnClick="ButtonCancel_Click" />
                                            <br />
                                            <asp:Label ID="LabelError" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                  
                                    </table>
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" />
                                    </fieldset>
                                    </td>
                                     </tr>
                                </table>
                        </td>
                    </tr>
                </table>
               </div>
              </td>
        </tr>
    </table>
    
    </asp:Content>
