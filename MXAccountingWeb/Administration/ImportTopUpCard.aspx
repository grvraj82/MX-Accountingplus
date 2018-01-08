<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true" CodeBehind="ImportTopUpCard.aspx.cs" Inherits="AccountingPlusWeb.Administration.ImportTopUpCard" %>
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
                <asp:Image ID="Image3" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" align="left" valign="top" colspan="3">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg">
                        <td class="HeadingMiddleBg" style="width: 10%">
                            <div style="padding: 4px 10px 0px 10px;">
                                <asp:Label ID="LabelUserManagement" runat="server" Text="Import Topup Card(s)"></asp:Label></div>
                        </td>
                        <td>
                            <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                        </td>
                        <td width="2%">
                        </td>
                        <td width="1%" align="left" valign="middle">
                            <asp:Label ID="LabelDummy" runat="server" Text=""></asp:Label>
                        </td>
                        <td width="1%" align="left" valign="middle">
                            &nbsp;
                            <asp:ImageButton ID="ImageButtonExportToCsv" ToolTip="Click here to download my balance"
                                runat="server" OnClick="ImageButtonExportToCsv_Click" SkinID="DownloadCSV" />
                        </td>
                        <td width="1%" class="Menu_split" style="display: none">
                        </td>
                        <td width="1%" align="left" valign="middle" style="display: none">
                            &nbsp;<asp:ImageButton ID="ImageButtonExportXml" runat="server" SkinID="JoblogimgXML"
                                OnClick="ImageButtonExportXml_Click" />
                        </td>
                        <td width="1%" class="Menu_split">
                        </td>
                        <td width="1%" align="left" valign="middle">
                            &nbsp;<asp:ImageButton ID="ImageButtonBack" runat="server" OnClick="ImageButtonBack_Click"
                                SkinID="DBUploadBackPage" />
                        </td>
                        <td width="1%" class="Menu_split">
                        </td>
                        <td width="1%" align="left" valign="middle" style="display:none">
                            &nbsp;<asp:ImageButton ID="ImageButtonHelp" ToolTip=""
                                runat="server" OnClick="ImageButtonHelp_Click" SkinID="HelpUsers" />
                        </td>
                        <td width="80%" align="left" valign="middle">
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
            <td class="CenterBG">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top" align="center" class="CenterBG">
                <div id="divHelpforcardpinMapping" runat="server" visible="false">
                    <table width="100%" border="0" cellpadding="3" cellspacing="3" class="">
                        <tr>
                            <td width="50%" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
                                    height="283">
                                    <tr class="Top_menu_bg">
                                        <td width="50%" align="left" valign="middle">
                                            &nbsp;<asp:Label ID="LabelMapUserCardPin" runat="server" Text=""></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="f10b" align="left">
                                            &nbsp;<asp:Label ID="LabelNumber" runat="server" Text="1."></asp:Label>
                                            <asp:Image ID="ImageDownLoadCSV" SkinID="DownloadCSVFile" runat="server" />
                                            &nbsp;<asp:Label ID="Label2" runat="server" Text="Download AccountingPlus Users."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="f10b" align="left">
                                            &nbsp;<asp:Label ID="Label4" runat="server" Text="2. Open downloaded file and change the 'CardId' and 'PinId' column values only. "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="f10b">
                                            &nbsp;<asp:Label ID="Label6" runat="server" Text="3. Don't change column names and 'User Name' and 'User Source' column values. "></asp:Label>
                                        </td>
                                    </tr>
                                    <td align="center">
                                        <asp:Button ID="ButtonHelpClose" CssClass="Login_Button" runat="server" Text=""
                                            OnClick="ButtonHelpClose_Click" />
                                    </td>
                                    <tr height="100">
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_border_org"
                                    height="170">
                                    <tr class="Top_menu_bg">
                                        <td width="50%" align="left" valign="middle">
                                            &nbsp;<asp:Label ID="LabelNote" runat="server" Text=""></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            &nbsp;<asp:Image ID="Image1" ImageUrl="~/App_Images/ManageuserCardandPinValues.png"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr style="margin-top: 0;">
            <td>
            </td>
            <td valign="top" align="center" style="margin-top: 0" class="CenterBG">
                <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                    <tr class="Grid_tr">
                        <td>
                            <asp:Table EnableViewState="false" ID="TableMyBalance" CellSpacing="1" CellPadding="4"
                                Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                    <asp:TableHeaderCell Width="30" CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellRechargeID" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                     <asp:TableHeaderCell Wrap="false" ID="TableHeaderIsRecharge" Text="" HorizontalAlign="Left"
                                        CssClass="H_title"></asp:TableHeaderCell>
                                          <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellAmount" Text=""
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
                            <asp:Label ID="LabelUsersSource" class="f10b" runat="server" Text="Top-up cards"></asp:Label>:&nbsp;&nbsp;&nbsp;
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
        <tr style="margin-top: 0;">
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
                            <asp:Label ID="LabelPleaseWait" runat="server" Text=""></asp:Label>
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
                                                <asp:Label ID="LabelTotalRecords" class="f10b" runat="server" Font-Bold="True"></asp:Label>
                                                <asp:Label ID="labelTotalUsers" class="f10b" runat="server" Text="" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="">
                                <tr class="CenterBG" style="display:none">
                                    <td align="center" class="" height="33" width="100%">
                                        <asp:Label ID="LabelColumnMapping" class="f10b" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="CenterBG" style="display:none">
                                    <td align="char" class="" height="25" width="100%">
                                        <table width="100%" cellpadding="0" cellspacing="3" border="0">
                                            <tr>
                                                <td align="right" width="50%" class="Grid_topbg">
                                                    <asp:Label ID="LabelDatabaseColumns" class="f10b" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td align="left" width="60%" class="Grid_topbg">
                                                    &nbsp;<asp:Label ID="LabelUsersCsvColumns" class="f10b" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="" style="display:none">
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" width="40%" valign="top">
                                                    <asp:ListBox ID="listManageUsers" runat="server" Height="150px" Width="161px"></asp:ListBox>
                                                </td>
                                                <td valign="top" width="10%">
                                                    <asp:ListBox ID="listCSVColumns" runat="server" Height="150px" Width="161px"></asp:ListBox>
                                                </td>
                                                <td valign="top" width="30%" align="left" style="visibility: hidden">
                                                    <table width="20%" height="150px">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:ImageButton ID="ImageButtonUp" runat="server" SkinID="UpArrow" OnClick="ImageButtonUp_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
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
                                <div id="divPreviewUsers" runat="server" style="display:none">
                                    <tr class="">
                                        <td align="center" class="" height="25" width="100%">
                                            <asp:Label class="f10b" ID="LabelPreviewUsers" runat="server" Text=""></asp:Label>
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
                                <div id="divDuplicateRecordsPanel" visible="false" runat="server" style="display:none">
                                    <tr>
                                        <td align="center" class="Grid_tr">
                                            <table width="50%" cellpadding="3" cellspacing="0" border="0" class="">
                                                <tr class="Grid_tr">
                                                    <td colspan="2" align="center" class="">
                                                        <asp:Label ID="LabelDuplicatesFound" runat="server" class="f10b"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td width="50%" align="right">
                                                        <asp:Label ID="LabelValidCardsText" class="f10b" Text="Total Valid Card/Pin Count:"
                                                            runat="server"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelValidCards" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td width="50%" align="right">
                                                        <asp:Label ID="Label1" class="f10b" Text="Total InValid Card/Pin Count:" runat="server"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label class="f10b" ID="LabelTotalInvalidRecords" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td width="50%" align="right">
                                                        <asp:Label ID="LabelInValidCardsText" class="f10b" Text="Invalid Cards Count:" runat="server"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelInValidCards" class="f10b" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td width="50%" align="right">
                                                        <asp:Label ID="LabelInvalidPins" class="f10b" Text="Invalid Pins Count:" runat="server"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="LabelInvalidPinsCount" class="f10b" runat="server"></asp:Label>
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