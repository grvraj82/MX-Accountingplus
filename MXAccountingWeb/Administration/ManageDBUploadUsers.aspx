<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="ManageDBUploadUsers.aspx.cs" Inherits="PrintRoverWeb.Administration.ManageDBUploadUsers"
    Title="" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">

        fnShowCellUsers();
        Meuselected("UserID");

    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <div style="border: 0px solid black; width: 100%; display: none;">
        <div id="divStatus" style="width: 20%;">
        </div>
    </div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" align="left" valign="top" colspan="3">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg">
                        <td class="HeadingMiddleBg" style="width: 10%">
                            <div style="padding: 4px 10px 0px 10px;">
                                <asp:Label ID="LabelHeadingDBUpload" runat="server" Text="Label"></asp:Label></div>
                        </td>
                        <td>
                            <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                        </td>
                        <td width="1%" align="center" valign="middle">
                            <asp:Label ID="LabelDummy" runat="server" Text=""></asp:Label>
                        </td>
                        <td width="5%" align="center" valign="middle">
                            &nbsp;
                            <asp:ImageButton ID="ImageButtonExportToCsv" runat="server" OnClick="ImageButtonExportToCsv_Click"
                                SkinID="JoblogimgCSV" />
                        </td>
                        <td width="1%" class="Menu_split" style="display: none">
                        </td>
                        <td width="5%" align="center" valign="middle" style="display: none">
                            &nbsp;<asp:ImageButton ID="ImageButtonExportXml" runat="server" SkinID="JoblogimgXML"
                                OnClick="ImageButtonExportXml_Click" />
                        </td>
                        <td width="1%" class="Menu_split">
                        </td>
                        <td width="5%" align="left" valign="middle">
                            &nbsp;<asp:ImageButton ID="ImageButtonBack" runat="server" OnClick="ImageButtonBack_Click"
                                SkinID="DBUploadBackPage" />
                        </td>
                        <td width="83%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="2">
            <td>
            </td>
            <td colspan="3" class="CenterBG">
                &nbsp;
            </td>
        </tr>
        <tr style="margin-top: 0;">
            <td>
            </td>
            <td colspan="3" valign="top" align="center" style="margin-top: 0" class="CenterBG">
                <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                    <tr class="Grid_tr">
                        <td>
                            <asp:Table EnableViewState="false" ID="TableUsers" CellSpacing="1" CellPadding="4"
                                Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                    <asp:TableHeaderCell Width="30" CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellLoginName" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellUserName" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellPassword" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellPrintPin" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellCardID" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellEmail" Text="" HorizontalAlign="Left"
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
            </td>
            <td align="center" valign="top" colspan="3" class="CenterBG">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr height="10">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="40%" class="LoginUserFont" height="20">
                            <asp:Label ID="LabelUsersSource" class="f10b" runat="server" Text=""></asp:Label>:&nbsp;&nbsp;&nbsp;
                        </td>
                        <td colspan="2" align="left">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="50%" />
                            <asp:Button ID="ButtonUpload" CssClass="Login_Button" runat="server" OnClick="ButtonUpload_Click"
                                Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="LoginUserFont" height="20">
                            <asp:Label ID="LabelAllowedFormat" class="f10b" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr height="10">
                        <td colspan="3">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr >
            <td>
            </td>
            <td runat="server" id="tdShowUploadUsers" class="CenterBG">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="10000">
                </asp:ScriptManager>
                <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                    <ProgressTemplate>
                        <div id="progressBackgroundFilter">
                        </div>
                        <div id="processMessage">
                            <asp:Label ID="LabelPleaseWait" class="f10b" runat="server" Text=""></asp:Label>
                            <br>
                            <br />
                            <asp:Image ID="ImgLoader" SkinID="ImageLoader" runat="server" Width="64" Height="64" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel runat="server" ID="Panel">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="">
                            <tr style="margin-top: 0">
                                <td colspan="3" valign="top" align="center" style="margin-top: 0" class="">
                                    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="">
                                        <tr class="Grid_tr">
                                            <td>
                                                <div class="gvclass">
                                                    <asp:GridView ID="GridUploadedUsers" runat="server" Width="100%" OnRowCreated="GridUploadedUsers_RowCreated"
                                                        CellPadding="3" GridLines="Both" HeaderStyle-CssClass="GridViewHeaderStyle1">
                                                        <FooterStyle Width="100%" />
                                                        <RowStyle CssClass="GridViewRowStyle" Height="30px" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" height="25" width="100%">
                                                <asp:Label ID="LabelTotalRecords" SkinID="" class="f10b" runat="server" Font-Bold="false"></asp:Label>
                                                <asp:Label ID="labelTotalUsers" SkinID="" class="f10b" runat="server" Text="" Font-Bold="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="">
                                <tr class="CenterBG">
                                    <td align="center" class="" height="33" width="100%">
                                        <asp:Label ID="LabelColumnMapping" class="f10b" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="CenterBG">
                                    <td align="char" class="" height="25" width="100%">
                                        <table width="100%" cellpadding="0" cellspacing="3" border="0">
                                            <tr>
                                                <td align="right" width="50%" class="Grid_topbg">
                                                    <asp:Label class="f10b" ID="LabelDatabaseColumns" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td align="left" width="60%" class="Grid_topbg">
                                                    &nbsp;<asp:Label class="f10b" ID="LabelUsersCsvColumns" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="">
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" width="40%" valign="top">
                                                    <asp:ListBox ID="listManageUsers" runat="server" Height="150px" Width="161px"></asp:ListBox>
                                                </td>
                                                <td valign="top" width="10%">
                                                    <asp:ListBox ID="listCSVColumns" runat="server" Height="150px" Width="161px"></asp:ListBox>
                                                </td>
                                                <td valign="top" width="30%" align="left">
                                                    <table width="20%" height="150px">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:ImageButton ID="ImageButtonUp" runat="server" SkinID="UpArrow" OnClick="ImageButtonUp_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="display: none">
                                                                <asp:ImageButton ID="ImageButtonDelete" runat="server" SkinID="DeleteArrow" OnClick="ImageButtonDelete_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:ImageButton ID="ImageButtonDown" runat="server" SkinID="DownArrow" OnClick="ImageButtonDown_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <div id="divPreviewUsers" runat="server">
                                    <tr class="">
                                        <td align="center" class="" height="25" width="100%">
                                            <asp:Label ID="LabelPreviewUsers" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="Grid_tr">
                                        <td align="center">
                                            <div class="gvclass">
                                                <asp:GridView ID="GridPreview" runat="server" Width="98%" OnRowCreated="GridPreview_RowCreated"
                                                    CellPadding="3" GridLines="Both" HeaderStyle-CssClass="GridViewHeaderStyle1">
                                                    <FooterStyle CssClass="GridViewFooterStyle" Width="100%" />
                                                    <RowStyle CssClass="GridViewRowStyle" Height="30px" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </div>
                                <div id="divDuplicateRecordsPanel" visible="false" runat="server">
                                    <tr>
                                        <td align="center" class="Grid_tr">
                                            <table width="50%" cellpadding="3" cellspacing="0" border="0" class="">
                                                <tr class="">
                                                    <td colspan="2" align="center" class="">
                                                        <asp:Label ID="LabelDuplicatesFound" class="f10b" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td width="50%" align="right">
                                                        <asp:Label ID="LabelUploadedUsersText" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelUploadedUsersCount" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelDuplicateUsersText"  class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelDuplicateUsersCount" class="f10b" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td width="50%" align="right">
                                                        <asp:Label ID="LabelDuplicateCardsText" class="f10b" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelDuplicateCardsCount" class="f10b" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelDuplicatePinIdText" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelDuplicatePinidsCount" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelemptyUsernamesCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelEmptyUsernames" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelemptyPasswordsCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelEmptyPasswords" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelemptyUserIDsCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelEmptyUserIds" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tdEmptyCardIDsCount" runat="server" class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelEmptyCardIDsCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelEmptyCardIDs" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tdEmptyPinIdsCount" runat="server" class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelEmptyPinIdsCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelEmptyPinIDs" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelNonnumericPinIdsCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelNonNumericCount" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelInvalidEmailIdsCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"
                                                            Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelInvalidEmailIdssNumericCount" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="60%">
                                                        <asp:Label ID="LabelinvalidLengthCardIDsCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelInvalidlengthCard" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="60%">
                                                        <asp:Label ID="LabelinvalidLengthPinIDsCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelInvalidlengthPin" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="center" width="60%" colspan="2" class="Grid_topbg">
                                                        <asp:Label ID="LabelUploadFileDuplicatesTitle" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                    </td>
                                                </tr>
                                                <div runat="server" id="divDuplicateRecords">
                                                    <tr class="Grid_tr">
                                                        <td align="right" width="60%">
                                                            <asp:Label ID="LabelUploadFileDuplicatesUserID" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelUploadFileDuplicatesUserIDCount" class="f10b" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="Grid_tr">
                                                        <td align="right" width="60%">
                                                            <asp:Label ID="LabelUploadFileDuplicatesCardID" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelUploadFileDuplicatesCardIDCount" class="f10b" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="Grid_tr">
                                                        <td align="right" width="60%"> 
                                                            <asp:Label ID="LabelUploadFileDuplicatesPinID" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelUploadFileDuplicatesPinIDCount" class="f10b" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </div>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="60%">
                                                        <asp:Label ID="LabelUploadFileDuplicatesCount" class="f10b" runat="server" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelUploadFileDuplicates" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="50%" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="LabelValidRecords" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelValidRecordsCount" class="f10b" runat="server"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td colspan="2" align="center">
                                                        <asp:Label ID="LabelDuplicateWarning" class="f10b" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </div>
                                <div id="div1" runat="server">
                                    <tr class="">
                                        <td align="center">
                                            <asp:Button ID="ButtonIgnoreDuplicatesSave" Visible="false" runat="server" CssClass="Login_ButtonBig"
                                                OnClick="ButtonIgnoreDuplicatesSave_Click" Text="" />
                                            <asp:Button ID="ButtonInsert" runat="server" CssClass="Login_Button" OnClick="ButtonInsert_Click"
                                                Text="" />
                                            <asp:Button ID="ButtonPreview" runat="server" CssClass="Login_Button" OnClick="ButtonPreview_Click"
                                                Text="" />
                                            <asp:Button ID="ButtonClose" runat="server" CssClass="Login_Button" OnClick="ButtonClose_Click"
                                                Text="" />
                                        </td>
                                    </tr>
                                    <tr height="20">
                                        <td>
                                        </td>
                                    </tr>
                                </div>
                            </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ClientMessages">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
