﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobSettings.aspx.cs" Inherits="AccountingPlusEA.PSPModel.JobSettings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
    <meta name="Browser" content="NetFront" />
    <script language="javascript" type="text/javascript">
     var themeRootURL = "";
        <asp:Literal ID="LiteralThemeName" runat="server"></asp:Literal>

        function DisplaySettings(selectedTab) {
            document.sound(0);
            var objMainTab = document.getElementById("MainTab");
            var objFinishingTab = document.getElementById("FinishingTab");
            var objFilingTab = document.getElementById("FilingTab");

            var objMainTabSettings = document.getElementById("MainTabSettings");
            var objFinishingTabSettings = document.getElementById("FinishingTabSettings");
            var objFilingTabSettings = document.getElementById("FilingTabSettings");
            var Tab_split1 = document.getElementById("Selection_01");
            var Tab_split2 = document.getElementById("Selection_02");

            objMainTab.className = "SettingTab";
            objFinishingTab.className = "SettingTab";
            objFilingTab.className = "SettingTab";

            objMainTabSettings.style.display = 'none';
            objFinishingTabSettings.style.display = 'none';
            objFilingTabSettings.style.display = 'none';

            if (selectedTab == "MainTab") {
                objMainTab.className = "SelectedSettingTab";
                objMainTabSettings.style.display = 'block';
                Tab_split1.className = "Setting_tab_Right_bg";
                Tab_split2.className = "Setting_tab_Right_bg";
            }
            else if (selectedTab == "FinishingTab") {
                objFinishingTab.className = "SelectedSettingTab";
                Tab_split1.className = "Setting_tab_Right_Split";
                Tab_split2.className = "Setting_tab_Right_bg";
                objFinishingTabSettings.style.display = 'block';
            }
            else if (selectedTab == "FilingTab") {
                objFilingTab.className = "SelectedSettingTab";
                Tab_split2.className = "Setting_tab_Right_Split";
                Tab_split1.className = "Setting_tab_Right_bg";

                objFilingTabSettings.style.display = 'block';
            }
            punchDisable();
             ResetTrackCounter();
        }

        function Select_Main() {
            document.sound(0);
            document.getElementById("Main").className = "Menu_bar_selectedBG";
            document.getElementById("Finishing").className = "Menu_bar_unselect";
            document.getElementById("Filing").className = "Menu_bar_unselect";

            document.getElementById("Split_1").className = "Menu_split_Center";
            document.getElementById("Split_2").className = "Menu_split_Center";

            document.getElementById("LabelMain").className = "Tab_divPostion";
            document.getElementById("LabelFinishing").className = "Tab_divPostion_Select";
            document.getElementById("LabelFiling").className = "Tab_divPostion_Select";

            var objMainTabSettings = document.getElementById("MainTabSettings");
            var objFinishingTabSettings = document.getElementById("FinishingTabSettings");
            var objFilingTabSettings = document.getElementById("FilingTabSettings");
            objMainTabSettings.style.display = 'none';
            objFinishingTabSettings.style.display = 'none';
            objFilingTabSettings.style.display = 'none';

            objMainTabSettings.style.display = 'block';
            punchDisable();
             ResetTrackCounter();
        }


        function Select_Finishing() {
            document.sound(0);
            var tx = document.getElementById("TextBoxCopies");
            if (parseInt(tx.value) > 1) {
                makeEnable();
            }
            else {
                makeDisable();
            }

            document.getElementById("Main").className = "Menu_bar_unselect";
            document.getElementById("Finishing").className = "Menu_bar_selectedBG";
            document.getElementById("Filing").className = "Menu_bar_unselect";

            document.getElementById("Split_1").className = "Menu_split_selectTime";
            document.getElementById("Split_2").className = "Menu_split_Center";

            document.getElementById("LabelMain").className = "Tab_divPostion_Select";
            document.getElementById("LabelFinishing").className = "Tab_divPostion";
            document.getElementById("LabelFiling").className = "Tab_divPostion_Select";

            var objMainTabSettings = document.getElementById("MainTabSettings");
            var objFinishingTabSettings = document.getElementById("FinishingTabSettings");
            var objFilingTabSettings = document.getElementById("FilingTabSettings");

            objMainTabSettings.style.display = 'none';
            objFinishingTabSettings.style.display = 'none';
            objFilingTabSettings.style.display = 'none';

            objFinishingTabSettings.style.display = 'block';
             ResetTrackCounter();
        }

        function Select_Filing() {
            document.sound(0);
            document.getElementById("Main").className = "Menu_bar_unselect";
            document.getElementById("Finishing").className = "Menu_bar_unselect";
            document.getElementById("Filing").className = "Menu_bar_selectedBG";

            document.getElementById("Split_1").className = "Menu_split_Center";
            document.getElementById("Split_2").className = "Menu_split_selectTime";

            document.getElementById("LabelMain").className = "Tab_divPostion_Select";
            document.getElementById("LabelFinishing").className = "Tab_divPostion_Select";
            document.getElementById("LabelFiling").className = "Tab_divPostion";

            var objMainTabSettings = document.getElementById("MainTabSettings");
            var objFinishingTabSettings = document.getElementById("FinishingTabSettings");
            var objFilingTabSettings = document.getElementById("FilingTabSettings");

            objMainTabSettings.style.display = 'none';
            objFinishingTabSettings.style.display = 'none';
            objFilingTabSettings.style.display = 'none';

            objFilingTabSettings.style.display = 'block';
            punchDisable();
            ResetTrackCounter();
        }

        function AllowNumeric() {
            var charCode = event.keyCode //( evt.which ) ? evt.which : event.keyCode;                 
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else
                return false;
        }

        function DisplaySettingImage() {
            checkStaple();
            punchDisable();
            var duplexDirectionControl = document.getElementById("DropDownListDuplexDir");
            var duplexModeControl = document.getElementById("DropDownListDuplexMode");
            var selectedDirection = duplexDirectionControl[duplexDirectionControl.selectedIndex].value;
            var selectedMode = duplexModeControl[duplexModeControl.selectedIndex].value;

            if (selectedMode == "SIMPLEX") {
                duplexDirectionControl.disabled = true;
            }
            else {
                duplexDirectionControl.disabled = false;
            }
            ResetTrackCounter();
        }

        function CopiesCountDown() {
            var tx = document.getElementById("TextBoxCopies");

            if (!parseInt(tx.value) <= 0) {
                tx.value = parseInt(tx.value) - 1;
            }
            if (tx.value == "" || tx.value == 0) {
                tx.value = 1;
            }
            if (parseInt(tx.value) == 1) {
                makeDisable();
            }
            else {
                makeEnable();
            }
            DisplaySettingImage();
        }

        function makeDisable() {
            var collate = document.getElementById("DropDownListCollate");
            collate.disabled = true;
            var hiddenFieldCollate = document.getElementById("HiddenFieldCollateEnabled");
            hiddenFieldCollate.value = "False";
        }

        function makeEnable() {
            var collate = document.getElementById("DropDownListCollate");
            collate.disabled = false;
            var hiddenFieldCollate = document.getElementById("HiddenFieldCollateEnabled");
            hiddenFieldCollate.value = "True";
        }

        function CopiesCountUp() {
            var tx = document.getElementById("TextBoxCopies");
            if (parseInt(tx.value) < 999) {
                tx.value = parseInt(tx.value) + 1;
            }

            if (tx.value == "") {
                tx.value = 1;
            }

            if (parseInt(tx.value) == 1) {
                makeDisable();
            }
            else {
                makeEnable();
            }
            DisplaySettingImage();
            ResetTrackCounter();
        }

        function RetentionChange() {

            var selectedRetention = document.getElementById("DropDownListRetention").value;
            var documentFilingSettings = document.getElementById("HiddenFielddocumentFilingSettings").value;

            if (selectedRetention == 'NONE') {

                document.getElementById("DropDownListFolder").disabled = true;
            }
            else if (selectedRetention == 'HOLD') {

                document.getElementById("DropDownListFolder").disabled = false;
                document.getElementById("DropDownListFolder").value = 'MAIN';
            }
            else if (selectedRetention == 'HOLD_AFTER_PRINT') {

                document.getElementById("DropDownListFolder").disabled = false;
                document.getElementById("DropDownListFolder").value = "MAIN";

            }
            else if (selectedRetention == 'PROOF') {

                document.getElementById("DropDownListFolder").disabled = false;
                document.getElementById("DropDownListFolder").value = "MAIN";
            }
            if (documentFilingSettings == 'false') {
                document.getElementById("DropDownListFolder").disabled = true;
            }

        }

        function FolderChange() {
            var selectedFolder = document.getElementById("DropDownListFolder").value;
            if (selectedFolder == 'MAIN') {
                document.getElementById("DropDownListRetention").value = 'HOLD_AFTER_PRINT';
            }
            else if (selectedFolder == 'QUICK') {
                document.getElementById("DropDownListRetention").value = 'HOLD_AFTER_PRINT';
            }
        }

        function checkStaple() {
            var isStaple = document.getElementById("DropDownListStaple").value;
            if (isStaple != 'NONE') {

                document.getElementById("CheckBoxOffset").disabled = true;
                document.getElementById("CheckBoxOffset").checked = false;
            }
            else {
                document.getElementById("CheckBoxOffset").disabled = false;
            }
            punchDisable();
        }

        function punchDisable() {
            var duplexDirectionControl = document.getElementById("DropDownListDuplexDir");
            var selectedDirection = duplexDirectionControl[duplexDirectionControl.selectedIndex].value;
            var staple = document.getElementById("DropDownListStaple").value;
            var duplexMode = document.getElementById("DropDownListDuplexMode").value;
            var isdevicePunchSupport = document.getElementById("HiddenFieldDevicePunchSupport").value;
            if ((staple == '1STAPLE' && selectedDirection == 'TABLET' && duplexMode != 'SIMPLEX') || (staple == '2STAPLE' && selectedDirection == 'TABLET' && duplexMode != 'SIMPLEX')) {
                document.getElementById("CheckBoxPunch").disabled = true;
                document.getElementById("CheckBoxPunch").checked = false;
            }

            else {
                document.getElementById("CheckBoxPunch").disabled = false;
            }

            if (isdevicePunchSupport == 'false') {
                document.getElementById("CheckBoxPunch").disabled = true;
                document.getElementById("CheckBoxPunch").checked = false;
            }
        }

        function TrackUserInteraction()
          {
              var timerId = self.setInterval("StartTimer()", 1000);
              document.getElementById("TimerID").value = timerId;
          }
        
          function StartTimer()
          {
              var elapsedTime = parseInt(document.getElementById("ElapsedTime").value);
              elapsedTime = elapsedTime + 1;
              document.getElementById("ElapsedTime").value = elapsedTime;
              var timeOut = document.getElementById("HiddenFieldIntervalTime").value;
              if(elapsedTime >= timeOut)
              {
                 ClearTimer();
                 location.href = "../Mfp/LogOn.aspx";
              }
          }
        
          function ResetTrackCounter()
          {
             document.getElementById("ElapsedTime").value = "0";
          }
        
          function ClearTimer()
          {
             var timeId = document.getElementById("TimerID").value;
             if (timeId != "") {
                self.clearInterval(timeId);
             }
          }
        
    </script>
