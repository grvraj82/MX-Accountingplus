<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CounterInformation.aspx.cs"
    Inherits="AccountingPlusWeb.Administration.CounterInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .Datatable_Leftalign
        {
            border: 1px solid #B7B7B7;
            border-bottom: 0 !important;
        }
        
        .Datatable_Leftalign tr td:first-child
        {
            background-color: #F1F1F1;
            line-height: 30px;
            font-family: Arial;
            font-size: 14px;
            font-weight: bold;
            color: #333333;
            padding: 0px 10px 0px 10px;
            border-bottom: 1px solid #B7B7B7;
            border-right: 1px solid #B7B7B7;
            text-align: right;
        }
        
        .Datatable_Leftalign tr td
        {
            line-height: 30px;
            font-family: Arial;
            font-size: 13px;
            font-weight: normal;
            color: #333333;
            padding: 0px 10px 0px 10px;
            border-bottom: 1px solid #B7B7B7;
            text-align: left;
        }
        
        .Datatable_Topalign
        {
            border: 1px solid #B7B7B7;
            border-bottom: 0 !important;
        }
        
        .Datatable_Topalign tr:first-child td
        {
            background-color: #F1F1F1;
            line-height: 30px;
            font-family: Arial;
            font-size: 14px;
            font-weight: bold;
            color: #333333;
            border-bottom: 1px solid #B7B7B7;
            border-right: 1px solid #B7B7B7;
        }
        
        .Datatable_Topalign tr td
        {
            line-height: 30px;
            font-family: Arial;
            font-size: 13px;
            font-weight: normal;
            color: #333333;
            padding: 0px 10px 0px 10px;
            border-bottom: 1px solid #B7B7B7;
            border-right: 1px solid #B7B7B7;
        }
        
        .Datatable_Topalign tr:first-child td .Data_Innertable tr td
        {
            line-height: 30px;
            font-family: Arial;
            font-size: 13px;
            font-weight: normal;
            color: #333333;
            padding: 0px 10px 0px 10px;
            background: #FFFFFF !important;
            border-bottom: 0 !important;
            border-right: 0 !important;
        }
        
        .Datatable_Topalign tr td .Data_Innertable tr td
        {
            line-height: 30px;
            font-family: Arial;
            font-size: 13px;
            font-weight: normal;
            color: #333333;
            padding: 0px 10px 0px 10px;
            background: #FFFFFF !important;
            border-bottom: 1px solid #B7B7B7;
            border-right: 0 !important;
        }
        
        .Padding0
        {
            padding: 0 !important;
        }
        
        .BorderBot0
        {
            border-bottom: 0 !important;
        }
    </style>
