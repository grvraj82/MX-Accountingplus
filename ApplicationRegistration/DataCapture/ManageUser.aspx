<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master"
    CodeBehind="ManageUser.aspx.cs" Inherits="ApplicationRegistration.DataCapture.ManageUser" %>

<asp:Content ID="UserDefinition" runat="server" ContentPlaceHolderID="PageContentHolder">
    <script language="javascript" type="text/javascript">

        function onkeyPress(e) {
            var key = window.event ? e.keyCode : e.which;
            if (key == 13)
                e.cancelBubble = true;
            e.returnValue = false;
            return false;
        }

    
    </script>
    <asp:Label ID="LabelScript" runat="server"></asp:Label>
    <script language="javascript" type="text/javascript">

        function GetStates(countryID, stateOrOthersID, stateOthersID, selectedStateID) {

            var objCountry = eval("document.forms[0]." + countryID);
            var objSelectedState = eval("document.forms[0]." + selectedStateID);
            var selectedIndex = objCountry.selectedIndex;

            var states = countries[selectedIndex];
            var statesArray = states.split("!");
            var objStateOrOthers = eval("document.forms[0]." + stateOrOthersID);

            objStatesDiv = document.all.divStates;
            if (statesArray.length > 0 && states != "") {
                var statesHTML = "<select name='UserState' style='width:327px'>"
                statesHTML += "<option value='0'></option>"

                for (var state = 0; state < statesArray.length - 1; state++) {
                    var stateCodeNameArray = statesArray[state].split("^")
                    if (stateCodeNameArray.length == 2) {
                        var selectedOption = "";
                        if (stateCodeNameArray[0] == objSelectedState.value) {
                            selectedOption = " selected";
                        }
                        statesHTML += "<option value='" + stateCodeNameArray[0] + "'" + selectedOption + ">" + stateCodeNameArray[1] + "</option>";
                    }
                }
                statesHTML += "</select>"
                //alert(statesHTML);
                objStatesDiv.innerHTML = statesHTML;
                objStateOrOthers.value = "StateDropdownList";
            }
            else {
                var objStateOther = eval("document.forms[0]." + stateOthersID);
                objStatesDiv.innerHTML = "<input type='textbox' name='UserState' value='" + objStateOther.value + "' style='width:328px' />";
                objStateOrOthers.value = "StateTextBox";
            }
        }

        function AdjustHeight() {
            GetStates('ctl00_PageContentHolder_DropDownListCountry', 'ctl00_PageContentHolder_HiddenFieldStateSource', 'ctl00_PageContentHolder_HiddenFieldStateOthers', 'ctl00_PageContentHolder_HiddenFieldState');
        }
 
    </script>
    <div>
        <asp:Panel ID="PanelManageProduct" runat="server">
            <br />
            <table cellpadding="2" cellspacing="3" border="0" align="center" id="UserForm" runat="server">
                <tr>
                    <td>
                        <fieldset>
                            <legend>
                                <asp:Label ID="LabelFieldSetTitle" runat="server" Text="User Information" Font-Bold="True"></asp:Label></legend>
                            <asp:Table ID="TableManageUsers" runat="server" Width="100%" CellPadding="1" CellSpacing="1">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                                        <asp:Label ID="LabelRequiredFields" CssClass="f11" Text="" runat="server"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelName" runat="server" Text="Name"></asp:Label>&nbsp;*</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxName" runat="server" Width="328px" MaxLength="50" CssClass="DataCapture"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requiredFieldName" runat="server" ControlToValidate="TextBoxName"
                                            ErrorMessage="Please enter Name" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regularExpressionValidatorName" runat="server"
                                            ControlToValidate="TextBoxName" Display="None" ErrorMessage="Please enter valid alphanumeric User Name"
                                            ValidationExpression="^[a-zA-Z0-9 ,]+$" SetFocusOnError="True">*</asp:RegularExpressionValidator>
                                        <asp:HiddenField ID="HiddenUserId" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelUserId" runat="server" Text="LogOn ID"></asp:Label>&nbsp;*</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxUserId" runat="server" Width="328px" MaxLength="30" CssClass="DataCapture"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requiredFieldLabelUserID" runat="server" ControlToValidate="TextBoxUserID"
                                            ErrorMessage="Please enter valid alphanumeric user ID" SetFocusOnError="True"
                                            Display="None"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                runat="server" ControlToValidate="TextBoxUserId" Display="None" ErrorMessage="Please enter alphanumeric value"
                                                ValidationExpression="\w*">*</asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelPassword" runat="server" Text="Password"></asp:Label>&nbsp;*</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxPassword" runat="server" Width="328px" MaxLength="20" TextMode="Password"
                                            CssClass="DataCapture"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requiredFieldPassword" runat="server" ControlToValidate="TextBoxPassword"
                                            ErrorMessage="Please enter valid password" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>&nbsp;*</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxConfirmPassword" runat="server" Width="328px" MaxLength="20"
                                            TextMode="Password" CssClass="DataCapture"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidatorConfirmPassword" runat="server" ControlToCompare="TextBoxPassword"
                                            ControlToValidate="TextBoxConfirmPassword" CssClass="f11b" Display="None" ErrorMessage="Confirm pasword is not matching with password value."
                                            SetFocusOnError="True"></asp:CompareValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelAddress1" runat="server" Text="Address Line 1"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxAddress1" runat="server" Width="328px" MaxLength="250" CssClass="DataCapture"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelAddress2" runat="server" Text="Address Line 2"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxAddress2" runat="server" Width="328px" MaxLength="250" CssClass="DataCapture"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelCity" runat="server" Text="City"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxCity" runat="server" Width="328px" MaxLength="50" CssClass="DataCapture"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelCountry" runat="server" Text="Country"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="DropDownListCountry" runat="server" Width="327px" AutoPostBack="False"
                                            CssClass="DataCapture">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelState" runat="server" Text="State"></asp:Label></asp:TableCell>
                                    <asp:TableCell><div id="divStates"></div></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelZipCode" runat="server" Text="Postal Code"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxZipCode" runat="server" Width="328px" MaxLength="10" CssClass="DataCapture"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">Telephone Number</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxPhone" runat="server" MaxLength="15" Width="328px" CssClass="DataCapture"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">Email *</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxEmail" runat="server" MaxLength="50" Width="328px" CssClass="DataCapture"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                            ErrorMessage="Please enter valid Email ID" ControlToValidate="TextBoxEmail" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxEmail"
                                            Display="None" ErrorMessage="Please enter Valid Emial ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            SetFocusOnError="True">*</asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelProductCompany" runat="server" Text="Company Name"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="DropDownListCompany" runat="server" Width="327px" CssClass="DataCapture">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="requiredFieldCompany" runat="server" ControlToValidate="DropDownListCompany"
                                            ErrorMessage="Please select the company">*</asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Wrap="false">
                                        <asp:Label ID="LabelPreferredProduct" runat="server" Text="Preferred Product"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="DropDownListPreferredProduct" runat="server" Width="327px"
                                            CssClass="DataCapture">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:CheckBox ID="CheckBoxEnableAccess" runat="server" CssClass="DataCapture" /><asp:Label
                                            ID="LabelEnableAccess" runat="server" Text="Access Enabled"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click"
                                            Width="85px" Visible="False" />
                                        <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" Width="85px"
                                            Visible="False" />&nbsp;
                                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" Width="85px" CausesValidation="False"
                                            OnClick="ButtonCancel_Click" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                            <asp:HiddenField ID="HiddenFieldState" runat="server" />
                            <asp:HiddenField ID="HiddenFieldStateOthers" runat="server" />
                            <asp:HiddenField ID="HiddenFieldStateSource" runat="server" />
                        </fieldset>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
