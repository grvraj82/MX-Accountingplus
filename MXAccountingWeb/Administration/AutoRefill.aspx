<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="AutoRefill.aspx.cs" Inherits="AccountingPlusWeb.Administration.AutoRefill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowCellSettings();
        Meuselected("Settings");

        function DeleteAutorefill() {
           
                var confirmflag = 'selected autorefill will be deleted. Are you sure?';
                if (!confirmflag) {
                    return false;
                }
                else {
                    return true;
                }
           
        }

        function SetValue(isChecked, controlName) {
            var controlObject = eval("document.forms[0]." + controlName);
            if (isChecked) {
                controlObject.value = 1;
            }
            else {
                controlObject.value = 0;
            }
            ToggleSelectAllPermissions();
        }

        function ChangeOverDraft(isChecked) {
            var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
            for (var i = 1; i < jobTypesCount.value; i++) {
                var destControlObject = eval("document.forms[0].__ALLOWEDOVERDRAFT_" + i);
                if (isChecked) {
                    destControlObject.disabled = false;
                }
                else {
                    destControlObject.disabled = false;//true to enable allow over draft
                }
            }
            ToggleOD();
        }

        function funNumber() {
            if (event.keyCode >= 48 && event.keyCode <= 57)
            { return true; }
            else if (event.keyCode >= 45 && event.keyCode <= 46) {
                event.returnValue = false;
                return false;
            }
            else {
                event.returnValue = false;
                return false;
            }
        }

        function SetUnlimitedValue(isUnlimitedChecked, destControlName, dbLimitControlName) {

            var destControlObject = eval("document.forms[0]." + destControlName);
            var dbLimitControlObject = eval("document.forms[0]." + dbLimitControlName);

            if (isUnlimitedChecked) {
                destControlObject.value = document.forms[0].infinityValue.value;
                destControlObject.disabled = true;
            }
            else {
                if (dbLimitControlObject.value == "2147483647") // int.MaxValue
                {
                    destControlObject.value = 0;
                }
                else {
                    destControlObject.value = dbLimitControlObject.value;
                }
                destControlObject.disabled = false;
            }
            ToggleSelectAllLimits();
        }

        function ChkandUnchkLimits() {
            var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
            for (var i = 1; i < jobTypesCount.value; i++) {
                var destControlObject = eval("document.forms[0].__JOBLIMITDB_" + i);
                var isChecked = destControlObject.value;

                if (document.getElementById('selectAllLimits').checked) {
                    document.getElementById('__ISJOBLIMITSET_' + i).checked = true; //__JOBLIMIT_
                    destControlObject.value = 1;
                    var dbLimitControlObject = eval("document.forms[0].__JOBLIMIT_" + i);
                    dbLimitControlObject.value = document.forms[0].infinityValue.value;
                    dbLimitControlObject.disabled = true;
                    document.getElementById('jobLimit').value = document.forms[0].infinityValue.value;
                    document.getElementById('jobLimit').disabled = true;
                }
                else {
                    document.getElementById('__ISJOBLIMITSET_' + i).checked = false;
                    destControlObject.value = 0;
                    var dbLimitControlObject = eval("document.forms[0].__JOBLIMIT_" + i);
                    dbLimitControlObject.value = 0;
                    dbLimitControlObject.disabled = false;
                    document.getElementById('jobLimit').value = 0;
                    document.getElementById('jobLimit').disabled = false;
                }
            }
            ToggleSelectAllLimits();
        }

        function ToggleSelectAllLimits() {
            try {
                var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
                var totalJobsCount = jobTypesCount.value;
                totalJobsCount = totalJobsCount - 1;
                var selectedCount = 1;
                for (var i = 1; i < totalJobsCount; i++) {
                    //var destControlObject = eval("document.forms[0].__ISJOBLIMITSET_" + i);
                    var destControlObject = document.getElementById("__ISJOBLIMITSET_" + i);
                    var isChecked = destControlObject.checked;
                    if (isChecked) {
                        selectedCount++;
                    }
                    else {

                    }
                }
                if (selectedCount == totalJobsCount) {
                    document.getElementById('selectAllLimits').checked = true;
                    document.getElementById('jobLimit').value = document.forms[0].infinityValue.value;
                    document.getElementById('jobLimit').disabled = true;
                    document.getElementById('jobLimitApplyToAll').style.display = "none";
                    document.getElementById('jobLimitImage').style.display = "none";
                }
                else {
                    document.getElementById('selectAllLimits').checked = false;
                    document.getElementById('jobLimit').value = 0;
                    document.getElementById('jobLimit').disabled = false;

                    document.getElementById('jobLimitApplyToAll').style.display = "inline";
                    document.getElementById('jobLimitImage').style.display = "inline";
                }
            }
            catch (Error) {
            }
        }

        function ChkandUnchkPermissions() {
            var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
            for (var i = 1; i <= jobTypesCount.value; i++) {
                var destControlObject = eval("document.forms[0].__ISJOBTYPESELECTED_" + i);
                var jobType = eval("document.forms[0].__JOBTYPEID_" + i).value;
                //alert(jobType);
                if (jobType != "Scan BW") {
                    var isChecked = destControlObject.value;
                    if (document.getElementById('selectAllPermissions').checked) {
                        document.getElementById('__ISJOBALLOWED_' + i).checked = true;
                        destControlObject.value = 1;
                    }
                    else {
                        document.getElementById('__ISJOBALLOWED_' + i).checked = false;
                        destControlObject.value = 0;
                    }
                }
            }
            ToggleSelectAllPermissions();
        }

        function ChkandUnchkUsers() {
            if (document.getElementById('CheckboxUserAll').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].value != 'admin') {
                        if (document.getElementById('aspnetForm').elements[i].name == '__USERID') {

                            document.getElementById('aspnetForm').elements[i].checked = true;
                        }
                    }

                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__USERID') {

                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }

                }
            }
        }

        function ToggleSelectAllPermissions() {
            try {
                var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
                var totalJobsCount = jobTypesCount.value;
                var selectedCount = 0;
                for (var i = 1; i <= totalJobsCount; i++) {
                    var destControlObject = eval("document.forms[0].__ISJOBTYPESELECTED_" + i);
                    var isChecked = destControlObject.value;
                    if (isChecked == "1") {
                        selectedCount++;
                    }
                    else
                    { }
                }
                if (selectedCount == totalJobsCount) {
                    document.getElementById('selectAllPermissions').checked = true;
                }
                else {
                    document.getElementById('selectAllPermissions').checked = false;
                }
            }
            catch (Error) {
            }
        }

        function ApplyAlertLimitToAll() {
            var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
            var totalJobsCount = jobTypesCount.value;
            totalJobsCount = totalJobsCount - 1;

            var jobTypesCount = document.getElementById('AlertLimits');

            for (var i = 1; i <= totalJobsCount; i++) {
                var destControlObject = eval("document.forms[0].__JOBALLOWEDLIMIT_" + i);
                destControlObject.value = jobTypesCount.value;
            }
        }

        function ApplyJobLimitToAll() {
            var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
            var totalJobsCount = jobTypesCount.value;
            totalJobsCount = totalJobsCount - 1;

            var jobTypesCount = document.getElementById('jobLimit');

            for (var i = 1; i <= totalJobsCount; i++) {
                var destControlObject = eval("document.forms[0].__JOBLIMIT_" + i);
                destControlObject.value = jobTypesCount.value;
            }
        }

        function ApplyODToAll() {
            var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
            var totalJobsCount = jobTypesCount.value;
            totalJobsCount = totalJobsCount - 1;

            var jobTypesCount = document.getElementById('OverDraft');

            for (var i = 1; i <= totalJobsCount; i++) {
                var destControlObject = eval("document.forms[0].__ALLOWEDOVERDRAFT_" + i);
                destControlObject.value = jobTypesCount.value;
            }
        }

        function ToggleOD() {
            var overDraftStatus = document.getElementById('ctl00_PageContent_CheckBoxAllowOverDraft');
            if (overDraftStatus.checked) {
                document.getElementById('OverDraft').disabled = false;
                document.getElementById('applyOverDraft').style.display = "inline";
                document.getElementById('ctl00_PageContent_applyODImage').style.display = "inline";
            }
            else {
                document.getElementById('OverDraft').disabled = false;
                document.getElementById('applyOverDraft').style.display = "inline";
                document.getElementById('ctl00_PageContent_applyODImage').style.display = "inline";
            }
        }

        function ToggleDisplay() {
            var refillType = document.getElementById('ctl00_PageContent_HiddenFieldRefillType');

            if (refillType.value == "Manual") {
                document.getElementById('selectAllPermissions').disabled = false;
                document.getElementById('selectAllLimits').disabled = false;
                document.getElementById('AlertLimits').disabled = false;
                document.getElementById('OverDraft').disabled = false;
            }
            else {
                document.getElementById('selectAllPermissions').disabled = true;
                document.getElementById('selectAllLimits').disabled = true;
                document.getElementById('AlertLimits').disabled = true;
                document.getElementById('OverDraft').disabled = true;
            }
        }
    </script>
  
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image2" SkinID="HeadingLeft" runat="server" />
            </td>
            <td height="25" align="left" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadAutoRefill" runat="server" Text=""></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="20px"></td>
                                    <td>
                                     <asp:ImageButton ID="ImageButtonBack" SkinID="SettingsImageButtonBack" Visible="true"
                                            runat="server" ImageAlign="Middle" ToolTip="click here to goto Permission and Limits" OnClick="ImageButtonBack_Click" />
                                    </td>
                                    <td width="20px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td height="33" align="left" valign="middle" width="25%" class="HeaderPadding" style="display: none">
                            <table cellpadding="1" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td width="0%" align="center" valign="middle" style="display: none">
                                       
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                    </td>
                                    <td width="4%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" SkinID="SettingsImageButtonSave"
                                            CausesValidation="true" ImageAlign="Middle" ToolTip="Click here to save/update"
                                            OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                    </td>
                                    <td width="4%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" SkinID="SettingsImageButtonReset"
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="Click here to reset" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <%--<td width="1%" class="Menu_split">
                                    </td>--%>
                                    <td width="100%" align="left" valign="middle" style="display: none">
                                        <asp:Label ID="LabelAutoRefillTitle" runat="server" SkinID="TotalResource" Text=""
                                            CssClass="f10b"></asp:Label>
                                    </td>
                                    <%-- <td width="5%" class="Menu_split">
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="2">
                &nbsp;
            </td>
            <td height="2" class="CenterBG">
                &nbsp;
            </td>
        </tr>
        <tr class="Grid_tr">
            <td>
            </td>
            <td class="CenterBG" valign="top" height="500" align="center">
                <asp:HiddenField ID="HiddenFieldRefillType" Value="" runat="server" />
                <asp:HiddenField ID="HdnJobTypesCount" Value="0" runat="server" />
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="table_border_org">
                    <tr>
                        <td align="center" valign="top">
                            <asp:Table ID="TableRefillOptions" runat="server" Width="100%" CellPadding="0" CellSpacing="0"
                                border="0">
                                <asp:TableRow CssClass="Top_menu_bg">
                                    <asp:TableCell CssClass="f10b" Height="35" ColumnSpan="2" HorizontalAlign="Left">
                                        &nbsp;
                                        <asp:Label ID="LabelAutoRefillSettings" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="2">
                                    <asp:TableCell>
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow Style="display: inline">
                                    <asp:TableCell Width="47%" Height="35" HorizontalAlign="Right" Wrap="false">
                                        <asp:Label ID="LabelPermissionsAndLimitsOn" runat="server" Text="AutoRefill for"
                                            class="f10b"></asp:Label>
                                        &nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Wrap="false">
                                        <asp:DropDownList ID="DropDownListPerLimitsOn" AutoPostBack="true" runat="server"
                                            CssClass="Dropdown_CSS" OnSelectedIndexChanged="DropDownListPerLimitsOn_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="LabelRefillType" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Wrap="false">
                                        <asp:DropDownList ID="DropDownListRefillType" runat="server" AutoPostBack="true"
                                            CssClass="Dropdown_CSS" OnSelectedIndexChanged="DropDownListRefillType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                               
                                <asp:TableRow>
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="LabelAddToExistLimits" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListAddToExistLimits" runat="server" AutoPostBack="false"
                                            CssClass="Dropdown_CSS" OnSelectedIndexChanged="DropDownListAddToExistLimits_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="LabelRefillOn" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListRefillOn" runat="server" AutoPostBack="true" CssClass="Dropdown_CSS"
                                            OnSelectedIndexChanged="DropDownListRefillOn_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowRefillTime" Visible="false">
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35" Wrap="false">
                                        <asp:Label ID="LabelRefillTime" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Wrap="false">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td width="50px">
                                                    <asp:DropDownList ID="DropDownListHour" runat="server">
                                                    </asp:DropDownList>
                                                    :
                                                </td>
                                                <td width="50px">
                                                    <asp:DropDownList ID="DropDownListMinute" runat="server">
                                                    </asp:DropDownList>
                                                    :
                                                </td>
                                                <td width="50px">
                                                    <asp:DropDownList ID="DropDownListMeridian" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="80%"></td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="RefillWeek" Visible="false">
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35">
                                        <asp:Label ID="LabelRefillWeek" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListRefillWeek" runat="server" CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowRefillDate">
                                    <asp:TableCell Width="47%" HorizontalAlign="Right" Height="35">
                                        <asp:Label ID="LabelRefillDate" runat="server" Text="" class="f10b"></asp:Label>&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListRefillDate" runat="server">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Top_menu_bg">
                                    <asp:TableCell CssClass="f10b" Height="35" ColumnSpan="2" HorizontalAlign="Left">
                                        &nbsp;
                                        <asp:Label ID="LabelSetPermissionLimts" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="2">
                                    <asp:TableCell ColumnSpan="2">
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRowCostCenter" Style="display: none">
                                    <asp:TableCell Width="47%" Height="35" HorizontalAlign="Right">
                                        <asp:Label ID="LabelSelectGroup" runat="server" Text="" class="f10b"></asp:Label>
                                        &nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListCostCenters" AutoPostBack="true" runat="server"
                                            CssClass="Dropdown_CSS" OnSelectedIndexChanged="DropDownListCostCenters_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRowUsers" Style="display: none">
                                    <asp:TableCell Width="47%" Height="35" HorizontalAlign="Right">
                                        <asp:Label ID="LabelUsers" runat="server" Text="" class="f10b"></asp:Label>
                                        &nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="DropDownListUsers" AutoPostBack="true" runat="server" CssClass="Dropdown_CSS"
                                            OnSelectedIndexChanged="DropDownListUsers_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="TableRowOverDraft" Style="display: none">
                                    <asp:TableCell Width="47%" Height="35" HorizontalAlign="Right">
                                        <asp:Label ID="LabelAllowedOD" class="f10b" runat="server" Text=""></asp:Label>
                                        &nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" >
                                        <asp:CheckBox ID="CheckBoxAllowOverDraft" runat="server" Checked="true" onclick="javascript:ChangeOverDraft(this.checked)" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="2">
                                    <asp:TableCell ColumnSpan="2">
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowLimitsTable">
                                    <asp:TableCell ColumnSpan="0" Width="20%" HorizontalAlign="Left" VerticalAlign="Top"
                                        BorderWidth="0">
                                        <asp:Table ID="TableFilterData" Width="100%" CellPadding="0" CellSpacing="0" runat="server"
                                            border="0">
                                            <asp:TableRow>
                                                <asp:TableCell VerticalAlign="Top" >
                                                    <asp:Panel ID="PanelUsers" runat="server" Width="99%" CssClass="LoginFont" Style="padding-left: 10px">
                                                        <asp:Table ID="TableUsers" runat="server" CellPadding="0" CellSpacing="0" Width="95%"
                                                            border="0" Height="250px" CssClass="BorderForInnerTable BorderForInnerTableTop BorderForInnerTableBottom ">
                                                            <asp:TableRow>
                                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="5" Width="100%" CssClass="GridViewHeaderStyle FontForInnerTable" Visible="true">
                                                                    <asp:Table runat="server" Width="100%" CellPadding="0" CellSpacing="0" border="0">
                                                                        <asp:TableRow>
                                                                            <asp:TableCell HorizontalAlign="Right">
                                                                                <asp:Label ID="Label1" runat="server" Text="User Source"></asp:Label></asp:TableCell>
                                                                                <asp:TableCell Width="5px"></asp:TableCell>
                                                                            <asp:TableCell HorizontalAlign="Left">
                                                                                <asp:DropDownList ID="DropDownListUserSource" runat="server" OnSelectedIndexChanged="DropDownListUserSource_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                    </asp:Table>
                                                                </asp:TableCell>
                                                                <asp:TableCell HorizontalAlign="Right" ColumnSpan="5" Width="100%" CssClass="GridViewHeaderStyle FontForInnerTable">
                                                                    <asp:Table ID="TableUsersPaging" runat="server" CellPadding="3" CellSpacing="0">
                                                                        <asp:TableRow>
                                                                            <asp:TableCell>
                                                                                <asp:Label ID="LabelPageSize" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell>
                                                                                <asp:DropDownList ID="DropDownPageSize" CssClass="Normal_FontLabel" runat="server"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownPageSize_SelectedIndexChanged">
                                                                                    <asp:ListItem Selected="true">50</asp:ListItem>
                                                                                    <asp:ListItem>100</asp:ListItem>
                                                                                    <asp:ListItem>200</asp:ListItem>
                                                                                    <asp:ListItem>500</asp:ListItem>
                                                                                    <asp:ListItem>1000</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell>
                                                                                <asp:Label ID="LabelPage" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell>
                                                                                <asp:DropDownList ID="DropDownCurrentPage" runat="server" AutoPostBack="true" CssClass="Normal_FontLabel"
                                                                                    OnSelectedIndexChanged="DropDownCurrentPage_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell>
                                                                                <asp:Label ID="LabelTotalRecordsTitle" runat="server" SkinID="TotalResource" Text=""></asp:Label>:
                                                                            </asp:TableCell>
                                                                            <asp:TableCell>
                                                                                <asp:Label ID="LabelTotalRecordsValue" runat="server" SkinID="TotalResource" Text=""></asp:Label>
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                    </asp:Table>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow >
                                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="5" Height="20px" Width="40%" CssClass="paddingForInnerTableHeader">
                                                                    <table cellpadding="0" cellspacing="0" style="background-color: White;" border="0" width="75%">
                                                                        <tr style="background-color: White">
                                                                            <td >
                                                                                <asp:Label ID="Label2" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                                            </td>
                                                                            <td >
                                                                                <asp:TextBox BorderWidth="0" ID="TextBoxUserSearch" Text="*" runat="server" CssClass="SearchTextBox">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                            <td >
                                                                                <asp:ImageButton ID="ImageButtonUserGo" runat="server" SkinID="SearchList" OnClick="ImageButtonUserGo_Click" />
                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TextBoxUserSearch"
                                                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="12" CompletionInterval="1000"
                                                                                    ServiceMethod="GetUserList" CompletionListCssClass="autocomplete_completionListElement"
                                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                </cc1:AutoCompleteExtender>
                                                                            </td>
                                                                            <td >
                                                                               <asp:ImageButton ID="ImageButtonCancelUsers" runat="server" SkinID="CancelSearch"
                                                                                    OnClick="ImageButtonCancelUsers_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="1" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:TableCell>
                                                                <asp:TableCell HorizontalAlign="Right" ColumnSpan="6" Width="50%" CssClass="BorderForInnerTableRight">
                                                                 
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell Style="height: 2px" HorizontalAlign="Right" ColumnSpan="6" Width="100%" CssClass="BorderForInnerTableRight">
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell ColumnSpan="5" VerticalAlign="Top" Height="0px">
                                                                    <asp:Menu runat="server" ForeColor="Black" ID="MenuUser" SkinID="NavigationMenu"
                                                                        Orientation="Horizontal" DynamicHorizontalOffset="2" OnMenuItemClick="MenuUser_MenuItemClick"
                                                                        border="0" Width="100%" Visible="false">
                                                                        <Items>
                                                                            <asp:MenuItem Text="" Value="-"></asp:MenuItem>
                                                                            <asp:MenuItem Text="*" Value="ALL" Selected="true"></asp:MenuItem>
                                                                            <asp:MenuItem Text="A" Value="A"></asp:MenuItem>
                                                                            <asp:MenuItem Text="B" Value="B"></asp:MenuItem>
                                                                            <asp:MenuItem Text="C" Value="C"></asp:MenuItem>
                                                                            <asp:MenuItem Text="D" Value="D"></asp:MenuItem>
                                                                            <asp:MenuItem Text="E" Value="E"></asp:MenuItem>
                                                                            <asp:MenuItem Text="F" Value="F"></asp:MenuItem>
                                                                            <asp:MenuItem Text="G" Value="G"></asp:MenuItem>
                                                                            <asp:MenuItem Text="H" Value="H"></asp:MenuItem>
                                                                            <asp:MenuItem Text="I" Value="I"></asp:MenuItem>
                                                                            <asp:MenuItem Text="J" Value="J"></asp:MenuItem>
                                                                            <asp:MenuItem Text="K" Value="K"></asp:MenuItem>
                                                                            <asp:MenuItem Text="L" Value="L"></asp:MenuItem>
                                                                            <asp:MenuItem Text="M" Value="M"></asp:MenuItem>
                                                                            <asp:MenuItem Text="N" Value="N"></asp:MenuItem>
                                                                            <asp:MenuItem Text="O" Value="O"></asp:MenuItem>
                                                                            <asp:MenuItem Text="P" Value="P"></asp:MenuItem>
                                                                            <asp:MenuItem Text="Q" Value="Q"></asp:MenuItem>
                                                                            <asp:MenuItem Text="R" Value="R"></asp:MenuItem>
                                                                            <asp:MenuItem Text="S" Value="S"></asp:MenuItem>
                                                                            <asp:MenuItem Text="T" Value="T"></asp:MenuItem>
                                                                            <asp:MenuItem Text="U" Value="U"></asp:MenuItem>
                                                                            <asp:MenuItem Text="V" Value="V"></asp:MenuItem>
                                                                            <asp:MenuItem Text="W" Value="W"></asp:MenuItem>
                                                                            <asp:MenuItem Text="X" Value="X"></asp:MenuItem>
                                                                            <asp:MenuItem Text="Y" Value="Y"></asp:MenuItem>
                                                                            <asp:MenuItem Text="Z" Value="Z"></asp:MenuItem>
                                                                            <asp:MenuItem Text="[0-9]" Value="[0123456789]"></asp:MenuItem>
                                                                        </Items>
                                                                    </asp:Menu>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell ColumnSpan="10" HorizontalAlign="Center" VerticalAlign="Top">
                                                                    <asp:Panel ID="PanelUsersData" Height="250px" ScrollBars="Auto" runat="server" Width="100%">
                                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                            <tr class="Grid_tr">
                                                                                <td class="TableGridColor">
                                                                                    <asp:Table ID="TableUserData" runat="server" CellPadding="0" CellSpacing="1" border="0"
                                                                                        Width="100%" CssClass="Table_bg" SkinID="Grid">
                                                                                    </asp:Table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                        <asp:Table runat="server">
                                                            <asp:TableRow>
                                                                <asp:TableCell Height="10px"></asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                        <asp:Table ID="TablePLMessage" Visible="false" HorizontalAlign="Center" runat="server"
                                                            CellPadding="0" CellSpacing="0" border="0" Height="70px" CssClass="BorderForInnerTable BorderForInnerTableTop BorderForInnerTableBottom">
                                                            <asp:TableRow CssClass="GridRow">
                                                                <asp:TableCell ID="TableCell2" HorizontalAlign="Center" Width="20%">
                                                                    <asp:Image ID="Image3" runat="server" SkinID="PermessionsAndLimitsCritical" />
                                                                </asp:TableCell>
                                                                <asp:TableCell ID="TableCell3" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                                           <p class="LabelLoginFont">  <span style="color:Red">Permissions and Limits are not set for the selection &nbsp;</span></p>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:Panel>
                                                </asp:TableCell>
                                                <asp:TableCell VerticalAlign="Top" Width="10px">&nbsp;&nbsp;</asp:TableCell>
                                                <asp:TableCell VerticalAlign="Top">
                                                    <asp:Panel ID="PanelCostCenters" runat="server" Width="100%" CssClass="LoginFont">
                                                        <asp:Table ID="TableCostCenters" runat="server" CellPadding="0" CellSpacing="0" Width="95%"
                                                            Height="250px" border="0" CssClass="BorderForInnerTable BorderForInnerTableTop BorderForInnerTableBottom">
                                                            <asp:TableRow HorizontalAlign="left">
                                                                <asp:TableCell HorizontalAlign="left" ColumnSpan="4" Width="100%" CssClass="GridViewHeaderStyle FontForInnerTable">
                                                                    <asp:Label ID="LabelCC" runat="server" Text=""></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="4" VerticalAlign="Middle" CssClass="paddingForInnerTableHeader">
                                                                    <table cellpadding="0" cellspacing="0" width="250px" style="background-color: White;"
                                                                        border="0">
                                                                        <tr style="background-color: White; height: 10px;">
                                                                            <td>
                                                                                <asp:TextBox BorderWidth="0" ID="TextBoxCostCenterSearch" Text="*" runat="server"
                                                                                    CssClass="SearchTextBox" Width="100%">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButtonCCGo" runat="server" SkinID="SearchList" OnClick="ImageButtonCCGo_Click" />
                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxCostCenterSearch"
                                                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="12" CompletionInterval="1000"
                                                                                    ServiceMethod="GetCostCenterList" CompletionListCssClass="autocomplete_completionListElement"
                                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                </cc1:AutoCompleteExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButtonCancelSearch" runat="server" SkinID="CancelSearch"
                                                                                    OnClick="ImageButtonCancelSearch_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell ColumnSpan="4" Style="height: 2px">
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell ColumnSpan="4" VerticalAlign="Top" Height="0px">
                                                                    <asp:Menu runat="server" ForeColor="Black" ID="MenuCostCenter" SkinID="NavigationMenu"
                                                                        Orientation="Horizontal" DynamicHorizontalOffset="2" OnMenuItemClick="MenuCostCenter_MenuItemClick"
                                                                        StaticSubMenuIndent="20px" border="0" Width="100%" Visible="false">
                                                                        <Items>
                                                                            <asp:MenuItem Text="" Value="-"></asp:MenuItem>
                                                                            <asp:MenuItem Text="*" Value="ALL" Selected="true"></asp:MenuItem>
                                                                            <asp:MenuItem Text="A" Value="A"></asp:MenuItem>
                                                                            <asp:MenuItem Text="B" Value="B"></asp:MenuItem>
                                                                            <asp:MenuItem Text="C" Value="C"></asp:MenuItem>
                                                                            <asp:MenuItem Text="D" Value="D"></asp:MenuItem>
                                                                            <asp:MenuItem Text="E" Value="E"></asp:MenuItem>
                                                                            <asp:MenuItem Text="F" Value="F"></asp:MenuItem>
                                                                            <asp:MenuItem Text="G" Value="G"></asp:MenuItem>
                                                                            <asp:MenuItem Text="H" Value="H"></asp:MenuItem>
                                                                            <asp:MenuItem Text="I" Value="I"></asp:MenuItem>
                                                                            <asp:MenuItem Text="J" Value="J"></asp:MenuItem>
                                                                            <asp:MenuItem Text="K" Value="K"></asp:MenuItem>
                                                                            <asp:MenuItem Text="L" Value="L"></asp:MenuItem>
                                                                            <asp:MenuItem Text="M" Value="M"></asp:MenuItem>
                                                                            <asp:MenuItem Text="N" Value="N"></asp:MenuItem>
                                                                            <asp:MenuItem Text="O" Value="O"></asp:MenuItem>
                                                                            <asp:MenuItem Text="P" Value="P"></asp:MenuItem>
                                                                            <asp:MenuItem Text="Q" Value="Q"></asp:MenuItem>
                                                                            <asp:MenuItem Text="R" Value="R"></asp:MenuItem>
                                                                            <asp:MenuItem Text="S" Value="S"></asp:MenuItem>
                                                                            <asp:MenuItem Text="T" Value="T"></asp:MenuItem>
                                                                            <asp:MenuItem Text="U" Value="U"></asp:MenuItem>
                                                                            <asp:MenuItem Text="V" Value="V"></asp:MenuItem>
                                                                            <asp:MenuItem Text="W" Value="W"></asp:MenuItem>
                                                                            <asp:MenuItem Text="X" Value="X"></asp:MenuItem>
                                                                            <asp:MenuItem Text="Y" Value="Y"></asp:MenuItem>
                                                                            <asp:MenuItem Text="Z" Value="Z"></asp:MenuItem>
                                                                            <asp:MenuItem Text="0-9" Value="[0123456789]"></asp:MenuItem>
                                                                        </Items>
                                                                    </asp:Menu>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" VerticalAlign="Top">
                                                                    <asp:Panel ID="PanelCostCenerData" Height="250px" ScrollBars="Auto" runat="server"
                                                                        Width="98%">
                                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                            <tr class="Grid_tr">
                                                                                <td class="TableGridColor">
                                                                                    <asp:Table ID="TableCostCenerData" runat="server" CellPadding="0" CellSpacing="1"
                                                                                        border="0" Width="100%" CssClass="Table_bg" SkinID="Grid">
                                                                                    </asp:Table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                        <asp:Table ID="Table1" runat="server">
                                                            <asp:TableRow>
                                                                <asp:TableCell Height="10px"></asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                        <asp:Table ID="TablePLCC" Visible="false" HorizontalAlign="Center" runat="server"
                                                            CellPadding="0" CellSpacing="0" border="0" Height="70px" CssClass="BorderForInnerTable BorderForInnerTableTop BorderForInnerTableBottom">
                                                            <asp:TableRow CssClass="GridRow">
                                                                <asp:TableCell ID="TableCell1" HorizontalAlign="Center" Width="20%">
                                                                    <asp:Image ID="Image5" runat="server" SkinID="PermessionsAndLimitsCritical" />
                                                                </asp:TableCell>
                                                                <asp:TableCell ID="TableCell4" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                                           <p class="LabelLoginFont">  <span style="color:Red">Permissions and Limits are not set for the selection &nbsp;</span></p>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:Panel>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                    <asp:TableCell Width="78%" ColumnSpan="0" HorizontalAlign="Right">
                                        <table width="98%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                            <tr class="Grid_tr">
                                                <td>
                                                    <asp:Table ID="TableLimits" Width="100%" CellSpacing="0" BorderWidth="0" SkinID="Grid"
                                                        BorderColor="#efefef" CellPadding="4" runat="server">
                                                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                            <asp:TableHeaderCell CssClass="RowHeader" Width="30"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="HeaderCellJobType" HorizontalAlign="Left" Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell HorizontalAlign="Left" ID="TableHeaderCellPermissions" Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellLimits" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellAllowedLimit" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                            <asp:TableHeaderCell ID="TableHeaderCellOverDraft" HorizontalAlign="Left" CssClass="H_title"
                                                                Wrap="false"></asp:TableHeaderCell>
                                                        </asp:TableHeaderRow>
                                                        <asp:TableRow ID="TableRowSelect" CssClass="GridRow">
                                                            <asp:TableCell HorizontalAlign="Left"></asp:TableCell>
                                                            <asp:TableCell CssClass="GridLeftAlign" HorizontalAlign="Left"> ALL </asp:TableCell>
                                                            <asp:TableCell HorizontalAlign="Left"><input  type="checkbox"  id="selectAllPermissions" onclick="ChkandUnchkPermissions()"/> </asp:TableCell>
                                                            <asp:TableCell CssClass="GridLeftAlign" HorizontalAlign="Left">
                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <input id="jobLimit" type="text" value='0' maxlength="8" style="width: 80px" onkeypress='funNumber();' />
                                                                        </td>
                                                                        <td>
                                                                            <input type="checkbox" id="selectAllLimits" onclick="ChkandUnchkLimits()" />
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;<a id="jobLimitApplyToAll" href="#" onclick="ApplyJobLimitToAll()" style="text-decoration: none">Apply
                                                                                to ALL</a>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="jobLimitImage" SkinID="AutoRefilldownarrow" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:TableCell>
                                                            <asp:TableCell CssClass="GridLeftAlign" HorizontalAlign="Left">
                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <input id="AlertLimits" type="text" value='0' maxlength="3" style="width: 30px" onkeypress='funNumber();' />
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;<a href="#" onclick="ApplyAlertLimitToAll()" style="text-decoration: none">Apply
                                                                                to ALL</a>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="Image1" SkinID="AutoRefilldownarrow" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:TableCell>
                                                            <asp:TableCell CssClass="GridLeftAlign" HorizontalAlign="Left">
                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <input id="OverDraft" type="text" value='0' maxlength="3" style="width: 30px" onkeypress='funNumber();' />
                                                                        </td>
                                                                        <td>
                                                                            &nbsp; <a id="applyOverDraft" href="#" onclick="ApplyODToAll()" style="text-decoration: none">
                                                                                Apply to ALL</a>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="applyODImage" SkinID="AutoRefilldownarrow" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="2">
                                    <asp:TableCell ColumnSpan="2">
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="10px">
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td width="50%" align="right">
                                                    <asp:Button ID="ButtonUpdate" runat="server" CssClass="Login_Button" OnClick="ButtonUpdate_Click"
                                                        Text="" Visible="True" />
                                                </td>
                                                <td width="10%" align="center">
                                                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CssClass="Login_Button"
                                                        OnClick="ButtonDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this selected AutoRefill setting?');" CausesValidation="false" />
                                                </td>
                                                <td width="40%" align="left">
                                                    <asp:Button runat="server" ID="ButtonReset" Text="" CssClass="Login_Button" OnClientClick="this.form.reset();return false;" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="ButtonCancel" runat="server" CssClass="Cancel_button" Text="" Visible="false"
                                                        OnClick="ButtonCancel_Click" CausesValidation="false" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="ButtonAutoRefillNow" runat="server" CssClass="Cancel_button" Text="Refill Now"
                                                        Visible="false" OnClick="ButtonAutoRefillNow_Click" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="2">
                                    <asp:TableCell ColumnSpan="2">
                                        &nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="2">
                &nbsp;
            </td>
            <td height="2" class="CenterBG">
                &nbsp;
            </td>
        </tr>
    </table>
    <input type="hidden" value="&infin;" name="infinityValue" />
    <script type="text/javascript" language="javascript">
        var refillType = document.getElementById('ctl00_PageContent_HiddenFieldRefillType');
        if (refillType.value == "Automatic") {
            ToggleSelectAllPermissions();
            ToggleSelectAllLimits();
            ToggleOD();
        }
    </script>
</asp:Content>
