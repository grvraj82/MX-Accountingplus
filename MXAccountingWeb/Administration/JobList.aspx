<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    CodeBehind="JobList.aspx.cs" Inherits="PrintRoverWeb.Administration.JobList"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageContent" ID="PC" runat="server">
    <script language="javascript" type="text/javascript">
        DisplayTabs();
        Meuselected("JobList");
        function togall(refcheckbox) {

            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenJobsCount").value);
            if (totalRecords == "1") {
                if (document.getElementById("chkALL").checked == true) {
                    document.getElementById("chkALL").checked = false
                }
                else {
                    document.getElementById("chkALL").checked = true
                }
                ChkandUnchk();
                return;
            }

            var thisForm = document.forms[0];
            var users = thisForm.__JOBLIST.length;
            var selectedCount = 0;
            if (thisForm.__JOBLIST[refcheckbox].checked) {

                thisForm.__JOBLIST[refcheckbox].checked = false;
            }
            else {
                thisForm.__JOBLIST[refcheckbox].checked = true;
            }
            ValidateSelectedCount();
        }
        function DeleteJobs() {
            if (IsJobSelected()) {
                return confirm(C_PRINTJOBS_DELETE_CONFIRM);
            }
            else {
                return false;
            }
        }

        function IsJobSelected() {
            var thisForm = document.forms[0];
            var jobs = thisForm.__JOBLIST.length;
            var selectedCount = 0;

            if (jobs > 0) {
                for (var item = 0; item < jobs; item++) {
                    if (thisForm.__JOBLIST[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__JOBLIST.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {

                jNotify(C_JOBLIST_SELECTONE);
                return false;
            }
        }

        function ChkandUnchk() {
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = true;
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = false;
                }
            }
            if (document.getElementById("ctl00_PageContent_HiddenJobsCount").value == 0) {

                document.getElementById("chkALL").checked = false;

            }
        }
        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__JOBLIST.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__JOBLIST[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__JOBLIST.checked) {
                    selectedCount++
                }
            }
            return selectedCount;

        }

        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenJobsCount").value);

            if (totalRecords == GetSeletedCount()) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;

            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }

        }
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" height="33" width="100%">
                    <tr class="Top_menu_bg">
                        <td class="HeadingMiddleBg" style="width: 10%">
                            <div style="padding: 4px 10px 0px 10px;">
                                <asp:Label ID="LabelHeadPrintJobs" runat="server" Text=""></asp:Label></div>
                        </td>
                        <td>
                            <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                        </td>
                        <td valign="middle">
                            <asp:Table ID="Table1" runat="server" CellPadding="2" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell ID="tabelCellLabelUserSource" align="center" valign="middle" runat="server"
                                        Visible="true">
                                        <asp:Label ID="LabelUserSource" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;:
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell1" align="center" valign="middle" runat="server" Visible="true">
                                        <asp:DropDownList ID="DropDownListUserSource" runat="server" AutoPostBack="true"
                                            CssClass="FormDropDown_Small" OnSelectedIndexChanged="DropDownListUserSource_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellDomainName" align="center" valign="middle" runat="server" Visible="true">
                                        <asp:Label ID="LabelDomainName" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;:
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellDomainDropDown" align="center" valign="middle" runat="server" Visible="true">
                                        <asp:DropDownList ID="DropDownListDomainName" runat="server" AutoPostBack="true"
                                            CssClass="FormDropDown_Small" OnSelectedIndexChanged="DropDownListDomainName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellUsers" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle">
                                        <asp:Label ID="LabelUsers" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;:
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellUserDropDown" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle">
                                        <asp:DropDownList ID="DropDownUser" CssClass="FormDropDown_Small" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="DropDownUser_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellDelete" CssClass="MenuSpliter" align="center" runat="server"
                                        Visible="true" HorizontalAlign="Left" VerticalAlign="Middle">
                                        <asp:ImageButton ID="ImageButtonDelete" SkinID="JoblogimgClear" runat="server" OnClick="ImageButtonDelete_Click" />&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell10" CssClass="MenuSpliter" align="center" runat="server"
                                        Visible="true" HorizontalAlign="Left" VerticalAlign="Top">
                                        <asp:ImageButton ID="ImageButton1" ToolTip="" runat="server" CausesValidation="False"
                                            SkinID="JobListSettings" OnClick="ImageButtonSetting_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell1MFP" CssClass="MenuSpliter" align="center" runat="server"
                                        Visible="true" HorizontalAlign="Left" VerticalAlign="Middle">
                                        <asp:Label ID="LabelMFP" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;:
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellDrpMFPList" CssClass="MenuSpliter" align="center" runat="server"
                                        Visible="true" HorizontalAlign="Left" VerticalAlign="Middle">
                                        <asp:DropDownList ID="DropDownListMFP" runat="server" CssClass="FormDropDown_Small">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellLabelPreferedCC" CssClass="MenuSpliter" align="center"
                                        runat="server" Visible="true" HorizontalAlign="Left" VerticalAlign="Middle">
                                        <asp:Label ID="LabelPreferredCostCenter" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;:
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellDrpPreCC" CssClass="MenuSpliter" align="center" runat="server"
                                        Visible="true" HorizontalAlign="Left" VerticalAlign="Middle">
                                        <asp:DropDownList ID="DropDownListPreferredCostCenter" runat="server" CssClass="FormDropDown_Small">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCellBtnPrint" CssClass="MenuSpliter" align="center" runat="server"
                                        Visible="true" HorizontalAlign="Left" VerticalAlign="Middle">
                                        <asp:Button ID="ButtonPrint" runat="server" Text="" CssClass="Login_Button" OnClick="ButtonPrint_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell8" CssClass="MenuSpliter" align="center" runat="server"
                                        Visible="true" HorizontalAlign="Left" VerticalAlign="Top">
                                        <asp:ImageButton ID="ImageButtonRefresh" ToolTip="" runat="server" CausesValidation="False"
                                            SkinID="JobListRefreshicon" OnClick="ImageButtonRefresh_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="5">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top" align="center" class="CenterBG">
                <asp:Panel ID="PanelMainData" runat="server">
                    <table width="98%" align="center" border="0" cellpadding="0" cellspacing="0" class="">
                        <tr class="Grid_tr" style="height: 500px">
                            <td valign="top" style="height: 500px">
                                <asp:Table EnableViewState="false" ID="TableJobList" CellPadding="3" Width="100%"
                                    CellSpacing="1" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                        <asp:TableHeaderCell Wrap="false" Width="30px" HorizontalAlign="Left">
                                            <input id="chkALL" onclick="ChkandUnchk()" type="checkbox" />
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" HorizontalAlign="Left" Width="30px"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellUser" Text="" HorizontalAlign="Left"
                                            CssClass="H_title" Width="10%"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellDate" Text="" HorizontalAlign="Left"
                                            CssClass="H_title" Width="15%"></asp:TableHeaderCell>
                                        <asp:TableHeaderCell Wrap="false" ID="TableHeaderCellJobName" Text="" HorizontalAlign="Left"
                                            CssClass="H_title"></asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                </asp:Table>
                                <asp:Label ID="LabelNoJobs" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                <asp:HiddenField ID="HiddenJobsCount" Value="0" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Table ID="TableWarningMessage" Visible="false" CellSpacing="1" CellPadding="3"
                    Width="50%" runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                        <asp:TableHeaderCell ID="TableHeaderCellDivName" CssClass="LabelWarningFont" ColumnSpan="2"
                            HorizontalAlign="Left"></asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow CssClass="GridRow">
                        <asp:TableCell ID="TableCellWarningImage" HorizontalAlign="Center" Width="20%">
                            <asp:Image ID="ImageWarning" runat="server" SkinID="PermessionsAndLimitsCritical" />
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell11" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                            <p class="LabelLoginFont">
                            </p>
                            <p class="LabelWarningFont">
                                <asp:Label ID="LabelWarningMessage" runat="server" Text=""></asp:Label></p>
                            <p class="LabelLoginFont">
                            </p>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Content>
