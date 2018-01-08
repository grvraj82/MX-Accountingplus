<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    Inherits="ViewsJobLog" CodeBehind="JobLog.aspx.cs" Title="AccountingPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageContent" ID="PC" runat="server">
    <script language="javascript" type="text/javascript">

        Meuselected("JobLog");
        DisplayTabs();
        function validatePageCount() {

            var totalRecords = document.getElementById('ctl00_PageContent_LabelTotalPages').innerText; //document.getElementById('<%= HiddenFieldPageCount.ClientID%>').value;
            var totalRecordsCount = totalRecords.substring(1, totalRecords.length);
            var inputCount = document.getElementById('<%= TextBoxCurrentPage.ClientID%>').value;

            if (parseInt(inputCount) > parseInt(totalRecordsCount)) {
                document.getElementById('<%= TextBoxCurrentPage.ClientID%>').value = totalRecordsCount;
                jNotify(C_PAGE_COUNT_LESS_THAN)
                return false;
            }
            else {
                return true;
            }

        }
        function Hide() {

            document.getElementById('divExport').style.display = "none";
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenFieldPageCount" runat="server" Value="0" />
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image7" SkinID="HeadingLeft" runat="server" />
            </td>
            <td align="left" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" height="33" width="100%">
                    <tr class="Top_menu_bg">
                        <td width="50%" align="left" valign="middle" height="33">
                            <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="HeadingMiddleBg" Width="2%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadJobLog" runat="server" Text="Label"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp; </asp:TableCell>
                                    <asp:TableCell ID="tabelCellLabelUserSource" align="center" valign="middle" runat="server"
                                        Visible="true" CssClass="NoWrap" Style="display: none">
                                        &nbsp;<asp:Label ID="LabelUsers" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                        :&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell1" align="center" valign="middle" runat="server" Visible="true"
                                        CssClass="NoWrap" Style="display: none">
                                        <asp:DropDownList ID="DropDownUsers" AutoPostBack="true" runat="server" CssClass="FormDropDown_Small">
                                            <asp:ListItem Value="-1" Text="All" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell Style="display: none">
                                        <asp:Image ID="ImageMenuSplit" runat="server" SkinID="ManageusersimgSplit" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                    <asp:TableCell ID="TableCell3" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
                                        &nbsp;<asp:Label ID="LabelDevice" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                        :&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell4" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
                                        <asp:DropDownList ID="DropDownDevice" AutoPostBack="true" runat="server" CssClass="FormDropDown_Small"
                                            OnSelectedIndexChanged="DropDownDevice_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image2" runat="server" SkinID="ManageusersimgSplit" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                    <asp:TableCell ID="TableCell6" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
                                        <asp:ImageButton SkinID="JoblogimgCSV" ID="ImageButtonExportToExcel" runat="server"
                                            OnClick="ImageButtonExportToExcel_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                    <asp:TableCell ID="TableCell7" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="Menu_split">
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                    <asp:TableCell ID="TableCell8" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
                                        <asp:ImageButton ID="ButtonClearLog" SkinID="JoblogimgClear" runat="server" OnClick="ButtonClearLog_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                    <asp:TableCell ID="TableCell9" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="Menu_split">
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell12" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
                                        <asp:ImageButton ID="ImageButtonSetting" SkinID="JobLogSettings" runat="server" OnClick="ImageButtonSetting_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                    <asp:TableCell ID="TableCell11" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="Menu_split">
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell10" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
                                        <asp:ImageButton ID="ImageButtonRefresh" SkinID="JoblogimgRefresh" runat="server"
                                            OnClick="ImageButtonRefresh_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="70%">
                                                
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" border="0" height="33" width="100%">
                    <tr class="Top_menu_bg">
                        <td class="HeaderPadding">
                            <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell ID="TableCell20" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
                                        &nbsp;&nbsp;<asp:Label ID="LabelFromDate" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                        :&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell21" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
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
                                            ErrorMessage=""  Operator="LessThanEqual"   Display="None"></asp:CompareValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="TopLeft"
                                            runat="server" TargetControlID="cmpStartDate">
                                        </cc1:ValidatorCalloutExtender>--%>
                                        <%--  <cc1:CalendarExtender ID="CalendarExtenderFrom" runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxFromDate">
                                        </cc1:CalendarExtender>--%>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    &nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image3" runat="server" SkinID="ManageusersimgSplit" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell22" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
                                        &nbsp;&nbsp;<asp:Label ID="LabelToDate" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                        :&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell23" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="NoWrap">
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
                                        <%-- <asp:CompareValidator ID="CompareValidatorToDate" runat="server" ControlToValidate="TextBoxToDate"
                                            ErrorMessage="End date cannot be greater than today's date" Operator="LessThanEqual"
                                             Display="None"></asp:CompareValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" PopupPosition="TopLeft"
                                            TargetControlID="CompareValidatorToDate" runat="server">
                                        </cc1:ValidatorCalloutExtender>--%>
                                        <%-- <cc1:CalendarExtender ID="CalendarExtenderTo" runat="server" Format="MM/dd/yyyy" TargetControlID="TextBoxToDate">
                                        </cc1:CalendarExtender>--%>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell24" align="center" valign="middle" runat="server" Visible="true"
                                        HorizontalAlign="Left" CssClass="NoWrap">
                                        &nbsp;&nbsp;<asp:Button ID="ButtonGo" CssClass="Login_Button" runat="server" Text=""
                                            ToolTip="" OnClick="ButtonGo_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                        <td width="50%" height="33" align="right" valign="middle" class="HeaderPaddingR">
                            <asp:UpdatePanel runat="server" ID="PaginationPanel">
                                <ContentTemplate>
                                    <asp:Table ID="Table2" runat="server" CellPadding="2" CellSpacing="0">
                                        <asp:TableRow>
                                            <asp:TableCell ID="TableCell14" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="NoWrap">
                                                <asp:Label ID="LabelPageSize" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell15" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="NoWrap">
                                                <asp:DropDownList ID="DropDownPageSize" runat="server" AutoPostBack="true" CssClass="Normal_FontLabel"
                                                    OnSelectedIndexChanged="DropDownPageSize_SelectedIndexChanged">
                                                    <asp:ListItem Selected="true">50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>500</asp:ListItem>
                                                    <asp:ListItem>1000</asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell16" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="Menu_split">
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell17" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="NoWrap">
                                                <asp:Label ID="LabelPage" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell18" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="NoWrap">
                                                <asp:ImageButton ID="ButtonPrevious" SkinID="AuditLogSearchPrev" runat="server" OnClick="ButtonPrevious_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell19" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="NoWrap">
                                                <asp:TextBox ID="TextBoxCurrentPage" runat="server" CssClass="Normal_FontLabel" OnTextChanged="TextBoxCurrentPage_OnTextChanged"
                                                    Width="25">1</asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell25" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="NoWrap">
                                                <asp:DropDownList ID="DropDownCurrentPage" Visible="false" runat="server" AutoPostBack="true"
                                                    CssClass="Normal_FontLabel" OnSelectedIndexChanged="DropDownCurrentPage_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell26" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="NoWrap">
                                                <asp:Label ID="LabelTotalPages" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell27" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="NoWrap">
                                                <asp:ImageButton ID="ButtonNext" runat="server" SkinID="AuditLogSearchNext" ImageUrl="~/App_Themes/Blue/Images/Search_Next.png"
                                                    OnClick="ButtonNext_Click" />
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell13" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                                VerticalAlign="Middle" CssClass="Menu_split">
                                                &nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell2" align="center" valign="middle" runat="server" Visible="true"
                                                CssClass="NoWrap">
                                                <asp:Label ID="LabelTotalRecordsTitle" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell5" align="center" valign="middle" runat="server" Visible="true"
                                                CssClass="NoWrap">
                                                <asp:Label ID="LabelTotalRecordsValue" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DropDownUsers" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownDevice" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownPageSize" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
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
                                                <td align="left">
                                                    <div id="JobLogDisplay_Overflow" style="width: 100%; overflow: auto;">
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                            <ProgressTemplate>
                                                                <asp:Label ID="LabelUpdatingPleaseWait" runat="server" Text="Updating Please wait..."></asp:Label><br />
                                                                <asp:Image ID="ImgUpdateProgress" runat="server" SkinID="UpdateProgress" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:UpdatePanel runat="server" ID="Panel">
                                                            <ContentTemplate>
                                                                <asp:Table EnableViewState="false" Width="100%" ID="TableJobLog" HorizontalAlign="Left"
                                                                    CellPadding="4" CellSpacing="1" BorderWidth="0" runat="server" SkinID="DoubleHeaderGrid">
                                                                    <asp:TableRow SkinID="TableDoubleRow">
                                                                        <asp:TableCell RowSpan="2" Wrap="false" Width="40px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellReferenceNo" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>
                                                                        <asp:TableCell ID="TableCellMfpIPAddress" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Mfp IP Address</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellMfpMACAddress" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Mfp 
                                                            MAC Address</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellJobID" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Job 
                                                            ID</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellJobMode" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Job 
                                                            Mode</asp:TableCell>
                                                                       <%-- <asp:TableCell ID="TableCellJobName" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Job 
                                                            Name</asp:TableCell>--%>
                                                            <asp:TableCell ID="TableCellFileName" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">File 
                                                            Name</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellPaperSize" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Paper 
                                                            Size</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellComputerName" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Computer 
                                                            Name</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellUserName" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">User 
                                                            Name</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellLoginName" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M">Login 
                                                            Name</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellDate" ColumnSpan="2" Wrap="false" CssClass="DoubleRowHeaderSmall">Date</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellTotalCount" ColumnSpan="4" CssClass="DoubleRowHeaderSmall">Total Count</asp:TableCell>
                                                                         <asp:TableCell ID="TableCellTotal" Wrap="false" CssClass="DoubleRowHeader_M" RowSpan="2">Total</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellResult" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>
                                                                        <asp:TableCell ID="TableCellNote" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>
                                                                        <asp:TableCell ID="TableCellCostCenterName" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>
                                                                        <asp:TableCell ID="TableCellPrice" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>
                                                                        <asp:TableCell ID="TableCellUserSource" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>
                                                                        <asp:TableCell ID="TableCellDomain" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>
                                                                        <asp:TableCell ID="TableCellServerDate" RowSpan="2" Wrap="false" CssClass="DoubleRowHeader_M"></asp:TableCell>                                                                        
                                                                    </asp:TableRow>
                                                                    <asp:TableRow Height="24px" SkinID="TableDoubleRow">
                                                                        <asp:TableCell ID="TableCellStart" Wrap="false" CssClass="DoubleRowHeaderSmall">Start</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellComplete" Wrap="false" CssClass="DoubleRowHeaderSmall">Complete</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellBlackWhite" Wrap="false" CssClass="DoubleRowHeaderSmall">Black 
                                                            &amp; White</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellFullColor" Wrap="false" CssClass="DoubleRowHeaderSmall">Full 
                                                            Color</asp:TableCell>
                                                                        <asp:TableCell ID="TableCell2Color" Wrap="false" CssClass="DoubleRowHeaderSmall">2 Color</asp:TableCell>
                                                                        <asp:TableCell ID="TableCellSingleColor" Wrap="false" CssClass="DoubleRowHeaderSmall">Single Color</asp:TableCell>                                                                       
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="DropDownUsers" EventName="SelectedIndexChanged" />
                                                                <asp:AsyncPostBackTrigger ControlID="DropDownDevice" EventName="SelectedIndexChanged" />
                                                                <asp:AsyncPostBackTrigger ControlID="DropDownPageSize" EventName="SelectedIndexChanged" />
                                                                <asp:AsyncPostBackTrigger ControlID="ButtonClearLog" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr height="2">
                                    <td>
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
    <div id="divExport" style="width: 500px; margin: 0 auto; position: absolute; top: 200px;
        left: 400px;">
        <asp:Table ID="TableWarningMessage" CellSpacing="0" CellPadding="3" Width="100%"
            BorderWidth="1" Visible="false" runat="server" CssClass="Table_bg" SkinID="Grid">
            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                <asp:TableHeaderCell ID="TableHeaderCellJobLogExport" CssClass="Grid_topbg1" ColumnSpan="2"
                    HorizontalAlign="Left" Text=""></asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow BackColor="White">
                <asp:TableCell ID="TableCell28" HorizontalAlign="Left" Width="20%">
                    <asp:Image ID="Image1" runat="server" SkinID="PermessionsAndLimitsCritical" />
                </asp:TableCell>
                <asp:TableCell ID="TableCell29" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                    <asp:Label ID="LabelExportStatus" CssClass="LabelLoginFont" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BackColor="White">
                <asp:TableCell ID="TableCell30" HorizontalAlign="Left" Width="20%">
               
                </asp:TableCell>
                <asp:TableCell ID="TableCell31" HorizontalAlign="Left" Width="80%">
                    <asp:Table CellSpacing="5" CellPadding="5" ID="Table4" runat="server">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Button ID="ButtonOk" runat="server" Text="" Width="100px" OnClick="ButtonOk_Click"
                                    OnClientClick="Hide();" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Button ID="ButtonCancel" runat="server" Text="" Width="100px" OnClick="ButtonCancel_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <script language="javascript" type="text/javascript">
        function ReSizeDiv() {
            var divID = document.getElementById("JobLogDisplay_Overflow");
            divID.style.width = parseInt(screen.availWidth - 300) + 'px';
            //divID.style.height = parseInt(screen.availHeight - 300) + 'px';
        }
        ReSizeDiv();
        
    </script>
</asp:Content>
