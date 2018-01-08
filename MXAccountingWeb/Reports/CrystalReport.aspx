<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="CrystalReport.aspx.cs" Inherits="AccountingPlusWeb.Reports.CrystalReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowCellReports();
        Meuselected("Reports");

    </script>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image7" SkinID="HeadingLeft" runat="server" />
            </td>
            <td align="left" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" height="33" width="100%">
                    <tr class="Top_menu_bg">
                        <td width="90%" align="left" valign="middle" height="33">
                            <asp:Table ID="Table2" runat="server" CellPadding="0" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="HeadingMiddleBg" Width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadInvoiceReport" runat="server" Text="Advanced Report"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                            Width="40%">
                                            <asp:TableRow Height="30px">
                                            <asp:TableCell align="center" runat="server" Visible="true" HorizontalAlign="Right"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="User Source"></asp:Label>
                                            </asp:TableCell>

                                             <asp:TableCell ID="TableCell1" align="center" runat="server" Visible="true" HorizontalAlign="Right"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                                 &nbsp;&nbsp;<asp:DropDownList ID="DropDownListUserSource" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListUserSource_SelectedIndexChanged">
                                                            <asp:ListItem Value="-1" Text="All" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="DB"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="AD"></asp:ListItem>
                                                        </asp:DropDownList>
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell2" align="center" runat="server" Visible="true" HorizontalAlign="Right"
                                                    CssClass="NoWrap" VerticalAlign="Middle">
                                            &nbsp;&nbsp;<asp:DropDownList ID="DropDownListADlist" runat="server" Visible="false">

                                                        </asp:DropDownList>
                                            </asp:TableCell>

                                                <asp:TableCell ID="TableCell3" align="center" runat="server" Visible="true" HorizontalAlign="Right"
                                                    CssClass="NoWrap" VerticalAlign="Middle" Width="70px">
                                                    &nbsp;&nbsp;<asp:Label ID="LabelFromDate" runat="server" Text="From Date" SkinID="TotalResource"></asp:Label>
                                                    :&nbsp;&nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell4" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle" Width="150px">
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
                                                    <%-- <asp:TextBox ID="TextBoxFromDate" Width="80px" CssClass="Normal_FontLabel" runat="server"></asp:TextBox>
                                                     <asp:CompareValidator ID="cmpStartDate" runat="server" ControlToValidate="TextBoxFromDate"
                                                        ErrorMessage="" Type="Date" Operator="LessThanEqual" Display="None"></asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="TopLeft"
                                                        runat="server" TargetControlID="cmpStartDate">
                                                    </cc1:ValidatorCalloutExtender>
                                                     <cc1:CalendarExtender ID="CalendarExtenderFrom"  runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxFromDate">
                                                    </cc1:CalendarExtender>--%>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell6" align="center" runat="server" Visible="true" HorizontalAlign="Right"
                                                    CssClass="NoWrap" VerticalAlign="Middle" Width="70px">
                                                    &nbsp;&nbsp;<asp:Label ID="LabelToDate" runat="server" Text="To Date" SkinID="TotalResource"></asp:Label>
                                                    :&nbsp;&nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell7" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                    CssClass="NoWrap" VerticalAlign="Middle" Width="150px">
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
                                                    <%-- <asp:TextBox ID="TextBoxToDate" Width="80px" CssClass="Normal_FontLabel" runat="server"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidatorToDate" runat="server" Type="Date" ControlToValidate="TextBoxToDate"
                                                        ErrorMessage="End date cannot be greater than today's date" Operator="LessThanEqual"
                                                         Display="None"></asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" PopupPosition="TopLeft"
                                                        TargetControlID="CompareValidatorToDate" runat="server">
                                                    </cc1:ValidatorCalloutExtender>
                                                     <cc1:CalendarExtender ID="CalendarExtenderTo" runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxToDate">
                                                    </cc1:CalendarExtender>--%>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Table runat="server" CellPadding="3" CellSpacing="3">
                                                        <asp:TableRow>
                                                            <asp:TableCell>
                                                                <asp:ImageButton ID="ImageButtonExportToExcel" runat="server" OnClick="ExportToExcel_Click"
                                                                    ImageUrl="~/App_Themes/Blue/Images/excel-icon.png" />
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:ImageButton ID="ImageButtonExportToWord" runat="server" OnClick="ExportToWord_Click"
                                                                    ImageUrl="~/App_Themes/Blue/Images/html.png" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                        <td width="65%" height="33" align="right" valign="middle" class="HeaderPaddingR">
                        </td>
                    </tr>
                    <tr>
                        <td height="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="center">
                            <asp:Panel ID="Panel1" runat="server" GroupingText="Report Level(s)" Width="500px">
                                <table width="100%" cellpadding="3" cellspacing="3" border="0">
                                    <tr>
                                        <td align="right" width="40%">
                                            <asp:Label ID="Label2" runat="server" Text="Number of Level(s)"></asp:Label>&nbsp;&nbsp;
                                        </td>
                                        <td align="left" width="60%">
                                            <asp:DropDownList ID="DropDownListLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListLevel_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table cellpadding="10" cellspacing="3" border="0" width="100%">
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td width="60px">
                                                        <asp:Label ID="LabelLevel1" runat="server" Text="Level 1"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:DropDownList ID="DropDownListGroup1" runat="server" Visible="false">
                                                            <asp:ListItem Value="MFPIP" Selected="True">MFP IP</asp:ListItem>
                                                            <asp:ListItem Value="UserName">User Name</asp:ListItem>
                                                            <asp:ListItem Value="CostCenter">Cost Center</asp:ListItem>
                                                            <asp:ListItem Value="PaperSize">Paper Size</asp:ListItem>
                                                            <asp:ListItem Value="Department">Department</asp:ListItem>
                                                            <asp:ListItem Value="JOB_FILE_NAME">Job Name</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LabelGroup01" runat="server" Text=" = "></asp:Label>
                                                        <asp:TextBox ID="TextBoxGroup1" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td width="60px">
                                                        <asp:Label ID="LabelLevel2" Visible="false" runat="server" Text="Level 2"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="DropDownListGroup2" runat="server" Visible="false">
                                                            <asp:ListItem Value="MFPIP">MFP IP</asp:ListItem>
                                                            <asp:ListItem Value="UserName">User Name</asp:ListItem>
                                                            <asp:ListItem Value="CostCenter">Cost Center</asp:ListItem>
                                                            <asp:ListItem Value="PaperSize">PaperSize</asp:ListItem>
                                                            <asp:ListItem Value="Department">Department</asp:ListItem>
                                                         
                                                            <asp:ListItem Value="JOB_FILE_NAME">Job Name</asp:ListItem>
                                                            <asp:ListItem Value="None" Selected="True">None</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LabelGroup02" Visible="false" runat="server" Text=" = "></asp:Label>
                                                        <asp:TextBox ID="TextBoxGroup2" Visible="false" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td width="60px">
                                                        <asp:Label ID="LabelLevel3" Visible="false" runat="server" Text="Level 3"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="DropDownListGroup3" runat="server" Visible="false">
                                                            <asp:ListItem Value="MFPIP">MFP IP</asp:ListItem>
                                                            <asp:ListItem Value="UserName">User Name</asp:ListItem>
                                                            <asp:ListItem Value="CostCenter">Cost Center</asp:ListItem>
                                                            <asp:ListItem Value="PaperSize">Paper Size</asp:ListItem>
                                                            <asp:ListItem Value="Department">Department</asp:ListItem>
                                                       
                                                            <asp:ListItem Value="JOB_FILE_NAME">Job Name</asp:ListItem>
                                                            <asp:ListItem Value="None" Selected="True">None</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LabelGroup03" Visible="false" runat="server" Text=" = "></asp:Label>
                                                        <asp:TextBox ID="TextBoxGroup3" Visible="false" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20px">
                                                    </td>
                                                    <td width="60px">
                                                        <asp:Label ID="LabelLevel4" Visible="false" runat="server" Text="Level 4"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListGroup4" runat="server" Visible="false">
                                                            <asp:ListItem Value="MFPIP">MFP IP</asp:ListItem>
                                                            <asp:ListItem Value="UserName">User Name</asp:ListItem>
                                                            <asp:ListItem Value="CostCenter">Cost Center</asp:ListItem>
                                                            <asp:ListItem Value="PaperSize">Paper Size</asp:ListItem>
                                                            <asp:ListItem Value="Department">Department</asp:ListItem>
                                                          
                                                            <asp:ListItem Value="JOB_FILE_NAME">Job Name</asp:ListItem>
                                                            <asp:ListItem Value="None" Selected="True">None</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LabelGroup04" Visible="false" runat="server" Text=" = "></asp:Label>
                                                        <asp:TextBox ID="TextBoxGroup4" Visible="false" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td height="2" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="ButtonGenerate" runat="server" CssClass="Login_Button" Text="Generate"
                                OnClick="ButtonGenerate_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td height="2" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" class="" width="95%">
                                <tr class="Grid_tr" align="center">
                                    <td align="left">
                                        <asp:Panel ID="PanelDataContainer" runat="server">
                                            <asp:Table EnableViewState="false" ID="TableReport" HorizontalAlign="Left" CellPadding="3"
                                                CellSpacing="1" BorderWidth="0" runat="server" CssClass="Table_bg AlternateRow" Width="100%"
                                                border="0">
                                            </asp:Table>
                                        </asp:Panel>
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
