<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="CounterDetails.aspx.cs" Inherits="AccountingPlusWeb.Administration.CounterDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
<asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <script type="text/javascript" src="../JavaScript/jsUpdateProgress.js"></script>
      <link href="../App_Themes/Blue/AppStyle/ApplicationStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        fnShowCellMFPs();
        Meuselected("Device");
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <asp:Panel ID="PanelCounterDetails" runat="server" Visible="true">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height: 500px">
            <tr>
                <td align="right" valign="top" style="height: 500px">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="right" valign="top" style="width: 1px">
                                <asp:Image ID="Image1" SkinID="HeadingLeft" runat="server" />
                            </td>
                            <td height="25" align="left" valign="top" class="CenterBG">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="Top_menu_bg">
                                    <tr>
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="HeadingMiddleBg">
                                                        <div style="padding: 4px 10px 0px 10px;">
                                                            <asp:Label ID="LabelHeadCounterDetails" runat="server" Text="Counter Details"></asp:Label></div>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image2" SkinID="HeadingRight" runat="server" />
                                                    </td>
                                                    <td width="20px">
                                                    </td>
                                                    <td align="left">
                                                        &nbsp;
                                                        <asp:Label ID="LabelMFPIP" runat="server" Text="MFP IP Address" Visible="false" SkinID="TotalResource"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:DropDownList ID="DropDownListMFPs" AutoPostBack="true" runat="server" Visible="false"
                                                            CssClass="FormDropDown_Small" OnSelectedIndexChanged="DropDownListMFPs_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td valign="middle" align="left" class="MenuSpliter">
                                                        <asp:ImageButton ID="ImageButtonHistory" runat="server" ToolTip="History" ImageUrl="~/App_Themes/Blue/Images/History.png"
                                                            OnClick="ImageButtonHistory_Click" />
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:ImageButton SkinID="JoblogimgCSV" CssClass="MenuSpliter" ID="ImageButtonExportToExcel"
                                                            runat="server" ToolTip="Export CSV" OnClick="ImageButtonExportToExcel_Click" />
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:ImageButton ID="ImageButtonUpdate" CssClass="MenuSpliter" ImageUrl="~/App_Themes/Blue/Images/Update.png"
                                                            ToolTip="Update" runat="server" OnClick="ImageButtonRefresh_Click" />
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:ImageButton ID="ImageButtonPublish" CssClass="MenuSpliter" ImageUrl="~/App_Themes/Blue/Images/Publish.png"
                                                            ToolTip="Publish" runat="server" OnClick="ImageButtonPublish_Click" />
                                                    </td>
                                                    <%--<td height="33" align="left" valign="middle" class="HeaderPadding" style="display:none">
                                                    <table cellpadding="3" cellspacing="3" border="0">
                                                        <tr>
                                                            <td class="f10b TextWrapping" align="left">
                                                                <asp:Label ID="LabelIPAddress" runat="server" SkinID="TotalResource" Text="MFP IP ADDRESS"></asp:Label>
                                                            </td>
                                                            <td valign="middle" align="left">
                                                                <asp:DropDownList ID="DropDownListMFPs" AutoPostBack="true" CssClass="FormDropDown_Small" OnSelectedIndexChanged="DropDownListMFPs_SelectedIndexChanged"
                                                                    runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                    <td height="33" align="left" valign="middle" class="HeaderPadding">
                                                        <table cellpadding="3" cellspacing="3" border="0">
                                                            <tr>
                                                                <td class="f10b TextWrapping" align="left">
                                                                    <asp:Label ID="PageTitle" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 1px">
                                        </td>
                                    </tr>
                                </table>
                                <%-- <table cellpadding="0" cellspacing="0" border="0" width="100%" style="padding: 30px 0px 30px 0px">
                                <tr>
                                    <td valign="top">
                                        <table align="center" width="30%" cellpadding="0" cellspacing="0" border="0" class="table_border_org">
                                            <tr>
                                                <td colspan="3" align="left" valign="top">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                        <tr class="Top_menu_bg">
                                                            <td width="60%" align="left" valign="middle">
                                                                &nbsp;<asp:Label ID="LabelDescription" runat="server" Text="IP Address" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                            </td>
                                                            <td align="right" width="10%" valign="middle">
                                                                <asp:Image ID="Image3" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                            </td>
                                                            <td align="left" width="30%">
                                                                <asp:Label ID="LabelRequiredfield" runat="server" Text="Required Fields" SkinID="Normal_FontLabel_bold"></asp:Label>
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
                                                <td align="right" height="30">
                                                    &nbsp;&nbsp;<asp:Label ID="LabelIPAddress" runat="server" Text="IP Address"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td valign="middle" align="left" colspan="2">
                                                    <asp:TextBox ID="TextBoxIPAddress" runat="server" MaxLength="100" Width="150px" TabIndex="1"></asp:TextBox>
                                                    <asp:Image ID="Imageuser" runat="server" SkinID="LogonImgRequired" Style="vertical-align: text-top;
                                                        padding-left: 5px;" />
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td align="right" height="30">
                                                    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Data"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td valign="middle" align="left" colspan="2" >
                                                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="100" Width="150px" TabIndex="1"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="white-space: nowrap;">
                                                    <asp:Button ID="ButtonOK" CssClass="Login_Button" runat="server" Text="OK" TabIndex="3"
                                                        OnClick="ButtonOK_OnClick" />
                                                </td>
                                                 <td align="center" colspan="2" style="white-space: nowrap;">
                                                    <asp:Button ID="ButtonDownloadXml" CssClass="Login_Button" runat="server" Text="Download XML" TabIndex="3"
                                                        OnClick="ButtonDownloadXml_OnClick" />
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
                        </td>
                    </tr>
                     <tr align="center" id="TableRowGroup" runat="server" visible="true">
                        <td colspan="4" height="35">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td height="10">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2" style="white-space: nowrap;">
                                        <asp:Button ID="ButtonOK" CssClass="Login_Button" runat="server" Text="OK" TabIndex="3" />
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
            </td>
        </tr>
    </table>--%>
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <table cellpadding="0" width="100%" cellspacing="0" border="0">
                                                <tr>
                                                    <td height="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" valign="top">
                                                        <table border="0" cellpadding="0" align="center" width="98%" cellspacing="0" class="TableGridColor">
                                                            <tr class="Grid_tr">
                                                                <td colspan="2'" align="center" valign="middle">
                                                                    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
                                                                        <asp:UpdateProgress ID="updateProgress" DisplayAfter="0" runat="server">
                                                                            <ProgressTemplate>
                                                                                <div style="position: relative; vertical-align: middle; text-align: center;">
                                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                        <tr>
                                                                                            <td width="20%" valign="middle">
                                                                                                <asp:Image ID="Image11" SkinID="ImportLdapUsersloading" runat="server" Style="vertical-align: middle" />
                                                                                            </td>
                                                                                            <td width="80%" align="left" valign="middle">
                                                                                                <asp:Label ID="LabelPageLoading" runat="server" Text="Retrieving Active Directory User information. <br> Please Wait ..."
                                                                                                    class="f10b"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </asp:Panel>
                                                                    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
                                                                        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
                                                                    <asp:UpdatePanel ID="UpdatePanelRefresh" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Table EnableViewState="false" Width="100%" ID="TableCounterDetails" HorizontalAlign="Left"
                                                                                CellPadding="4" CellSpacing="1" BorderWidth="0" runat="server" SkinID="DoubleHeaderGrid">
                                                                                <asp:TableRow SkinID="TableDoubleRow">
                                                                                    <%--<asp:TableCell RowSpan="2" Wrap="false" Width="40px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableCell>--%>
                                                                                    <asp:TableCell RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellDate" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Date</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellMfpMACAddress" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">MAC Address</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellModelName" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Model Name</asp:TableCell>
                                                                                    <%--<asp:TableCell ID="TableCellMfpIPAddress" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">IP Address</asp:TableCell>--%>
                                                                                    <asp:TableCell ID="TableCellSerialNo" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Serial No</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellCounter" ColumnSpan="30" Wrap="false" CssClass="DoubleRowHeaderSmall">Counter</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellToner" ColumnSpan="4" CssClass="DoubleRowHeaderSmall">Toner</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellPaper" ColumnSpan="6" CssClass="DoubleRowHeaderSmall">Paper</asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow Height="24px" SkinID="TableDoubleRow">
                                                                                    <asp:TableCell ID="TableCellPrints" Wrap="false" CssClass="DoubleRowHeaderSmall">Prints</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellBWPrintCount" Wrap="false" CssClass="DoubleRowHeaderSmall">BW Print</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFullColorPrintCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Full Color Print</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellDuplex" Wrap="false" CssClass="DoubleRowHeaderSmall">Duplex</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellCopies" Wrap="false" CssClass="DoubleRowHeaderSmall">Copies</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellBWCopyCount" Wrap="false" CssClass="DoubleRowHeaderSmall">BW Copy</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFullColorCopyCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Full Color Copy</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTwoColorCopyCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Two Color Copy</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellSingleColorCopyCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Single Color Copy</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellBWTotalCount" Wrap="false" CssClass="DoubleRowHeaderSmall">BW Total</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFullColorTotalCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Full Color Total</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTwoColorTotalCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Two Color Total</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellSingleColorTotalCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Single Color Total</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellBWOtherCount" Wrap="false" CssClass="DoubleRowHeaderSmall">BW Other</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFullColorOtherCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Full Color Other</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellScanToHDD" Wrap="false" CssClass="DoubleRowHeaderSmall">Scan To HDD</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellBWScanToHDDCount" Wrap="false" CssClass="DoubleRowHeaderSmall">BW Scan To HDD</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFullColorScanToHDDCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Full Color Scan To HDD</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTwoColorScanToHDDCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Two ColorScan To HDD</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellPrintsDocumentFilling" Wrap="false" CssClass="DoubleRowHeaderSmall">Prints Document Filing</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellBWDocumentFilingPrintCount" Wrap="false" CssClass="DoubleRowHeaderSmall">BW Document Filing Print</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFullColorDocumentFilingPrintCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Full Color Document Filing Print</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTwoColorDocumentFilingPrintCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Two Color Document Filing Print</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellDocumentFeeder" Wrap="false" CssClass="DoubleRowHeaderSmall">Document Feeder</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFaxsend" Wrap="false" CssClass="DoubleRowHeaderSmall">Fax Send</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFaxreceive" Wrap="false" CssClass="DoubleRowHeaderSmall">Fax Receive</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellIFaxsendcount" Wrap="false" CssClass="DoubleRowHeaderSmall">I-Fax Send Count</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellScantoEmailFTP" Wrap="false" CssClass="DoubleRowHeaderSmall">Scan to Email/FTP</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellBWScannerCount" Wrap="false" CssClass="DoubleRowHeaderSmall">BW Scanner</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellFullColorScannerCount" Wrap="false" CssClass="DoubleRowHeaderSmall">Full Color Scanner</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellCyan" Wrap="false" CssClass="DoubleRowHeaderSmall">Cyan</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellYellow" Wrap="false" CssClass="DoubleRowHeaderSmall">Yellow</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellMagenta" Wrap="false" CssClass="DoubleRowHeaderSmall">Magenta</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellBlack" Wrap="false" CssClass="DoubleRowHeaderSmall">Black</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTray1" Wrap="false" CssClass="DoubleRowHeaderSmall">Tray1</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTray2" Wrap="false" CssClass="DoubleRowHeaderSmall">Tray2</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTray3" Wrap="false" CssClass="DoubleRowHeaderSmall">Tray3</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTray4" Wrap="false" CssClass="DoubleRowHeaderSmall">Tray4</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTray5" Wrap="false" CssClass="DoubleRowHeaderSmall">Tray5</asp:TableCell>
                                                                                    <asp:TableCell ID="TableCellTray6" Wrap="false" CssClass="DoubleRowHeaderSmall">Tray6</asp:TableCell>
                                                                                </asp:TableRow>
                                                                            </asp:Table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
