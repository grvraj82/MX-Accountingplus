<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/InnerPage.master"
    Inherits="WebMyProfile" CodeBehind="MyProfile.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PC" ContentPlaceHolderID="PageContent" runat="Server">
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


    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image7" SkinID="HeadingLeft" runat="server" />
            </td>
            <td align="left" class="CenterBG">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                    <tr>
                        <td align="left" valign="middle">
                            <table cellpadding="0" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                              <asp:Label ID="LabelHeadingMyProfile" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td width="7%" align="left" valign="middle">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" CausesValidation="true" ImageAlign="Middle"
                                            SkinID="SettingsImageButtonSave" ToolTip="" OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split">
                                    </td>
                                    <%--<td width="1%">
                                        <asp:Image ID="Image5" runat="server" SkinID="ManageusersimgSplit" />
                                    </td>--%>
                                    <td width="7%" align="left" valign="middle">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="SettingsImageButtonReset"
                                            ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelMyProfile" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                                    </td>
                                    <%--<td width="85%" align="left" valign="middle">
                                        &nbsp; 
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="middle" style="padding-top: 3px;" width="75%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td height="10" class="CenterBG">
            </td>
        </tr>
        <tr class="Grid_tr">
            <td>
            </td>
            <td class="CenterBG">
                <table align="center" width="50%">
                </table>
                <table cellpadding="0" cellspacing="0" width="100%" height="500">
                    <tr valign="top">
                        <td height="10">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table align="center" cellpadding="0" cellspacing="0" border="0" class="table_border_org"
                                width="50%">
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" height="33" class="Inside_tableborder">
                                            <tr class="Top_menu_bg">
                                                <td width="50%" align="left" valign="middle">
                                                    &nbsp;
                                                    <asp:Label ID="LabelEditUserDetails" runat="server"></asp:Label>
                                                </td>
                                                <td align="right" width="50%">
                                                    <asp:Image ID="Image3" runat="server" SkinID="LogonImgRequired" />
                                                    <asp:Label ID="LabelRequiredField" runat="server" SkinID="NormalFontLabel"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td class="f10b" align="right" height="35" width="35%">
                                                    <asp:Label ID="LabelLogOnName" runat="server" Text=""></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="white-space: nowrap" align="left" width="64%">
                                                    <asp:TextBox ID="TextBoxUserID" MaxLength="20" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    <asp:Image ID="ImageRequired" runat="server" SkinID="LogonImgRequired" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="f10b" align="right" height="35" style="padding-right: 3px;">
                                                    <asp:Label ID="LabelUserName" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td style="white-space: nowrap" align="left">
                                                    <asp:TextBox ID="TextBoxName" MaxLength="50" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="f10b" align="right" height="35" style="padding-right: 3px;">
                                                    <asp:Label ID="LabelEmailId" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td style="white-space: nowrap" align="left">
                                                    <asp:TextBox ID="TextBoxEmail" CssClass="FormTextBox_bg" MaxLength="100" runat="server"></asp:TextBox>
                                                    <asp:Image ID="Image2" runat="server" SkinID="LogonImgRequired" />
                                                </td>
                                            </tr>
                                            <tr id="tableRowPassword" runat="server">
                                                <td class="f10b" align="right" height="35" style="padding-right: 3px;">
                                                    <asp:Label ID="LabelPassword" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td style="white-space: nowrap" align="left">
                                                    <asp:TextBox ID="TextBoxPassword" MaxLength="30" TextMode="Password" CssClass="FormTextBox_bg"
                                                        runat="server"></asp:TextBox>
                                                    <asp:Image ID="ImagePasswordRequired" runat="server" SkinID="LogonImgRequired" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="f10b" align="right" height="35" style="padding-right: 3px;">
                                                    <asp:Label ID="LabelPrintPin" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td style="white-space: nowrap" align="left">
                                                    <asp:TextBox ID="TextBoxPin" CssClass="FormTextBox_bg" MaxLength="10" TextMode="Password"
                                                        runat="server" TabIndex="8" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="f10b" align="right" height="35">
                                                    <asp:Label ID="LabelCostCenter" runat="server" Text="" class="f10b"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="white-space: nowrap" align="left">
                                                    <asp:DropDownList ID="DropDownListCostCenters" runat="server" CssClass="Dropdown_CSS"
                                                        TabIndex="10">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="f10b" align="right" height="35">
                                                    <asp:Label ID="Label1" runat="server" Text="My Account" class="f10b"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="white-space: nowrap" align="left">
                                                     <asp:Label ID="LabelMyAccStatus" runat="server" Text="" class="Dropdown_CSS"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center" height="35">
                                                <td colspan="3">
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="50">
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="" OnClick="ButtonSave_Click" />
                                                                &nbsp;&nbsp;
                                                                <asp:Button runat="server" ID="ButtonReset" Text="" CausesValidation="false" CssClass="Login_Button"
                                                                    OnClientClick="this.form.reset();return false;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap">
                            <asp:HiddenField ID="HdUserID" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxPassword"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator2" ID="ValidatorCalloutExtender2"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmilId" ControlToValidate="TextBoxEmail"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorEmilId" ID="ValidatorCalloutExtender3"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPin" runat="server"
                                ControlToValidate="TextBoxPin" ErrorMessage="" ValidationExpression=".{4}.*"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorPin" ID="ValidatorCalloutExtender10"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" ErrorMessage=""
                                ControlToValidate="TextBoxEmail" Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionEmail" ID="ValidatorCalloutExtender1"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
