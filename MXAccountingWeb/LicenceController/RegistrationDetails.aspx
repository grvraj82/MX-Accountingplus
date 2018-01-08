<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="RegistrationDetails.aspx.cs" Inherits="AccountingPlusWeb.LicenceController.RegistrationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
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
    <table width="98%" align="center" border="0" cellpadding="0" cellspacing="0" height="550"
        class="table_border_org">
        <tr>
            <td width="72%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="left" style="padding-left: 5px;">
                            <table cellpadding="0" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td width="10%">
                                        <asp:Label ID="LblPageSubTitle" runat="server" SkinID="PageSubTitle" Text=""></asp:Label>
                                    </td>
                                    <td width="10%">
                                        &nbsp;
                                    </td>
                                    <td width="80%" align="left" style="display: none">
                                        <asp:ImageButton ID="ImgBtnAddLicences" PostBackUrl="~/LicenceController/ApplicationActivator.aspx"
                                            ToolTip="Register" SkinID="Register" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <table align="center" cellpadding="0" cellspacing="0" border="0" class="table_border_org"
                                            width="70%">
                                            <tr>
                                                <td colspan="2" align="left" valign="top">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                        <tr class="Top_menu_bg">
                                                            <td colspan="3" height="25">
                                                                <asp:Label ID="LblRegistrationPageHeader" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="300" style="background-color: White">
                                                        <tr style="display: none">
                                                            <td class="f10b" height="30" align="right" width="49%">
                                                                <asp:Label ID="LblClientCodeLabel" runat="server" Text="" SkinID="Fields"></asp:Label>:
                                                            </td>
                                                            <td valign="middle" align="left" width="30%" width="49%">
                                                                &nbsp;
                                                                <asp:Label ID="LblClientCode" runat="server" Text="" SkinID="Fields"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="f10b" height="30" align="right" width="20%">
                                                            <td height="35" align="right" width="49%">
                                                                <asp:Label ID="LblInstallationDateLabel" runat="server" Text="" SkinID="Fields"></asp:Label>:
                                                            </td>
                                                            <td valign="middle" align="left" width="49%">
                                                                &nbsp;<asp:Label ID="LblInstallationDate" runat="server" Text="" SkinID="Fields"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr  style="display: none" runat="server" class="f10b" height="30" align="right" width="20%">
                                                            <td align="right" height="30" width="49%">
                                                                <asp:Label ID="LblTrialDaysLabel" runat="server" Text="" SkinID="Fields"></asp:Label>:
                                                            </td>
                                                            <td valign="middle" align="left" colspan="2" width="49%">
                                                                &nbsp;
                                                                <asp:Label ID="LblTrialDays" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="tdTrailLicense" class="f10b" height="30" runat="server" align="right" width="20%">
                                                            <td height="35" align="right" width="49%">
                                                                <asp:Label ID="LblTrialLicencesLabel" runat="server" Text="" SkinID="Fields"></asp:Label>:
                                                            </td>
                                                            <td valign="middle" align="left" width="49%">
                                                                &nbsp;
                                                                <asp:Label ID="LblTrialLicences" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="f10b" height="30" align="right" width="20%">
                                                            <td height="35" align="right" width="49%">
                                                                <asp:Label ID="LblSerialKeyLabel" runat="server" Text="" SkinID="Fields"></asp:Label>:
                                                            </td>
                                                            <td valign="middle" align="left" width="49%">
                                                                &nbsp;
                                                                <asp:Label ID="LblSerialKeys" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="f10b" height="30" align="right" width="20%">
                                                            <td height="35" align="right" width="49%">
                                                                <asp:Label ID="LblRegisteredLicencesLabel" runat="server" Text="" SkinID="Fields"></asp:Label>:
                                                            </td>
                                                            <td valign="middle" align="left" width="49%">
                                                                &nbsp;
                                                                <asp:Label ID="LblRegisteredLicences" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="Tablerowpass" runat="server" class="f10b" height="30" align="right" width="20%">
                                                            <td height="35" align="right" width="49%">
                                                                <asp:Label ID="LblNotesLabel" runat="server" Text="" SkinID="Fields"></asp:Label>
                                                                :
                                                            </td>
                                                            <td valign="middle" align="left" width="49%">
                                                                &nbsp;
                                                                <asp:Label ID="LblNotes" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
