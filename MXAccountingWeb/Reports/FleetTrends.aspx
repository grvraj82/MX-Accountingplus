<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TrendsMaster.master"
    AutoEventWireup="true" CodeBehind="FleetTrends.aspx.cs" Inherits="AccountingPlusWeb.Reports.FleetTrends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" border="0" class="table_border_org"
        height="550">
        <tr class="CenterBG">
            <td align="center" valign="top">
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="Latest 10 Records"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <asp:Chart ID="ChartOutPutReports" runat="server" Height="300px" Width="700px" Visible="true"
                                ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderlineDashStyle="Solid"
                                Palette="Fire" BackGradientStyle="None" BorderWidth="2" BackImageTransparentColor="Transparent"
                                BackColor="Transparent" BorderColor="181, 64, 1" BorderlineColor="Green" BackHatchStyle="None"
                                CssClass="CenterBG">
                                <Legends>
                                    <asp:Legend Enabled="False" IsTextAutoFit="false" Name="Default" BackColor="Transparent"
                                        Font="Calibri, 12pt, style=bold" LegendItemOrder="SameAsSeriesOrder" Alignment="Near">
                                    </asp:Legend>
                                </Legends>
                                <Series>
                                    <asp:Series BorderWidth="2" Name="Print" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                    <asp:Series BorderWidth="2" Name="Copy" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                    <asp:Series BorderWidth="2" Name="InternetFax" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                    <asp:Series BorderWidth="2" Name="DocumentFiling" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                        BackGradientStyle="None" BackColor="Transparent">
                                        <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                                            WallWidth="0" IsClustered="False"></Area3DStyle>
                                        <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero="True">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="N0" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisY>
                                        <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="N0" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                            <MajorTickMark Size="2" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                            <asp:Chart ID="ChartDeptReoprt" runat="server" Height="300px" Width="700px" Visible="false"
                                ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderlineDashStyle="Solid"
                                Palette="Fire" BorderWidth="2" BackColor="Transparent" BorderColor="181, 64, 1"
                                BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                                <Legends>
                                    <asp:Legend Enabled="False" IsTextAutoFit="false" Name="Default" BackColor="Transparent"
                                        Font="Calibri, 12pt, style=bold">
                                    </asp:Legend>
                                </Legends>
                                <Series>
                                    <asp:Series BorderWidth="2" Name="Print" ChartType="StackedColumn" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                    <asp:Series BorderWidth="2" Name="Copy" ChartType="StackedColumn" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                        BackGradientStyle="None" BackColor="Transparent">
                                        <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                                            WallWidth="0" IsClustered="False"></Area3DStyle>
                                        <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero="True">
                                            <LabelStyle Font="Calibri, 12pt, style=Bold" Format="N0" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisY>
                                        <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                            <LabelStyle Font="Calibri, 12pt, style=Bold" Format="N0" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                            <MajorTickMark Size="2" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td width="80%" valign="top">
                            <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                <tr class="Grid_tr">
                                    <td>
                                        <asp:Table ID="TableOutPutUsage" runat="server" CellPadding="0" CellSpacing="1" Width="100%"
                                            CssClass="Table_bg">
                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Sl No</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Date/Time</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Print</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Copy</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Internet Fax Receive</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Document Filing</asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="CenterBG">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td align="left" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" >
                            <asp:Chart ID="ChartSendReports" runat="server" Height="300px" Width="700px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                ImageType="Png" BorderlineDashStyle="Solid" Palette="Fire" BackGradientStyle="None"
                                BorderWidth="2" BackImageTransparentColor="Transparent" BackColor="Transparent"
                                BorderColor="181, 64, 1" BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                                <Legends>
                                    <asp:Legend Enabled="False" IsTextAutoFit="false" Name="Default" BackColor="Transparent"
                                        Font="Calibri, 12pt, style=bold" LegendItemOrder="SameAsSeriesOrder" Alignment="Near">
                                    </asp:Legend>
                                </Legends>
                                <Series>
                                    <asp:Series BorderWidth="2" Name="ScanSend" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                    <asp:Series BorderWidth="2" Name="InternetFaxSend" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                    <asp:Series BorderWidth="2" Name="ScanToHDD" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                        BorderColor="180, 26, 59, 105" ShadowOffset="0">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                        BackGradientStyle="None" BackColor="Transparent">
                                        <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                                            WallWidth="0" IsClustered="False"></Area3DStyle>
                                        <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero="True">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="N0" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisY>
                                        <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="N0" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                            <MajorTickMark Size="2" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                        <td width="80%" valign="top">
                            <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                <tr class="Grid_tr">
                                    <td>
                                        <asp:Table ID="TableSendUsage" runat="server" CellPadding="0" CellSpacing="1" Width="100%"
                                            CssClass="Table_bg">
                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Sl No</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Date/Time</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Scan Send</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Internet Fax Send</asp:TableHeaderCell>
                                                <asp:TableHeaderCell CssClass="Grid_topbg1">Scan To HDD</asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
