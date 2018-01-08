<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.master" AutoEventWireup="true"
    CodeBehind="FirstLogOn.aspx.cs" Inherits="AccountingPlusWeb.Web.FirstLogOn" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="server">
    <script type="text/javascript" language="javascript">
        function isSpclChar() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57) || (charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) || (charCode == 32))
                return true;
            else
                return false;
        }
    </script>
    <asp:ScriptManager EnableScriptGlobalization="True" ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenFieldApplicationSettingValue" runat="server" />
    <table width="98%" height="600" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="2%" height="22" align="right" valign="bottom">
                <asp:Image ID="Image1" runat="server" SkinID="FirstLogOnTopLeft" Width="16" Height="22" />
            </td>
            <td width="96%" class="Top_TopBG">
            </td>
            <td width="2%" align="left" valign="bottom">
                <asp:Image ID="Image2" SkinID="FirstLogOnTopRight" Width="25" Height="22" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="Top_LeftBG">
            </td>
            <td height="600" align="center" valign="middle" class="CenterBG">
                <table width="60%" border="0" cellpadding="0" cellspacing="0" height="304" class="table_border_org">
                    <tr>
                        <td height="42" align="left" valign="top" class="Login_TopBG">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" height="42">
                                <tr>
                                    <td width="7%" align="center" valign="middle">
                                        <asp:Image ID="Image3" Width="37" Height="34" SkinID="FirstLogOnLoginUserIcon" runat="server" />
                                    </td>
                                    <td width="31%" class="Title_Noraml_BlackFont">
                                        <asp:Label ID="LabelLogOn" runat="server" SkinID="LabelLogon"></asp:Label>
                                    </td>
                                    <td width="37%" align="right">
                                      
                                        <asp:Image ID="Image4" runat="server" SkinID="FirstLogOnRequired" Width="5" Height="5" />
                                    </td>
                                    <td width="25%" class="Noraml_BlackFont">
                                        &nbsp;
                                        <asp:Label ID="LabelRequired" SkinID="LabelLogon" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="CenterBG">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="35%" height="266" align="right" valign="middle">
                                        <asp:Image ID="Image5" SkinID="FirstLogOnLoginUser" runat="server" Width="213" Height="175" />
                                    </td>
                                    <td width="65%" align="center" valign="middle">
                                        <table width="91%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="33%" height="50" align="right" class="LoginFont">
                                                    <asp:Label ID="LabelUserSource" runat="server"></asp:Label>
                                                </td>
                                                <td width="56%" valign="middle">
                                                    <asp:DropDownList ID="DropDownListUserSource" runat="server" AutoPostBack="True"
                                                        CssClass="TextBox_BG" OnSelectedIndexChanged="DropDownListUserSource_SelectedIndexChanged"
                                                        TabIndex="1">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="11%" align="left">
                                                     <asp:Image ID="Image6" runat="server" SkinID="FirstLogOnRequired" Width="5" Height="5" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="50" align="right" class="LoginFont">
                                                    <asp:Label ID="LabelUserId" runat="server"></asp:Label>
                                                </td>
                                                <td height="50" align="center">
                                                    &nbsp;
                                                    <asp:TextBox ID="TextBoxUserId" CssClass="TextBox_BG" Width="220px" MaxLength="80"
                                                        AccessKey="u" Text="" runat="server" TabIndex="3"></asp:TextBox>
                                                </td>
                                                <td height="50" align="left">
                                                     <asp:Image ID="Image7" runat="server" SkinID="FirstLogOnRequired" Width="5" Height="5" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="50" align="right" class="LoginFont">
                                                    <asp:Label ID="LabelPassword" runat="server"></asp:Label>
                                                </td>
                                                <td height="50" align="center">
                                                    &nbsp;
                                                    <asp:TextBox ID="TextBoxUserPassword" Width="220px" MaxLength="30" AccessKey="p"
                                                        TextMode="Password" CssClass="TextBox_BG" runat="server" TabIndex="3"></asp:TextBox>
                                                </td>
                                                <td height="50" align="left">
                                                     <asp:Image ID="Image8" runat="server" SkinID="FirstLogOnRequired" Width="5" Height="5" />
                                                </td>
                                            </tr>
                                            <tr id="tdDomainControls" runat="server">
                                                <td height="50" valign="middle" class="LoginFont" align="right">
                                                    <asp:Label ID="LabelDomainName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td height="50" align="center">
                                                    &nbsp;
                                                    <asp:TextBox ID="TextBoxDomainName" CssClass="TextBox_BG" Width="220px" MaxLength="30"
                                                        AccessKey="u" Text="" runat="server" TabIndex="4"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <asp:Image ID="ImageDomain" runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="60" align="center" valign="middle">
                                                </td>
                                                <td height="60" colspan="2" align="left" valign="middle">
                                                    &nbsp;&nbsp;<asp:Button ID="ButtonAddUser" CssClass="Login_Button" runat="server"
                                                        Text="" OnClick="ButtonAddUser_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="ButtonCancel" CssClass="Login_Button" runat="server" Text="" OnClick="ButtonCancel_Click"
                                                        CausesValidation="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="10" colspan="5" align="left" valign="top">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ErrorMessage="Login Name cannot be empty"
                                                        ControlToValidate="TextBoxUserId" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" SetFocusOnError="true"
                                                        runat="server" ErrorMessage="Password cannot be empty" ControlToValidate="TextBoxUserPassword"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDomainName" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Domain field cannot be empty" Display="None"
                                                        ControlToValidate="TextBoxDomainName"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidatorUserName">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidatorPassword">
                                                    </cc1:ValidatorCalloutExtender>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidatorDomainName">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="Right_RightBG">
            </td>
        </tr>
    </table>
</asp:Content>
