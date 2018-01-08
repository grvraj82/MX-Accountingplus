<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ReportWithChart.aspx.cs" Inherits="AccountingPlusWeb.Reports.ReportWithChart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
    <meta name="GENERATOR" content="MSHTML 11.00.9600.16521">
    <link href="../th/base.css" rel="stylesheet" type="text/css" />
    <script src="../th/jquery.min.js" type="text/javascript"></script>
    <script src="../th/codehighlighter.js" type="text/javascript"></script>
    <script src="../th/javascript.js" type="text/javascript"></script>
    <link href="../th/jquery-ui-1.8.4.custom.css" rel="stylesheet" type="text/css" />
    <script src="../th/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../th/jquery.fixheadertable.min.js" type="text/javascript"></script>
    <script type="text/javascript">

//        function printGrid() {
//            var gridData = document.getElementById('<%= TablePrintReport.ClientID %>');
//            var windowUrl = 'about:blank';

//            //set print document name for gridview
//            var uniqueName = new Date();
//            var windowName = 'Print_' + uniqueName.getTime();

//            var prtWindow = window.open(windowUrl, windowName, 'left=100,top=100,right=100,bottom=100');
//            prtWindow.document.write('<html><head></head>');
//            prtWindow.document.write('<body style="background:none !important">');
//            prtWindow.document.write(gridData.outerHTML);
//            prtWindow.document.write('</body></html>');
//            prtWindow.document.close();
//            prtWindow.focus();
//            prtWindow.print();
//            prtWindow.close();

