<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="QuickJobSummary.aspx.cs" Inherits="AccountingPlusWeb.Reports.QuickJobSummary" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">

        $(document).ready(function () {

            $('#ctl00_PageContent_TableQuickReport').fixheadertable({
                caption: 'My employees (2007)',
                colratio: [100, 150, 150, 150, 220, 150],
                height: 300,
                width: 800,
                zebra: true,
                sortable: true,
                sortedColId: 1,
                resizeCol: true,
                pager: true,
                rowsPerPage: 10,
                sortType: ['string', 'integer', 'string', 'string', 'string', 'string']
            });
        });

        function printGrid() {
            var gridData = document.getElementById('<%= TablePrintQuickJobSummary.ClientID %>');
            var windowUrl = 'about:blank';

            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName, 'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }

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
                                            <asp:Label ID="LabelHeadQuickJobTypeSummary" runat="server" Text=""></asp:Label>
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
                                                <asp:TableCell ID="TableCellPrint" align="center" valign="middle" runat="server"
                                                    Visible="false" CssClass="NoWrap" HorizontalAlign="Left">
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
                                                <asp:TableCell ID="TableCellPdf" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle" ToolTip="Export to Pdf">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonPdf" runat="server" ImageUrl="~/App_Themes/Blue/Images/Pdf.png"
                                                        OnClick="ImageButtonPdf_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellExcel" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle" ToolTip="Export to Excel">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonExcel" runat="server" ImageUrl="~/App_Themes/Blue/Images/excel-icon.png"
                                                        OnClick="ImageButtonExcel_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell11" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image3" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellHtml" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle" ToolTip="Export to Html">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonHtml" runat="server" ImageUrl="~/App_Themes/Blue/Images/html.png"
                                                        OnClick="ImageButtonHtml_Click" />
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
        <asp:Panel ID="PanelPrint" runat="server">
            <tr>
                <td colspan="3" style="height: 2">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2" valign="top" class="CenterBG">
                    <asp:UpdatePanel runat="server" ID="PaginationPanel">
                        <ContentTemplate>
                            <table width="90%" border="0" align="center" cellpadding="3" cellspacing="3">
                                <tr class="Grid_tr" align="center">
                                    <td align="center">
                                        <table id="TablePrintQuickJobSummary" runat="server" width="80%">
                                            <tr>
                                                <td>
                                                    <table id="TablePrintQuickJobSummaryHeader" runat="server" visible="false" width="100%"
                                                        border="0" align="center" cellpadding="3" cellspacing="3">
                                                        <tr>
                                                            <td colspan="3" align="center">
                                                                Accounting Plus Standard
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="center">
                                                                Quick Job Summary Report
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LabelFromRt" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelToRt" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:Label ID="LabelFilterRt" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Table EnableViewState="false" Visible="true" ID="TableQuickReport" Width="100%"
                                                        CssClass="Table_bg AlternateRow" HorizontalAlign="Center" CellPadding="0" CellSpacing="1" BorderWidth="0"
                                                        runat="server">
                                                    </asp:Table>
                                                    <%--<asp:GridView ID="gridViewReport" runat="server" Visible="false" BackColor="White"
                                                        BorderWidth="1px" BorderStyle="None" CellPadding="0" GridLines="Both" CellSpacing="0"
                                                        AutoGenerateColumns="False" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                                                        PagerStyle-CssClass="pgr" OnRowCreated="gridViewReport_RowCreated" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="JobType" HeaderText="Job Type" SortExpression="" />
                                                            <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="" />
                                                            <asp:BoundField DataField="BW" HeaderText="BW" SortExpression="" />
                                                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="" />
                                                        </Columns>
                                                    </asp:GridView>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <%-------------------------------------------JobTypeChartSummary----------------------------------------------------%>
            <tr>
                <td>
                </td>
                <td align="center" class="CenterBG">
                    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="3">
                        <tr>
                            <%--<td align="center" colspan="4" class=" Table_HeaderES">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelPrintGraph" runat="server" Text=""></asp:Label>
                        </td>--%>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Chart ID="ChartPrint" runat="server" Height="200px" Width="350px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                    ImageType="Png" BorderlineDashStyle="Solid" Palette="Fire" BorderWidth="2" BackColor="Transparent"
                                    BorderColor="181, 64, 1" BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                                    <Legends>
                                        <asp:Legend Enabled="true" IsTextAutoFit="true" Title="" Name="Default" BackColor="Transparent"
                                            Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 13pt, style=Bold">
                                        </asp:Legend>
                                    </Legends>
                                    <Series>
                                        <asp:Series BorderWidth="0" Name="PrintUser" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                            BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                            BackGradientStyle="None" BackColor="Transparent">
                                            <Area3DStyle Rotation="10" Enable3D="true" Perspective="50" Inclination="30" IsRightAngleAxes="true"
                                                WallWidth="0" IsClustered="false"></Area3DStyle>
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
                            <td align="center">
                                <asp:Chart ID="ChartCopy" runat="server" Height="200px" Width="350px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                    ImageType="Png" BorderlineDashStyle="Solid" Palette="Fire" BorderWidth="2" BackColor="Transparent"
                                    BorderColor="181, 64, 1" BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                                    <Legends>
                                        <asp:Legend Enabled="true" IsTextAutoFit="true" Title="" Name="Default" BackColor="Transparent"
                                            Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 13pt, style=Bold">
                                        </asp:Legend>
                                    </Legends>
                                    <Series>
                                        <asp:Series BorderWidth="0" Name="CopyUser" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                            BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                            BackGradientStyle="None" BackColor="Transparent">
                                            <Area3DStyle Rotation="10" Enable3D="true" Perspective="50" Inclination="30" IsRightAngleAxes="true"
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
                            <td align="center">
                                <asp:Chart ID="ChartScan" runat="server" Height="200px" Width="350px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                    ImageType="Png" BorderlineDashStyle="Solid" Palette="Fire" BorderWidth="2" BackColor="Transparent"
                                    BorderColor="181, 64, 1" BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                                    <Legends>
                                        <asp:Legend Enabled="true" IsTextAutoFit="true" Title="" Name="Default" BackColor="Transparent"
                                            Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 13pt, style=Bold">
                                        </asp:Legend>
                                    </Legends>
                                    <Series>
                                        <asp:Series BorderWidth="0" Name="ScanUser" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                            BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                            BackGradientStyle="None" BackColor="Transparent">
                                            <Area3DStyle Rotation="10" Enable3D="true" Perspective="50" Inclination="30" IsRightAngleAxes="true"
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
                        <tr>
                            <%--<td align="center" colspan="4" class=" Table_HeaderES">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelCopyGraph" runat="server" Text=""></asp:Label>
                        </td>--%>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <%-- <td align="center" colspan="4" class=" Table_HeaderES">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelScanGraph" runat="server" Text=""></asp:Label>
                        </td>--%>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
    </table>
</asp:Content>
