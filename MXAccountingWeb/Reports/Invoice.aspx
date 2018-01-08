<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="Invoice.aspx.cs" Inherits="AccountingPlusWeb.Reports.Invoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        .MinWidth2
        {
            padding-right: 5px;
        }
        .PaddingLeft
        {
            padding-left: 5px;
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
        
        .PageBreak
        {
            page-break-after: always;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image7" SkinID="HeadingLeft" runat="server" />
            </td>
            <td align="left" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" height="33" width="100%">
                    <tr class="Top_menu_bg">
                        <td width="90%" align="left" valign="middle" height="33">
                            <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="HeadingMiddleBg" Width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadInvoiceReport" runat="server" Text="Invoice"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp</asp:TableCell>
                                    <asp:TableCell>
                                       
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                        <td width="65%" height="33" align="right" valign="middle" class="HeaderPaddingR">
                        </td>
                    </tr>
                    <tr>
                        <td height="2" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="left">
                            <fieldset>
                                <legend>Filter & Export options</legend>
                                <table cellpadding="2" cellspacing="0" border="0" align="left">
                                    <tr>
                                        <td>
                                            <table cellpadding="2" cellspacing="0" border="0">
                                                <tr>
                                                    <td valign="middle" align="center">
                                                        <asp:Label ID="Label3" runat="server" Text="Cost Center"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td valign="middle" align="center">
                                                        <asp:Label ID="Label4" runat="server" Text="MFP"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="3px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="NoWrap">
                                                        <asp:ListBox ID="ListBoxCC" SelectionMode="Multiple" Rows="10" runat="server"></asp:ListBox>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td class="NoWrap">
                                                        <asp:ListBox ID="ListBoxMFp" SelectionMode="Multiple" Rows="10" runat="server"></asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        Note: To select multiple items hold Ctrl key
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding-left: 20px">
                                            <table cellpadding="3" cellspacing="5" border="0">
                                                <tr>
                                                    <td>
                                                        User Source
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListUserSource" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListUserSource_SelectedIndexChanged">
                                                            <asp:ListItem Value="-1" Text="All" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="DB"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="AD"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListADlist" runat="server" Visible="false">

                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LabelFromDate" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                    </td>
                                                    <td colspan="2" class="NoWrap">
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LabelToDate" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                    </td>
                                                    <td colspan="2" class="NoWrap">
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" height="20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="ButtonGo" runat="server" Width="32px" CssClass="Login_Button" Text="Generate"
                                                                        OnClick="ButtonGo_Click" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:ImageButton ID="ImageButtonExcel" runat="server" ImageUrl="~/App_Themes/Blue/Images/excel-icon.png"
                                                                        OnClick="ImageButtonExcel_Click" />
                                                                </td>
                                                                <td align="center" s>
                                                                    <asp:ImageButton ID="ImageButtonHtml" runat="server" ImageUrl="~/App_Themes/Blue/Images/html.png"
                                                                        OnClick="ImageButtonHtml_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding-left: 20px" valign="middle">
                                            <table cellpadding="5" cellspacing="6" border="0">
                                                <tr>
                                                    <td valign="middle" class="NoWrap">
                                                        <asp:Label ID="Label1" runat="server" Text="1.When costcenter end include"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListCCE" runat="server">
                                                            <asp:ListItem Text="None" Value="none" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="New Blank Row" Value="blankrow"></asp:ListItem>
                                                            <asp:ListItem Text="Page Break" Value="pagebreak"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" class="NoWrap">
                                                        <asp:Label ID="Label2" runat="server" Text="2.Include SubTotal"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBoxSub" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" class="NoWrap">
                                                        <asp:Label ID="Label5" runat="server" Text="3.Display CostCenter Summary"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBoxSum" runat="server" />
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="4.Choose columns to display"></asp:Label>
                                                        
                                                    </td>
                                                    <td>
                                                        <table cellpadding="3" cellspacing="3" border="0">
                                                            <tr>
                                                                <td style="padding-left:0">
                                                                    <asp:CheckBox ID="CheckBoxPrint" Text="Print" Checked="true" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="CheckBoxCopy" Text="Copy" Checked="true" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="CheckBoxScan" Text="Scan" Checked="true" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="CheckBoxFax" Text="Fax" Checked="true" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="CheckBoxPrices" Text="Price"  runat="server" />
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="5.A3 count as"></asp:Label>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListA3" runat="server">
                                                            <asp:ListItem Text="Two A4 Count" Value="A4"></asp:ListItem>
                                                            <asp:ListItem Text="One A3 Count" Value="A3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="2" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" class="" width="100%">
                                <tr class="Grid_tr" align="center">
                                    <td align="center">
                                        <asp:Panel ID="PanelPdf" runat="server">
                                            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="3">
                                                <tr>
                                                    <td>
                                                        <div id="DivJobType" runat="server" class="widthcss" style="border: 1px solid #cccccc;"
                                                            align="center">
                                                            <asp:Table ID="TableJobType1" CellSpacing="1" Visible="false" CellPadding="0" CssClass="BorderOuterTable AlternateRow"
                                                                Width="100%" BorderWidth="0" runat="server" GridLines="Horizontal">
                                                                <asp:TableHeaderRow Height="30px" CssClass="Table_HeaderBG BorderBottomForHeader">
                                                                    <asp:TableHeaderCell ID="TableHeaderCell56" HorizontalAlign="Center" Width="30px"
                                                                        CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                        Text=""></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell13" HorizontalAlign="Center" Width="" CssClass="H_title BorderRight"
                                                                        Wrap="false" VerticalAlign="Middle" Font-Bold="true" Text="Cost Center"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell57" HorizontalAlign="Center" Width="200px"
                                                                        CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                        Text="IP Adrress"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell54" HorizontalAlign="Center" Width="" CssClass="H_title BorderRight"
                                                                        Wrap="false" VerticalAlign="Middle" Font-Bold="true" Text="Location"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellType" HorizontalAlign="Center" Width="" CssClass="H_title BorderRight"
                                                                        Wrap="false" VerticalAlign="Middle" Font-Bold="true" Text="Model"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellPrint" HorizontalAlign="Center" Width="90px"
                                                                        CssClass="H_title BorderRight" Wrap="false" VerticalAlign="Middle" Font-Bold="true"
                                                                        Text="Print " ColumnSpan="3"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellCopy" HorizontalAlign="Center" Width="" CssClass="H_title BorderRight "
                                                                        Wrap="false" Font-Bold="true" Text="Copy" ColumnSpan="3"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellScan" HorizontalAlign="Center" Width="" CssClass="H_title BorderRight"
                                                                        Wrap="false" Font-Bold="true" Text="Scan" ColumnSpan="3"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCellFax" HorizontalAlign="Center" Width="" CssClass="H_title BorderRight"
                                                                        Wrap="false" Font-Bold="true" Text="Fax" ColumnSpan="3"></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell16" HorizontalAlign="Center" Width="" CssClass="H_title BorderRight"
                                                                        Wrap="false" VerticalAlign="Middle" Font-Bold="true" Text="Price"></asp:TableHeaderCell>
                                                                </asp:TableHeaderRow>
                                                                <asp:TableHeaderRow Height="30px" CssClass="BorderBottomForHeader SubHeaderBg">
                                                                    <asp:TableHeaderCell ID="TableHeaderCell19" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                        Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell20" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                        Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell21" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                        Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell22" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                        Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell15" HorizontalAlign="Left" CssClass="H_title BorderRight"
                                                                        Wrap="false" Text=""></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell14" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="BW "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell23" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="Color "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell24" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="Total "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell1" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="BW "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell2" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="Color "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell3" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="Total "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell4" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="BW "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell5" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="Color "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell6" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="Total "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell7" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="BW "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell8" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="Color "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell9" HorizontalAlign="Center" CssClass="H_title BorderRight MinWidth2"
                                                                        Width="40px" Wrap="false" Text="Total "></asp:TableHeaderCell>
                                                                    <asp:TableHeaderCell ID="TableHeaderCell10" HorizontalAlign="Left" CssClass="H_title BorderRight  MinWidth2"
                                                                        Wrap="false" Text=""></asp:TableHeaderCell>
                                                                </asp:TableHeaderRow>
                                                            </asp:Table>
                                                            <asp:Literal ID="Panel1" runat="server"></asp:Literal>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--         -------------------------------------------------%>
                                            <asp:Table EnableViewState="false" ID="TableInvoice" HorizontalAlign="Left" CellPadding="3"
                                                CellSpacing="1" BorderWidth="0" runat="server" CssClass="Table_bg AlternateRow"
                                                Width="100%" border="0" Visible="false">
                                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                    <asp:TableHeaderCell runat="server" ID="TableHeaderCellJobType" HorizontalAlign="Left"
                                                        CssClass="H_title" Text=""></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell runat="server" ID="TableHeaderCellColorCount" HorizontalAlign="Left"
                                                        CssClass="H_title" Text=""></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell runat="server" ID="TableHeaderCellColorUnits" HorizontalAlign="Left"
                                                        CssClass="H_title" Text=""></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell runat="server" ID="TableHeaderCellBWCount" HorizontalAlign="Left"
                                                        CssClass="H_title" Text=""></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell runat="server" ID="TableHeaderCellBWUnits" HorizontalAlign="Left"
                                                        CssClass="H_title" Text=""></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell runat="server" ID="TableHeaderCellTotalUnits" HorizontalAlign="Left"
                                                        CssClass="H_title" Text=""></asp:TableHeaderCell>
                                                    <asp:TableHeaderCell runat="server" ID="TableHeaderCellTotalPrice" HorizontalAlign="Left"
                                                        CssClass="H_title" Text=""></asp:TableHeaderCell>
                                                </asp:TableHeaderRow>
                                            </asp:Table>
                                        </asp:Panel>
                                        <asp:GridView ID="gridViewReport" runat="server" BackColor="White" BorderWidth="1px"
                                            BorderStyle="None" CellPadding="0" GridLines="Both" CellSpacing="0" AutoGenerateColumns="False"
                                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                                            Visible="false" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="JOB_MODE" HeaderText="Job Mode" SortExpression="" />
                                                <asp:BoundField DataField="COLOR_PRICE" HeaderText="Color(Price)" SortExpression="" />
                                                <asp:BoundField DataField="COLOR_COUNT" HeaderText="Color(Count)" SortExpression="" />
                                                <asp:BoundField DataField="MONOCHROME_PRICE" HeaderText="BW(Price)" SortExpression="" />
                                                <asp:BoundField DataField="MONOCHROME_COUNT" HeaderText="BW(Count)" SortExpression="" />
                                                <asp:BoundField DataField="TOTAL_PRICE" HeaderText="Total(Price)" SortExpression="" />
                                                <asp:BoundField DataField="TOTAL_COUNT" HeaderText="Total(Count)" SortExpression="" />
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
    </table>
</asp:Content>
