<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    Inherits="PrintRoverWeb.AdministrationAddUsers" CodeBehind="AddUsers.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PC" ContentPlaceHolderID="PageContent" runat="Server">
    <script type="text/javascript" language="javascript">
        fnShowCellUsers();
        Meuselected("UserID");
        function RestrictChar() {

            var kCode = parseInt(event.keyCode);
            if ((kCode >= 65 && kCode <= 90) || (kCode >= 97 && kCode <= 122) || (kCode >= 48 && kCode <= 57) || (kCode == 46)) {
                event.returnvalue = true;
                return true;
            }
            else {

                event.returnValue = false;
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
        function readCardID() {

            document.getElementById('ctl00_PageContent_ButtonSave').focus();

        }
        function myKeyPressHandler() {
            if (event.keyCode == 13) {
                var hiddenvalue = document.getElementById('ctl00_PageContent_HdUserID').value;
                var userid;
                if (hiddenvalue == "" || hiddenvalue == null) {
                    userid = document.getElementById('ctl00_PageContent_TextBoxUserID').value;
                }
                else {
                    userid = hiddenvalue;
                }
                var usernaeme = document.getElementById('ctl00_PageContent_TextBoxName').value;
                var userrole = document.getElementById('ctl00_PageContent_DropDown_UserRole').value;
                var department = document.getElementById('ctl00_PageContent_DropDownDepartment').value;
                var password = document.getElementById('ctl00_PageContent_TextBoxPassword').value;

                if (userid == "" || userid == null) {
                    if (hiddenvalue == "" || hiddenvalue == null) {
                        document.getElementById('ctl00_PageContent_TextBoxUserID').focus();
                        return false;
                    }
                }
                else if (usernaeme == "" || usernaeme == null) {
                    document.getElementById('ctl00_PageContent_TextBoxName').focus();
                    return false;
                }
                else if (userrole == "Select") {
                    document.getElementById('ctl00_PageContent_DropDown_UserRole').focus();
                    return false;
                }
                else if (department == "-1") {
                    document.getElementById('ctl00_PageContent_DropDownDepartment').focus();
                    return false;
                }
                else if (password == "" || password == null) {
                    document.getElementById('ctl00_PageContent_TextBoxPassword').focus();
                    return false;
                }
                else if (userid != "" && usernaeme != "" && userrole != "-1" && department != "-1" && password != "") {
                    document.getElementById('ctl00_PageContent_ButtonSave').focus();
                }

            }
        }


        document.onkeypress = myKeyPressHandler;


        function isSpclChar() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57) || (charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) || (charCode == 32))
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
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="table_border_org"
        height="550">
        <tr>
            <td height="25" align="left" valign="top">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadUserManagement" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td height="33" align="left" valign="middle" width="25%">
                            <table cellpadding="0" cellspacing="0" width="14%" border="0">
                                <tr>
                                    <td width="4%" align="center" valign="middle">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="AddUsersImageButtonBack"
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="" OnClick="ImageButtonBack_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split">
                                    </td>
                                    <td width="4%" align="left" valign="middle">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" CausesValidation="true" ImageAlign="Middle"
                                            SkinID="AddUsersImageButtonSave" ToolTip="" OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split">
                                    </td>
                                    <td width="4%" align="left" valign="middle">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="AddUsersImageButtonReset"
                                            ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="15" class="CenterBG">
            </td>
        </tr>
        <tr class="Grid_tr">
            <td class="CenterBG">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" align="center">
                    <tr>
                        <td align="center">
                            <table cellpadding="0" cellspacing="0" border="0" class="table_border_org" width="50%">
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                            <tr class="Top_menu_bg">
                                                <td width="50%" align="left" valign="middle">
                                                    &nbsp;<asp:Label ID="LabelUserManagement" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                </td>
                                                <td align="right" width="30%" valign="middle">
                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
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
                                    <td>
                                        <div>
                                            <asp:Menu ID="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                                                Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" runat="server">
                                                
                                            </asp:Menu>
                                            <div class="tabBody">
                                                <asp:MultiView ID="multiTabs" ActiveViewIndex="0" runat="server">
                                                    <asp:View ID="view1" runat="server">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td class="f10b" height="33" align="right" width="40%">
                                                                    <asp:Label ID="LabelLogOnName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                </td>
                                                                <td valign="middle" align="left" width="35%">
                                                                    <asp:TextBox ID="TextBoxUserID" MaxLength="30" CssClass="FormTextBox_bg" runat="server"
                                                                        TabIndex="1"></asp:TextBox>
                                                                    <asp:Label ID="Labeluser" runat="server" Text="" Visible="false"></asp:Label>
                                                                </td>
                                                                <td width="25%" align="left" valign="middle">
                                                                    <asp:Image ID="Imageuser" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                                        padding-left: 5px;" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelUserName" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:TextBox ID="TextBoxName" MaxLength="30" CssClass="FormTextBox_bg" runat="server"
                                                                        TabIndex="2"></asp:TextBox>
                                                                    <asp:Label ID="Labelfullname" runat="server" Text="" Visible="false"></asp:Label>
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Image ID="Imagename" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                                        padding-left: 5px;" />
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none">
                                                                <td align="right" height="30">
                                                                    <asp:Label ID="LabelDefaultPrintProfile" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left" colspan="2">
                                                                    <asp:DropDownList ID="DropDownPrintProfile" CssClass="Dropdown_CSS" runat="server"
                                                                        TabIndex="3">
                                                                    </asp:DropDownList>
                                                                    test
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelUserRole" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:DropDownList ID="DropDown_UserRole" runat="server" CssClass="Dropdown_CSS" TabIndex="4">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center;
                                                                        padding-left: 5px;" />
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none">
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelDepartment" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:DropDownList ID="DropDownDepartment" runat="server" CssClass="Dropdown_CSS"
                                                                        TabIndex="5">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center;
                                                                        padding-left: 5px;" ID="ImageDepartment" />
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelEmailId" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:TextBox ID="TextBoxEmail" CssClass="FormTextBox_bg" MaxLength="50" runat="server"
                                                                        TabIndex="6"></asp:TextBox>
                                                                    <asp:Label ID="Labelemail" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Image ID="ImageEmailRequired" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center;
                                                                        padding-left: 5px;" />
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr id="Tablerowpass" runat="server">
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelPassword" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:TextBox ID="TextBoxPassword" MaxLength="30" TextMode="Password" CssClass="FormTextBox_bg"
                                                                        runat="server" TabIndex="7"></asp:TextBox>
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                    <asp:Image runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                                        padding-left: 5px;" ID="ImagePasswordRequired" />
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelPrintPin" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:TextBox ID="TextBoxPin" CssClass="FormTextBox_bg" TextMode="Password" runat="server"
                                                                        TabIndex="8"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelCardID" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:TextBox ID="TextBoxCard" CssClass="FormTextBox_bg" MaxLength="400" TextMode="Password"
                                                                        runat="server" TabIndex="9"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelPreferedCostCenter" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:DropDownList ID="DropDownListCostCenters" runat="server" CssClass="Dropdown_CSS"
                                                                        TabIndex="10">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="white-space: nowrap" height="35" align="right">
                                                                    <asp:Label ID="LabelEnableLogOn" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:CheckBox ID="CheckBoxEnableLogOn" runat="server" TabIndex="11" />
                                                                </td>
                                                            </tr>
                                                            <tr >
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelMyAccount" runat="server" Text="" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:DropDownList ID="DropDownListMyAccount" runat="server" CssClass="Dropdown_CSS"
                                                                        TabIndex="12">
                                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr id="trDepartment" runat = "server" >
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelDepartKey" runat="server" Text="Department" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:Label ID="LabelDepartValue" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                </td>
                                                            </tr>
                                                            <tr id="trCompany" runat="server">
                                                                <td height="35" align="right">
                                                                    <asp:Label ID="LabelCompany" runat="server" Text="Company" class="f10b"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:Label ID="LabelCompanyValue" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td align="left" valign="middle">
                                                                </td>
                                                            </tr>

                                                            <tr align="center">
                                                                <td colspan="4" height="35">
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                        <tr>
                                                                            <td height="10">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="" OnClick="ButtonSave_Click"
                                                                                    TabIndex="12" />
                                                                                <asp:Button ID="ButtonCancel" CssClass="Cancel_button" CausesValidation="false" runat="server"
                                                                                    Text="" OnClick="ButtonCancel_Click" TabIndex="13" />
                                                                                <asp:Button runat="server" ID="ButtonReset" Text="Reset" TabIndex="14" CssClass="Login_Button"
                                                                                    OnClientClick="this.form.reset();return false;" />
                                                                                      <asp:Button runat="server" ID="ButtonUpdateAD" Text="Update from AD" TabIndex="15" CssClass="Login_Button"
                                                                                     OnClick="ButtonUpdateAD_Click" Visible="false" />
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
                                                    </asp:View>
                                                    <asp:View ID="view2" runat="server">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top: 5px">
                                                            <tr>
                                                                <td height="430px" valign="top" align="center">
                                                                    <table cellpadding="0" cellspacing="0" width="90%" style="margin-bottom: 5px">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table cellpadding="0" cellspacing="0" border="1" width="90%">
                                                                        <tr class="Grid_tr">
                                                                            <td>
                                                                                <asp:Table EnableViewState="false" ID="TableUsers" SkinID="Grid" CellSpacing="0"
                                                                                    CellPadding="3" Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg"
                                                                                    border="0">
                                                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                        <asp:TableHeaderCell HorizontalAlign="Left" Width="30" CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ID="TableHeaderCellMember" runat="server" HorizontalAlign="Left"
                                                                                            CssClass="Grid_topbg1">
                                                                                           
                                                                                        </asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ID="TableHeaderCellIsCostCenter" runat="server" HorizontalAlign="Left"
                                                                                            CssClass="Grid_topbg1" Text="Is MemberOf CostCenter?">
                                                                                           
                                                                                        </asp:TableHeaderCell>

                                                                                    </asp:TableHeaderRow>
                                                                                </asp:Table>
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
                                </tr>
                            </table>
                            <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxUserID"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator1" ID="ValidatorCalloutExtender1"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage=""
                        ControlToValidate="TextBoxUserID" Display="None" SetFocusOnError="True" ValidationExpression="\w+"></asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidator3" ID="ValidatorCalloutExtender8"
                        runat="server">
                    </cc1:ValidatorCalloutExtender>--%>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" ControlToValidate="TextBoxName"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorName" ID="ValidatorCalloutExtenderName"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRole" ControlToValidate="DropDown_UserRole"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None" InitialValue="Select"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorRole" ID="ValidatorCalloutExtender4"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorDepartment" ControlToValidate="DropDownDepartment"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None" InitialValue="-1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorDepartment"
                                ID="ValidatorCalloutExtender5" runat="server">
                            </cc1:ValidatorCalloutExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmailRequired" ControlToValidate="TextBoxEmail"
                                runat="server" ErrorMessage="EmailId cannot be empty." SetFocusOnError="true"
                                Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorEmailRequired"
                                ID="ValidatorCalloutExtender3" runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" ControlToValidate="TextBoxPassword"
                                runat="server" ErrorMessage="" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidatorPassword" ID="ValidatorCalloutExtender2"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage=""
                                ControlToValidate="TextBoxEmail" Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidator1" ID="ValidatorCalloutExtender7"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPin" runat="server"
                                ControlToValidate="TextBoxPin" ErrorMessage="" Display="None" />
                            <cc1:ValidatorCalloutExtender TargetControlID="RegularExpressionValidatorPin" ID="ValidatorCalloutExtender10"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:HiddenField ID="HdUserID" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    &nbsp;
</asp:Content>