//        }
    </script>
    <style type="text/css">
        body
        {
            font-family: Verdana,Arial,Geneva,Helvetica,sans-serif;
            font-size: 10px;
        }
        
        pre
        {
            padding: 5px;
            font-size: 12px;
            border: 2px solid #F0F0F0;
            background: #F5F5F5;
            width: 100%;
            display: none;
            width: 800px;
        }
        
        .javascript .comment
        {
            color: green;
        }
        
        .javascript .string
        {
            color: maroon;
        }
        
        .javascript .keywords
        {
            font-weight: bold;
        }
        
        .javascript .global
        {
            color: blue;
            font-weight: bolder;
        }
        
        .javascript .brackets
        {
            color: Gray;
        }
        
        .javascript .thing
        {
            font-size: 10px;
        }
        
        span.text
        {
            font-weight: normal;
            font-style: italic;
            margin-left: 10px;
        }
        
        div.title
        {
            font-size: 18px;
            padding: 15px 0;
            font-weight: bold;
        }
        
        div.title span
        {
            font-weight: normal;
        }
        
        div.themes
        {
            overflow: hidden;
            width: 150px;
            position: fixed;
            top: 180px;
            left: 10px;
        }
        
        div.themes button
        {
            width: 120px;
            margin-bottom: 5px;
        }
        
        div.themes a
        {
            display: block;
            font-size: 1.1em;
            margin-bottom: 5px;
            text-decoration: none;
            padding: 3px;
            width: 120px;
        }
        
        div.themes a:focus
        {
            outline: none;
        }
        
        div.themes a.top
        {
            color: black;
        }
        
        div.themes a.top:hover
        {
            text-decoration: underline;
        }
        .Grid
        {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: "segoe ui" , Verdana, Arial, Helvetica;
            font-weight: normal;
            color: #474747;
        }
        
        .Grid td
        {
            padding: 5px;
            border: solid 1px #c1c1c1;
        }
        
        .Grid th
        {
            background-image: url('../App_Themes/Blue/Images/Table_HeaderBG.png');
            background-repeat: repeat-x;
            background-repeat-y: 0%;
            font: normal 12px "segoe ui" , Verdana, Arial, Helvetica;
            text-decoration: none;
            font-weight: normal;
            color: #000000;
            height: 30px;
        }
        
        .Grid .alt
        {
            background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
        }
        
        .Grid .pgr
        {
            background: #363670 url(Images/grid-pgr.png) repeat-x top;
        }
        
        .Grid .pgr table
        {
            margin: 3px 0;
        }
        
        .Grid .pgr td
        {
            border-width: 0;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }
        
        .Grid .pgr a
        {
            color: Gray;
            text-decoration: none;
        }
        
        .Grid .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellReports();
        Meuselected("Reports");
    </script>
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image7" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="middle" class="CenterBG">
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr class="Top_menu_bg" height="33" align="left">
                        <td>
                            <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell class="HeadingMiddleBg" Width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelSummary" runat="server" Text="Report-Print/Copies"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Table ID="Table2" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                            Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell ID="tabelCellLabelUserSource" Visible="true" align="center" valign="middle"
                                                    CssClass="NoWrap" runat="server">
                                                    <asp:Label ID="LabelFilter" runat="server" Text="Filter by" SkinID="TotalResource"></asp:Label>
                                                    :&nbsp;&nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellFilterDropdown" Visible="true" align="center" valign="middle"
                                                    CssClass="NoWrap" runat="server">
                                                    <asp:DropDownList ID="DropDownFilter" CssClass="FormDropDown_Small" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownFilter_SelectedIndexChanged"
                                                        CausesValidation="True">
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell3" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;<asp:Label ID="LabelFromDate" runat="server" Text="From Date" SkinID="TotalResource"></asp:Label>
                                                    :&nbsp;&nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell4" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    <asp:DropDownList ID="DropDownListFromMonth" runat="server">
                                                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownListFromDate" runat="server">
                                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownListFromYear" runat="server">
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell6" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;<asp:Label ID="LabelToDate" runat="server" Text="To Date" SkinID="TotalResource"></asp:Label>
                                                    :&nbsp;&nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell7" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    <asp:DropDownList ID="DropDownListToMonth" runat="server">
                                                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownListToDate" runat="server">
                                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownListToYear" runat="server">
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell2" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap" HorizontalAlign="Left">
                                                    &nbsp;&nbsp;<asp:Button ID="ButtonGenerate" runat="server" Text="" OnClick="ButtonGenerate_click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell9" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image1" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellPrint" align="center" Visible="false" valign="middle" runat="server"
                                                   CssClass="NoWrap" HorizontalAlign="Left">
                                                    &nbsp;&nbsp;<asp:Button ID="ButtonPrint" runat="server" Text="" OnClick="ButtonPrint_click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell5" align="center" Visible="false" valign="middle" runat="server"
                                                    CssClass="NoWrap">
                                                   <%-- &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonExportToCsv" SkinID="JoblogimgCSV" ToolTip=""
                                                        runat="server" OnClick="ImageButtonExportToCsv_Click" />--%>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell8" align="center" valign="middle" runat="server" Visible="false"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image5" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell13" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonRefresh" SkinID="ManageMfpSimgRefresh"
                                                        runat="server" OnClick="ImageButtonRefresh_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell1" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image2" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellExcel" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonExcel" ImageUrl="~/App_Themes/Blue/Images/excel-icon.png"
                                                        runat="server" ToolTip="Export to Excel" OnClick="ImageButtonExcel_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell11" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image3" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellHtml" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonHtml" ImageUrl="~/App_Themes/Blue/Images/html.png"
                                                        runat="server" ToolTip="Export to Html" OnClick="ImageButtonHtml_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellPdf" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle" ToolTip="Export to Pdf">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonPdf" runat="server" ImageUrl="~/App_Themes/Blue/Images/Pdf.png"
                                                        OnClick="ImageButtonPdf_Click" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                    <asp:TableCell Width="50%"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="TablePrintReport" runat="server" cellpadding="0" cellspacing="0" border="0"
                    width="100%">
                    <tr>
                        <td colspan="2">
                            <table id="TablePrintReportHeader" runat="server" visible="false" width="100%" border="0"
                                align="center" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td colspan="3" align="center">
                                        Accounting Plus Standard
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        Detail Report
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelFromRt" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelToRt" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelFilterRt" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="center" class=" CenterBG">
                            <asp:Chart ID="Top10Reports" runat="server" Height="400px" Width="800px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                ImageType="Png" Palette="None" BorderWidth="2" BackColor="Transparent">
                                <Legends>
                                    <asp:Legend Enabled="false" IsTextAutoFit="true" Title="Pages" Name="Default" BackColor="Transparent"
                                        Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 12pt, style=Bold">
                                    </asp:Legend>
                                </Legends>
                                <Series>
                                    <asp:Series BorderWidth="2" Name="Color" ChartType="StackedColumn" ShadowColor="64, 0, 0, 0"
                                        ShadowOffset="0">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                        BackGradientStyle="None" BackColor="Transparent">
                                        <Area3DStyle Rotation="10" Perspective="10" Inclination="05" IsRightAngleAxes="false"
                                            WallWidth="0" IsClustered="False"></Area3DStyle>
                                        <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero="True">
                                            <LabelStyle Font="Calibri, 9pt, style=Regular" Format="N0" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisY>
                                        <AxisX Interval="1" LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                            <LabelStyle Font="Calibri, 9pt, style=Regular" Format="N0" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                            <MajorTickMark Size="2" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 2">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td class=" Table_HeaderES">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="center" class="CenterBG" style="width: 50%; margin-left: 10%">
                                        <asp:Chart ID="TotalVolumeBreakdown" runat="server" Height="300px" Width="600px"
                                            ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="EarthTones"
                                            BackColor="Transparent">
                                            <Legends>
                                                <asp:Legend Enabled="true" IsTextAutoFit="true" Title="Color Mode" Name="Default"
                                                    BackColor="Transparent" Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 13pt, style=Bold">
                                                </asp:Legend>
                                            </Legends>
                                            <Series>
                                                <asp:Series BorderWidth="2" Name="User" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                                    ShadowOffset="0">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                                    BackGradientStyle="None" BackColor="Transparent" BorderWidth="0">
                                                    <Area3DStyle Rotation="0" />
                                                    <AxisY LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisY>
                                                    <AxisX LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </td>
                                    <td align="center" class="CenterBG" style="width: 50%">
                                        <asp:Chart ID="CopyPrint" runat="server" Height="300px" Width="600px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                            ImageType="Png" Palette="Berry" BackColor="Transparent">
                                            <Legends>
                                                <asp:Legend Enabled="true" IsTextAutoFit="true" Title="Color Mode" Name="Default"
                                                    BackColor="Transparent" Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 13pt, style=Bold">
                                                </asp:Legend>
                                            </Legends>
                                            <Series>
                                                <asp:Series BorderWidth="2" Name="User" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                                    ShadowOffset="0">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                                    BackGradientStyle="None" BackColor="Transparent">
                                                    <Area3DStyle Rotation="10" Perspective="20" Inclination="15" IsRightAngleAxes="False"
                                                        WallWidth="0" IsClustered="False"></Area3DStyle>
                                                    <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero="True">
                                                        <LabelStyle Font="Calibri, 9pt, style=Regular" Format="N0" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisY>
                                                    <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                                        <LabelStyle Font="Calibri, 9pt, style=Regular" Format="N0" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                        <MajorTickMark Size="2" />
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" valign="top" class="CenterBG" align="">
                            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="3">
                                <tr class="Grid_tr">
                                    <td valign="top" align="">
                                        <asp:Table EnableViewState="false" Visible="true" ID="TableReportWithChart" Width="100%"
                                            CssClass="Table_bg AlternateRow" HorizontalAlign="Left" CellPadding="0" CellSpacing="1" BorderWidth="0"
                                            runat="server">
                                            <asp:TableHeaderRow Height="30px" CssClass="Table_HeaderBG BorderBottomForHeader">
                                                <asp:TableHeaderCell ID="TableHeaderCellSlno" HorizontalAlign="Center" 
                                                    CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                    Text="#"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellUsername" HorizontalAlign="Center" 
                                                    CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                    Text="User Name"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellBW" HorizontalAlign="Center" 
                                                    CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                    Text="BW"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellColor" HorizontalAlign="Center" 
                                                    CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                    Text="Color "></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellCopy" HorizontalAlign="Center" 
                                                    CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                    Text="Copy "></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellPrint" HorizontalAlign="Center" 
                                                    CssClass="H_title BorderRight " Wrap="false" Font-Bold="true" Text="Print"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellScan" HorizontalAlign="Center" 
                                                    CssClass="H_title BorderRight" Wrap="false" Font-Bold="true" Text="Scan"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellDuplex" HorizontalAlign="Center" 
                                                    CssClass="H_title BorderRight" Wrap="false" Font-Bold="true" Text="Duplex"></asp:TableHeaderCell>
                                                <asp:TableHeaderCell ID="TableHeaderCellPricing" HorizontalAlign="Center"  CssClass="H_title BorderRight"
                                                    Wrap="false" Font-Bold="true" Text="Pricing"></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell ID="TableHeaderCellTotal" HorizontalAlign="Center" Visible="false"  CssClass="H_title BorderRight"
                                                    Wrap="false" Font-Bold="true" Text="Total"></asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                        <asp:GridView ID="gridViewReport" runat="server" Visible="false" BackColor="White"
                                            BorderWidth="1px" BorderStyle="None" CellPadding="0" GridLines="Both" CellSpacing="0"
                                            AutoGenerateColumns="False" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                                            PagerStyle-CssClass="pgr" OnRowCreated="gridViewReport_RowCreated" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="slno" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center"
                                                    HeaderText="#" SortExpression="" />
                                                <asp:BoundField DataField="" HeaderText="" SortExpression="" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="TotalBW" HeaderText="BW" SortExpression="" ItemStyle-Height="20"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="TotalColor" HeaderText="Color" SortExpression="" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Copy" HeaderText="Copy" SortExpression="" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Prints" HeaderText="Print" SortExpression="" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Scan" HeaderText="Scan" SortExpression="" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Duplex" HeaderText="Duplex" SortExpression="" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Pricing" HeaderText="Price(units)" SortExpression="" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%-------------------------------------------JobTypeChartSummary----------------------------------------------------%>
    </table>
</asp:Content>
