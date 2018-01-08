<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="SMTPSettings.aspx.cs" Inherits="AccountingPlusWeb.Administration.SMTPSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
        function AllowNumeric() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else
                return false;
        }


        function HidenSetting() {
            document.getElementById("ShowConfig").style.display = "none";
            document.getElementById("HidenSettingConfig").innerHTML = "<a href=\"#\" onclick=\"return ShowSetting();\">Show Setting</a>";
        }
        function ShowSetting() {
            document.getElementById("ShowConfig").style.display = "inline";
            document.getElementById("HidenSettingConfig").innerHTML = "<a href=\"#\" onclick=\"return HidenSetting();\">Hide Setting </a>";
        }
    </script>
    
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image7" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="left">
                            <table cellpadding="0" cellspacing="0" width="60%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingSMTPSettings" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="5%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonSave" SkinID="SettingsImageButtonSave" runat="server"
                                            CausesValidation="true" ImageAlign="Middle" ToolTip="Click here to save/update"
                                            OnClick="ButtonUpdate_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                    </td>
                                    <td width="7%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" SkinID="SettingsImageButtonReset"
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="Click here to reset" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table align="center" cellpadding="0" cellspacing="0" border="0" class="table_border_org"
                                width="50%">
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                            <tr class="Top_menu_bg">
                                                <td width="50%" align="left" valign="middle">
                                                    &nbsp;<asp:Label ID="LabelSMTPSettings" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                </td>
                                                <td align="right" width="30%" valign="middle">
                                                    <asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                </td>
                                                <td align="left" width="20%">
                                                    <asp:Label ID="LabelRequiredField" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                    &nbsp;
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
                                    <td align="center">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td height="35" align="right" width="40%">
                                                    <asp:Label CssClass="f10b" ID="LabelFromAddress" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxFromAddress" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle;
                                                        padding-left: 5px;" ID="ImageDepartment" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelCCAddress" CssClass="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxCCAddress" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelBCCAddress" CssClass="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxBCCAddress" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelServerIpAddress" CssClass="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxServerIpAddress" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle;
                                                        padding-left: 5px;" ID="Image5" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelPortNumber" CssClass="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxPortNumber" MaxLength="4" CssClass="FormTextBox_bg" onkeypress="javascript:return AllowNumeric()"
                                                        runat="server"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle;
                                                        padding-left: 5px;" ID="Image4" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelDomainName" CssClass="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxDomainName" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle;
                                                        padding-left: 5px;" ID="Image6" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelUserName" CssClass="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxUserName" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle;
                                                        padding-left: 5px;" ID="Image2" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelPassword" CssClass="f10b" Text="" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxPassword" CssClass="FormTextBox_bg" TextMode="Password" runat="server"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle;
                                                        padding-left: 5px;" ID="Image3" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="35" align="right">
                                                    <asp:Label ID="LabelRequireSSL" CssClass="f10b" Text="Require SSL" runat="server"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="CheckBoxRequireSSL" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" height="35" align="center">
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="90">
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="ButtonUpdate" CssClass="Login_Button" runat="server" Text="" TabIndex="12"
                                                                    OnClick="ButtonUpdate_Click" />
                                                                <asp:Button runat="server" ID="ButtonReset" Text="" CausesValidation="false" CssClass="Login_Button"
                                                                    OnClientClick="this.form.reset();return false;" />
                                                                <asp:Button ID="ButtonTest" runat="server" Text="Test" OnClick="ButtonTest_Click" />
                                                              
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="2">
                                        <asp:HiddenField ID="HiddenFieldValue" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFromAddress" ControlToValidate="TextBoxFromAddress"
                                            runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorFromAddress"
                                            ID="ValidatorCalloutExtender1" runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorSMTPHost" ControlToValidate="TextBoxServerIpAddress"
                                            runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorSMTPHost" ID="ValidatorCalloutExtender2"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorSMTPPort" ControlToValidate="TextBoxPortNumber"
                                            runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorSMTPPort" ID="ValidatorCalloutExtender3"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDomain" ControlToValidate="TextBoxDomainName"
                                            runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDomain" ID="ValidatorCalloutExtender4"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" ControlToValidate="TextBoxUserName"
                                            runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorUsername" ID="ValidatorCalloutExtender5"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" ControlToValidate="TextBoxPassword"
                                            runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorPassword" ID="ValidatorCalloutExtender6"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Enter valid Email"
                                            ControlToValidate="TextBoxFromAddress" Display="None" SetFocusOnError="True"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidator3" ID="ValidatorCalloutExtender7"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter valid Email"
                                            ControlToValidate="TextBoxCCAddress" Display="None" SetFocusOnError="True" ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*,\s*|\s*$))*"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidator2" ID="ValidatorCalloutExtender9"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Enter valid Email"
                                            ControlToValidate="TextBoxBCCAddress" Display="None" SetFocusOnError="True" ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*,\s*|\s*$))*"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidator4" ID="ValidatorCalloutExtender10"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid IP Address"
                                            ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"
                                            ControlToValidate="TextBoxServerIpAddress" Display="None"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RegularExpressionValidator1"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                 
               
                <asp:Panel ID="panelDialog" runat="server" Visible="false">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td height="35" align="right" width="40%">
                                <asp:Label CssClass="f10b" ID="Label1" Text="From" runat="server"></asp:Label>&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxfrom" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="right">
                                <asp:Label ID="Label2" CssClass="f10b" Text="To" runat="server"></asp:Label>&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxTo" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="right">
                                <asp:Label ID="Label3" CssClass="f10b" Text="Subject" runat="server"></asp:Label>&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxSub" Text="Test Message from SMTP (ACP)" CssClass="FormTextBox_bg"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="right">
                                <asp:Label ID="Label4" CssClass="f10b" Text="Body" runat="server"></asp:Label>&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="TextBoxbody" TextMode="MultiLine" Text="This is a test message from ACP"
                                    CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" height="35" align="center">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td width="90">
                                        </td>
                                        <td>
                                            <asp:Button ID="ButtonSend" CssClass="Login_Button" runat="server" Text="Send" TabIndex="12"
                                                OnClick="ButtonSend_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
               
            </td>
        </tr>
    </table>
</asp:Content>
