<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="EmailPrintSettings.aspx.cs" Inherits="AccountingPlusWeb.Administration.EmailPrintSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
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
                    destControlObject.disabled = false; //true to enable allow over draft
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
        function AllowNumeric() {

            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else

                return false;
        }
    </script>
    <asp:Panel ID="PnlRegistrationDetails" runat="server">
        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
            <tr>
                <td align="right" valign="top" style="width: 1px">
                    <asp:HiddenField ID="HdnJobTypesCount" Value="0" runat="server" />
                    <asp:Image ID="Image4" SkinID="HeadingLeft" runat="server" />
                </td>
                <td width="100%" valign="top" class="CenterBG">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td height="35" class="Top_menu_bg" align="left">
                                <table cellpadding="0" cellspacing="0" width="98%" border="0">
                                    <tr>
                                        <td class="HeadingMiddleBg" width="2%">
                                            <div style="padding: 4px 10px 0px 10px;">
                                                <asp:Label ID="LbLPageSubTitle" runat="server" Text="Email Settings"></asp:Label>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image8" SkinID="HeadingRight" runat="server" />
                                        </td>
                                        <td width="10%" style="display: none;">
                                        </td>
                                        <td width="25%" align="left" valign="middle">
                                        </td>
                                        <td align="center">
                                        </td>
                                        <td align="right">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="25">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                    <tr>
                                        <td width="15%">
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="middle" width="70%">
                                            <div>
                                                <asp:Menu ID="menuTabs" CssClass="menuTabs" StaticMenuItemStyle-CssClass="tab" StaticSelectedStyle-CssClass="selectedTab"
                                                    Orientation="Horizontal" OnMenuItemClick="menuTabs_MenuItemClick" runat="server">
                                                    <Items>
                                                        <asp:MenuItem Text="Email Settings" Value="0" Selected="true" />
                                                        <asp:MenuItem Text="Anonymous User Settings" Value="1" />
                                                        <asp:MenuItem Text="Permision and Limits" Value="2" />
                                                    </Items>
                                                </asp:Menu>
                                                <div class="tabBody">
                                                    <asp:MultiView ID="multiTabs" ActiveViewIndex="0" runat="server">
                                                        <asp:View ID="view1" runat="server">
                                                            <table width="99%" class="table_border_org" align="center" cellpadding="0" cellspacing="0"
                                                                border="0">
                                                                <tr class="Top_menu_bg">
                                                                    <td class="f10b" height="35" colspan="4" align="left">
                                                                        &nbsp;
                                                                        <asp:Label ID="LabelUserInformation" SkinID="LabelLogon" runat="server" Text="Email Settings"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" align="center">
                                                                        <table class="Menu_bg_insidetable3" cellpadding="3" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td align="right" width="25%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelSendcredentialsto" runat="server" Text="Send credentials to"
                                                                                        class="f10b"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="DropDownListSender" CssClass="Dropdown_CSS" runat="server">
                                                                                        <asp:ListItem Text="Administrator" Value="admin"></asp:ListItem>
                                                                                        <asp:ListItem Text="Sender" Value="sender"></asp:ListItem>
                                                                                        <asp:ListItem Text="None" Value="none"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="left" width="20%">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="25%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelEmailTo" runat="server" Text="To" class="f10b"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TextboxEmailTo" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="25%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelEmailCC" runat="server" Text="CC" class="f10b"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TextboxCC" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="25%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelBCC" runat="server" Text="BCC" class="f10b"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TextboxBCC" CssClass="FormTextBox_bg" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="25%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelSubject" runat="server" Text="Subject" class="f10b"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="TextboxSubject" CssClass="FormTextBox_bg" MaxLength="100" runat="server"></asp:TextBox>&nbsp;
                                                                                    <asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TextboxSubject"
                                                                                        runat="server" ErrorMessage="Please enter subject" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator3" ID="ValidatorCalloutExtender3"
                                                                                        runat="server">
                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td align="left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="25%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelBody" runat="server" Text="Body" class="f10b"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <HTMLEditor:Editor ID="HTMLEditorContextBody" Visible="true" Width="100%" AutoFocus="false"
                                                                                        runat="server" Height="200px" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="HTMLEditorContextBody"
                                                                                        runat="server" ErrorMessage="Please enter body" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator1" ID="ValidatorCalloutExtender1"
                                                                                        runat="server">
                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td align="left" valign="top">
                                                                                    <asp:Image ID="Image2" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" width="25%" height="25" class="f10b">
                                                                                    <asp:Label ID="LabelSignature" runat="server" Text="Signature" class="f10b"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <HTMLEditor:Editor ID="HTMLEditorContextSignature" Visible="true" Width="100%" AutoFocus="false"
                                                                                        runat="server" Height="200px" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="HTMLEditorContextSignature"
                                                                                        runat="server" ErrorMessage="Please enter signature" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                    <cc1:ValidatorCalloutExtender TargetControlID="RequiredFieldValidator2" ID="ValidatorCalloutExtender2"
                                                                                        runat="server">
                                                                                    </cc1:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td align="left" valign="top">
                                                                                    &nbsp;<asp:Image ID="Image3" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center" height="50">
                                                                        <asp:Button ID="BtnUpdate" runat="server" CssClass="Login_Button" Text="Update" OnClick="BtnUpdate_Click" />
                                                                        <asp:Button runat="server" ID="ButtonReset" Text="Reset" CssClass="Login_Button"
                                                                            OnClick="ButtonReset_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:View>
                                                        <asp:View ID="view2" runat="server">
                                                            <table width="99%" class="table_border_org" align="center" cellpadding="0" cellspacing="0"
                                                                border="0">
                                                                <tr class="Top_menu_bg">
                                                                    <td class="f10b" height="35" colspan="4" align="left">
                                                                        &nbsp;
                                                                        <asp:Label ID="Label1" SkinID="LabelLogon" runat="server" Text="Anonymous User Account"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" align="center">
                                                                        <table class="Menu_bg_insidetable3" cellpadding="3" cellspacing="0" border="0">
                                                                            <tr>
                                                                                <td align="right" width="40%" height="25" class="f10b">
                                                                                    <asp:Label ID="Label2" runat="server" Text="User Account Expiry" class="f10b"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="DropDownListUserAccountExpiry" CssClass="Dropdown_CSS" runat="server">
                                                                                        <asp:ListItem Text="No Expiry" Value="expiry"></asp:ListItem>
                                                                                        <asp:ListItem Text="As Per Settings" Value="setting"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                              <td class="f10b" align="right" >
                                                                               Account Expires after
                                                                            </td>
                                                                            <td>
                                                                            <table  cellpadding="3" cellspacing="0" border="0" widyh="100%">
                                                                            <tr>
                                                                          
                                                                             <td align="right"  height="25" >
                                                                                    Days
                                                                                </td>
                                                                                <td  >
                                                                                    <asp:TextBox ID="TextboxDays" width="20px" onkeypress="javascript:return AllowNumeric()" MaxLength="3" runat="server"></asp:TextBox> :
                                                                                </td>
                                                                                  <td align="right" height="25" >
                                                                                    Hours
                                                                                </td>
                                                                                <td align="left"   >
                                                                                    <asp:TextBox ID="TextboxHours"  width="20px"  onkeypress="javascript:return AllowNumeric()" MaxLength="2" runat="server"></asp:TextBox> :
                                                                                </td>
                                                                                <td align="right" height="25" >
                                                                                    Minutes
                                                                                </td>
                                                                                <td align="left"   >
                                                                                    <asp:TextBox ID="TextboxMinutes"   width="20px" onkeypress="javascript:return AllowNumeric()" MaxLength="2" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            
                                                                            </tr>
                                                                            </table>
                                                                            </td>
                                                                               
                                                                            </tr>
                                                                         
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center" height="50">
                                                                        <asp:Button ID="ButtonUpdateUserAccount" runat="server" CssClass="Login_Button" Text="Update"
                                                                            OnClick="ButtonUpdateUserAccount_Click" />
                                                                        <asp:Button runat="server" ID="ButtonResetUserAccount" Text="Reset" CssClass="Login_Button"
                                                                            OnClick="ButtonResetUserAccount_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:View>
                                                        <asp:View ID="view3" runat="server">
                                                            <table width="98%" class="TableGridColor" align="center" cellpadding="0" cellspacing="0"
                                                                border="0">
                                                                <tr class="Top_menu_bg">
                                                                    <td class="f10b" height="35" colspan="4" align="left">
                                                                        &nbsp;
                                                                        <asp:Label ID="Label3" SkinID="LabelLogon" runat="server" Text="Permissions and Limits"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="Grid_tr">
                                                                    <td>
                                                                        <asp:Table ID="TableLimits" Width="100%" CellSpacing="0" BorderWidth="0" SkinID="Grid"
                                                                            BorderColor="#efefef" CellPadding="4" runat="server">
                                                                            <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                                <asp:TableHeaderCell CssClass="RowHeader" Width="30"></asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell ID="HeaderCellJobType" HorizontalAlign="Left" Text="Job Type"
                                                                                    Wrap="false"></asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell HorizontalAlign="Left" ID="TableHeaderCellPermissions" Text="Permissions"
                                                                                    Wrap="false"></asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell ID="TableHeaderCellLimits" HorizontalAlign="Left" CssClass="H_title"
                                                                                    Text="Page Limit" Wrap="false"></asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell ID="TableHeaderCellAllowedLimit" HorizontalAlign="Left" CssClass="H_title"
                                                                                    Text="Alert Limit" Wrap="false"></asp:TableHeaderCell>
                                                                                <asp:TableHeaderCell ID="TableHeaderCellOverDraft" HorizontalAlign="Left" CssClass="H_title"
                                                                                    Text="Allowed Overdraft" Wrap="false"></asp:TableHeaderCell>
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
                                                                                                <asp:Image ID="Image5" SkinID="AutoRefilldownarrow" runat="server" />
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
                                                                <tr >
                                                                    <td colspan="2" >
                                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White">
                                                                            <tr>
                                                                                <td width="50%" align="right" style="padding-top:5px">
                                                                                    <asp:Button ID="ButtonUpdatePermessions" runat="server" CssClass="Login_Button" OnClick="ButtonUpdatePermessions_Click"
                                                                                        Text="Update" Visible="True" />&nbsp;
                                                                                </td>
                                                                              
                                                                                <td width="50%" align="left" style="padding-top:5px">
                                                                                    <asp:Button runat="server" ID="ButtonResetPermessions" Text="Reset" CssClass="Login_Button"
                                                                                        OnClientClick="this.form.reset();return false;" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:View>
                                                    </asp:MultiView>
                                                </div>
                                            </div>
                                        </td>
                                        <td width="15%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                    <input type="hidden" value="&infin;" name="infinityValue" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
