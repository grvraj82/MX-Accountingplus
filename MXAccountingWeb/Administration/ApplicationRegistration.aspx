<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationRegistration.aspx.cs"
    Inherits="PrintRoverWeb.Administration.ApplicationRegistration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="ClientMessages" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="PageContent" runat="server">
<script language="javascript" type="text/javascript">
    fnShowCellSettings();
    Meuselected("Settings");
    function myKeyPressHandler() {
        if (event.keyCode == 13) {
            document.getElementById('ctl00_PageContent_ButtonSave').focus();
        }
    }

    document.onkeypress = myKeyPressHandler;

</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div style=" height:10px;">&nbsp;</div>
    <table width="98%" align="center" border="0" cellpadding="0" cellspacing="0" height="500">
        <tr>
              <td width="72%" valign="top">
                <table border="0" cellpadding="0" cellspacing="0" class="table_border_org" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="left" class="Top_menu_bg" height="35" style="padding-left: 5px;">
                                                                    &nbsp;<asp:Label ID="LabelHeadingGeneral" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td align="right" class="Top_menu_bg" height="35" style="padding-right: 10px;">
                                                                    <asp:ImageButton ID="ImageHome" runat="server" CausesValidation="False" OnClick="ImageHome_Click"
                                                                        SkinID="InnerpageimgHome" ToolTip="Home" Visible="false" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table border="0" cellpadding="4" cellspacing="0" width="100%">
                                                            <tr style="display: none">
                                                                <td align="right" height="31" width="45%">
                                                                    <asp:Label ID="LabelInstallationDateText" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" width="55%">
                                                                    <asp:Label ID="LabelInstallationDate" runat="server" CssClass="FormTextBoxReg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="TableTrialdaysLeft" runat="server" height="31">
                                                                <td align="right">
                                                                    <asp:Label ID="LabelTrialDaysText" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="LabelTrialDays" runat="server" CssClass="FormTextBox_bg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="middle">
                                                                    <asp:Label ID="LabelRegisteredLicencesText" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="LabelRegisteredLicences" runat="server" CssClass="FormTextBox_bg"
                                                                        Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr height="31px" style="display: none">
                                                                <td align="right" valign="middle">
                                                                    <asp:Label ID="LabelSerialKeysText" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Label ID="LabelSerialKeys" runat="server" CssClass="FormTextBox_bg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="TableTrialLicenseText" runat="server" height="31px">
                                                                <td align="Right">
                                                                    <asp:Label ID="LabelTrialLicencesText" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="LabelTrialLicences" runat="server" CssClass="FormTextBox_bg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr height="31px">
                                                                <td align="right">
                                                                    <asp:Label ID="LabelNotesText" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="LabelNotes" runat="server" CssClass="FormTextBox_bg" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" height="35" valign="middle">
                                                                    <asp:Label ID="LabelRequestCode" runat="server" class="f10b" Text=""></asp:Label>&nbsp;
                                                                </td>
                                                                <td align="left" valign="bottom">
                                                                    <asp:Label ID="TextBoxRequestCode" runat="server" BorderWidth="0" CssClass="FormTextBox_bg"
                                                                        MaxLength="15"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="Right" height="35" valign="middle">
                                                                    <asp:Label ID="LabelRegistartionCode" runat="server" class="f10b" Text=""></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:TextBox ID="TextBoxRegistartionCode" runat="server" CssClass="FormTextBoxRegBOX"
                                                                        MaxLength="100"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" height="15">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                     <asp:Button ID="ButtonSave" runat="server" CssClass="Login_Button" OnClick="ButtonSave_Click"
                                                                    Text="" />
                                                                <asp:Button ID="ButtonCancel" runat="server" CausesValidation="false" CssClass="Cancel_button"
                                                                    OnClick="ButtonCancel_Click" Text="" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" height="15">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
            </td>
        </tr>
    </table>
</asp:content>
