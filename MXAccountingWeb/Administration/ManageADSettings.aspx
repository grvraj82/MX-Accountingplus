<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ManageADSettings.aspx.cs" Inherits="PrintRoverWeb.Administration.ManageADSettings"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">

        fnShowCellSettings();
        Meuselected("Settings");
        function AllowNumeric() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else
                return false;
        }
        function myKeyPressHandler() {
            if (event.keyCode == 13) {
                document.getElementById('ctl00_PageContent_ButtonUpdate').focus();
            }
        }

        document.onkeypress = myKeyPressHandler;

    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <%--<div style="height: 10px;">&nbsp;</div>--%>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg " align="left">
                            <table cellpadding="0" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" style="width: 10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingActiveDirectorySettings" runat="server" Text="Active Directory"></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                        <td id="Tablecellback" runat="server" width="5%" visible="false" align="left" valign="middle"
                                            style="display: none">
                                            <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="SettingsImageButtonBack"
                                                CausesValidation="False" ToolTip="" OnClick="ImageButtonBack_Click" />
                                        </td>
                                        <td id="Tablecellimage" runat="server" visible="false" width="1%" class="Menu_split"
                                            style="display: none">
                                        </td>
                                        <td width="7%" align="left" valign="middle" style="display: none">
                                            <asp:ImageButton ID="ImageButtonSave" SkinID="SettingsImageButtonSave" runat="server"
                                                CausesValidation="true" ImageAlign="Middle" ToolTip="" OnClick="ImageButtonSave_Click" />
                                        </td>
                                        <td width="1%" class="Menu_split" style="display: none">
                                        </td>
                                        <td width="7%" align="left" valign="middle" style="display: none">
                                            <asp:ImageButton ID="ImageButtonReset" SkinID="SettingsImageButtonReset" runat="server"
                                                CausesValidation="False" ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                        </td>
                                        <td width="85%" align="left" valign="middle">
                                            &nbsp;
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
                        <td align="center" valign="top">
                            <table width="70%" class="table_border_org" cellpadding="0" cellspacing="0" border="0">
                                <tr class="Top_menu_bg">
                                    <td class="f10b" height="35" colspan="2" align="left">
                                        &nbsp;
                                        <asp:Label ID="Label1" runat="server" SkinID="LabelLogon" Text="Active Directory / Domain"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" width="100%" cellspacing="0" border="0">
                                            <tr>
                                                <td align="right" width="45%" height="35">
                                                    <asp:Label ID="LabelDomainController" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left" width="55%" height="35">
                                                    <asp:TextBox ID="TextBoxDomainController" runat="server" MaxLength="50" CssClass="FormTextBox_bg"
                                                        TabIndex="1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="35">
                                                    <asp:Label ID="LabelDomainName" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxDomainName" runat="server" MaxLength="50" CssClass="FormTextBox_bg"
                                                        TabIndex="2"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                        ID="ImageDomainName" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="35">
                                                    <asp:Label ID="LabelUserName" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxUserName" runat="server" MaxLength="30" CssClass="FormTextBox_bg"
                                                        TabIndex="3"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                        ID="ImageUserName" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="35">
                                                    <asp:Label ID="LabelPassword" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server" MaxLength="50"
                                                        CssClass="FormTextBox_bg" TabIndex="4"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                        ID="ImagePassword" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="35">
                                                    <asp:Label ID="LabelPort" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxPort" runat="server" MaxLength="50" CssClass="FormTextBox_bg"
                                                        TabIndex="5" onkeypress="javascript:return AllowNumeric()"></asp:TextBox>
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                                        ID="ImagePort" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="35">
                                                    <asp:Label ID="LabelFullName" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="DropDownListFullName" CssClass="Dropdown_CSS" runat="server"
                                                        TabIndex="6">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="2" align="center" width="100%">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="60%" align="right">
                                                                <asp:Button ID="ButtonUpdate" runat="server" CssClass="Login_Button" OnClick="ButtonUpdate_Click"
                                                                    Text="" Visible="True" TabIndex="7" />
                                                            </td>
                                                            <td align="right" width="13%">
                                                                <asp:Button runat="server" ID="ButtonReset" Text="Reset" CssClass="Login_Button"
                                                                    OnClientClick="this.form.reset();return false;" />
                                                            </td>
                                                            <td width="2px"></td>
                                                            <td align="left">
                                                                <asp:Button ID="ButtonCancel" runat="server" CssClass="Cancel_button" Text="" Visible="True"
                                                                    OnClick="ButtonCancel_Click" CausesValidation="false" TabIndex="9" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--   
                                                    <asp:Button ID="ButtonTest" runat="server" CssClass="Login_Button" Text="Test" Visible="True"
                                                        OnClick="ButtonTest_Click" CausesValidation="false" TabIndex="8" />
                                                    <asp:Button runat="server" ID="ButtonReset" Text="Reset" CssClass="Login_Button"
                                                        OnClientClick="this.form.reset();return false;" />
                                                    <asp:Button ID="ButtonCancel" runat="server" CssClass="Cancel_button" Text="" Visible="True"
                                                        OnClick="ButtonCancel_Click" CausesValidation="false" TabIndex="9" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="5">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDomainName" ControlToValidate="TextBoxDomainName"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDomainName"
                                ID="ValidatorCalloutExtenderDomainName" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" ControlToValidate="TextBoxUserName"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorUserName" ID="ValidatorCalloutExtenderUserName"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" ControlToValidate="TextBoxPassword"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorPassword" ID="ValidatorCalloutExtenderPassword"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPort" ControlToValidate="TextBoxPort"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorPort" ID="ValidatorCalloutExtenderPort"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
