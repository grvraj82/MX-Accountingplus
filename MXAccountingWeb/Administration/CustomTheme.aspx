<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="CustomTheme.aspx.cs" Inherits="AdministrationCustomTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">

        fnShowCellSettings();
        Meuselected("Settings");
        function togall(refcheckbox) {

            var thisForm = document.forms[0];
            var users = thisForm.__CUSTOMID.length;
            var selectedCount = 0;
            if (thisForm.__CUSTOMID[refcheckbox - 1].checked) {

                thisForm.__CUSTOMID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__CUSTOMID[refcheckbox - 1].checked = true;
            }
        }

        function DeleteUsers() {
            if (IsUnlockDeleteUserSelected()) {
                var confirmflag = true;
                if (!confirmflag) {
                    return false;
                }
                else {
                    return true;
                }

            }
            else {
                return false;
            }
        }

        function IsUnlockDeleteUserSelected() {

            try {
                var thisForm = document.forms[0];
                var users = thisForm.__CUSTOMID.length;
                var selectedCount = 0;

                if (users > 0) {
                    for (var item = 0; item < users; item++) {
                        if (thisForm.__CUSTOMID[item].checked) {
                            selectedCount++
                            return true;
                        }
                    }
                }
                else {
                    if (thisForm.__CUSTOMID.checked) {
                        selectedCount++
                        return true;
                    }
                }

                if (selectedCount == 0) {
                    jNotify(C_PLEASE_SELECT_APPLICATION_MFP_MODEL);
                    return false;
                }
            }
            catch (Error) {
                  jNotify('Please select wallpaper(s)')
                //jNotify(C_PLEASE_SELECT_APPLICATION_MFP_MODEL)
                return false;
            }
        }

        function ChkandUnchk() {
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = true;
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = false;
                }
            }
        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenUsersCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__CUSTOMID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__CUSTOMID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__CUSTOMID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }

        function UpdateUserDetails() {
            if (IsUnlockDeleteUserSelected()) {
                if (GetSeletedCount() > 1) {
                    for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                    jNotify('Select only one WallPaper')
                    return false;
                }
            }
            else {
                return false;
            }
        }

       

    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <%--<div style="height: 10px;"> &nbsp;</div>--%>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image7" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="left">
                            <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0">
                                <asp:TableRow>
                                    <%--<asp:TableCell CssClass="Menu_split" Visible="false">
                                    </asp:TableCell>--%>
                                    <asp:TableCell CssClass="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingGeneralPage" runat="server" Text=""></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="20px">
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <table cellpadding="3" cellspacing="3">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonAdd" ToolTip="" SkinID="IBAdd" Visible="true" runat="server"
                                                        CausesValidation="False" OnClick="ImageButtonAdd_Click" />
                                                </td>
                                                <td class="MenuSpliter">
                                                    <asp:ImageButton ID="ImageButtonEdit" ToolTip="" SkinID="IBEdit" runat="server" CausesValidation="False"
                                                        OnClick="ImageButtonEdit_Click" />
                                                </td>
                                                <td class="MenuSpliter">
                                                    <asp:ImageButton ID="ImageButtonDelete" ToolTip="" runat="server" CausesValidation="False"
                                                        SkinID="SettingsImageButtonWallPaperDelete" OnClick="ImageButtonDelete_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr class="Grid_tr">
                                    <td align="center">
                                        <asp:Table EnableViewState="false" ID="TableCustomThems" CellSpacing="1" CellPadding="3"
                                            Width="98%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                            <asp:TableRow CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell ID="TableHeaderCellThemeDetails" ColumnSpan="6" CssClass="H_title"
                                                    Text="">
                                                
                                                </asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellCustomThemeDetails" ColumnSpan="3" CssClass="H_title"
                                                    Text=""></asp:TableHeaderCell>
                                            </asp:TableRow>
                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell HorizontalAlign="Left" CssClass="Grid_topbg1"><input id="chkALL" onclick="ChkandUnchk()" type="checkbox" /></asp:TableHeaderCell>
                                                <asp:TableHeaderCell Width="30" CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellApplicationName" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title" Width="20%"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellThemeName" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellHeight" Text="" HorizontalAlign="Left" CssClass="H_title"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellWidth" Text="" HorizontalAlign="Left" CssClass="H_title"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellCustomWallPaper" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellCustomHeight" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellCustomWidth" Text="" HorizontalAlign="Left"
                                                    CssClass="H_title"></asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="HiddenUsersCount" Value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <div id="divBrowseCustomWallPaper" runat="server" visible="false">
                                <table width="100%" border="0" cellpadding="3" cellspacing="3">
                                    <tr>
                                        <td width="50%" valign="top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
                                                height="170">
                                                <tr class="Top_menu_bg">
                                                    <td width="50%" align="left" valign="middle">
                                                        &nbsp;<asp:Label ID="LabelThemes" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                    </td>
                                                    <td align="right" valign="middle">
                                                        <asp:Image ID="Image3" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                        <asp:Label ID="LabelRequiredField" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%" align="right" class="f10b">
                                                        <asp:Label ID="LabelSelectImage" runat="server" Text=""></asp:Label>&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="DropDownListImageType" OnSelectedIndexChanged="DropDownListImageType_SelectedIndexChanged"
                                                            runat="server" AutoPostBack="true" CssClass="Dropdown_CSS">
                                                        </asp:DropDownList>
                                                        <asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center;
                                                            padding-left: 5px;" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%" align="right" class="f10b">
                                                        <asp:Label ID="LabelUploadImage" runat="server" Text=""></asp:Label>&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:FileUpload Width="225px" ID="FileUploadCustomTheme" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%" align="right" class="f10b">
                                                        <asp:Label ID="LabelAppTheme" runat="server" Text=""></asp:Label>&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="DropDownListApplicationTheme" CssClass="Dropdown_CSS" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:Image ID="Image2" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-center;
                                                            padding-left: 5px;" />
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td width="50%" align="right" class="f10b">
                                                        <asp:Label ID="LabelAppBckColor" runat="server" Text=""></asp:Label>&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxApplicationBackgroundColor" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td width="50%" align="right">
                                                        <asp:Label ID="LabelAppTitleBar" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxTittlebarBackground" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td width="50%" align="right">
                                                        <asp:Label ID="LabelAppFontColor" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="TextBoxApplicationFontColor" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="2">
                                                            <tr>
                                                                <td width="60%" align="right">
                                                                    <asp:Button ID="ButtonUpload" CssClass="Login_Button" runat="server" Text="" OnClick="ButtonUpload_Click" />
                                                                </td>
                                                                <td width="13%" align="left">
                                                                    <asp:Button runat="server" ID="ButtonReset" Text="" CausesValidation="false" CssClass="Login_Button"
                                                                        OnClientClick="this.form.reset();return false;" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Button ID="ButtonCancel" CssClass="Login_Button" runat="server" Text="" OnClick="ButtonCancel_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
                                                height="170">
                                                <tr class="Top_menu_bg">
                                                    <td align="left">
                                                        &nbsp;<asp:Label ID="LabelNote" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;<asp:Label ID="LabelFilesize" CssClass="f10b" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="LabelFileSizeValue" ForeColor="Red" CssClass="f10b" runat="server"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;<asp:Label ID="LabelHeightWidth" CssClass="f10b" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="LabelValues" CssClass="f10b" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;<asp:Label ID="LabelNotes" CssClass="f10b" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
