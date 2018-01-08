<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    CodeBehind="AddUpdateDevices.aspx.cs" Inherits="PrintRoverWeb.Administration.AddUpdateDevices" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        fnShowCellMFPs();
        Meuselected("Device");
        function Required() {

            if (document.getElementById('ctl00_PageContent_TextBoxIP').value == "") {
                jNotify(C_ENTER_VALID_IP)

                return false;
            }
        }

        function AllowNumeric() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else
                return false;
        }
        function myKeyPressHandler() {
            if (event.keyCode == 13) {
                var hiddenvalue = document.getElementById('ctl00_PageContent_HiddenFieldDevicesIP').value;
                if (hiddenvalue == "" || hiddenvalue == null) {
                    var deviceip = document.getElementById('ctl00_PageContent_TextBoxIP').value;
                    if (deviceip == "" || deviceip == null) {
                        document.getElementById('ctl00_PageContent_TextBoxIP').focus();
                        return false;
                    }
                    else {
                        document.getElementById('ctl00_PageContent_ImageButtonSave').focus();
                    }
                }
                else {
                    document.getElementById('ctl00_PageContent_ImageButtonSave').focus();
                }
            }
        }

        document.onkeypress = myKeyPressHandler;
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td valign="top">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg"
                    height="33">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadDeviceManagement" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="33" align="left" valign="middle" width="40%" style="display: none">
                            <table cellpadding="0" cellspacing="0" width="32%" border="0">
                                <tr>
                                    <td width="10%">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="DeviceAddImageButtonBack"
                                            CausesValidation="False" ImageAlign="Middle" OnClick="ImageButtonBack_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split">
                                    </td>
                                    <td width="10%">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" ImageAlign="Middle" SkinID="DeviceAddImageButtonSave"
                                            OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split">
                                    </td>
                                    <td width="10%">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="DeviceAddImageButtonReset"
                                            ImageAlign="Middle" OnClick="ImageButtonReset_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" valign="middle" width="60%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="2" class="CenterBG">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="CenterBG" align="center">
                <table cellpadding="0" cellspacing="0" border="0" class="table_border_org" width="50%">
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" height="33">
                                <tr class="Top_menu_bg">
                                    <td width="50%" align="left" valign="middle">
                                        &nbsp;
                                        <asp:Label ID="LabelUserHeading" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="LabelDeviceHeading" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td align="right" width="30%" valign="middle">
                                        <asp:Image ID="Image3" runat="server" SkinID="LogonImgRequired" Style="padding-right: 3px;" />
                                    </td>
                                    <td align="left" width="20%">
                                        &nbsp;<asp:Label ID="LabelRequiredField" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr id="TableRowDeviceName" runat="server">
                                    <td class="f10b" height="35" align="right" width="30%">
                                        <asp:Label ID="LabelDeviceName" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left" width="70%">
                                        <asp:TextBox ID="TextBoxDeviceName" MaxLength="29" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="TableRowIP" runat="server">
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelIP" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:Label ID="LabelTextIP" runat="server" Text="" Visible="false" CssClass="FormTextBox_bg"></asp:Label>
                                        <asp:TextBox ID="TextBoxIP" MaxLength="15" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                        <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                            ID="ImageIpRequired" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=""
                                            Display="None" ControlToValidate="TextBoxIP"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage=""
                                            ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                            ControlToValidate="TextBoxIP" Display="None"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RegularExpressionValidator1"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr id="TablerowHostName" runat="server" visible="false">
                                    <td class="f10b" height="35" width="35%" align="right">
                                        <asp:Label ID="LabelHostName" runat="server" Text="HostName"></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left" width="64%">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelHostNameReadonly" runat="server" Text="" CssClass="FormTextBox_bg"></asp:Label>
                                                </td>
                                                <td width="40px">
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="ButtonUpdateHostName" runat="server" Text="Update" CssClass="Login_Button"
                                                        OnClick="ButtonUpdateHostName_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="TableRowDeviceID" runat="server">
                                    <td class="f10b" height="35" width="35%" align="right">
                                        <asp:Label ID="LabelDeviceID" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left" width="64%">
                                        <asp:Label ID="LabelTextDeviceID" runat="server" Visible="false" Text="" CssClass="FormTextBox_bg"></asp:Label>
                                        <asp:TextBox ID="TextBoxDeviceID" MaxLength="30" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="TableRowSerialNumber" runat="server">
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelSerialNumber" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:Label ID="LabelTextSerialNumber" runat="server" Visible="false" Text="" CssClass="FormTextBox_bg"></asp:Label>
                                        <asp:TextBox ID="TextBoxSerialNumber" MaxLength="15" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="TableRowLocation" runat="server">
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelLocation" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:TextBox ID="TextBoxLocation" MaxLength="80" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelLogOnMode" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownLogOnMode" CssClass="Dropdown_CSS" runat="server" OnSelectedIndexChanged="DropDownLogOnMode_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" name="TrCardReaderType" id="TableRowcardreaderType" visible="false">
                                    <td class="f10b" style="white-space: nowrap" height="35" align="right">
                                        <asp:Label ID="LabelCardReaderType" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownListCardReaderType" CssClass="Dropdown_CSS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" name="TrCardType" id="TableRowCardType" visible="false">
                                    <td class="f10b" style="white-space: nowrap" height="35" align="right">
                                        <asp:Label ID="LabelCardType" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownCardType" CssClass="Dropdown_CSS" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="DropDownCardType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" name="TrMAnualAuthType" id="TableRowManualAuthType" visible="true">
                                    <td class="f10b" style="white-space: nowrap" height="35" align="right">
                                        <asp:Label ID="LabelManualAuthType" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownManualAuthType" CssClass="Dropdown_CSS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelAuthSource" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownAuthSource" CssClass="Dropdown_CSS" runat="server"
                                            OnSelectedIndexChanged="DropDownAuthSource_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelUseSSO" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownUseSSO" CssClass="Dropdown_CSS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelPrintJobAccess" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownListPrintJobAccess" CssClass="Dropdown_CSS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="TableLockDomain" runat="server" style="display: none">
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelLockDomainField" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:CheckBox ID="CheckBoxLockDomainField" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelEnableDevice" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:CheckBox ID="CheckBoxEnableDevice" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelOsaICCard" runat="server" Text="OSA IC Card"></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownListOSACardIC" CssClass="Dropdown_CSS" runat="server">
                                            <asp:ListItem Text="Enable" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="Disable" Value="False" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="Label1" runat="server" Text="Guest Account"></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownListGuest" CssClass="Dropdown_CSS" runat="server">
                                            <asp:ListItem Text="Enable" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="Disable" Value="False" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="TableAllowNetworkPassword" align="right" visible="false" style="display: none">
                                    <td class="f10b" height="35">
                                        <asp:Label ID="LabelAllowNetworkPassword" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:CheckBox ID="CheckBoxAllowNetworkPassword" runat="server" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="f10b" height="35">
                                        <asp:Label ID="LabelURL" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:TextBox ID="TextBoxURL" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="Adduser" style="display: none">
                                    <td class="f10b" height="35" colspan="2">
                                        &nbsp;
                                        <asp:Label ID="LabelPrintReleaseAPI" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelPRProtocol" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left" height="45">
                                        <asp:DropDownList ID="DropDownListPRProtocol" CssClass="Dropdown_CSS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <asp:Panel ID="PanelFTPDetails" runat="server">
                                    <tr class="Top_menu_bg">
                                        <td class="f10b" height="35" colspan="2">
                                            &nbsp;
                                            <asp:Label ID="LabelFTPDetail" runat="server" Text="Print Release Settings"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="Label3" runat="server" Text="Print Type"></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:DropDownList ID="DropDownListPrintType" CssClass="Dropdown_CSS" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="DropDownListPrintType_SelectedIndexChanged">
                                                <asp:ListItem Value="dir" Text="Direct"></asp:ListItem>
                                                <asp:ListItem Value="ftp" Text="FTP" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <asp:Panel runat="server" ID="panelPrintProtocol" Visible="false">
                                        <tr>
                                            <td class="f10b" height="35" align="right">
                                                <asp:Label ID="LabelProtocol" runat="server" Text=""></asp:Label>
                                                <span style="padding-right: 5px;"></span>
                                            </td>
                                            <td style="white-space: nowrap" align="left">
                                                <asp:DropDownList ID="DropDownListProtocol" CssClass="Dropdown_CSS" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr id="TableFTPAd" runat="server">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelFtpAddress" runat="server" Text="Ip Address"></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxFtpAddress" MaxLength="80" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelFtpPort" runat="server" Text="Port"></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxPort" MaxLength="5" CssClass="FormTextBox_bg" onkeypress="javascript:return AllowNumeric()"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr2" runat="server" style="display: none;">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelUserID" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxUserID" MaxLength="27" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr3" runat="server" style="display: none;">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelUserPass" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxPasword" MaxLength="47" CssClass="FormTextBox_bg" TextMode="Password"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr class="Top_menu_bg">
                                    <td class="f10b" height="35" colspan="2">
                                        &nbsp;
                                        <asp:Label ID="LabelThemes" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server">
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelThemeName" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownListApplicationTheme" CssClass="Dropdown_CSS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="TableRowDeviceLanguage" runat="server">
                                    <td class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelMfpDisplayLanguage" runat="server" Text=""></asp:Label>
                                        <span style="padding-right: 5px;"></span>
                                    </td>
                                    <td style="white-space: nowrap" align="left">
                                        <asp:DropDownList ID="DropDownDeviceLanguage" CssClass="Dropdown_CSS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <asp:Panel ID="PanelEmail" runat="server" Visible="false">
                                    <tr class="Top_menu_bg">
                                        <td class="f10b" height="35" colspan="2">
                                            &nbsp;
                                            <asp:Label ID="LabelEmailSettings" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr8" runat="server">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelEmailAddress" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxEmail" MaxLength="47" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr5" runat="server">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelHost" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxEmailHost" MaxLength="47" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr6" runat="server">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelPort" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxEmailPort" MaxLength="5" CssClass="FormTextBox_bg" onkeypress="javascript:return AllowNumeric()"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr7" runat="server">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelUserName" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxEmailUserName" MaxLength="47" CssClass="FormTextBox_bg" runat="server"></asp:TextBox><br />
                                            <span style="font-size: 8pt">e.g. for Exchange:Domain\username@domain.com</span>
                                        </td>
                                    </tr>
                                    <tr id="Tr9" runat="server">
                                        <td class="f10b" height="35" align="right">
                                            <asp:Label ID="LabelPassword" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxEmailPassword" MaxLength="47" CssClass="FormTextBox_bg" TextMode="Password"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr10" align="right">
                                        <td class="f10b" height="35">
                                            <asp:Label ID="LabelSSL" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:CheckBox ID="CheckBoxEmailSSL" runat="server" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr11" align="right">
                                        <td class="f10b" height="35">
                                            <asp:Label ID="LabelEmailDirectPrint" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:CheckBox ID="CheckBoxEmailDirectPrint" runat="server" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr12" align="right">
                                        <td class="f10b" height="35">
                                            <asp:Label ID="LabelEmailMessageCount" runat="server" Text=""></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxEMC" MaxLength="12" onkeypress="javascript:return AllowNumeric()"
                                                CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr13" align="right" style="display: none">
                                        <td class="f10b" height="35">
                                            <asp:Label ID="Label2" runat="server" Text="Send Creditials to Admin eMail"></asp:Label>
                                            <span style="padding-right: 5px;"></span>
                                        </td>
                                        <td style="white-space: nowrap" align="left">
                                            <asp:TextBox ID="TextBoxEmailIdAdmin" MaxLength="80" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>&nbsp;
                                            <asp:CheckBox ID="CheckBoxapplySendEmailtoAll" runat="server" Text="&nbsp;Apply for all devices" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td align="center" height="35" border="0" colspan="3">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td height="10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="ButtonUpdateAllEmailConfigurations" CssClass="Login_Button" runat="server"
                                                        Text="Update all device email settings" OnClick="ButtonUpdateAllEmailConfigurations_Click" />
                                                    <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="" OnClick="ButtonSave_Click" />
                                                    <asp:Button ID="ButtonCancel" CssClass="Cancel_button" CausesValidation="false" runat="server"
                                                        Text="" OnClick="ButtonCancel_Click" />
                                                    <asp:Button runat="server" ID="ButtonReset" Text="Reset" CssClass="Login_Button"
                                                        OnClientClick="this.form.reset();return false;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="10">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenFieldDevicesIP" runat="server" />
            </td>
        </tr>
        <tr height="2px">
            <td class="CenterBG">
                &nbsp;
            </td>
        </tr>
        <tr class="Mfp_tr">
            <td colspan="2">
                <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