</head>
<style type="text/css">
     <asp:Literal ID="PageBackground" runat="server"></asp:Literal>
</style>
<body leftmargin="0" topmargin="0" scroll="NO" class="InsideBG" onload=" PageShowingEve(),TrackUserInteraction()">
    <div style="display: inline; width: 300px; left: 30px; z-index: 1; position: absolute"
        id="PageLoadingID" class="InsidePage_BGcolor">
        <table cellpadding="0" cellspacing="0" border="0" width="250" height="150">
            <tr>
                <td align="left" style="padding-left: 5px;" valign="middle">
                    <asp:Image ID="ImagePageLoading" runat="server" />
                </td>
                <td align="left" style="padding-left: 5px;" valign="middle" class="Login_TextFonts">
                    <asp:Label ID="LabelPageLoading" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none" id="PageShowingID">
        <form id="form1" runat="server">
        <input type="hidden" size="4" id="ElapsedTime" value="0" />
        <input type="hidden" size="4" value="" id="TimerID" />
        <asp:HiddenField ID="HiddenFieldIntervalTime" runat="server" Value="0" />
        <asp:HiddenField ID="HiddenDriverType" Value="PCL" runat="server" />
        <asp:HiddenField ID="isDuplexEnabled" runat="server" />
        <asp:HiddenField ID="HiddenFieldCollateEnabled" runat="server" />
        <asp:HiddenField ID="HiddenFieldDevicePunchSupport" Value="true" runat="server" />
        <asp:HiddenField ID="HiddenFieldDuplexDisableDuplexMode" Value="PCL" runat="server" />
        <asp:HiddenField ID="HiddenFieldDuplexDisableDuplexDirMode" runat="server" />
        <asp:HiddenField ID="HiddenFielddocumentFilingSettings" Value="true" runat="server" />
        <asp:Table ID="TblJobSettings" runat="server" BorderWidth="0" CellPadding="0" CellSpacing="0"
            Width="440" Height="225">
            <asp:TableRow>
                <asp:TableCell align="left" valign="top" Height="35">
                    <!-- Title Bar and Logout and Delete button-->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" height="35">
                        <tr>
                            <td width="80%" class="Inside_TOPTitleFontBold">
                                &nbsp;
                                <asp:Label ID="LabelPageTitle" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="11%" align="center">
                                <asp:ImageButton ID="ImageButtonCancel" runat="server" OnClick="ImageButtonCancel_Click" />
                            </td>
                            <td width="9%" align="center">
                                <asp:ImageButton ID="ImageButtonPrint" runat="server" OnClick="ImageButtonPrint_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell align="left" valign="top" height="2" class="HR_Line">
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell align="left" valign="top">
                    <!-- Setting Top level Menu-->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="top" height="38" class="Menubar_bg">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" height="38">
                                    <tr>
                                        <td width="3%" align="left" valign="top" class="Menu_split_right">
                                        </td>
                                        <td width="20%" align="center" valign="middle" class="Menu_bar_selectedBG" id="Main">
                                            <a href="#" onclick="Select_Main()">
                                                <div style="width: 100%; height: 25px;">
                                                    <asp:Label ID="LabelMain" runat="server" Text="" CssClass="Tab_divPostion"></asp:Label>
                                                </div>
                                            </a>
                                        </td>
                                        <td width="1%" align="left" valign="top" class="Menu_split_Center" id="Split_1">
                                        </td>
                                        <td width="19%" align="center" valign="middle" class="Menu_bar_unselect" id="Finishing">
                                            <a href="#" onclick="Select_Finishing()">
                                                <div style="width: 100%; height: 25px;">
                                                    <asp:Label ID="LabelFinishing" runat="server" Text="" CssClass="Tab_divPostion_Select"></asp:Label>
                                                </div>
                                            </a>
                                        </td>
                                        <td width="1%" align="left" valign="top" class="Menu_split_Center" id="Split_2">
                                        </td>
                                        <td width="20%" align="center" valign="middle" class="Menu_bar_unselect" id="Filing">
                                            <a href="#" onclick="Select_Filing()">
                                                <div style="width: 100%; height: 25px;">
                                                    <asp:Label ID="LabelFiling" runat="server" Text="" CssClass="Tab_divPostion_Select"></asp:Label>
                                                </div>
                                            </a>
                                        </td>
                                        <td width="1%" align="left" valign="top" class="Menu_split_Left">
                                        </td>
                                        <td width="35%" align="left" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell valign="top">
                    <asp:Table ID="TableControls" runat="server">
                        <asp:TableRow>
                            <asp:TableCell Width="50">
                            </asp:TableCell>
                            <asp:TableCell>
                                <table width="100%" height="150" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" width="70%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="100%">
                                                        <div id="MainTabSettings" style="display: block">
                                                            <asp:Table ID="TableMainJobSettings" CellPadding="4" CellSpacing="4" runat="server">
                                                                <asp:TableRow runat="server" ID="TableRowCopies">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelCopies" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:TextBox ID="TextBoxCopies" MaxLength="3" runat="server"></asp:TextBox>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <a href="#" onclick="javascript:CopiesCountDown()">
                                                                            <asp:Image ID="ImageCopiesUp" runat="server" />
                                                                        </a>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <a href="#" onclick="javascript:CopiesCountUp()">
                                                                            <asp:Image ID="ImageCopiesDown" runat="server" />
                                                                        </a>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowColorMode">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelColorMode" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListColorMode" runat="server" Width="225px">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowDuplexMode" Visible="true">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelDuplexMode" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListDuplexMode" runat="server">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowDuplexDir" Visible="true">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelDuplexDir" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListDuplexDir" runat="server" Width="225px">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </div>
                                                        <div id="FinishingTabSettings" style="display: none">
                                                            <asp:Table ID="TableFinishingSettings" CellPadding="4" CellSpacing="4" runat="server">
                                                                <asp:TableRow runat="server" ID="TableRowOrientation" Visible="false">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelOrientation" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListOrientation" runat="server" Width="225px">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowPaperSize" Visible="false">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelPaperSize" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListPaperSize" runat="server" Width="225px">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowFitToPage" Visible="false">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelFitToPage" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:CheckBox ID="CheckBoxFitToPage" runat="server" />
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowCollate">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelCollate" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListCollate" runat="server" Width="225px">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowStaple">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelStaple" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <%-- <asp:CheckBox ID="CheckBoxStaple" runat="server" />--%>
                                                                        <asp:DropDownList ID="DropDownListStaple" runat="server" Width="225px">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowPunch">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelPunch" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:CheckBox ID="CheckBoxPunch" runat="server" EnableViewState="false" />
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowOffset">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelOffset" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:CheckBox ID="CheckBoxOffset" runat="server" />
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowOutPutTray">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelOutPutTray" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListOutPutTray" runat="server" Width="225px">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </div>
                                                        <div id="FilingTabSettings" style="display: none">
                                                            <asp:Table ID="TableFilingSettings" CellPadding="4" CellSpacing="4" runat="server">
                                                                <asp:TableRow runat="server" ID="TableRowRetention">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelRetention" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListRetention" runat="server" Width="225px" onchange="RetentionChange();">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowPassword" Visible="false">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelPassword" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow runat="server" ID="TableRowFolder">
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <asp:Label ID="LabelFolder" runat="server" CssClass="PrintSetting" Text=""></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Left">
                                                                        <asp:DropDownList ID="DropDownListFolder" runat="server" onchange="FolderChange();"
                                                                            Width="225px">
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <div runat="server" id="divCommunicator">
                        <asp:Panel ID="PanelCommunicator" runat="server" Visible="false" CssClass="CommunicatorPannel"
                            Width="100%">
                            <asp:Table ID="TableCommunicator" EnableViewState="false" CellPadding="0" CellSpacing="0"
                                BorderWidth="0" HorizontalAlign="Center" runat="server" class="Error_msgTable"
                                Width="100%">
                                <asp:TableRow CssClass="Error_msgcenter" HorizontalAlign="center">
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Normal_FontLabel"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Error_msgcenter">
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="middle" Width="49%">
                                        <div style="width: 40%;">
                                            <asp:LinkButton ID="LinkButtonDivCancel" OnClick="LinkButtonDivCancel_Click" runat="server">
                                                <table cellpadding="0" cellspacing="0" height="38" width="100%">
                                                    <tr>
                                                        <td width="10%" align="left" valign="top" class="Button_Left">
                                                            &nbsp;
                                                        </td>
                                                        <td width="60%" align="center" valign="middle" class="Button_center">
                                                            <div class="Login_TextFonts">
                                                                <asp:Label ID="LabelOK" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td width="30%" align="left" valign="middle" class="Button_Right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:LinkButton></div>
                                    </asp:TableCell>
                                    <asp:TableCell Width="2%">
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="49%" Visible="false">
                                        <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                            <table cellpadding="0" cellspacing="0" border="0" height="38" width="70%">
                                                <tr>
                                                    <td width="10%" align="left" valign="top" class="Button_Left">
                                                        &nbsp;
                                                    </td>
                                                    <td width="50%" align="left" valign="middle" class="Button_center">
                                                        <div class="Login_TextFonts">
                                                            <asp:Label ID="LabelContinue" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td width="10%" align="left" valign="middle" class="Button_Right">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:LinkButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                    </div>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Visible="false">
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Label ID="LblSettingNotSupported" runat="server" Text="For this, job settings are not supported! Please try reprinting the document">
                    </asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        function PageShowingEve() {
            setTimeout(PageShowing(), 50000);
        }
        function PageShowing() {
            document.getElementById("PageLoadingID").style.display = "none";
            document.getElementById("PageShowingID").style.display = "inline";
        }
    
    </script>
</body>
</html>
