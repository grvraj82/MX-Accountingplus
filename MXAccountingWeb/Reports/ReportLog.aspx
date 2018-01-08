<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ReportLog.aspx.cs" Inherits="PrintRoverWeb.Reports.ReportLog" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellReports();
        Meuselected("Reports");        
    </script>
    <style>
        table.DoubleHeaderGrid td, th
        {
            border: 0 !important;
        }
        
        .BorderOuterTable
        {
            /*border: 1px solid #cccccc !important;*/
            background-color: #ffffff;
            height: auto;
            padding-bottom: 0 !important;
            margin-bottom: 0 !important;
        }
        
        .BorderBottomForHeader
        {
            border-bottom: 1px solid #cccccc !important;
        }
        
        .BorderRight
        {
            border-right: 1px solid #cccccc !important;
        }
        
        .SubHeaderBg
        {
            background-color: #e9f6f9;
        }
        
        .SubHeaderBgNew
        {
            background-color: #f9f6f9;
            height: 25px;
            font-family: "segoe ui" , Verdana, Arial, Helvetica;
            font-weight: normal;
            font-size: 12px;
            white-space: nowrap;
        }
        
        .SubHeaderBgNew td
        {
            padding: 0px 5px 0px 5px;
        }
        
        .TableScroll
        {
            width: 300px;
            overflow: auto;
        }
        
        .MinWidth3
        {
            min-width: 50px;
        }
        
        .MinWidth2
        {
            min-width: 205px;
        }
        
        .BorderBottomNew
        {
            border-bottom: 1px solid #cccccc !important;
        }
        
        .titleclass
        {
            padding-right:25px;
        }
        
        @media (min-width: 1024px)
        {
            .widthcss
            {
                width: 900px;
            }
        }
        @media (min-width: 1280px)
        {
            .widthcss
            {
                width: 960px;
            }
        }
        @media (min-width: 1360px)
        {
            .widthcss
            {
                width: 1040px;
            }
        }
        @media (min-width: 1366px)
        {
            .widthcss
            {
                width: 1046px;
            }
        }
        @media (min-width: 1440px)
        {
            .widthcss
            {
                width: 1120px;
            }
        }
        @media (min-width: 1600px)
        {
            .widthcss
            {
                width: 1280px;
            }
        }
        @media (min-width: 1920px)
        {
            .widthcss
            {
                width: 1600px;
            }
        }
    </style>
    <asp:ScriptManager EnableScriptGlobalization="True" ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenFieldAll" runat="server" />
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
                                            <asp:Label ID="LabelHeadDetailedReport" runat="server" Text="Label"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Table runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell ID="tabelCellLabelUserSource" Visible="false" align="center" valign="middle"
                                                    CssClass="NoWrap" runat="server">
                                                    <asp:Label ID="LabelFilter" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                    :&nbsp;&nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellFilterDropdown" Visible="false" align="center" valign="middle"
                                                    CssClass="NoWrap" runat="server">
                                                    <asp:DropDownList ID="DropDownFilter" CssClass="FormDropDown_Small" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownFilter_SelectedIndexChanged"
                                                        CausesValidation="True">
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellSplitFilter" Visible="false" align="center" valign="middle"
                                                    CssClass="NoWrap" runat="server">
                                                    &nbsp;&nbsp;<asp:Image ID="Image3" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellLabelFilterType" Visible="false" align="center" valign="middle"
                                                    CssClass="NoWrap" runat="server">
                                                    &nbsp;&nbsp;<asp:Label ID="LabelFilterType" runat="server" Text="Filter by Job Type"
                                                        SkinID="TotalResource"></asp:Label>
                                                    :&nbsp;&nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellFilter" Visible="false" align="center" valign="middle"
                                                    CssClass="NoWrap" runat="server">
                                                    <asp:DropDownList ID="DropDownListFilterType" CssClass="FormDropDown_Small" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DropDownFilter_SelectedIndexChanged"
                                                        CausesValidation="True">
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellSplitFilter2" Visible="false" align="center" valign="middle"
                                                    CssClass="NoWrap" runat="server">
                                                    &nbsp;&nbsp;<asp:Image ID="Image4" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell3" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;<asp:Label ID="LabelFromDate" runat="server" Text="" SkinID="TotalResource"></asp:Label>
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
                                                    <%--<asp:TextBox ID="TextBoxFromDate" Width="80px" CssClass="Normal_FontLabel" runat="server"></asp:TextBox>--%>
                                                    <%-- <asp:CompareValidator ID="cmpStartDate" runat="server" ControlToValidate="TextBoxFromDate"
                                                        ErrorMessage="" Type="Date" Operator="LessThanEqual" Display="None"></asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="TopLeft"
                                                        runat="server" TargetControlID="cmpStartDate">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                    <%-- <cc1:CalendarExtender ID="CalendarExtenderFrom"  runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxFromDate">
                                                    </cc1:CalendarExtender>--%>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell6" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;<asp:Label ID="LabelToDate" runat="server" Text="" SkinID="TotalResource"></asp:Label>
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
                                                    <%-- <asp:TextBox ID="TextBoxToDate" Width="80px" CssClass="Normal_FontLabel" runat="server"></asp:TextBox>--%>
                                                    <%--<asp:CompareValidator ID="CompareValidatorToDate" runat="server" Type="Date" ControlToValidate="TextBoxToDate"
                                                        ErrorMessage="End date cannot be greater than today's date" Operator="LessThanEqual"
                                                         Display="None"></asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" PopupPosition="TopLeft"
                                                        TargetControlID="CompareValidatorToDate" runat="server">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                    <%-- <cc1:CalendarExtender ID="CalendarExtenderTo" runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxToDate">
                                                    </cc1:CalendarExtender>--%>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell2" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap" HorizontalAlign="Left">
                                                    &nbsp;&nbsp;<asp:Button ID="ButtonGo" CssClass="Login_Button" runat="server" Text=""
                                                        ToolTip="" OnClick="ButtonGo_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell9" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image1" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell5" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<%--<asp:ImageButton ID="ImageButtonExportToCsv" SkinID="JoblogimgCSV" ToolTip=""
                                                        runat="server" OnClick="ImageButtonExportToCsv_Click" />--%>
                                                        <asp:ImageButton ID="ImageButtonExcel" ToolTip="Export to Excel" ImageUrl="~/App_Themes/Blue/Images/excel-icon.png"
                                                        runat="server" OnClick="ImageButtonExcel_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell8" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image5" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellHtml" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle" ToolTip="Import to Html">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonHtml" ImageUrl="~/App_Themes/Blue/Images/html.png"
                                                        runat="server" OnClick="ImageButtonHtml_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell1" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image9" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell13" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonRefresh" SkinID="ManageMfpSimgRefresh"
                                                        runat="server" OnClick="ImageButtonRefresh_Click" />
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
            <td class="Top_menu_bg " valign="middle" height="33" align="right">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <asp:Table ID="Table2" runat="server" CellPadding="2" CellSpacing="0" Visible="false">
                            <asp:TableRow>
                                <asp:TableCell ID="TableCell10" align="center" valign="middle" runat="server" Visible="true">
                                    <asp:Label ID="LabelTotalRecordsTitle" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell11" align="center" valign="middle" runat="server" Visible="true">
                                    <asp:Label ID="LabelTotalRecordsValue" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell12" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                    VerticalAlign="Middle">
                                    &nbsp;<asp:Image ID="Image6" Width="2" runat="server" SkinID="ManageusersimgSplit" />
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell14" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                    VerticalAlign="Middle">
                                    <asp:Label ID="LabelPageSize" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell15" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                    VerticalAlign="Middle">
                                    <asp:DropDownList ID="DropDownPageSize" runat="server" AutoPostBack="true" EnableViewState="false"
                                        CssClass="Normal_FontLabel" OnSelectedIndexChanged="DropDownPageSize_SelectedIndexChanged">
                                        <asp:ListItem Selected="true">50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell16" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                    VerticalAlign="Middle">
                                    <asp:Image ID="Image2" Width="2" runat="server" SkinID="ManageusersimgSplit" />
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell17" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                    VerticalAlign="Middle">
                                    <asp:Label ID="LabelPage" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell18" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                    VerticalAlign="Middle">
                                    <asp:DropDownList ID="DropDownCurrentPage" runat="server" EnableViewState="false"
                                        Visible="false" AutoPostBack="true" CssClass="Normal_FontLabel" OnSelectedIndexChanged="DropDownCurrentPage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DropDownPageSize" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ImageButtonRefresh" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 2">
            </td>
        </tr>
        <tr style="height: 550px">
            <td>
            </td>
            <td colspan="2" valign="top" class="CenterBG" align="">
                <asp:UpdatePanel runat="server" ID="PaginationPanel">
                    <ContentTemplate>
                        <table width="100%" border="0" align="center" cellpadding="3" cellspacing="3">
                            <tr>
                                <td>
                                    <div id="DivJobType" runat="server" class="widthcss" style="overflow-x: scroll; border: 1px solid #cccccc;">
                                        <asp:Table EnableViewState="false" ID="TableJobType" Visible="true" CellSpacing="0"
                                            CellPadding="0" BorderWidth="0" runat="server" CssClass="BorderOuterTable AlternateRow">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:Table ID="TableJobType1" CellSpacing="1" CellPadding="0" CssClass="Table_bg"
                                                        Width="100%" BorderWidth="0" runat="server">
                                                        <asp:TableHeaderRow Height="30px" CssClass="Table_HeaderBG BorderBottomForHeader">
                                                            <asp:TableHeaderCell ID="TableHeaderCell56" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                Text=""></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCell57" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                Text="IP Address"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCell54" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                Text="Host Name"></asp:TableHeaderCell>
                                                                   <asp:TableHeaderCell ID="TableHeaderCell1Company" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                Text=""></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellType" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                Text="Type "></asp:TableHeaderCell>
                                                                
                                                            <asp:TableHeaderCell ID="TableHeaderCellPrint" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                Text="Print "></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellCopy" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight " Wrap="false" Font-Bold="true" Text="Copy"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellScan" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight" Wrap="false" Font-Bold="true" Text="Scan"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellFax" HorizontalAlign="Center" Width="20%"
                                                                CssClass="H_title BorderRight" Wrap="false" Font-Bold="true" Text="Fax"></asp:TableHeaderCell>
                                                        </asp:TableHeaderRow>
                                                        <asp:TableRow CssClass="BorderBottomForHeader SubHeaderBg">
                                                            <asp:TableHeaderCell ID="TableHeaderCell45_1" Width="25%" Height="30px">
                                                                <asp:Table ID="TableType" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell45" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                            Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCell58_1" Width="25%" Height="30px">
                                                                <asp:Table ID="Table16" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell58" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                            Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCell59_1" Width="25%" Height="30px">
                                                                <asp:Table ID="Table17" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell59" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                            Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                             <asp:TableHeaderCell ID="TableHeaderCell61" Width="25%" Height="30px">
                                                                <asp:Table ID="Table12" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell62" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                            Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCell55_1" Width="25%" Height="30px">
                                                                <asp:Table ID="Table15" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell55" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                            Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                              
                                                            <asp:TableHeaderCell Width="25%" Height="30px">
                                                                <asp:Table ID="TablePrint" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell1" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                            Width="33%" Wrap="false" Text="BW "></asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCell2" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                            Width="33%" Wrap="false" Text="Color "></asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCell3" HorizontalAlign="Center" CssClass="H_title BorderRight titleclass"
                                                                            Width="33%" Wrap="false" Text="Total "></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell Width="25%" Height="30px">
                                                                <asp:Table ID="Table3" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell4" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                            Width="33%" Wrap="false" Text="BW "></asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCell5" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                            Width="33%" Wrap="false" Text="Color "></asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCell6" HorizontalAlign="Center" CssClass="H_title BorderRight titleclass"
                                                                            Width="33%" Wrap="false" Text="Total "></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell Width="25%" Height="30px">
                                                                <asp:Table ID="Table4" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell7" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                            Width="33%" Wrap="false" Text="BW "></asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCell8" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                            Width="33%" Wrap="false" Text="Color "></asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCell9" HorizontalAlign="Center" CssClass="H_title BorderRight titleclass"
                                                                            Width="33%" Wrap="false" Text="Total "></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell Width="25%" Height="30px">
                                                                <asp:Table ID="Table5" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCell10" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                            Width="33%" Wrap="false" Text="BW "></asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCell11" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                            Width="33%" Wrap="false" Text="Color "></asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCell12" HorizontalAlign="Center" CssClass="H_title BorderRight titleclass"
                                                                            Width="33%" Wrap="false" Text="Total "></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableHeaderCell ID="TableHeaderCellserialnum" Width="25%" VerticalAlign="Top"
                                                                Height="30px" CssClass="BorderBottomNew">
                                                                <asp:Table ID="Table7" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell Width="33%" CssClass="Grid_topbg">
                                                                            <asp:Table ID="TableSerialnumber" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew BorderRight" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCellSerialNumber" HorizontalAlign="Left" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Serial No."></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellIPaddress" Width="25%" VerticalAlign="Top"
                                                                Height="30px" CssClass="BorderBottomNew">
                                                                <asp:Table ID="Table11" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell Width="33%" CssClass="Grid_topbg">
                                                                            <asp:Table ID="TableIPAddress" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew BorderRight" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell52" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="IP Address"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellHostname" Width="25%" VerticalAlign="Top"
                                                                Height="30px" CssClass="BorderRight BorderBottomNew">
                                                                <asp:Table ID="Table13" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell Width="33%" CssClass="Grid_topbg">
                                                                            <asp:Table ID="TableHostName" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell53" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Host Name"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                                 <asp:TableHeaderCell ID="TableHeaderCellCompany" Width="25%" VerticalAlign="Top"
                                                                Height="30px" CssClass="BorderRight BorderBottomNew">
                                                                <asp:Table ID="Table10" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell Width="33%" CssClass="Grid_topbg">
                                                                            <asp:Table ID="TableCompany" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell60" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Company"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell Width="25%" VerticalAlign="Top" Height="30px" CssClass="BorderBottomNew">
                                                                <asp:Table ID="Table6" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell ID="TableHeaderCellType11" Width="33%" CssClass="Grid_topbg">
                                                                            <asp:Table ID="TableFilterList" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew BorderRight" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell46" HorizontalAlign="Left" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                                <%--<asp:TableHeaderRow CssClass="BorderBottomNew">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell51" HorizontalAlign="Left" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="SerialNumber"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>--%>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                       
                                                            <asp:TableHeaderCell Width="25%" Height="30px">
                                                                <asp:Table ID="TablePrintCount" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderRight BorderBottomNew Grid_topbg">
                                                                            <asp:Table ID="TablePrintBW" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew " Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell13" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A4"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell14" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A3"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell15" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Others"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell16" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Wrap="false" Text="Total"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderRight BorderBottomNew Grid_topbg">
                                                                            <asp:Table ID="TablePrintColor" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell17" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A4"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell18" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A3"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell19" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Others"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell20" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Total"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderBottomNew Grid_topbg">
                                                                            <asp:Table ID="TablePrintTotal" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew BorderRight" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell48" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell Width="25%" Height="30px" CssClass="BorderRight BorderBottomNew">
                                                                <asp:Table ID="Table8" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderRight Grid_topbg">
                                                                            <asp:Table ID="TableCopyBW" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell21" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A4"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell22" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A3"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell23" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Others"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell24" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Total"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderRight Grid_topbg">
                                                                            <asp:Table ID="TableCopyC" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell25" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A4"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell26" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A3"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell27" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Others"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell28" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Total"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Width="33%" CssClass="Grid_topbg">
                                                                            <asp:Table ID="TableCopyTotal" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell49" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell Width="25%" Height="30px" CssClass="BorderRight">
                                                                <asp:Table ID="Table9" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell Width="33%" CssClass="Grid_topbg BorderBottomNew">
                                                                            <asp:Table ID="TableScanBW" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell29" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A4"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell30" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A3"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell31" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Others"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell32" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Total"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderRight Grid_topbg BorderBottomNew">
                                                                            <asp:Table ID="TableScanC" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell33" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A4"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell34" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A3"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell35" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Others"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell36" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Total"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderBottomNew Grid_topbg">
                                                                            <asp:Table ID="TableScanTotal" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell47" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                            <asp:TableHeaderCell Width="25%" Height="30px" CssClass="BorderRight ">
                                                                <asp:Table ID="Table20" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                    runat="server">
                                                                    <asp:TableHeaderRow Height="30px">
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderRight Grid_topbg BorderBottomNew">
                                                                            <asp:Table ID="TableFaxBW" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell37" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A4"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell38" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A3"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell39" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Others"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell40" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Total"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderRight Grid_topbg BorderBottomNew">
                                                                            <asp:Table ID="TableFaxC" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell41" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A4"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell42" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="A3"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell43" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Others"></asp:TableHeaderCell>
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell44" HorizontalAlign="Center" CssClass="H_title_New MinWidth3"
                                                                                        Width="33%" Wrap="false" Text="Total"></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Width="33%" CssClass="BorderBottomNew Grid_topbg">
                                                                            <asp:Table ID="TableFaxTotal" CellSpacing="0" CellPadding="0" Width="100%" BorderWidth="0"
                                                                                runat="server">
                                                                                <asp:TableHeaderRow CssClass="BorderBottomNew" Height="30px">
                                                                                    <asp:TableHeaderCell ID="TableHeaderCell50" HorizontalAlign="Center" CssClass="H_title_New BorderRight MinWidth3"
                                                                                        Width="33%" Wrap="false" Text=""></asp:TableHeaderCell>
                                                                                </asp:TableHeaderRow>
                                                                            </asp:Table>
                                                                        </asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </asp:TableHeaderCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </td>
                            </tr>
                            <tr class="Grid_tr">
                                <td valign="top" align="">
                                    <asp:Table EnableViewState="false" Visible="false" ID="TableReport" Width="100%"
                                        class="TableGridColor" HorizontalAlign="Left" CellPadding="3" CellSpacing="3"
                                        BorderWidth="0" runat="server" SkinID="Grid">
                                    </asp:Table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DropDownFilter" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ButtonGo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <table width="98%" border="0" cellpadding="0" align="center" cellspacing="0" class="TableGridColor"
                    style="display: none">
                    <tr class="Grid_tr">
                        <td class="CenterBG">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <asp:Label ID="LabelUpdatingPleaseWait" runat="server" Text="Updating Please wait..."></asp:Label><br />
                                    <asp:Image ID="ImgUpdateProgress" runat="server" SkinID="UpdateProgress" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel runat="server" ID="Panel">
                                <ContentTemplate>
                                    <asp:Table EnableViewState="false" ID="TableJobLog" Visible="false" HorizontalAlign="Left"
                                        CellPadding="3" CellSpacing="3" Width="100%" BorderWidth="1" runat="server" CssClass="Table_bg"
                                        SkinID="Grid">
                                        <asp:TableRow CssClass="Table_HeaderBG">
                                            <asp:TableCell RowSpan="2" Width="30px">&nbsp;&nbsp;</asp:TableCell>
                                            <asp:TableCell ID="TableCellMfpIPAddress" RowSpan="2" CssClass="DoubleRowHeader_M">Mfp 
                                                            IP Address</asp:TableCell>
                                            <asp:TableCell ID="TableCellMfpMACAddress" RowSpan="2" CssClass="DoubleRowHeader_M">Mfp 
                                                            MAC Address</asp:TableCell>
                                            <asp:TableCell ID="TableCellJobID" RowSpan="2" CssClass="DoubleRowHeader_M">Job 
                                                            ID</asp:TableCell>
                                            <asp:TableCell Visible="true" ID="TableCellJobMode" RowSpan="2" CssClass="DoubleRowHeader_M">Job 
                                                            Mode</asp:TableCell>
                                            <asp:TableCell ID="TableCellJobName" RowSpan="2" CssClass="DoubleRowHeader_M">Job 
                                                            Name</asp:TableCell>
                                            <asp:TableCell ID="TableCellPaperSize" RowSpan="2" CssClass="DoubleRowHeader_M">Paper 
                                                            Size</asp:TableCell>
                                            <asp:TableCell ID="TableCellComputerName" RowSpan="2" CssClass="DoubleRowHeader_M">Computer 
                                                            Name</asp:TableCell>
                                            <asp:TableCell ID="TableCellUserName" RowSpan="2" CssClass="DoubleRowHeader_M">User 
                                                            Name</asp:TableCell>
                                            <asp:TableCell ID="TableCellLoginName" RowSpan="2" CssClass="DoubleRowHeader_M">Login 
                                                            Name</asp:TableCell>
                                            <asp:TableCell ID="TableCellDate" ColumnSpan="2" Wrap="false" CssClass="Grid_topbg1">Date</asp:TableCell>
                                            <asp:TableCell ID="TableCellTotalCount" ColumnSpan="4" CssClass="Grid_topbg1">Total 
                                                            Count</asp:TableCell>
                                            <asp:TableCell ID="TableCellResult" RowSpan="2" CssClass="DoubleRowHeader_M">Result</asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow CssClass="Table_HeaderBG">
                                            <asp:TableCell ID="TableCellStart" Wrap="false" CssClass="Grid_topbg1">Start</asp:TableCell>
                                            <asp:TableCell ID="TableCellComplete" Wrap="false" CssClass="Grid_topbg1">Complete</asp:TableCell>
                                            <asp:TableCell ID="TableCellBlackWhite" Wrap="false" CssClass="Grid_topbg1">Black 
                                                            &amp; White</asp:TableCell>
                                            <asp:TableCell ID="TableCellFullColor" Wrap="false" CssClass="Grid_topbg1">Full 
                                                            Color</asp:TableCell>
                                            <asp:TableCell ID="TableCell2Color" Wrap="false" CssClass="Grid_topbg1">2 Color</asp:TableCell>
                                            <asp:TableCell ID="TableCellSingleColor" Wrap="false" CssClass="Grid_topbg1">Single 
                                                            Color</asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DropDownCurrentPage" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownPageSize" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
