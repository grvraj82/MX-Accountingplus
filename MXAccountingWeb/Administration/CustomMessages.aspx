<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="CustomMessages.aspx.cs" Inherits="PrintRoverWeb.Administration.CustomMessages"
    Title="" %>

<%@ Register Src="../UserControls/SettingsMenu.ascx" TagName="SettingsMenu" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");
        function IsTextChanged(rowID) {

            var objMessageControlID = eval("document.forms[0].__LocalizedMessage_" + rowID);
            var objIsTextChangedControlID = eval("document.forms[0].__IsTextChanged_" + rowID);
            var objOriginalMessageControlID = eval("document.forms[0].__OriginalMessageControlID_" + rowID);
            var objEditableChangedControlID = eval("document.forms[0].__EditableMessageControlID_" + rowID);
            var objTableRow = document.getElementById("RowID_" + rowID);
            var resetControlID = document.getElementById("__ResetImage_" + rowID);

            if (objEditableChangedControlID.value != objOriginalMessageControlID.value) {
                objTableRow.className = "GridRowOnmouseOver";
                objIsTextChangedControlID.value = "1";
                resetControlID.src = '../App_Themes/Blue/Images/undo.png';
                resetControlID.setAttribute('title', C_UNDO);
            }
            else {
                objTableRow.className = "GridRow";
                objIsTextChangedControlID.value = "0";
                resetControlID.src = '../App_Themes/Blue/Images/blank.png';
                resetControlID.setAttribute('title', C_UNDO);
            }

        }

        function ResetText(rowID) {
            var objMessageControlID = eval("document.forms[0].__LocalizedMessage_" + rowID);
            var objIsTextChangedControlID = eval("document.forms[0].__IsTextChanged_" + rowID);
            var objOriginalMessageControlID = eval("document.forms[0].__OriginalMessageControlID_" + rowID);
            var objEditableChangedControlID = eval("document.forms[0].__EditableMessageControlID_" + rowID);
            var objTableRow = document.getElementById("RowID_" + rowID);
            var resetControlID = document.getElementById("__ResetImage_" + rowID);

            objEditableChangedControlID.value = objOriginalMessageControlID.value;
            objTableRow.className = "GridRow";
            objIsTextChangedControlID.value = "0";
            resetControlID.src = '../App_Themes/Blue/Images/blank.png';
            resetControlID.alt = C_UNDO;
        }
        function myKeyPressHandler() {
            if (event.keyCode == 13) {
                document.getElementById('ctl00_PageContent_ButtonUpdate').focus();
            }
        }

        document.onkeypress = myKeyPressHandler;
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="content" style="display: none;">
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ButtonUpdate" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <%--<div style="height: 10px;"> &nbsp;</div>--%>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550"
        >
        <tr>
         <td align="right" valign="top" style="width:1px">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg" align="left">
                        <td height="35"  align="left">
                            <table cellpadding="0" cellspacing="0" border="0" align="left" width="100%">
                                <tr>
                                <td class="HeadingMiddleBg" width="10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            Custom Messages</div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image7" SkinID="HeadingRight" runat="server" />
                                    </td>
                                   
                                    <td width="3%" style="display:none">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" CausesValidation="true" SkinID="SettingsImageButtonSave"
                                            ImageAlign="Middle" ToolTip="" OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="15%">                                       
                                        <asp:Label ID="LabelHeadingCustomMessages"  Visible="false" runat="server" SkinID="TotalResource"
                                            Text=""></asp:Label>
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
                     <td align="right" style="white-space: nowrap"  valign="bottom">
                                        <asp:Table ID="Table3" runat="server" CellPadding="2" CellSpacing="0">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Label ID="LabelPageSize" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell> 
                                                    <asp:DropDownList ID="DropDownPageSize" CssClass="Normal_FontLabel" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownPageSize_SelectedIndexChanged">
                                                        <asp:ListItem Selected="true">10</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                        <asp:ListItem>100</asp:ListItem>
                                                        <asp:ListItem>200</asp:ListItem>
                                                        <asp:ListItem>500</asp:ListItem>
                                                        <asp:ListItem>1000</asp:ListItem>
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="LabelPage" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList ID="DropDownCurrentPage" runat="server" AutoPostBack="true" CssClass="Normal_FontLabel"
                                                        OnSelectedIndexChanged="DropDownCurrentPage_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="LabelTotalRecordsTitle" runat="server" SkinID="TotalResource" Text=""></asp:Label>:
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="LabelTotalRecordsValue" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" height="20">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="45%" height="35">
                                        <asp:Label ID="LabelType" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </td>
                                    <td width="55%" align="left" valign="middle">
                                        <asp:DropDownList CssClass="Dropdown_CSS" ID="DropDownListType" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="DropDownListType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display: none; height: 30px">
                                    <td align="right" height="35">
                                        <asp:Label ID="LabelModule" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                        <asp:DropDownList ID="DropDownListModule" CssClass="Dropdown_CSS" runat="server">
                                            <asp:ListItem Value="admin">Administration Module</asp:ListItem>
                                            <asp:ListItem Value="mfp">MFP</asp:ListItem>
                                            <asp:ListItem Value="comman">Common</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td align="right">
                                        <asp:Label ID="LabelLanguage" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                        <asp:DropDownList ID="DropDownListLanguage" runat="server" CssClass="Dropdown_CSS"
                                            AutoPostBack="True" OnSelectedIndexChanged="DropDownListLanguage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:UpdatePanel runat="server" ID="PaginationPanel">
                                <ContentTemplate>
                                    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                        <tr class="Grid_tr">
                                            <td>
                                                <asp:Table ID="TableMessages" CellSpacing="1" CellPadding="1" Width="100%" BorderWidth="0"
                                                    runat="server" CssClass="Table_bg" SkinID="Grid">
                                                    <asp:TableHeaderRow Width="100%" Height="30" CssClass="Table_HeaderBG">
                                                        <asp:TableHeaderCell Width="3%" HorizontalAlign="Left"  ></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell Width="47%" ID="TableHeaderCellEnglish" Text=" " HorizontalAlign="Left" ></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell Width="50%" ID="TableHeaderCellSelctedMessage" Text="" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                        <tr style="background-color: White">
                                            <td align="center" height="50">
                                                <asp:Button ID="ButtonUpdate" CssClass="Login_Button" runat="server" Text="" OnClick="ButtonUpdate_Click" />
                                                <asp:HiddenField ID="HiddenFieldTotalCount" Value="0" runat="server" />
                                                <br />
                                                <asp:Label ID="LabelSqlQuries" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                   
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
