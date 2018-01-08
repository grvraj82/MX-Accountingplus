<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="PermissionsAndLimits.aspx.cs" Inherits="AccountingPlusWeb.Administration.PermissionsAndLimits" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
  
        fnShowCellUsers();
        Meuselected("UserID");

        function SetValue(isChecked, controlName) {
            //alert(controlName);
            var controlObject = eval("document.forms[0]." + controlName);
            if (isChecked) {
                controlObject.value = 1;
            }
            else {
                controlObject.value = 0;
            }
            ToggleSelectAllPermissions();
        }

        function DeleteUsers() {            
                var confirmflag = confirm(C_DO_YOU_WANT_TO_RESET_PAGESUSED);
                if (!confirmflag) {
                    return false;
                }
                else {
                    return true;
                }          
        }

        function ChangeOverDraft(isChecked) {
            var jobTypesCount = document.getElementById('ctl00_PageContent_HdnJobTypesCount');
            for (var i = 1; i < jobTypesCount.value; i++) {
                var destControlObject = eval("document.forms[0].__ALLOWEDOVERDRAFT_" + i);
                if (isChecked) {
                    destControlObject.disabled = false;
                }
                else {
                    destControlObject.disabled = true;
                }
            }
            ToggleOD();
        }

        function funNumber() {
            if (event.keyCode >= 48 && event.keyCode <= 57) {
                return true;
            }
            if (event.keyCode == 45) // - HYPHEN
            {
                return true;
            }

            else {
                event.returnValue = false;
                return false;
            }
        }

        function funNumberOnlyNumeric() {
            if (event.keyCode >= 48 && event.keyCode <= 57) {
                return true;
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

                var isChecked = destControlObject.value;
                var jobType = eval("document.forms[0].__JOBTYPEID_" + i).value;
                //alert(jobType);
                if (jobType != "Scan BW") {
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
                document.getElementById('OverDraft').disabled = true;
                document.getElementById('applyOverDraft').style.display = "none";
                document.getElementById('ctl00_PageContent_applyODImage').style.display = "none";
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

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__COSTCENTERID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__COSTCENTERID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__COSTCENTERID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }

        function ChkandUnchk() {
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__COSTCENTERID') {

                        document.getElementById('aspnetForm').elements[i].checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    if (document.getElementById('aspnetForm').elements[i].name == '__COSTCENTERID') {

                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                }
            }
            if (document.getElementById("ctl00_PageContent_HiddenFieldDeviceCount").value == 0) {
                document.getElementById("chkALL").checked = false;
            }
        }
        function togall(refcheckbox) {

            var thisForm = document.forms[0];
            var users = thisForm.__COSTCENTERID.length;
            var selectedCount = 0;
            if (thisForm.__COSTCENTERID[refcheckbox - 1].checked) {

                thisForm.__COSTCENTERID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__COSTCENTERID[refcheckbox - 1].checked = true; 
            }
            ValidateSelectedCount();
        }
        function ValidateSelectedCount() {
            var selectedCount = 0;
            var totalRecords = parseInt(document.getElementById("ctl00_PageContent_HiddenFieldDeviceCount").value);

            if (totalRecords == parseInt(GetSeletedCount() + 1)) {
                var checkBoxAll = document.getElementById("chkALL").checked = true;
            }
            else {
                var checkBoxAll = document.getElementById("chkALL").checked = false;
            }
        }
        function EKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13)) {
                return false;
            }
        }
        document.onkeypress = EKey;
    </script>
    <table border="0" cellpadding="0" cellspacing="0" align="center" width="100%">
        <tr align="right">
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image3" SkinID="HeadingLeft" runat="server" />
            </td>
            <td height="33" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg">
                        <td class="HeadingMiddleBg" width="8%">
                            <div style="padding: 4px 10px 0px 10px;">
                                <asp:Label ID="LabelHeadingPermissionsandLimits" runat="server" Text="Label"></asp:Label>
                            </div>
                        </td>
                        <td width="1px">
                            <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td id="TableCellButtonSave" width="3%" align="left" valign="middle" runat="server"
                            style="display: none">
                            <asp:ImageButton ID="ImageButtonSave" runat="server" CausesValidation="true" ImageAlign="Middle"
                                SkinID="PermessionsAndLimitsSave" ToolTip="Click here to save/update" OnClick="ImageButtonSave_Click" />
                        </td>
                        <td id="menuSplit1" width="1%" class="Menu_split" runat="server" style="display: none">
                        </td>
                        <td id="TableCellButtonReset" width="3%" align="left" valign="middle" runat="server"
                            style="display: none">
                            <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="PermessionsAndLimitsReset"
                                ImageAlign="Middle" ToolTip="Click here to reset" OnClick="ImageButtonReset_Click" />
                        </td>
                        <td id="menuSplit2" width="1%" class="Menu_split" runat="server" style="display: none">
                        </td>
                        <td width="12%" class="f12b" align="left" style="white-space: nowrap">
                            <asp:Label ID="LabelLimitsOn" SkinID="TotalResource" runat="server" Text=""></asp:Label>
                            :&nbsp;&nbsp;
                        </td>
                        <td width="9%" align="left" style="white-space: nowrap">
                            <asp:DropDownList ID="DropDownListLimitsOn" CssClass="FormDropDown_Small" runat="server"
                                AutoPostBack="True" OnTextChanged="DropDownListLimitsOn_SelectedIndexChanged">
                                <asp:ListItem Text="" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="Menu_split" width="1%" style="display: none">
                        </td>
                        <td align="left" width="1%" style="white-space: nowrap; display: none">
                            <asp:ImageButton ID="ImageButtonAutoRefill" ToolTip="" runat="server" SkinID="PermessionsAndLimitsSettings"
                                OnClick="ImageButtonAutoRefill_Click" />
                        </td>
                        <td width="62%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 2px">
            <td>
            </td>
            <td class="CenterBG" align="center" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top" align="center" class="CenterBG" style="height: 550px">
                <asp:HiddenField ID="HiddenFieldRefillType" Value="" runat="server" />
                <asp:HiddenField ID="HdnJobTypesCount" Value="0" runat="server" />
                <asp:HiddenField ID="HiddenPageState" Value="False" runat="server" />
                <asp:UpdatePanel ID="updatePanelList" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table align="center" cellpadding="0" width="98%" cellspacing="0" border="0">
                            <tr>
                                <td width="60%" valign="top">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:Table ID="TableFilterData" Width="100%" CellPadding="0" CellSpacing="0" runat="server"
                                                    border="0">
                                                    <asp:TableRow>
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
                                                            </asp:Panel>
                                                        </asp:TableCell>
                                                        <asp:TableCell VerticalAlign="Top" Width="10px">&nbsp;&nbsp;</asp:TableCell>
                                                        <asp:TableCell VerticalAlign="Top">
                                                            <asp:Panel ID="PanelUsers" runat="server" Width="100%" CssClass="LoginFont">
                                                                <asp:Table ID="TableUsers" runat="server" CellPadding="0" CellSpacing="0" Width="100%"
                                                                    border="0" Height="250px" CssClass="BorderForInnerTable BorderForInnerTableTop BorderForInnerTableBottom">
                                                                    <asp:TableRow HorizontalAlign="left">
                                                                        <asp:TableCell HorizontalAlign="left" ColumnSpan="5" Width="100%" CssClass="GridViewHeaderStyle FontForInnerTable">
                                                                            <asp:Label ID="LabelUsers" runat="server" Text=""></asp:Label>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow>
                                                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="4" Height="20px" CssClass="paddingForInnerTableHeader">
                                                                            <table cellpadding="0" cellspacing="0" width="250px" style="background-color: White;"
                                                                                border="0">
                                                                                <tr style="background-color: White">
                                                                                    <td>
                                                                                        <asp:Label ID="Label1" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox BorderWidth="0" ID="TextBoxUserSearch" Text="*" runat="server" CssClass="SearchTextBox">
                                                                                        </asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ImageButtonUserGo" runat="server" SkinID="SearchList" OnClick="ImageButtonUserGo_Click" />
                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TextBoxUserSearch"
                                                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="12" CompletionInterval="1000"
                                                                                            ServiceMethod="GetUserList" CompletionListCssClass="autocomplete_completionListElement"
                                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td>
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
                                                                        <asp:TableCell HorizontalAlign="Right">
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
                                                                    <asp:TableRow>
                                                                        <asp:TableCell Style="height: 2px">
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
                                                                        <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" VerticalAlign="Top">
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
                                                            </asp:Panel>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="2px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Table ID="TableSelection" runat="server" CellPadding="0" CellSpacing="0" CssClass="BorderForInnerTable BorderForInnerTableTop BorderForInnerTableBottom"
                                                    Width="100%">
                                                    <asp:TableRow>
                                                        <asp:TableCell HorizontalAlign="Left">
                                                            <asp:CheckBox ID="CheckBoxUpdateCostCenter" runat="server" CssClass="FontForInnerTable"
                                                                Text="" /> <%--OnCheckedChanged="CheckBoxUpdateCostCenter_CheckChanged" AutoPostBack="true"--%> 
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell HorizontalAlign="Left">
                                                            <asp:RadioButton ID="RadioButtonApplyToAll" runat="server" CssClass="FontForInnerTable"
                                                                GroupName="UpdateList" Text="" /> <%-- OnCheckedChanged="RadioButtonApplyToAll_CheckChanged" AutoPostBack="true"--%> 
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell HorizontalAlign="Left">
                                                            <asp:RadioButton ID="RadioButtonApplyToSelection" runat="server" CssClass="FontForInnerTable"
                                                                GroupName="UpdateList" Checked="true" Text="" /> <%--OnCheckedChanged="RadioButtonApplyToSelection_CheckChanged" AutoPostBack="true" />--%>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:CheckBox ID="CheckBoxAllowOverDraft" runat="server" onclick="javascript:ChangeOverDraft(this.checked)"
                                                    CssClass="FontForInnerTablePL" />
                                            </td>
                                        </tr>
                                        <tr class="Grid_tr">
                                            <td class="TableGridColor">
                                                <asp:Table ID="tblLimits" Width="100%" CellSpacing="1" BorderWidth="0" CellPadding="0"
                                                    runat="server" CssClass="Table_bg" SkinID="Grid">
                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                        <asp:TableHeaderCell CssClass="RowHeader" Width="30">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellJobType" HorizontalAlign="Left" CssClass="H_title"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell HorizontalAlign="Left" CssClass="H_title" ID="TableHeaderCellJobPermission"
                                                            Wrap="false"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellCurrentLimit" Text="" HorizontalAlign="Left"
                                                            CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellPageLimit" Text="" HorizontalAlign="Left"
                                                            CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellAllowedLimit" HorizontalAlign="Left" CssClass="H_title"
                                                            Wrap="false"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellOverDraft" HorizontalAlign="Left" CssClass="H_title"
                                                            Wrap="false"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellTotalAllowedLimit" Text="" HorizontalAlign="Left"
                                                            CssClass="H_title" Wrap="false"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCellJobUsed" HorizontalAlign="Left" CssClass="H_title"
                                                            Wrap="false"></asp:TableHeaderCell>
                                                        <asp:TableHeaderCell ID="TableHeaderCell2" Text="" HorizontalAlign="Left" CssClass="H_title"
                                                            Wrap="false"></asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>
                                                    <asp:TableRow ID="TableRowSelect" CssClass="GridRow">
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell CssClass="GridLeftAlign" Wrap="false">ALL</asp:TableCell>
                                                        <asp:TableCell HorizontalAlign="Left" Wrap="false"> <input  type="checkbox"  id="selectAllPermissions" onclick="ChkandUnchkPermissions()"/> </asp:TableCell>
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell CssClass="GridLeftAlign" Wrap="false">
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <input id="jobLimit" type="text" value='0' maxlength="8" style="width: 80px" onkeypress="funNumber()" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="checkbox" id="selectAllLimits" onclick="ChkandUnchkLimits()" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;<a id="jobLimitApplyToAll" href="#" onclick="ApplyJobLimitToAll()" style="text-decoration: none">Apply
                                                                            to ALL</a>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Image ID="jobLimitImage" runat="server" SkinID="PermessionsAndLimitsDownArrow">
                                                                        </asp:Image>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="GridLeftAlign" Wrap="false">
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <input id="AlertLimits" type="text" value='0' maxlength="3" style="width: 30px" onkeypress="funNumberOnlyNumeric()" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;<a href="#" onclick="ApplyAlertLimitToAll()" style="text-decoration: none">Apply
                                                                            to ALL</a>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Image ID="Image1" runat="server" SkinID="PermessionsAndLimitsDownArrow"></asp:Image>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell CssClass="GridLeftAlign" Width="15%" Wrap="false">
                                                            <table cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <input id="OverDraft" type="text" value='0' maxlength="3" style="width: 30px" onkeypress="funNumberOnlyNumeric()" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp; <a id="applyOverDraft" href="#" onclick="ApplyODToAll()" style="text-decoration: none">
                                                                            Apply to ALL</a>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Image ID="applyODImage" runat="server" SkinID="PermessionsAndLimitsDownArrow">
                                                                        </asp:Image>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell></asp:TableCell>
                                                        <asp:TableCell HorizontalAlign="Center" Wrap="false"> </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="2px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="100%">
                                                <asp:Button ID="BtnUpdate" runat="server" Text="" CssClass="Login_Button" OnClick="BtnUpdate_Click" />&nbsp;
                                                &nbsp;<asp:Button ID="ButtonRemove" runat="server" Text="Remove" CssClass="Login_Button"
                                                    OnClick="ButtonRemove_Click" />
                                                <asp:Button runat="server" ID="ButtonReset" Text="" CssClass="Login_Button" OnClick="ButtonReset_Click" />&nbsp;
                                                &nbsp;
                                                <asp:Button ID="ButtonPagesUsedReset" runat="server" Text="" CssClass="Login_Button"
                                                    OnClick="ButtonPagesUsedReset_Click" />
                                                <asp:Button ID="ButtonAddforAutoRefill" runat="server" Text="Set for AutoRefill" CssClass="Login_Button" OnClick="ButtonAddforAutoRefill_Click" />
                                                <%--OnClientClick="this.form.reset();return false;"--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="2px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle">
                                                <asp:Table ID="TableAutoRefillMessage" CellSpacing="1" CellPadding="3" Width="90%"
                                                    runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                        <asp:TableHeaderCell ID="TableHeaderCellDivName" CssClass="Grid_topbg1" ColumnSpan="2"
                                                            HorizontalAlign="Left">Auto Refill</asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>
                                                    <asp:TableRow CssClass="GridRow">
                                                        <asp:TableCell ID="TableCellWarningImage" HorizontalAlign="Center" Width="20%">
                                                            <asp:Image ID="ImageWarning" runat="server" SkinID="PermessionsAndLimitsCritical" />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell1" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                                           <p  class="LabelLoginFont"> Auto Refill Status : <span style="color:Red">ON</span> </p>
                                           <p class="LabelLoginFont"> To Update Permissions and Limits please change Auto Refill mode to Manual.</p>
                                           <p class="LabelLoginFont"> Click <a href="AutoRefill.aspx">Here</a> for Auto Refill Settings.</p>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                <asp:Table ID="TableWarningMessage" CellSpacing="1" CellPadding="3" Width="90%" BorderWidth="0"
                                                    Visible="false" runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                        <asp:TableHeaderCell ID="TableHeaderCell1" CssClass="Grid_topbg1" ColumnSpan="2"
                                                            HorizontalAlign="Left">Permissions and Limits</asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>
                                                    <asp:TableRow CssClass="GridRow">
                                                        <asp:TableCell ID="TableCell2" HorizontalAlign="Center" Width="20%">
                                                            <asp:Image ID="Image2" runat="server" SkinID="PermessionsAndLimitsCritical" />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell3" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                                           <p class="LabelLoginFont">  <span style="color:Red">Permissions and Limits are not set for the selection</span></p>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
     <asp:HiddenField ID="HiddenFieldDeviceCount" Value="0" runat="server" />
    <input type="hidden" value="&infin;" name="infinityValue" />   
    <script type="text/javascript" language="javascript">
        var refillType = document.getElementById('ctl00_PageContent_HiddenFieldRefillType');
        if (refillType.value == "Manual") {
            ToggleSelectAllPermissions();
            ToggleSelectAllLimits();
            ToggleOD();
        }
    </script>
</asp:Content>