</head>
<body>
    <div>
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="HiddenFieldIPAddress" runat="server" />
        <asp:UpdatePanel runat="server" ID="Panel">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-width: 0px;
                    border-color: Highlight">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Table ID="TableBody" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:Image ID="ImageBody" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <table style="height: 20px">
                </table>
                <tr>
                    <td>
                        <table width="90%" align="center" cellpadding="0" cellspacing="0" border="0">
                            <tr class="Grid_tr">
                                <td valign="top" style="padding-bottom: 10px;">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="Datatable_Leftalign">
                                        <tr>
                                            <td width="20%">
                                                <asp:Label ID="LabelUpdatedTime" runat="server" Font-Bold="true" Text="Updated Time :"></asp:Label>
                                            </td>
                                            <td width="80%">
                                                <asp:Label ID="LabelUpdateddatetime" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelSerialNumber" runat="server" Font-Bold="true" Text="Serial Number :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelDisplaySerialNumber" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelModelNumber" runat="server" Font-Bold="true" Text="Model Number :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelDisplayModelNumber" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelIPAddress" runat="server" Font-Bold="true" Text="IP Address :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelDisplayIPAddress" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 10px;">
                                    <asp:Table ID="TableTotal" runat="server" CellPadding="0" CellSpacing="0" Width="100%"
                                        CssClass="Datatable_Topalign">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelTotalcount" runat="server" Text="Total Count" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell Visible="false">
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelCountnumber" runat="server" Text="Count Number" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelBW" runat="server" Text="Black & White Count"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell Visible="false">
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelBWcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelFullcolor" runat="server" Text="Full Color"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell Visible="false">
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelFullcolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelTwocolor" runat="server" Text="2 Color"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell Visible="false">
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelTwocolorCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelSinglecolor" runat="server" Text="Single Color"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell Visible="false">
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelSinglecolorCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>
                    </td>
                </tr>
                <tr class="Grid_tr">
                    <td>
                        <table border="0" align="center" cellpadding="0" cellspacing="0" width="90%" class="Datatable_Topalign">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LabelJobname" runat="server" Font-Bold="true" Text="Job Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LabelJobCountnumber" runat="server" Font-Bold="true" Text="Count Number"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelCopy" runat="server" Text="Copy"></asp:Label>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TableJobType" runat="server" CellPadding="0" CellSpacing="0" Width="100%"
                                        CssClass="Data_Innertable">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelJobBW" runat="server" Text="Black & White"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelJobFullcolor" runat="server" Text="Full Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelJobTwoColor" runat="server" Text="2 Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelJobSinglecolor" runat="server" Text="Single Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TableJobTypeCount" runat="server" CellPadding="0" CssClass="Data_Innertable"
                                        CellSpacing="0" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelCopyJobBWCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelCopyJobFullcolorCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelCopyJobTwocolorCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelCopyJobSinglecolorCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelPrinter" runat="server" Text="Printer"></asp:Label>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TablePrinter" runat="server" CellPadding="0" CellSpacing="0" CssClass="Data_Innertable"
                                        Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelPrinterBW" runat="server" Text="Black & White"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelPrinterFullColor" runat="server" Text="Full Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TablePrinterCount" runat="server" CellPadding="0" CssClass="Data_Innertable"
                                        CellSpacing="0" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelPrinterBWcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelPrinterFullcolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LabelInternetFaxReceive" runat="server" Text="Internet Fax Receive"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LabelInternetFaxReceiveCount" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LabelFaxReceive" runat="server" Text="Fax Receive"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LabelFaxReceiveCount" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelPrintsDocFiling" runat="server" Text="Prints(Doc Filing)"></asp:Label>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TablePrintsDocFiling" runat="server" CellPadding="0" CssClass="Data_Innertable"
                                        CellSpacing="0" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelPrintsDocFilingBW" runat="server" Text="Black & White"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelPrintsDocFilingFullcolor" runat="server" Text="Full Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelPrintsDocFilingTwocolor" runat="server" Text="2 Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Visible="false">
                                            <asp:TableCell>
                                                <asp:Label ID="LabelPrintsDocFilingSingleColor" runat="server" Text="Single Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TablePrintDocFilingCount" runat="server" CellPadding="0" CssClass="Data_Innertable"
                                        CellSpacing="0" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelPrintsDocFilingBWCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelPrintsDocFilingFullcolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelPrintsDocFilingTwocolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Visible="false">
                                            <asp:TableCell>
                                                <asp:Label ID="LabelPrintsDocFilingSinglecolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelOthers" runat="server" Text="Others"></asp:Label>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TableOthers" runat="server" CellPadding="0" CellSpacing="0" CssClass="Data_Innertable"
                                        Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelOthersBW" runat="server" Text="Black & White"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelOthersFullColor" runat="server" Text="Full Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TableOthersCount" runat="server" CellPadding="0" CellSpacing="0" CssClass="Data_Innertable"
                                        Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelOthersBWCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelOthersFullcolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelScanSend" runat="server" Text="Scan Send"></asp:Label>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TableScanSend" runat="server" CellPadding="0" CellSpacing="0" CssClass="Data_Innertable"
                                        Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelScansendBW" runat="server" Text="Black & White"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelScansendFullcolor" runat="server" Text="Full Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TableScanSendCount" runat="server" CellPadding="0" CellSpacing="0"
                                        CssClass="Data_Innertable" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelScansendBWCount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelScansendFullcolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LabelInternetFaxSend" runat="server" Text="Internet Fax Send"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LabelInternetFaxSendCount" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="LabelFaxSend" runat="server" Text="Fax Send"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LabelFaxSendCount" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LabelScantoHDD" runat="server" Text="Scan To HDD"></asp:Label>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TableScanToHDD" runat="server" CellPadding="0" CellSpacing="0" CssClass="Data_Innertable"
                                        Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelScantoHDDBW" runat="server" Text="Black & White"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelScantoHDDFullcolor" runat="server" Text="Full Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelScantoHDDTwocolor" runat="server" Text="Two Color"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                                <td class="Padding0">
                                    <asp:Table ID="TableScanToHDDCount" runat="server" CellPadding="0" CellSpacing="0"
                                        CssClass="Data_Innertable" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelScantoHDDBWcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="LabelScantoHDDFullcolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="BorderBot0">
                                                <asp:Label ID="LabelScantoHDDTwocolorcount" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="Grid_tr">
                    <td>
                        <div style="padding-top: 10px;">
                        </div>
                        <asp:Table ID="TableDeviceName" runat="server" HorizontalAlign="Center" CellPadding="0"
                            CellSpacing="0" CssClass="Datatable_Topalign" Width="90%">
                            <asp:TableRow Visible="false">
                                <asp:TableCell>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="LabelDeviceName" runat="server" Text="Device Name" Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Visible="false">
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="LabelDevicenamecount" runat="server" Text="Count Number" Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="LabelDocumentFeeder" runat="server" Text="Document Feeder"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Visible="false">
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="LabelDocumentFeederCount" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="LabelDuplex" runat="server" Text="Duplex"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Visible="false">
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="LabelDuplexCount" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </td>
                </tr>
                </table> </td> </tr>
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
    </div>
</body>
</html>
