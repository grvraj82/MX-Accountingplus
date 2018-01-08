<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="FleetMonitorReports.aspx.cs" Inherits="AccountingPlusWeb.Reports.FleetMonitorReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellReports();
        Meuselected("Reports");
        function OpenFleetTrends() {
            var width = 1024;
            var height = 750;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var windowFeatures = "width=" + width + ",height=" + height + ",scrollbars=yes,resizable=yes,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;

            window.open('FleetTrends.aspx', 'FleetReports', windowFeatures);
            //'width=1024,height=800,location=yes,scrollbars=no,resizable=yes');
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%" border="0" class="table_border_org"
        height="550">
        <tr>
            <td valign="top" class="CenterBG">
                <asp:UpdateProgress ID="UpdateProgress" runat="server" Visible="false">
                    <ProgressTemplate>
                        <asp:Label ID="LabelUpdatingPleaseWait" runat="server" Text=""></asp:Label><br />
                        <asp:Image ID="ImgUpdateProgress" runat="server" SkinID="UpdateProgress" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel runat="server" ID="Panel">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr class="Top_menu_bg" valign="top" height="33" align="left">
                                <td width="2%" align="center">
                                    <asp:ImageButton ID="ImageButtonBack" SkinID="FleetMonitorReportsBackPage" runat="server" 
                                        CausesValidation="False" ToolTip="" PostBackUrl="~/Administration/ManageDevice.aspx" />
                                </td>
                                <td width="2%" class="Menu_split" id="BackButtonSplit" runat="server" align="right">
                                </td>
                                <td valign="middle" width="8%" align="center">
                                    &nbsp;<asp:Label ID="LabelDevices" runat="server" Text="Device :"></asp:Label>
                                </td>
                                <td width="10%" valign="middle" align="left">
                                    &nbsp;&nbsp;<asp:DropDownList ID="DropDownListDevices" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="DropDownListDevices_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td width="87%" align="right" valign="middle">
                                    <asp:Button ID="ButtonTrends" runat="server" Text="Display Trends" OnClientClick="javascript:OpenFleetTrends()"
                                        Visible="true" />
                                </td>
                            </tr>
                            <tr height="2">
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center" colspan="5">
                                    <table width="98%" border="0" cellpadding="0" cellspacing="0">
                                        <tr class="Grid_tr">
                                            <td align="center">
                                                <table width="98%" class="table_border_org" cellpadding="0" cellspacing="0" border="0">
                                                    <tr class="Top_menu_bg">
                                                        <td class="f10b" height="35" colspan="3" align="left">
                                                            &nbsp;<asp:Label ID="LabelProperties" runat="server" Text="Device Properties"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="29%">
                                                            <table cellpadding="0" cellspacing="8" border="0" width="100%">
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Image ID="ImageMfp" SkinID="FleetMonitorReportsMfp"  runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="1px" bgcolor="">
                                                        </td>
                                                        <td width="70%" valign="top">
                                                            <table cellpadding="0" cellspacing="8" border="0" width="70%">
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelUnitSerial" runat="server" Text="Serial number :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelDeviceSerialnumber" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelName" runat="server" Text="Name :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelDeviceName" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelModel" runat="server" Text="Model :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelDeviceModel" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelLocation" runat="server" Text="Location :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelDeviceLocation" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelStatus" runat="server" Text="Status :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabeldeviceStatus" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelImpressionCount" runat="server" Text="Impression Count :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelDeviceImpCount" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelIpAddress" runat="server" Text="IP Address :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelDeviceIP" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelMacAdd" runat="server" Text="MAC Address :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelDeviceMac" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelStatusUpdate" runat="server" Text="Last Status Update :"></asp:Label>
                                                                    </td>
                                                                    <td width="50%" align="left" class="MFPNormal_FontLabel">
                                                                        <asp:Label ID="LabelDeviceLastStatusUpdate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr class="Top_menu_bg">
                                                        <td class="f10b" height="35" colspan="3" align="left">
                                                            &nbsp;<asp:Label ID="Label1" runat="server" Text="Device Usage (Output)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr height="2">
                                                        <td colspan="3">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="center">
                                                            <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                                                <tr class="Grid_tr">
                                                                    <td>
                                                                        <asp:Table ID="TableFleeetReports" runat="server" CellPadding="0" CellSpacing="1"
                                                                            Width="100%" CssClass="Table_bg">
                                                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                <asp:TableHeaderCell CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Total</asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Black & White</asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Full Color</asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell CssClass="Grid_topbg1">2 Color</asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Single Color</asp:TableHeaderCell>
                                                                            </asp:TableHeaderRow>
                                                                            <asp:TableRow runat="server" ID="TableRowGrandTotal">
                                                                                <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                    <asp:Label ID="LabelTotal" runat="server" Text="Total"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelGrandTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelGrandBW" runat="server" Text=""></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelGrandColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelGrandTwoColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelGrandSingleColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <asp:TableRow runat="server" ID="TableRowBW">
                                                                                <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                    <asp:Label ID="LabelCopy" runat="server" Text="Copy"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelCopyTotal" runat="server" Text=""></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelCopyBW" runat="server" Text=""></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelCopyColor" runat="server" Text=""></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelCopyTwoColor" runat="server" Text=""></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelCopySingleColor" runat="server" Text=""></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <asp:TableRow runat="server" ID="TableRowFullColor">
                                                                                <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                    <asp:Label ID="LabelPrint" runat="server" Text="Print"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelPrintTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelPrintBW" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelPrintColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelPrintTwoColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelPrintSingleColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <asp:TableRow runat="server" ID="TableRowTwoColor">
                                                                                <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                    <asp:Label ID="LabelFaxRecieve" runat="server" Text="Internet Fax Receive"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelFaxReceiveTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelFaxReceiveBW" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelFaxReceiveColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelFaxReceiveTwoColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelFaxReceiveSingleColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <asp:TableRow runat="server" ID="TableRowSingleColor">
                                                                                <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                    <asp:Label ID="LabelDocumentFiling" runat="server" Text="Prints (Document Filing)"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelDocumentFilingTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelDocumentFilingBW" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelDocumentFilingColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelDocumentFilingTwoColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelDocumentFilingSingleColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <asp:TableRow runat="server" ID="TableRowOthers">
                                                                                <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                    <asp:Label ID="LabelOthers" runat="server" Text="Others"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelOthersTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelOthersBW" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelOthersColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelOthersTwoColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="LabelOthersSingleColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                            </asp:TableRow>
                                                                        </asp:Table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr height="2">
                                                        <td colspan="3">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="center" colspan="3">
                                                            <table width="100%" class="" cellpadding="0" cellspacing="0" border="0">
                                                                <tr class="Top_menu_bg">
                                                                    <td class="f10b" height="35" colspan="3" align="left">
                                                                        &nbsp;<asp:Label ID="LabelDeviceUsageSend" runat="server" Text="Device Usage (Send)"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr height="2">
                                                                    <td colspan="3">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center">
                                                                        <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                                                            <tr class="Grid_tr">
                                                                                <td>
                                                                                    <asp:Table ID="TableUsageSend" runat="server" CellPadding="0" CellSpacing="1" Width="100%"
                                                                                        CssClass="Table_bg">
                                                                                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                            <asp:TableHeaderCell CssClass="Grid_topbg1"></asp:TableHeaderCell>
                                                                                            <asp:TableHeaderCell CssClass="Grid_topbg1">Total</asp:TableHeaderCell>
                                                                                            <asp:TableHeaderCell CssClass="Grid_topbg1">Black & White</asp:TableHeaderCell>
                                                                                            <asp:TableHeaderCell CssClass="Grid_topbg1">Full Color</asp:TableHeaderCell>
                                                                                            <asp:TableHeaderCell CssClass="Grid_topbg1">2 Color</asp:TableHeaderCell>
                                                                                            <asp:TableHeaderCell CssClass="Grid_topbg1">Single Color</asp:TableHeaderCell>
                                                                                        </asp:TableHeaderRow>
                                                                                        <asp:TableRow runat="server" ID="TableRowSendTotal">
                                                                                            <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                                <asp:Label ID="LabelUsageTotal" runat="server" Text="Total"></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelUsageSendTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelGrandSendBW" runat="server" Text=""></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelGrandSendColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelGrandSendTwoColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelGrandSendSingleColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                        </asp:TableRow>
                                                                                        <asp:TableRow runat="server" ID="TableRowScanSend">
                                                                                            <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                                <asp:Label ID="LabelScanSend" runat="server" Text="Scan Send"></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanSendTotal" runat="server" Text=""></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanSendBW" runat="server" Text=""></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanSendColor" runat="server" Text=""></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanSendTwoColor" runat="server" Text=""></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanSendSingleColor" runat="server" Text=""></asp:Label>
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>
                                                                                        <asp:TableRow runat="server" ID="TableRowInternetFaxSend">
                                                                                            <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                                <asp:Label ID="LabelInternetFaxSend" runat="server" Text="Internet Fax Send"></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelFaxSendTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelFaxSendBW" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelFaxSendColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelFaxSendTwoColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelFaxSendSingleColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                        </asp:TableRow>
                                                                                        <asp:TableRow runat="server" ID="TableRowScanToHDD">
                                                                                            <asp:TableCell Style="padding-left: 5px;" HorizontalAlign="Left" Width="30%" CssClass="MFPNormal_FontLabel">
                                                                                                <asp:Label ID="LabelScanToHDD" runat="server" Text="Scan to HDD"></asp:Label>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanToHDDTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanToHDDBW" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanToHDDColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanToHDDTwoColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                            <asp:TableCell>
                                                                                                <asp:Label ID="LabelScanToHDDSingleColor" runat="server" Text=""></asp:Label></asp:TableCell>
                                                                                        </asp:TableRow>
                                                                                    </asp:Table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr height="2">
                                                                    <td colspan="3">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr height="2">
                                                        <td colspan="3">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
