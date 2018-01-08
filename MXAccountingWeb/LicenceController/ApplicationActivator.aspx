<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ApplicationActivator.aspx.cs" Inherits="AccountingPlusWeb.LicenceController.ApplicationActivator" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
    <asp:Panel ID="PnlRegistrationDetails" runat="server">
        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
            <tr>
                <td align="right" valign="top" style="width: 1px">
                    <asp:Image ID="Image4" SkinID="HeadingLeft" runat="server" />
                </td>
                <td width="100%" valign="top" class="CenterBG">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td height="35" class="Top_menu_bg" align="left">
                                <table cellpadding="0" cellspacing="0" width="98%" border="0">
                                    <tr>
                                        <td class="HeadingMiddleBg" width="2%">
                                            <div style="padding: 4px 10px 0px 10px;">
                                                <asp:Label ID="LbLPageSubTitle" runat="server" Text=""></asp:Label>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                        </td>
                                        <td width="10%" style="display: none;">
                                            <asp:ImageButton ID="ImageButtonDeviceBack" Visible="false" ToolTip="Back" SkinID="ApplicationActivatorBackPage"
                                                runat="server" CausesValidation="False" ImageAlign="Middle" OnClick="ImageButtonDeviceBack_Click" />
                                        </td>
                                        <td width="25%" align="left" valign="middle">
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="LblRegResponse" runat="server" Font-Bold="true" Font-Size="Medium"
                                                Text=""></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="ImageHomeTrial" ToolTip="Home" runat="server" SkinID="InnerpageimgHome"
                                                CausesValidation="False" OnClick="ImageHomeTrial_Click" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="25">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                    <tr>
                                        <td width="15%">
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" width="70%">
                                            <div>
                                                <asp:Menu ID="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                                                    Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" runat="server">
                                                    <Items>
                                                        <asp:MenuItem Text="" Value="0" Selected="true" />
                                                        <asp:MenuItem Text="" Value="1" />
                                                        <asp:MenuItem Text="" Value="2" />
                                                        <asp:MenuItem Text="" Value="3" />
                                                    </Items>
                                                </asp:Menu>
                                                <div class="tabBody">
                                                    <asp:MultiView ID="multiTabs" ActiveViewIndex="0" runat="server">
                                                        <asp:View ID="view1" runat="server">
                                                            <table width="80%" class="table_border_org" align="center" cellpadding="0" cellspacing="0"
                                                                border="0">
                                                                <tr class="Top_menu_bg">
                                                                    <td class="f10b" height="35" colspan="2" align="left">
                                                                        &nbsp;
                                                                        <asp:Label ID="LabelSysInformation" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <table class="Menu_bg_insidetable3" cellpadding="3" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25">
                                                                                    <asp:Label ID="LabelSysId" class="f10b" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label Font-Bold="true" ID="LblSystemID" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelSerialKeyText" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbSerialKey" runat="server"  Width="250px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TbSerialKey"
                                                                                        runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator1" ID="ValidatorCalloutExtender1"
                                                                                        runat="server">
                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr class="Top_menu_bg">
                                                                    <td class="f10b" height="35" colspan="4" align="left">
                                                                        &nbsp;
                                                                        <asp:Label ID="LabelUserInformation" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" align="center">
                                                                        <table class="Menu_bg_insidetable3" cellpadding="3" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelName" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbName" runat="server" MaxLength="30" Width="250px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TbName"
                                                                                        runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator2" ID="ValidatorCalloutExtender2"
                                                                                        runat="server">
                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Image ID="Image2" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelEmailText" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbEmail" MaxLength="30" runat="server" Width="250px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TbEmail"
                                                                                        runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Enter valid Email"
                                                                                        ControlToValidate="TbEmail" Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator3" ID="ValidatorCalloutExtender3"
                                                                                        runat="server">
                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidator3" ID="ValidatorCalloutExtender7"
                                                                                        runat="server">
                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Image ID="Image3" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelPhoneMobile" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbPhone" MaxLength="30" runat="server" Width="250px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelCompany" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbCompany" MaxLength="30" runat="server" Width="250px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelAddress" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbAddress" MaxLength="100" runat="server" TextMode="MultiLine" Height="56px"
                                                                                        Width="253px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelCity" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbCity" MaxLength="30" runat="server" Width="253px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelState" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbState" MaxLength="30" runat="server" Width="253px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelCountry" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left" class="f10b">
                                                                                    <asp:DropDownList ID="DrpCountries" runat="server" Width="255px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="45%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelZipCode" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TbZipCode" MaxLength="30" runat="server" Width="253px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center" height="50">
                                                                        <asp:Button ID="BtnRegister" runat="server" CssClass="Login_Button" Text="" OnClick="BtnRegister_Click" />
                                                                        &nbsp;
                                                                        <asp:Button runat="server" ID="ButtonReset" Text="" CssClass="Login_Button" CausesValidation="false"
                                                                            OnClientClick="this.form.reset();return false;" /> &nbsp;
                                                                        <asp:Button ID="BtnCancel" runat="server" CssClass="Login_Button" CausesValidation="false"
                                                                            Text="" OnClick="BtnCancel_Click" Visible="false" />
                                                                             <asp:Button ID="ButtonTest" runat="server" CssClass="Login_Button" CausesValidation="false"
                                                                            Text="Test" OnClick="BtnTest_Click"  />

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:View>
                                                        <asp:View ID="view2" runat="server">
                                                            <table border="0" cellpadding="0" cellspacing="0" align="center" class="table_border_org"
                                                                width="80%">
                                                                <tr class="Top_menu_bg">
                                                                    <td class="f10b" height="35" colspan="2" align="left">
                                                                        &nbsp;
                                                                        <asp:Label ID="LabelRegInformation" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <table border="0" cellpadding="3" cellspacing="3" width="100%">
                                                                            <tr class="Grid_tr">
                                                                                <td align="right" height="31" width="50%">
                                                                                    <asp:Label ID="LabelInstallationDateText" runat="server" Text=""></asp:Label>&nbsp;:
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="left" width="50%">
                                                                                    <asp:Label ID="LabelInstallationDate" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="TableTrialdaysLeft" runat="server" height="31" class="Grid_tr">
                                                                                <td align="right">
                                                                                    <asp:Label ID="LabelTrialDaysText" runat="server" Text=""></asp:Label>&nbsp;: &nbsp;
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="LabelTrialDays" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="Grid_tr">
                                                                                <td align="right" valign="middle">
                                                                                    <asp:Label ID="LabelRegisteredLicencesText" runat="server" Text=""></asp:Label>&nbsp;:
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="LabelRegisteredLicences" runat="server" Text=""></asp:Label>
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
                                                                            <tr id="TableTrialLicenseText" runat="server" height="31px" class="Grid_tr">
                                                                                <td align="Right">
                                                                                    <asp:Label ID="LabelTrialLicencesText" runat="server" Text=""></asp:Label>&nbsp;:
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="LabelTrialLicences" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr height="31px" style="display: none">
                                                                                <td align="right">
                                                                                    <asp:Label ID="LabelNotesText" runat="server" class="f10b" Text=""></asp:Label>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="LabelNotes" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="Grid_tr">
                                                                                <td align="right" height="35" valign="middle">
                                                                                    <asp:Label ID="LabelRequestCode" runat="server" Text=""></asp:Label>&nbsp;: &nbsp;
                                                                                </td>
                                                                                <td align="left" valign="middle">
                                                                                    <asp:Label ID="TextBoxRequestCode" runat="server" BorderWidth="0" class="f10b" MaxLength="15"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="Grid_tr">
                                                                                <td align="Right" height="35" valign="middle">
                                                                                    <asp:Label ID="LabelRegistartionCode" runat="server" Text=""></asp:Label>&nbsp;:
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
                                                                                    <asp:Button runat="server" ID="Button1" Text="" CausesValidation="false" CssClass="Login_Button"
                                                                                        OnClientClick="this.form.reset();return false;" />
                                                                                    <asp:Button ID="ButtonCancel" runat="server" CausesValidation="false" CssClass="Cancel_button"
                                                                                        OnClick="ButtonCancel_Click" Visible='false' Text="" />
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
                                                        </asp:View>
                                                        <asp:View ID="view3" runat="server">
                                                            <table width="98%" align="center" border="0" cellpadding="3" cellspacing="3" class="">
                                                                <tr>
                                                                    <td width="98%" valign="top" class="">
                                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                            <tr style="display: none">
                                                                                <td height="" class="">
                                                                                    <table cellpadding="0" cellspacing="0" width="98%" border="0">
                                                                                        <tr>
                                                                                            <td width="10%">
                                                                                                <asp:Label ID="Label1" runat="server" SkinID="PageSubTitle" Text=""></asp:Label>
                                                                                            </td>
                                                                                            <td width="10%">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td width="98%" align="left" style="display: none">
                                                                                                <asp:ImageButton ID="ImgBtnAddLicences" PostBackUrl="~/LicenceController/ApplicationActivator.aspx"
                                                                                                    ToolTip="Register" SkinID="Register" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr height="2" style="display: none">
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
                                                                                                    width="98%">
                                                                                                    <tr>
                                                                                                        <td colspan="2" align="left" valign="top">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                                                                                <tr class="Top_menu_bg">
                                                                                                                    <td class="f10b" colspan="2" align="left" colspan="3">
                                                                                                                        &nbsp;
                                                                                                                        <asp:Label ID="LblRegistrationPageHeader" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td height="3">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr class="Grid_tr">
                                                                                                        <td>
                                                                                                            <table cellpadding="3" cellspacing="1" border="0" width="98%" class="Table_bg" align="center"
                                                                                                                style="background-color: Gray">
                                                                                                                <tr class="Table_HeaderBG">
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                    <td class="H_title">
                                                                                                                        <asp:Label ID="LabelLicensed" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="H_title">
                                                                                                                        <asp:Label ID="LabelRegistered" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="H_title">
                                                                                                                        <asp:Label ID="LabelAvailable" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td class="H_title">
                                                                                                                        <asp:Label ID="LabelTrial" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr class="GridRow">
                                                                                                                    <td align="left">
                                                                                                                        <asp:Label ID="LabelServer" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="LabelServerTotalLicense" runat="server" Text="-"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="LabelServerRegiesterdLicense" runat="server" Text="-"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="LabelServerAvailableLicense" runat="server" Text="-"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="LabelTrialServerLicense" runat="server" Text="1"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr class="GridRow">
                                                                                                                    <td align="left">
                                                                                                                        <asp:Label ID="LabelDevices" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="LabelClientTotalLicense" runat="server" Text="-"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="LabelClientRegiesterdLicense" runat="server" Text="-"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="LabelClientAvailableLicense" runat="server" Text="-"></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="LblTrialLicences" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr class="GridRow">
                                                                                                                    <td align="left">
                                                                                                                        <asp:Label ID="LabelLicence_Type" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td colspan="4" align="left">
                                                                                                                        <asp:Label ID="LabelLicenseType" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr class="GridRow">
                                                                                                                    <td align="left">
                                                                                                                        <asp:Label ID="LabelInstalled_On" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td colspan="4" align="left">
                                                                                                                        <asp:Label ID="LblInstallationDate" runat="server" Text="" SkinID="Fields"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr class="GridRow">
                                                                                                                    <td align="left">
                                                                                                                        <asp:Label ID="Label_Notes" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td colspan="4" align="left">
                                                                                                                        <asp:Label ID="LblNotes" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td height="1px">
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                            <table cellpadding="3" cellspacing="3" border="0" width="100%" style="background-color: White">
                                                                                                                <tr style="display: none">
                                                                                                                    <td class="Grid_tr" align="right" width="49%">
                                                                                                                        <asp:Label ID="LblClientCodeLabel" runat="server" Text="" SkinID="Fields"></asp:Label>&nbsp;:
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" width="30%" width="49%">
                                                                                                                        &nbsp;
                                                                                                                        <asp:Label ID="LblClientCode" runat="server" Text="" SkinID="Fields"></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr class="Grid_tr" align="right" width="20%" style="display: none">
                                                                                                                    <td height="35" align="right" width="49%">
                                                                                                                        <asp:Label ID="LblInstallationDateLabel" runat="server" Text="" SkinID="Fields"></asp:Label>&nbsp;:
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" width="49%">
                                                                                                                        &nbsp;
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr id="TableRowTrialDays" style="display: none" runat="server" class="Grid_tr" height="30"
                                                                                                                    align="right" width="20%">
                                                                                                                    <td align="right" height="30" width="49%">
                                                                                                                        <asp:Label ID="LblTrialDaysLabel" runat="server" Text="" SkinID="Fields"></asp:Label>&nbsp;:
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" colspan="2" width="49%">
                                                                                                                        &nbsp;
                                                                                                                        <asp:Label ID="LblTrialDays" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr id="tdTrailLicense" style="display: none" class="Grid_tr" runat="server" align="right"
                                                                                                                    width="20%">
                                                                                                                    <td height="35" align="right" width="49%">
                                                                                                                        <asp:Label ID="LblTrialLicencesLabel" runat="server" Text="" SkinID="Fields"></asp:Label>&nbsp;:
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" width="49%">
                                                                                                                        &nbsp;
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr class="Grid_tr" height="30" align="right" width="20%" style="display: none">
                                                                                                                    <td height="35" align="right" width="49%">
                                                                                                                        <asp:Label ID="LblSerialKeyLabel" runat="server" Text="" SkinID="Fields"></asp:Label>&nbsp;:
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" width="49%">
                                                                                                                        &nbsp;
                                                                                                                        <asp:Label ID="LblSerialKeys" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr class="Grid_tr" align="right" width="20%" style="display: none">
                                                                                                                    <td height="35" align="right" width="49%">
                                                                                                                        <asp:Label ID="LblRegisteredLicencesLabel" runat="server" Text="" SkinID="Fields"></asp:Label>&nbsp;:
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" width="49%">
                                                                                                                        &nbsp;
                                                                                                                        <asp:Label ID="LblRegisteredLicences" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr id="Tablerowpass" runat="server" class="Grid_tr" align="right" width="20%" style="display: none">
                                                                                                                    <td height="35" align="right" width="49%">
                                                                                                                        <asp:Label ID="LblNotesLabel" runat="server" Text="" SkinID="Fields"></asp:Label>
                                                                                                                        :
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" width="49%">
                                                                                                                        &nbsp;
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
                                                                            <tr>
                                                                                <td height="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:View>
                                                        <asp:View ID="view4" runat="server">
                                                            <table width="80%" align="center" border="0" cellpadding="3" cellspacing="3" class="">
                                                                <tr>
                                                                    <td width="80%" valign="top" class="">
                                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                            <tr style="display: none">
                                                                                <td height="" class="">
                                                                                    <table cellpadding="0" cellspacing="0" width="80%" border="0">
                                                                                        <tr>
                                                                                            <td width="10%">
                                                                                                <asp:Label ID="Label2" runat="server" SkinID="PageSubTitle" Text=""></asp:Label>
                                                                                            </td>
                                                                                            <td width="10%">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td width="80%" align="left" style="display: none">
                                                                                                <asp:ImageButton ID="ImageButton1" PostBackUrl="~/LicenceController/ApplicationActivator.aspx"
                                                                                                    ToolTip="Register" SkinID="Register" runat="server" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr height="2" style="display: none">
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
                                                                                                    width="98%">
                                                                                                    <tr>
                                                                                                        <td colspan="2" align="left" valign="top">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                                                                                <tr class="Top_menu_bg">
                                                                                                                    <td class="f10b" colspan="2" align="left" colspan="3">
                                                                                                                        &nbsp;
                                                                                                                        <asp:Label ID="Label3" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                <tr>
                                                                                                                    <td style="height: 10px">
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td valign="top" align="center">
                                                                                                                        <table align="center" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td height="35" align="right" width="40%">
                                                                                                                                                <asp:Label ID="LabelFromAddress" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                                                                            </td>
                                                                                                                                            <td align="left">
                                                                                                                                                <asp:DropDownList ID="DropDownListProxyEnabled" CssClass="Dropdown_CSS" runat="server">
                                                                                                                                                    <asp:ListItem Text="" Value="Yes"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="" Value="No"></asp:ListItem>
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td height="35" align="right">
                                                                                                                                                <asp:Label ID="LabelCCAddress" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                                                                            </td>
                                                                                                                                            <td align="left">
                                                                                                                                                <asp:TextBox ID="TextBoxServerUrl" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                            </td>
                                                                                                                                            <td align="left">
                                                                                                                                                <asp:Label ID="LabelServerURLExample" runat="server" Text="Server Url e.g: http://127.0.0.1"
                                                                                                                                                    class="f10b"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td height="35" align="right">
                                                                                                                                                <asp:Label ID="LabelBCCAddress" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                                                                            </td>
                                                                                                                                            <td align="left">
                                                                                                                                                <asp:TextBox ID="TextBoxDomain" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td height="35" align="right">
                                                                                                                                                <asp:Label ID="LabelServerIpAddress" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                                                                            </td>
                                                                                                                                            <td align="left">
                                                                                                                                                <asp:TextBox ID="TextBoxUserId" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td height="35" align="right">
                                                                                                                                                <asp:Label ID="LabelPortNumber" class="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                                                                            </td>
                                                                                                                                            <td align="left">
                                                                                                                                                <asp:TextBox ID="TextBoxPassword" TextMode="Password" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td colspan="4" height="35">
                                                                                                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                                                                                                    <tr>
                                                                                                                                                        <td width="240">
                                                                                                                                                        </td>
                                                                                                                                                        <td>
                                                                                                                                                            <asp:Button ID="ButtonUpdate" CssClass="Login_Button" runat="server" Text="" TabIndex="12"
                                                                                                                                                                OnClick="ButtonUpdate_Click" />
                                                                                                                                                            <asp:Button runat="server" ID="Button2" Text="" CausesValidation="false" CssClass="Login_Button"
                                                                                                                                                                OnClientClick="this.form.reset();return false;" />
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </table>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorUrlRequired" ControlToValidate="TextBoxServerUrl"
                                                                                                                                        runat="server" ErrorMessage="Server URL cannot be empty." SetFocusOnError="true"
                                                                                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorUrlRequired"
                                                                                                                                        ID="ValidatorCalloutExtender4" runat="server">
                                                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorUrlRequired" runat="server"
                                                                                                                                        ErrorMessage="" ControlToValidate="TextBoxServerUrl" Display="None" SetFocusOnError="True"
                                                                                                                                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></asp:RegularExpressionValidator>
                                                                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorUrlRequired"
                                                                                                                                        ID="ValidatorCalloutExtender5" runat="server">
                                                                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDomain" ControlToValidate="TextBoxDomain"
                                                                                                                                        runat="server" ErrorMessage="Domain Field cannot be empty." SetFocusOnError="true"
                                                                                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDomain" ID="ValidatorCalloutExtender6"
                                                                                                                                        runat="server">
                                                                                                                                    </cc1:ValidatorCalloutExtender>--%>
                                                                                                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidatorDomain" runat="server" ErrorMessage=""
                                                                                                                                        ControlToValidate="TextBoxDomain" Display="None" SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></asp:RegularExpressionValidator>
                                                                                                                                        <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorDomain" ID="ValidatorCalloutExtender4"
                                                                                                                                            runat="server">
                                                                                                                                        </cc1:ValidatorCalloutExtender>--%>
                                                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" ControlToValidate="TextBoxUserId"
                                                                                                                                        runat="server" ErrorMessage="User Name cannot be empty." SetFocusOnError="true"
                                                                                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorUsername" ID="ValidatorCalloutExtender8"
                                                                                                                                        runat="server">
                                                                                                                                    </cc1:ValidatorCalloutExtender>--%>
                                                                                                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage=""
                                                                                                                                        ControlToValidate="TextBoxUserId" Display="None" SetFocusOnError="True" ValidationExpression="/((?:https?\:\/\/|www\.)(?:[-a-z0-9]+\.)*[-a-z0-9]+.*)/i"></asp:RegularExpressionValidator>
                                                                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorUrlRequired" ID="ValidatorCalloutExtender6"
                                                                                                                                        runat="server">
                                                                                                                                    </cc1:ValidatorCalloutExtender>--%>
                                                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" ControlToValidate="TextBoxPassword"
                                                                                                                                        runat="server" ErrorMessage="Password cannot be empty." SetFocusOnError="true"
                                                                                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorPassword" ID="ValidatorCalloutExtender9"
                                                                                                                                        runat="server">
                                                                                                                                    </cc1:ValidatorCalloutExtender>--%>
                                                                                                                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage=""
                                                                                                                                    ControlToValidate="TextBoxPassword" Display="None" SetFocusOnError="True" ValidationExpression="/((?:https?\:\/\/|www\.)(?:[-a-z0-9]+\.)*[-a-z0-9]+.*)/i"></asp:RegularExpressionValidator>
                                                                                                                                <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorUrlRequired" ID="ValidatorCalloutExtender8"
                                                                                                                                    runat="server">
                                                                                                                                </cc1:ValidatorCalloutExtender>--%>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td height="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:View>
                                                    </asp:MultiView>
                                                </div>
                                            </div>
                                        </td>
                                        <td width="15%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlRegistrationResponse" Visible="false" runat="server">
        <div style="margin-left: 10%">
            <table width="80%" border="0" cellpadding="0" cellspacing="1" class="TableGridColor">
                <tr class="Grid_tr" align="left">
                    <td align="left">
                        <asp:Table ID="TableRegistrationResult" HorizontalAlign="Left" CellSpacing="1" CellPadding="4"
                            runat="server" CssClass="Table_bg" Width="100%">
                            <asp:TableRow class="Top_menu_bg" Height="35">
                                <asp:TableCell CssClass="f11b" HorizontalAlign="Left">
                                    <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="ApplicationActivatorBackPage"
                                        CausesValidation="False" ImageAlign="Middle" OnClick="ImageButtonBack_Click" />
                                </asp:TableCell>
                                <asp:TableCell CssClass="f11b" HorizontalAlign="Left">Registration Details</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow BackColor="white">
                                <asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="LblRegSuccessResponse" ForeColor="Green" Font-Bold="true" runat="server"
                                        Text=""></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow BackColor="white">
                                <asp:TableCell>Serial Number</asp:TableCell>
                                <asp:TableCell CssClass="f11b">
                                    <asp:Label ID="LabelSerialKey" runat="server" Text=""></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <%-- <asp:TableRow BackColor="white">
                            <asp:TableCell>Client Code</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelClientCode" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow BackColor="white">
                            <asp:TableCell>Activation Code</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelActivationCode" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>--%>
                            <asp:TableRow BackColor="white">
                                <asp:TableCell>Name</asp:TableCell>
                                <asp:TableCell CssClass="f11b">
                                    <asp:Label ID="LabelFirstName" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow BackColor="white">
                                <asp:TableCell>Email</asp:TableCell>
                                <asp:TableCell CssClass="f11b">
                                    <asp:Label ID="LabelEmail" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <%-- <asp:TableRow BackColor="white">
                            <asp:TableCell Visible="true">Phone</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelPhone" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>--%>
                        </asp:Table>
                        <asp:Table EnableViewState="false" ID="TableRegDetails" CellSpacing="0" CellPadding="0"
                            Width="100%" BorderWidth="0" runat="server" SkinID="Grid" CssClass="Table_bg">
                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                <asp:TableHeaderCell CssClass="Grid_topbg1" Wrap="false">Client Code
                                </asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="Grid_topbg1" Wrap="false">Activation Code
                                </asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="Grid_topbg1" Wrap="false">Activation Status
                                </asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
