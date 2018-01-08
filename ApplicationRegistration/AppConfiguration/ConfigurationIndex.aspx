<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="ConfigurationIndex.aspx.cs" Inherits="ApplicationRegistration.Settings.ConfigurationIndex" %>

<asp:Content ID="AppConfigurationDefaultPage" ContentPlaceHolderID="PageContentHolder" runat="server">
<br />
<table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td class="" height="25" style="height:25">&nbsp;</td>
    </tr>
    <tr>
        <td>
             <table width="90%" align="center">
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonManageLookuptables" CssClass="f11b" runat="server" OnClick="LinkButtonManageLookuptables_Click1">Manage Lookup tables</asp:LinkButton>
                        <p style="text-indent:45">Manage Lookup tables</p>
                    </td>
                </tr>
                <!--
                <tr>
                    <td><a href="ConfigurePageAccess.aspx"><b>Page Access Definition</b></a>
                        <p style="text-indent:45">Configure the Pages that can be accessed based on user Roles</p>
                    </td>
                </tr>
                -->
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonManageFieldAccessDefinition" runat="server" CssClass="f11b"
                            OnClick="LinkButtonManageFieldAccessDefinition_Click">Field Access Definition</asp:LinkButton>
                        <p style="text-indent:45">Configure the fields to be displayed/accessed as per user Role</p>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonActivationEmailContent" runat="server" CssClass="f11b"
                            OnClick="LinkButtonActivationEmailContent_Click">Activation Email Content </asp:LinkButton><a href="ManageEmailContent.aspx"></a><p style="text-indent:45">Manage Activation Email Content</p>
                    </td>
                </tr>
                
                
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonSupportContactDetails" runat="server" CssClass="f11b"
                            OnClick="LinkButtonSupportContactDetails_Click">Support Contact Details</asp:LinkButton><a href="../Others/SupportContactDetails.aspx"></a><p style="text-indent:45">Manage Support Contact Details</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonOthers" runat="server" CssClass="f11b" OnClick="LinkButtonOthers_Click">Others</asp:LinkButton>
              
                    </td>
                </tr>
                
            </table>
        </td>
    </tr>
    <tr>
        <td class=""  height="25" style="height:25">&nbsp;</td>
    </tr>
</table>

</asp:Content>