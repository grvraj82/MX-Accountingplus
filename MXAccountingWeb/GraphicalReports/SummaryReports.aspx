<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="SummaryReports.aspx.cs" Inherits="AccountingPlusWeb.GraphicalReports.SummaryReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">

        function PrintDetails() {

            //            var fromDate = document.getElementById('ctl00_PageContent_TextBoxFromDate').value;
            //            var toDate = document.getElementById('ctl00_PageContent_TextBoxToDate').value;
            //            // Response.Redirect("~/GraphicalReports/SummaryReports.aspx?mc=421," + fromDate + "," + toDate + "");
            //            var value = '421' + ',' + fromDate + ',' + toDate
            //            window.open("../GraphicalReports/SummaryReports.aspx?mc=" + value);
        }
        try {
            fnShowCellReports();
            Meuselected("Reports");
        }
        catch (Error) { }

    </script>
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <%--<thead>
            <tr>
                <td>
                    Your header goes here
                </td>
            </tr>
        </thead>--%>
        <tr id="TableRowMenu" runat="server">
            <td align="right" valign="top">
                <asp:Image ID="Image7" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="middle" class="CenterBG">
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr class="Top_menu_bg" height="33" align="left">
                        <td>
                            <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0">
                                <asp:TableRow>
                                    <asp:TableCell class="HeadingMiddleBg" Width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingSummary" runat="server"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Table runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0">
                                            <asp:TableRow>
                                                <asp:TableCell ID="TableCell3" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    <asp:Label ID="LabelFromDate" runat="server" Text="" SkinID="TotalResource"></asp:Label>
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
                                                    <%-- <asp:TextBox ID="TextBoxFromDate" Width="80px" CssClass="Normal_FontLabel" runat="server">
                                                    </asp:TextBox>--%>
                                                    <%-- <asp:CompareValidator ID="cmpStartDate" runat="server" ControlToValidate="TextBoxFromDate"
                                                        ErrorMessage="" Operator="LessThanEqual"  Display="None">
                                                    </asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="TopLeft"
                                                        runat="server" TargetControlID="cmpStartDate">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                    <%-- <cc1:CalendarExtender ID="CalendarExtenderFrom" runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxFromDate">
                                                    </cc1:CalendarExtender>--%>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell6" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="LabelToDate" runat="server" Text="" SkinID="TotalResource"></asp:Label>
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
                                                    <%-- <asp:TextBox ID="TextBoxToDate" Width="80px" CssClass="Normal_FontLabel" runat="server">
                                                    </asp:TextBox>--%>
                                                    <%--<asp:CompareValidator ID="CompareValidatorToDate" runat="server" ControlToValidate="TextBoxToDate"
                                                        ErrorMessage="" Operator="LessThanEqual"  Display="None">
                                                    </asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" PopupPosition="TopLeft"
                                                        TargetControlID="CompareValidatorToDate" runat="server">
                                                    </cc1:ValidatorCalloutExtender>--%>
                                                    <%-- <cc1:CalendarExtender ID="CalendarExtenderTo" runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxToDate">
                                                    </cc1:CalendarExtender>--%>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell2" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap" HorizontalAlign="Left">
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="ButtonGo" CssClass="GO_button" runat="server" Text="" ToolTip=""
                                                        OnClick="ButtonGo_Click" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellPrint" align="center" valign="middle" runat="server"
                                                    CssClass="NoWrap" Visible="true" HorizontalAlign="Left">
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="ButtonPrint" CssClass="GO_button" runat="server" Text="" ToolTip=""
                                                        OnClick="ButtonPrint_Click" />
                                                </asp:TableCell>
                                                 <asp:TableCell ID="TableCell10" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image2" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell5" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap" HorizontalAlign="Right">
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="ImageButtonChart" runat="server" OnClick="ImageButtonChart_Click"
                                                        SkinID="SummaryReportschart" />
                                                </asp:TableCell>
                                                 <asp:TableCell ID="TableCell9" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image1" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell8" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap" HorizontalAlign="Right">
                                                    &nbsp;&nbsp;
                                                    <asp:ImageButton ID="ImageButtonChartTable" ToolTip="" runat="server" OnClick="ImageButtonChartTable_Click"
                                                        SkinID="SummaryReportschartTable" />
                                                </asp:TableCell>
                                                 <asp:TableCell ID="TableCell1" align="center" valign="middle" runat="server" Visible="true"
                                                    CssClass="NoWrap">
                                                    &nbsp;&nbsp;<asp:Image ID="Image5" runat="server" SkinID="ManageusersimgSplit" />
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCellPdf" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle" ToolTip="Export to Pdf">
                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonPdf" ImageUrl="~/App_Themes/Blue/Images/Pdf.png"
                                                        runat="server" OnClick="ImageButtonPdf_Click" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                    <asp:TableCell Width="25%">
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelPrint" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td width="9">
                </td>
                <td class="CenterBG">
                    <asp:Panel ID="PanelSummaryReport" runat="server">
                        <table align="center" cellpadding="0" cellspacing="0" width="90%" border="0">
                            <tr>
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="center">
                                                <table cellpadding="1" cellspacing="1" width="100%" height="30px" border="0">
                                                    <tr>
                                                        <td align="center" class="Summary">
                                                            <asp:Label ID="LabelExecutiveSummary" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellpadding="0" cellspacing="0" width="100%" height="30px" border="0" class="Table_bg">
                                                    <tr class=" Table_HeaderES">
                                                        <td align="left" colspan="4">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelCurrentVolumes" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="GridRow" height="25px">
                                                        <td align="left">
                                                            <asp:Label ID="LabelTotalNumberofDaysCV" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelTotalNumberofDaysCV_Value" CssClass="Table_RowValueES" runat="server"
                                                                Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelTotalNumberofJobsCV" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelTotalNumberofJobsCV_Value" CssClass="Table_RowValueES" runat="server"
                                                                Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="GridRow" height="25px">
                                                        <td align="left">
                                                            <asp:Label ID="LabelTotalNumberofPagesCV" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelTotalNumberofPagesCV_Value" CssClass="Table_RowValueES" runat="server"
                                                                Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelTotalNumberofUsersCV" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelTotalNumberofUsersCV_Value" CssClass="Table_RowValueES" runat="server"
                                                                Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="GridRow" height="25px">
                                                        <td align="left">
                                                            <asp:Label ID="LabelTotalNumberofBWPagesCV" CssClass="Table_RowES" runat="server"
                                                                Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelTotalNumberofBWPagesCV_value" CssClass="Table_RowValueES" runat="server"
                                                                Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelTotalNumberofColorPagesCV" CssClass="Table_RowES" runat="server"
                                                                Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelTotalNumberofColorPagesCV_Value" CssClass="Table_RowValueES"
                                                                runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="GridRow" height="25px">
                                                        <td align="left">
                                                            <asp:Label ID="LabelA3BW" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelA3BWValue" CssClass="Table_RowValueES" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelA3C" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelA3CValue" CssClass="Table_RowValueES" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="GridRow" height="25px">
                                                        <td align="left">
                                                            <asp:Label ID="LabelA4BW" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelA4BWValue" CssClass="Table_RowValueES" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelA4C" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelA4CValue" CssClass="Table_RowValueES" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="GridRow" height="25px">
                                                        <td align="left">
                                                            <asp:Label ID="LabelOtherBW" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelOtherBWValue" CssClass="Table_RowValueES" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelOtherC" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelOtherCValue" CssClass="Table_RowValueES" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="GridRow" height="25px">
                                                        <td align="left">
                                                            <asp:Label ID="LabelTotalNumberofSimplex" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelTotalNumberofSimplex_value" CssClass="Table_RowValueES" runat="server"
                                                                Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LabelTotalNumberofduplex" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="LabelTotalNumberofduplex_value" CssClass="Table_RowValueES" runat="server"
                                                                Text="0"></asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class=" Table_HeaderES" style="font-size: 14">
                                <td align="left" colspan="4">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelCurentAssets" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px">
                                <td align="left">
                                    <asp:Label ID="LabelTotalNumberofDevicesCV" CssClass="Table_RowES" runat="server"
                                        Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelTotalNumberofDevicesCV_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelTotalNumberofWorkstationCV" CssClass="Table_RowES" runat="server"
                                        Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelTotalNumberofWorkstationCV_Value" CssClass="Table_RowValueES"
                                        runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr height="5">
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class=" Table_HeaderES" style="display: none">
                                <td align="left" colspan="4">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelCurrentCosts" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px" style="display: none">
                                <td align="left">
                                    <asp:Label ID="LabelAverageCostPerPage" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelAverageCostPerPage_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelAverageCostPerUser" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelAverageCostPerUser_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px" style="display: none">
                                <td align="left">
                                    <asp:Label ID="LabelAverageCostPerPrinter" CssClass="Table_RowES" runat="server"
                                        Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelAverageCostPerPrinter_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelCostofBWPrinting" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelCostofBWPrinting_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px" style="display: none">
                                <td align="left">
                                    <asp:Label ID="LabelCostofColorPrinting" CssClass="Table_RowES" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelCostofColorPrinting_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelTotalCostofPrinting" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelTotalCostofPrinting_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr height="5">
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class=" Table_HeaderES">
                                <td align="left" colspan="4">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelExtraValues" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px" style="display: none">
                                <td align="left">
                                    <asp:Label ID="LabelAverageCostPerUserPerDay" CssClass="Table_RowES" runat="server"
                                        Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelAverageCostPerUserPerDay_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelAverageCostPerPrinterPerDay" CssClass="Table_RowES" runat="server"
                                        Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelAverageCostPerPrinterPerDay_Value" CssClass="Table_RowValueES"
                                        runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px">
                                <td align="left">
                                    <asp:Label ID="LabelAverageColorPagesPerDay" CssClass="Table_RowES" runat="server"
                                        Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelAverageColorPagesPerDay_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelAverageBWPagesPerDay" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelAverageBWPagesPerDay_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px">
                                <td align="left">
                                    <asp:Label ID="LabelAverageTotalPagesPerDay" CssClass="Table_RowES" runat="server"
                                        Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelAverageTotalPagesPerDay_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <%--<asp:Label ID="LabelAverageTotalCostPerDay" CssClass="Table_RowES" runat="server"
                                Text="Average Total Cost Per Day:"></asp:Label>--%>
                                </td>
                                <td align="right">
                                    <%-- <asp:Label ID="LabelAverageTotalCostPerDay_Value" CssClass="Table_RowValueES" runat="server"
                                Text="0"></asp:Label>&nbsp;&nbsp;--%>
                                </td>
                            </tr>
                            <tr height="5">
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class=" Table_HeaderES">
                                <td align="left" colspan="4">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelProjections" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px" style="display: none">
                                <td align="left">
                                    <asp:Label ID="LabelCostPerMonth" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelCostPerMonth_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelCostPerQuarter" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelCostPerQuarter_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px" style="display: none">
                                <td align="left">
                                    <asp:Label ID="LabelCosts1Year" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelCosts1Year_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelCosts3Year" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelCosts3Year_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px">
                                <td align="left">
                                    <asp:Label ID="LabelPagesPermonth" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelPagesPermonth_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelPagesPerQuater" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelPagesPerQuater_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr class="GridRow" height="25px">
                                <td align="left">
                                    <asp:Label ID="LabelPagesPer1Year" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelPagesPer1Year_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="LabelPagesPer3Year" CssClass="Table_RowES" runat="server" Text=""></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelPagesPer3Year_Value" CssClass="Table_RowValueES" runat="server"
                                        Text="0"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <%-- <tr height="5">
        <td class="CenterBG">
            &nbsp;
        </td>
    </tr>
    <tr class=" Table_HeaderES">
        <td align="center" colspan="4">
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Contribution to Envrionment : "></asp:Label>
        </td>
        <td>
        <asp:Label ID="LabelEcoValue" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr height="5">
        <td class="CenterBG">
            &nbsp;
        </td>
    </tr>--%>
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center" colspan="4" class=" Table_HeaderES">
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <%-------------------------------------------TotalVolumeBreakdown----------------------------------------------------%>
            <tr>
                <td>
                </td>
                <td align="center" class="CenterBG">
                    <asp:Chart ID="TotalVolumeBreakdown" runat="server" Height="300px" Width="700px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderlineDashStyle="Solid"
                        Palette="Fire" BorderWidth="2" BackColor="Transparent" BorderColor="181, 64, 1"
                        BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                        <Legends>
                            <asp:Legend Enabled="true" IsTextAutoFit="true" Title="Color Mode" Name="Default"
                                BackColor="Transparent" Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 13pt, style=Bold">
                            </asp:Legend>
                        </Legends>
                        <Series>
                            <asp:Series BorderWidth="2" Name="User" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                BackGradientStyle="None" BackColor="Transparent">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
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
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="Center" class="CenterBG">
                    <asp:Table ID="TableTotalVolumeBreakdown" EnableViewState="false" CellSpacing="1"
                        CellPadding="3" Width="700px" BorderWidth="0" runat="server" CssClass="Table_bg">
                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellsNo" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellJobColorType" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellTotalPages" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPercentage" Text=""></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </td>
            </tr>
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <%-------------------------------------------TotalVolumeBreakdownPageSize----------------------------------------------------%>
            <tr>
                <td>
                </td>
                <td align="center" class="CenterBG">
                    <asp:Chart ID="TotalVolumeBreakdownPageSize" runat="server" Height="300px" Width="700px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderlineDashStyle="Solid"
                        Palette="Fire" BorderWidth="2" BackColor="Transparent" BorderColor="181, 64, 1"
                        BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                        <Legends>
                            <asp:Legend Enabled="true" IsTextAutoFit="true" Title="Page Size" Name="Default"
                                BackColor="Transparent" Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 12pt, style=Bold">
                            </asp:Legend>
                        </Legends>
                        <Series>
                            <asp:Series BorderWidth="2" Name="User" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                BackGradientStyle="None" BackColor="Transparent">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
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
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="Center" class="CenterBG">
                    <asp:Table ID="TableTotalVolumeBreakdownPageSize" EnableViewState="false" CellSpacing="1"
                        CellPadding="3" Width="700px" BorderWidth="0" runat="server" CssClass="Table_bg">
                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCell5" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellJobPaperSize" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellTotalPagesSize" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPercentageSize" Text=""></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </td>
            </tr>
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <%-------------------------------------------TotalVolumeBreakdownPageSizeBW----------------------------------------------------%>
            <tr>
                <td>
                </td>
                <td align="center" class="CenterBG">
                    <asp:Chart ID="TotalVolumeBreakdownPageSizeBW" runat="server" Height="300px" Width="700px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderlineDashStyle="Solid"
                        Palette="Fire" BorderWidth="2" BackColor="Transparent" BorderColor="181, 64, 1"
                        BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                        <Legends>
                            <asp:Legend Enabled="true" IsTextAutoFit="true" Title="Page Size" Name="Default"
                                BackColor="Transparent" Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 12pt, style=Bold">
                            </asp:Legend>
                        </Legends>
                        <Series>
                            <asp:Series BorderWidth="2" Name="User" ChartType="Pie" ShadowColor="64, 0, 0, 0"
                                BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                BackGradientStyle="None" BackColor="Transparent">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
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
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="Center" class="CenterBG">
                    <asp:Table ID="TableTotalVolumeBreakdownPageSizeBW" EnableViewState="false" CellSpacing="1"
                        CellPadding="3" Width="700px" BorderWidth="0" runat="server" CssClass="Table_bg">
                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCell4" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellJobPaperSizeBW" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellTotalPagesSizeBW" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPercentageBW" Text=""></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </td>
            </tr>
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center" colspan="4" class=" Table_HeaderES">
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelTopReport" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <%-------------------------------------------TotalVolumeBreakdownPrinters----------------------------------------------------%>
            <tr>
                <td>
                </td>
                <td align="center" class="CenterBG">
                    <asp:Chart ID="TotalVolumeBreakdownPrinters" runat="server" Height="300px" Width="700px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderlineDashStyle="Solid"
                        Palette="Fire" BorderWidth="2" BackColor="Transparent" BorderColor="181, 64, 1"
                        BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                        <Legends>
                            <asp:Legend Enabled="true" IsTextAutoFit="true" Title="Pages" Name="Default" BackColor="Transparent"
                                Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 12pt, style=Bold">
                            </asp:Legend>
                        </Legends>
                        <Series>
                            <asp:Series BorderWidth="2" Name="Color" ChartType="StackedColumn" ShadowColor="64, 0, 0, 0"
                                BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                            <asp:Series BorderWidth="2" Name="BW" ChartType="StackedColumn" ShadowColor="64, 0, 0, 0"
                                BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                BackGradientStyle="None" BackColor="Transparent">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
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
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="Center" class="CenterBG">
                    <asp:Table ID="TableTotalVolumeBreakdownPrinters" EnableViewState="false" CellSpacing="1"
                        CellPadding="3" Width="700px" BorderWidth="0" runat="server" CssClass="Table_bg">
                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellSerialno" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPrintersColor" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPrintersBW" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPrinterTotalPages" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPrinters" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPercentagePrinter" Text=""></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </td>
            </tr>
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <%-------------------------------------------TotalVolumeBreakdownUsers----------------------------------------------------%>
            <tr>
                <td>
                </td>
                <td align="center" class="CenterBG">
                    <asp:Chart ID="TotalVolumeBreakdownUsers" runat="server" Height="300px" Width="700px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderlineDashStyle="Solid"
                        Palette="Fire" BorderWidth="2" BackColor="Transparent" BorderColor="181, 64, 1"
                        BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                        <Legends>
                            <asp:Legend Enabled="true" IsTextAutoFit="true" Title="Pages" Name="Default" BackColor="Transparent"
                                Font="Calibri, 9pt, style=Regular" TitleFont="Calibri, 12pt, style=Bold">
                            </asp:Legend>
                        </Legends>
                        <Series>
                            <asp:Series BorderWidth="2" Name="Color" ChartType="StackedColumn" ShadowColor="64, 0, 0, 0"
                                BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                            <asp:Series BorderWidth="2" Name="BW" ChartType="StackedColumn" ShadowColor="64, 0, 0, 0"
                                BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                BackGradientStyle="None" BackColor="Transparent">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
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
            <tr height="5">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="Center" class="CenterBG">
                    <asp:Table ID="TableTotalVolumeBreakdownUsers" EnableViewState="false" CellSpacing="1"
                        CellPadding="3" Width="700px" BorderWidth="0" runat="server" CssClass="Table_bg">
                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCell2" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellUsesrColor" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellUsesrBW" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellUsesrTotalPages" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellUsesrsPages" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellUsesrPercentage" Text=""></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </td>
            </tr>
            <tr height="2">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <%-------------------------------------------WeekDayReport----------------------------------------------------%>
            <tr>
                <td>
                </td>
                <td align="center" class="CenterBG">
                    <asp:Chart ID="WeekDayReport" runat="server" Height="300px" Width="700px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                        ImageType="Png" BorderlineDashStyle="Solid" Palette="Fire" BorderWidth="2" BackColor="Transparent"
                        BorderColor="181, 64, 1" BorderlineColor="Green" BackHatchStyle="None" CssClass="CenterBG">
                        <Legends>
                            <asp:Legend Enabled="False" IsTextAutoFit="false" Name="Default" BackColor="Transparent"
                                Font="Calibri, 12pt, style=bold">
                            </asp:Legend>
                        </Legends>
                        <Series>
                            <asp:Series BorderWidth="2" Name="BW" Font="Calibri, 10pt,style=Regular" ChartType="Bubble"
                                ShadowColor="64, 0, 0, 0" BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                            <asp:Series BorderWidth="2" Name="Color" Font="Calibri, 10pt,style=Regular" ShadowColor="64, 0, 0, 0"
                                BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                            <asp:Series BorderWidth="2" Name="Total" Font="Calibri, 10pt,style=Regular" ChartType="Bubble"
                                ShadowColor="64, 0, 0, 0" BorderColor="180, 26, 59, 105" ShadowOffset="0">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" ShadowColor="Transparent"
                                BackGradientStyle="None" BackColor="Transparent">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                                    WallWidth="0" IsClustered="False"></Area3DStyle>
                                <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero="True">
                                    <LabelStyle Font="Calibri, 10pt" Format="N0" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisY>
                                <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                    <LabelStyle Font="Calibri, 10pt" Format="N0" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                    <MajorTickMark Size="2" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
            </tr>
            <tr height="2">
                <td>
                </td>
                <td class="CenterBG">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="Center" class="CenterBG">
                    <asp:Table ID="TableWeekDayReport" EnableViewState="false" CellSpacing="1" CellPadding="3"
                        Width="700px" BorderWidth="0" runat="server" CssClass="Table_bg">
                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCell3" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellDays" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellTotalBW" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellTotalColor" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellTotal" Text=""></asp:TableHeaderCell>
                            <asp:TableHeaderCell runat="server" ID="TableHeaderCellPercentageDayReport" Text=""></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
