<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    Inherits="AdministrationJobConfiguration" CodeBehind="JobConfiguration.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">

        fnShowCellSettings();
        Meuselected("Settings");

        function HidenSetting() {

            document.getElementById("ShowConfig").style.display = "none";
            document.getElementById("HidenSettingConfig").innerHTML = "<a href=\"#\" onclick=\"return ShowSetting();\">Show Setting</a>";
        }
        function ShowSetting() {
            document.getElementById("ShowConfig").style.display = "inline";
            document.getElementById("HidenSettingConfig").innerHTML = "<a href=\"#\" onclick=\"return HidenSetting();\">Hide Setting </a>";
        }
        function taust(asi1, col1) {

            asi1.bgColor = col1;
        }

        function AllowNumeric() {

            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else

                return false;
        }
        function IsUserSelected() {


            var thisForm = document.forms[0];
            var users = thisForm.__SelectedUsers.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__SelectedUsers[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__SelectedUsers.checked) {
                    selectedCount++
                    return true;
                }
            }

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
    <%--<div style="height: 10px;"> &nbsp;</div>--%>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image4" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="left">
                            <table cellpadding="0" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" width="10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingJobConfig" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image7" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td id="Tablecellback" runat="server" width="8%" visible="false" align="left" valign="middle"
                                        style="display: none">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="SettingsImageButtonBack"
                                            CausesValidation="False" ToolTip="" OnClick="ImageButtonBack_Click" />
                                    </td>
                                    <td id="Tablecellimage" runat="server" visible="false" width="1%" style="display: none">
                                        <asp:Image ID="Image3" runat="server" SkinID="ManageusersimgSplit" />
                                    </td>
                                    <td width="7%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" SkinID="SettingsImageButtonSave"
                                            CausesValidation="true" ImageAlign="Middle" ToolTip="" OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                        <asp:Image ID="Image5" runat="server" SkinID="ManageusersimgSplit" />
                                    </td>
                                    <td width="7%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="SettingsImageButtonReset"
                                            ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
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
                            <table width="90%" class="table_border_org" cellpadding="0" cellspacing="0" border="0">
                                <tr class="Top_menu_bg">
                                    <td class="f10b" height="35" colspan="2" align="left">
                                        &nbsp;
                                        <asp:Label ID="LabelJobRetention" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="47%" height="35">
                                        <asp:Label ID="LabelDays" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBoxDays" runat="server" MaxLength="3" CssClass="FormTextBox_bg"
                                            onkeypress="javascript:return AllowNumeric()" TabIndex="1"></asp:TextBox>
                                        <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                            ID="Image2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="47%" height="35">
                                        <asp:Label ID="LabelTime" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBoxTime" runat="server" MaxLength="5" CssClass="FormTextBox_bg"></asp:TextBox>
                                        <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center"
                                            ID="Image1" TabIndex="2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="5">
                                    </td>
                                </tr>
                                <tr class="Top_menu_bg">
                                    <td class="f10b" height="35" colspan="2" align="left">
                                        &nbsp;
                                        <asp:Label ID="LabelUserJobs" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="47%" height="30">
                                        <asp:Label ID="LabelAnonymousUserPrinting" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownListAnonymousUserPrinting" CssClass="Dropdown_CSS"
                                            runat="server">
                                            <asp:ListItem>Enable</asp:ListItem>
                                            <asp:ListItem>Disable</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="47%" height="30">
                                        <asp:Label ID="LabelOnNoJobs" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownListOnNoJobs" CssClass="Dropdown_CSS" runat="server">
                                            <asp:ListItem>Display Job List</asp:ListItem>
                                            <asp:ListItem>Navigate To MFP Mode</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="47%" height="30">
                                        <asp:Label ID="LabelPrintandRetain" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownListPrintandRetain" CssClass="Dropdown_CSS" runat="server">
                                            <asp:ListItem>Enable</asp:ListItem>
                                            <asp:ListItem>Disable</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="47%">
                                        <asp:Label ID="LabelSkipPrintSettings" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="CheckBoxSkipPrintSettings" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="5">
                                    </td>
                                </tr>
                                <tr class="Top_menu_bg">
                                    <td class="f10b" height="35" colspan="2" align="left">
                                        &nbsp;
                                        <asp:Label ID="LabelGenerateReports" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="5">
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td colspan="2">
                                        <table width="90%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor"
                                            align="center">
                                            <tr class="Grid_tr">
                                                <td align="center">
                                                    <asp:Table EnableViewState="false" ID="TableUsers" CellSpacing="1" CellPadding="3"
                                                        Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                            <asp:TableHeaderCell ID="TableHeaderCellSN" Wrap="false" Width="30px" CssClass="Grid_topbg1"> 
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellJobName" Wrap="false" HorizontalAlign="Left"
                                                                CssClass="H_title" Text=""> 
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellJobNameStatus" Wrap="false" HorizontalAlign="Left"
                                                                CssClass="H_title" Text=""></asp:TableHeaderCell>
                                                        </asp:TableHeaderRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr height="10px">
                                    <td colspan="2" align="center">
                                        <table width="50%">
                                            <tr>
                                                <td width="10%">
                                                    &nbsp;
                                                </td>
                                                <td align="right" width="50%">
                                                    <asp:Button ID="ButtonUpdate" runat="server" CssClass="Login_Button" OnClick="ButtonUpdate_Click"
                                                        Text="" Visible="True" />
                                                </td>
                                                <td align="left">
                                                    <asp:Button runat="server" ID="ButtonReset" Text="" CssClass="Login_Button"
                                                        OnClientClick="this.form.reset();return false;" />
                                                    <asp:Button ID="ButtonCancel" runat="server" CssClass="Cancel_button" Text="" Visible="false"
                                                        OnClick="ButtonCancel_Click" CausesValidation="false" />
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDays" ControlToValidate="TextBoxDays"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDays" ID="ValidatorCalloutExtenderDays"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTime" ControlToValidate="TextBoxTime"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorTime" ID="ValidatorCalloutExtenderTime"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorTime" runat="server"
                                ControlToValidate="TextBoxTime" ErrorMessage="" Display="None" SetFocusOnError="True"
                                ValidationExpression="^(([01][\d]+)|(2[0-3]))\:[0-5][0-9]$"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorTime" ID="ValidatorCalloutExtender2"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                </table>
            </td>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ClientMessages">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
    <style type="text/css">
        .FormTextBox_bg
        {
            margin-left: 0px;
        }
    </style>
</asp:Content>
