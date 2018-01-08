<%@ Page Language="C#" MasterPageFile="~/MasterPages/LogOn.master" AutoEventWireup="True"
    Inherits="WeblogOn" CodeBehind="LogOn.aspx.cs" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageContent" ID="PC" runat="server">

<script src="../JavaScript/AvoidBlankSpace.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function isSpclChar() {
            var charCode = event.keyCode;
//             alert(charCode);
             if ((charCode == 8) || (charCode >= 48 && charCode <= 57) || (charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) || (charCode == 32) || (charCode == 34) || (charCode == 39) || (charCode == 96))
                return true;
            else
                return false;
        }
        function capLock(e) {
            try {
                kc = e.keyCode ? e.keyCode : e.which;
                sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);
                if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
                    document.getElementById('divCaps').style.visibility = 'visible';
                else
                    document.getElementById('divCaps').style.visibility = 'hidden';
            }
            catch (Error)
            { }
        }
        
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenFieldUserSource" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="98%" height="600" border="0" cellpadding="0" cellspacing="0">
                <tr style="border-color: Red; display: none">
                    <td width="2%" valign="bottom" align="right">
                        <asp:Image ID="ImageTopLeft" SkinID="LogonTopLeft" runat="server" />
                    </td>
                    <td width="96%" class="Top_TopBG">
                    </td>
                    <td width="2%" align="left" valign="bottom">
                        <asp:Image ID="ImageTopRight" SkinID="LogonTopRight" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="Top_LeftBG">
                    </td>
                    <td align="center" valign="middle" class="CenterBG">
                        <div id="divLogin" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 600px; height: 225px"
                                class="table_border_org">
                                <tr>
                                    <td height="42" align="left" valign="top" class="Login_TopBG">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 95%">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Image ID="ImageLoginUserIcon" SkinID="LoginUserIcon" runat="server" />
                                                            </td>
                                                            <td style="width: 75%; padding-left: 5px;" align="left">
                                                                <asp:Label ID="LabelLogOn" runat="server" SkinID="LabelLogon"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Image ID="ImageRequired" SkinID="LogonRequired" runat="server" />
                                                            </td>
                                                            <td style="white-space: nowrap; padding-left: 5px;" align="left">
                                                                <asp:Label ID="LabelRequired" CssClass="LabelLoginFont1" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                                    <asp:Image ID="ImageLoginUser" SkinID="LogonLoginUser" runat="server" />
                                                </td>
                                                <td width="65%" align="center" valign="middle">
                                                    <table border="0" style="height: 200px" cellpadding="3" cellspacing="3">
                                                        <tr>
                                                            <td align="right" valign="middle" class="LoginFont" style="white-space: nowrap">
                                                                <asp:Label ID="LabelUserSource" runat="server"></asp:Label>
                                                            </td>
                                                            <td valign="middle">
                                                                <asp:DropDownList ID="DropDownListUserSource" runat="server" AutoPostBack="True"
                                                                    CssClass="TextBox_BG" OnSelectedIndexChanged="DropDownListUserSource_SelectedIndexChanged"
                                                                    TabIndex="1">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Image ID="ImageLogonOnRequired" SkinID="LogonRequired" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" class="LoginFont" style="white-space: nowrap">
                                                                <asp:Label ID="LabelUserId" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="TextBoxUserId" CssClass="TextBox_BG" Width="220px" MaxLength="80"
                                                                    AccessKey="u" Text="" runat="server" TabIndex="3"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Image ID="ImageLogonRequired2" SkinID="LogonRequired" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" class="LoginFont" style="white-space: nowrap">
                                                                <asp:Label ID="LabelPassword" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="TextBoxUserPassword" Width="220px" MaxLength="30"  onkeypress="capLock(event),AvoidBlankSpace();"
                                                                   AccessKey="p" TextMode="Password" CssClass="TextBox_BG" runat="server" TabIndex="3"></asp:TextBox>
                                                                <%--onkeypress="fade('divCaps');capLock(event)"--%>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Image ID="ImageLogonRequired3" SkinID="LogonRequired" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr id="tdDomainControls" runat="server">
                                                            <td valign="middle" class="LoginFont" align="right">
                                                                <asp:Label ID="LabelDomainName" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="TextBoxDomainName" CssClass="TextBox_BG" Width="220px" MaxLength="30"
                                                                    AccessKey="u" Text="" runat="server" TabIndex="4"></asp:TextBox>
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                <asp:Image ID="ImageDomain" runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="right">
                                                                <div id="divCaps" class="LoginFontCaps" style="visibility: hidden">
                                                                    Caps Lock is on!</div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle">
                                                            </td>
                                                            <td colspan="2" align="left" valign="middle">
                                                                <asp:Button ID="ButtonLogOn" runat="server" Text="" OnClick="ButtonLogOn_Click" />
                                                                &nbsp;
                                                                <asp:Button ID="ButtonCancel" runat="server" Text="" OnClick="ButtonCancel_Click"
                                                                    CausesValidation="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="right" class="LabelLoginFont1">
                                                                <asp:LinkButton ID="LinkButtonForgetPassword" CausesValidation="false" Visible="false"
                                                                    runat="server" CssClass="linkButtonReset" OnClick="LinkButtonForgetPassword_Click"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="left" valign="top">
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
                        </div>
                        <div id="divResetPassword" runat="server" visible="false">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 600px; height: 225px"
                                class="table_border_org">
                                <tr>
                                    <td height="42" align="left" valign="top" class="Login_TopBG">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 90%" valign="top">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Image ID="Image1" SkinID="LoginUserIcon" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelResetPassword" Text="" runat="server" SkinID="LabelLogon"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="3" cellspacing="3">
                                                        <tr>
                                                            <td>
                                                                <asp:Image ID="Image2" SkinID="LogonRequired" runat="server" />
                                                            </td>
                                                            <td style="white-space: nowrap">
                                                                <asp:Label ID="LabelRequiredFields" SkinID="LabelLogon" Text="" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" class="CenterBG">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="right" valign="middle">
                                                    <asp:Image ID="ImageResetLogin" SkinID="LogonResetLogin" runat="server" />
                                                </td>
                                                <td align="center" valign="middle">
                                                    <table width="100%" border="0" cellpadding="3" cellspacing="3">
                                                        <tr>
                                                            <td align="right" valign="middle" class="LoginFont" style="white-space: nowrap">
                                                                <asp:Label ID="LabelUsername" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="TextBoxResetUserId" CssClass="TextBox_BG" Width="220px" MaxLength="30"
                                                                    AccessKey="u" Text="" runat="server" TabIndex="3"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Image ID="Image3" runat="server" SkinID="LogonImgRequired" Style="vertical-align: middle" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td colspan="2" align="center" valign="middle" style="white-space: nowrap">
                                                                <asp:Button ID="ButtonReset" runat="server" Text="" OnClick="ButtonReset_Click" />
                                                                &nbsp;
                                                                <asp:Button ID="ButtonCancelReset" runat="server" Text="" OnClick="ButtonCancelReset_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td class="Right_RightBG">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
