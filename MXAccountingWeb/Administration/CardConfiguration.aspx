<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/InnerPage.Master"
    CodeBehind="CardConfiguration.aspx.cs" Inherits="PrintRoverWeb.Administration.CardConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageContent" ID="PCCardConfiguration" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
        document.onkeyup = KeyCheck;
        function AllowOnlyNumbers(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }


        function KeyCheck(e) {
            var KeyID = (window.event) ? event.keyCode : e.keyCode;
            switch (KeyID) {
                case 16:
                    //document.Form1.KeyName.value = "Shift";
                    break;
                case 17:
                    //document.Form1.KeyName.value = "Ctrl";
                    break;
                case 18:
                    //document.Form1.KeyName.value = "Alt";
                    break;
                case 19:
                    //document.Form1.KeyName.value = "Pause";
                    break;
                case 37:
                    //document.Form1.KeyName.value = "Arrow Left";
                    break;
                case 38:
                    //document.Form1.KeyName.value = "Arrow Up";
                    break;
                case 39:
                    //document.Form1.KeyName.value = "Arrow Right";
                    break;
                case 40:
                    //document.Form1.KeyName.value = "Arrow Down";
                    break;
            }
        }

        function SelectFSCOption(rowID) {
            var objFscOn = document.getElementById("FSC_RULEON_" + rowID);

            var objStartPosition = document.getElementById("FSC_START_POSITION_" + rowID);
            var objLength = document.getElementById("FSC_LENGTH_" + rowID);

            var objStartDelimeter = document.getElementById("FSC_START_DELIMETER_" + rowID);
            var objEndDelimeter = document.getElementById("FSC_END_DELIMETER_" + rowID);


            if (objFscOn[objFscOn.selectedIndex].value == 'P') {
                objStartDelimeter.style.display = 'none';
                objEndDelimeter.style.display = 'none';
                objStartPosition.style.display = 'block';
                objLength.style.display = 'block';
            }
            else {

                objStartDelimeter.style.display = 'block';
                objEndDelimeter.style.display = 'block';
                objStartPosition.style.display = 'none';
                objLength.style.display = 'none';
            }
        }
        function CheckDDR() {

            var ddrStatus = document.getElementById("DDR_ENABLERULE").checked;
            var dprStatus = document.getElementById("DPR_ENABLERULE").checked;
            if (ddrStatus == false) {
                if (dprStatus == true) {
                    jNotify(C_SELECT_DPR)

                    return false;
                }

            }
            var FSCStatus = document.getElementById("ctl00_PageContent_CheckBoxEnableFsc").checked;
            var FSCChildFalg = false;
            if (FSCStatus == true) {
                for (i = 1; i <= 5; i++) {

                    if (document.getElementById("FSC_ENABLERULE_" + i).checked == true) {
                        FSCChildFalg = true;
                    }

                }
                if (FSCChildFalg == false) {
                    jNotify(C_INVALID_SETTINGS)

                    return false;
                }

            }


        }


        function SelectDDROption() {
            var objDDROn = document.getElementById("DDR_RULEON");

            var objStartPosition = document.getElementById("DDR_START_POSITION");
            var objLength = document.getElementById("DDR_LENGTH");

            var objStartDelimeter = document.getElementById("DDR_START_DELIMETER");
            var objEndDelimeter = document.getElementById("DDR_END_DELIMETER");


            if (objDDROn[objDDROn.selectedIndex].value == 'P') {
                objStartDelimeter.style.display = 'none';
                objEndDelimeter.style.display = 'none';
                objStartPosition.style.display = 'block';
                objLength.style.display = 'block';
            }
            else {
                objStartDelimeter.style.display = 'block';
                objEndDelimeter.style.display = 'block';
                objStartPosition.style.display = 'none';
                objLength.style.display = 'none';
            }
        }
        function ReadDataFromReader() {
            return false;
        }

        function EnableandDisableCheckBox() {
            if (document.getElementById('ctl00_PageContent_CheckBoxEnableFsc').checked == true) {
                for (i = 1; i <= 5; i++) {
                    // alert(document.getElementById("FSC_ENABLERULE_1").checked);
                    document.getElementById("FSC_ENABLERULE_" + i).disabled = false;
                }
            }
            else {
                for (i = 1; i <= 5; i++) {
                    document.getElementById("FSC_ENABLERULE_" + i).disabled = true;
                    document.getElementById("FSC_ENABLERULE_" + i).checked = false;
                    //document.getElementById("FSC_RULEON_" + i).disabled=true;
                }
            }
        }
        function myKeyPressHandler() {
            if (event.keyCode == 13) {
                document.getElementById('ctl00_PageContent_ButtonSave').focus();
            }
        }

        document.onkeypress = myKeyPressHandler;
        
    </script>
    <div id="content" style="display: none;">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelRefresh" runat="server">
        <ContentTemplate>
            <%--<div style="height: 10px;">&nbsp;</div>--%>
            <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
                <tr>
                    <td align="right" valign="top" style="width: 1px">
                        <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
                    </td>
                    <td width="100%" valign="top" class="CenterBG">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr class="Top_menu_bg">
                                            <td class="HeadingMiddleBg" style="width: 10%">
                                                <div style="padding: 4px 10px 0px 10px;">
                                                    <asp:Label ID="LabelCardDataPageTitle" runat="server" Text=""></asp:Label></div>
                                            </td>
                                            <td>
                                                <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                            </td>
                                            <td height="35" align="left" style="width: 90%">
                                                <table cellpadding="1" cellspacing="0" width="50%" border="0">
                                                    <tr>
                                                        <td id="Tablecellback" runat="server" width="7%" visible="false" align="left" valign="middle"
                                                            style="display: none">
                                                            <asp:ImageButton ID="ImageButtonBack" SkinID="SettingsImageButtonBack" runat="server"
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
                                                        <%--<td width="1%" class="Menu_split">--%>
                                                        <td width="85%" align="left" valign="middle">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 2px">
                                </td>
                            </tr>
                            <tr>
                                <td class="Grid_topbg" align="center" style="padding-left: 5px;">
                                    <asp:Label ID="LabelCardType" CssClass="f10b" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="DropDownListCardType" CssClass="Dropdown_CSS" OnSelectedIndexChanged="DropDownListCardType_SelectedIndexChanged"
                                        AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top">
                                    <asp:Table EnableViewState="false" ID="TableFascilityCodeCheckSettings" CellPadding="3"
                                        CellSpacing="0" Width="98%" runat="server" BorderWidth="0" GridLines="Both" BorderColor="#efefef">
                                        <asp:TableRow SkinID="TableDoubleRow">
                                            <asp:TableCell RowSpan="2" HorizontalAlign="Left" ColumnSpan="3" Width="1%" Wrap="false"></asp:TableCell>
                                            <asp:TableCell CssClass="H_title" HorizontalAlign="Left" ID="TableCellByPosition"
                                                ColumnSpan="2" Width="25%" Wrap="false" Text=""></asp:TableCell>
                                            <asp:TableCell CssClass="H_title" HorizontalAlign="Left" ID="TableCellByDelimiter"
                                                ColumnSpan="2" Width="25%" Wrap="false" Text=""></asp:TableCell>
                                            <asp:TableCell CssClass="H_title" ID="TableCellFacilityCodeCheckValue" RowSpan="2"
                                                HorizontalAlign="Left" Width="25%" Wrap="true" Text=""></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow SkinID="TableDoubleRow">
                                            <asp:TableCell CssClass="H_title" ID="TableCellStartPosition" Wrap="false" Width="20%"
                                                HorizontalAlign="Left" Text=""></asp:TableCell>
                                            <asp:TableCell CssClass="H_title" ID="TableCellLength" HorizontalAlign="Left" Wrap="false"
                                                Width="20%" Text=""></asp:TableCell>
                                            <asp:TableCell CssClass="H_title" ID="TableCellStart" HorizontalAlign="Left" Wrap="false"
                                                Width="20%" Text=""></asp:TableCell>
                                            <asp:TableCell CssClass="DoubleH_titleRowHeaderSmall" ID="TableCellEnd" HorizontalAlign="Left"
                                                Wrap="false" Width="20%" Text=""></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableHeaderRow SkinID="TableRow">
                                            <asp:TableHeaderCell>
                                                <asp:CheckBox ID="CheckBoxEnableFsc" runat="server" />
                                            </asp:TableHeaderCell>
                                            <asp:TableHeaderCell ColumnSpan="7">
                                                <table cellpadding="3" cellspacing="3" border="0" style="height: 10px;">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="LabelFscTitle" CssClass="f10b" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:TableHeaderCell>
                                        </asp:TableHeaderRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ButtonSave" runat="server" Text="" CssClass="Login_Button" OnClick="ButtonSave_Click" />
                                    <asp:Button ID="ButtonCancel" runat="server" Text="" CssClass="Cancel_button" Visible="false"
                                        OnClick="ButtonCancel_Click" />
                                    <asp:Button runat="server" ID="ButtonReset" Text="" CssClass="Login_Button"
                                        OnClientClick="this.form.reset();return false;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="PanelTestCardConfiguration" Visible="true" runat="server">
                                        <br />
                                        <hr />
                                        <table align="center">
                                            <tr>
                                                <td align="left">
                                                    Card ID
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TextBoxCardID" runat="server"></asp:TextBox><asp:Button ID="ButtonValidateCard"
                                                        CssClass="Login_Button" runat="server" Text="Validate Card" OnClick="ButtonValidateCard_Click" />
                                                    <asp:CheckBox ID="CheckBoxAllUsers" Text="All Users" runat="server" />
                                                </td>
                                                <td align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left" style="background-color">
                                                    <table cellpadding="3" cellspacing="3">
                                                        <tr>
                                                            <td align="left">
                                                                Sliced Card ID
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="LabelSlicedCardID" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Is Valid Facility Code
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="LabelISValidFSC" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Validation Info
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="LabelValidationInfo" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Delimeter
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Strat Delimeter
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBoxSrartDelimeter" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                End Delimeter
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBoxEndDelimeter" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Result
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="LabelExtractedData" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label Font-Bold="true" ForeColor="Red" ID="LabelActionMessage" runat="server"
                                                                    Text=""></asp:Label>
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
