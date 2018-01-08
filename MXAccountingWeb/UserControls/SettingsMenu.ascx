<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsMenu.ascx.cs"
    Inherits="AccountingPlusWeb.UserControls.SettingsMenu" %>
<table cellpadding="0" cellspacing="0" width="100%" class="Setting_tableBorder">
    <tr>
        <td height="35" class="Top_menu_bg">
            &nbsp;<asp:Label ID="LabelConfiguration"  runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td height="450" valign="top" align="left">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td height="35" runat="server" id="tdLinkGeneralSettings" class="Setting_FontNormal">
                        
                        <asp:LinkButton CausesValidation="false" ID="LinkGeneralSettings" Text="" runat="server" OnClick="LinkGeneralSettings_Click"
                            CssClass="Setting_FontNormal_black_select" Height="100%" Width="100%"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
                <tr>
                    <td height="35" class="Setting_FontNormal" runat="server" id="tdLinkCardConfiguration">
                       <asp:LinkButton CausesValidation="false" ID="LinkCardConfiguration" runat="server" OnClick="LinkCardConfiguration_Click"
                            Text="" CssClass="Setting_FontNormal_black" Height="100%" Width="100%"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
                <tr>
                    <td height="35" class="Setting_FontNormal" runat="server" id="tdLinkActiveDirectory">
                         <asp:LinkButton CausesValidation="false" ID="LinkActiveDirectory" runat="server" OnClick="LinkActiveDirectory_Click"
                            Text="" CssClass="Setting_FontNormal_black" Height="100%" Width="100%"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
                <tr>
                    <td height="35" runat="server" id="tdLinkJobConfiguration" class="Setting_FontNormal">
                        
                        <asp:LinkButton CausesValidation="false" ID="LinkJobConfiguration" runat="server" OnClick="LinkJobConfiguration_Click"
                            Text="" CssClass="Setting_FontNormal_black" Height="100%" Width="100%"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
               
                <tr>
                    <td height="35" runat="server" id="tdLinkDepartments" class="Setting_FontNormal">
                        <asp:LinkButton CausesValidation="false" ID="LinkDepartments" runat="server" OnClick="LinkDepartments_Click"
                            Text="" CssClass="Setting_FontNormal_black" Height="100%" Width="100%"></asp:LinkButton>
                    </td>
                </tr>
                 <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
                 <tr>
                    <td height="35" runat="server" id="td1" class="Setting_FontNormal">
                        <asp:LinkButton CausesValidation="false" ID="LinkButtonPaperSize" runat="server" OnClick="LinkButtonPaperSize_Click"
                            Text="" CssClass="Setting_FontNormal_black" Height="100%" Width="100%"></asp:LinkButton>
                    </td>
                </tr>
                 <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
                 <tr>
                    <td height="35" runat="server" id="tdLinkGroups" class="Setting_FontNormal">
                        <asp:LinkButton CausesValidation="false" ID="LinkGroups" runat="server" OnClick="LinkGroups_Click"
                            Text="" CssClass="Setting_FontNormal_black" Height="100%" Width="100%"></asp:LinkButton>
                    </td>
                </tr>
                 <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
                <tr>
                    <td height="35" runat="server" id="tdLinkButtonMAnageLanguage" class="Setting_FontNormal">
                        <asp:LinkButton CausesValidation="false" ID="LinkButtonManageLanguage" 
                            runat="server" Text=""
                            CssClass="Setting_FontNormal_black" Height="100%" Width="100%" 
                            onclick="LinkButtonManageLanguage_Click" ></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
                <tr>
                    <td height="35" runat="server" id="tdLinkButtonCustomMessages" class="Setting_FontNormal">
                        <asp:LinkButton CausesValidation="false" ID="LinkButtonCustomMessages" runat="server" Text=""
                            CssClass="Setting_FontNormal_black" Height="100%" Width="100%" OnClick="LinkButtonCustomMessages_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
                 <tr>
                    <td height="35" runat="server" id="tdLinkButtonApplicationRegistration" class="Setting_FontNormal">
                        <asp:LinkButton CausesValidation="false" ID="LinkButtonApplicationRegistration" runat="server" Text=""
                            CssClass="Setting_FontNormal_black" Height="100%" Width="100%" 
                            onclick="LinkButtonApplicationRegistration_Click" ></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td height="1" class="Yellow_line">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
