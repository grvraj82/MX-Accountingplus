<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyScan.aspx.cs" Inherits="AccountingPlusEA.Mfp.CopyScan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="Browser" content="NetFront" />
    <title></title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
    <asp:Literal ID="PageBackground" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">
        //        function TrackUserInteraction() {
        //            var timerId = self.setInterval("StartTimer()", 1000);
        //            document.getElementById("TimerID").value = timerId;
        //        }

        //        function StartTimer() {
        //            alert("Signing out....ST");
        //            var elapsedTime = parseInt(document.getElementById("ElapsedTime").value);
        //            elapsedTime = elapsedTime + 1;
        //            document.getElementById("ElapsedTime").value = elapsedTime;
        //            var timeOut = document.getElementById("HiddenFieldIntervalTime").value;
        //            if (elapsedTime >= timeOut) {
        //                ClearTimer();
        //                location.href = "../Mfp/AccountingLogOn.aspx";
        //            }
        //        }

        //        function ClearTimer() {
        //            var timeId = document.getElementById("TimerID").value;
        //            if (timeId != "") {
        //                self.clearInterval(timeId);
        //            }
        //        }

        function ShowMessage(value) {
            if (value == "0") {
                document.getElementById("DivMessage").style.display = "inline";
            }
            else {
                document.getElementById("DivMessage").style.display = "none";
            }
        }

        function Close() {
            document.getElementById("DivMessage").style.display = "none";
        }



        function ShowColorMode() {
            document.getElementById("HdnColorMode").value = "FULL-COLOR";
            document.getElementById("ColorMode_Id").className = "LeftMenu_Selected_Bg";
            document.getElementById("BWMode_Id").className = "LeftMenu_Unselected_Bg";
            document.getElementById("MyAcc_Id").className = "LeftMenu_Unselected_Bg";

            document.getElementById("Arrow_Tab1").className = "Arrow_Position_Tab2";
            document.getElementById("MyAccount_Img").className = "LeftMenu_MyAccountImg_Unselected";

            document.getElementById("JobMode_Cont_Id").style.display = "block";
            document.getElementById("MyAcc_Cont_Id").style.display = "none";
            document.getElementById("hiddenfieldJobMode").value = "color";

            document.getElementById("LabelFileCount").innerHTML = (document.getElementById("hiddenfieldPrintFileCount").value);

        }

        function ShowBWMode() {
            document.getElementById("HdnColorMode").value = "MONOCHROME";
            document.getElementById("ColorMode_Id").className = "LeftMenu_Unselected_Bg";
            document.getElementById("BWMode_Id").className = "LeftMenu_Selected_Bg";
            document.getElementById("MyAcc_Id").className = "LeftMenu_Unselected_Bg";

            document.getElementById("Arrow_Tab1").className = "Arrow_Position_Tab1";
            document.getElementById("MyAccount_Img").className = "LeftMenu_MyAccountImg_Unselected";

            document.getElementById("JobMode_Cont_Id").style.display = "block";
            document.getElementById("MyAcc_Cont_Id").style.display = "none";
            document.getElementById("hiddenfieldJobMode").value = "BW";

            document.getElementById("LabelFileCount").innerHTML = (document.getElementById("hiddenfieldPrintFileCountBW").value);
        }

        function ShowMyAccount() {
            document.getElementById("ColorMode_Id").className = "LeftMenu_Unselected_Bg";
            document.getElementById("BWMode_Id").className = "LeftMenu_Unselected_Bg";
            document.getElementById("MyAcc_Id").className = "LeftMenu_Selected_Bg";

            document.getElementById("Arrow_Tab1").className = "Arrow_Position_Tab3";
            document.getElementById("MyAccount_Img").className = "LeftMenu_MyAccountImg_Selected";

            document.getElementById("JobMode_Cont_Id").style.display = "none";
            document.getElementById("MyAcc_Cont_Id").style.display = "block";
        }

        var timerID = "";
        var sessionOutTime = '<%=sessionTimeOut%>'; // Seconds

        function TrackSession() {
            document.sound(0);
            if (timerID != "") {
                self.clearInterval(timerID)
            }
            timerID = self.setTimeout("SignOff()", sessionOutTime * 1000);
            self.setTimeout("CloseValidationMessages()", 5000);
        }

        function SignOff() {
            location.href = "../Mfp/AccountingLogOn.aspx?AutoLogOff=Y";
        }

        function CloseValidationMessages() {
            document.getElementById("DivMessage").style.display = "none";
        }

    </script>
</head>
<body class="Main_Bg" onload="javascript:TrackSession();" onclick="javascript:TrackSession()">
    <div align="center" style="display: none;" id="DivMessage" runat="server" class="ErrorMsg_OuterDiv_JobMode">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center" class="ErrorMsg_Bg">
            <tr>
                <td align="left" valign="middle">
                    <asp:Label ID="LabelErrorMessage" CssClass="FontErrorMessage" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" valign="middle" class="Login_TextFonts" width="5%">
                    <div class="Close_Icon" onclick="Close()">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: inline" id="PageShowingID" runat="server">
        <form id="CopyScanOptions" runat="server" visible="true">
        <input type="hidden" size="4" id="ElapsedTime" value="0" />
        <input type="hidden" size="4" value="" id="TimerID" />
        <asp:HiddenField ID="HdnColorMode" Value="MONOCHROME" runat="server" />
        <asp:HiddenField ID="HiddenFieldIntervalTime" runat="server" Value="0" />
        <asp:HiddenField ID="hiddenfieldJobMode" runat="server" Value="BW" />
        <asp:HiddenField ID="hiddenfieldPrintFileCount" runat="server" Value="0" />
        <asp:HiddenField ID="hiddenfieldPrintFileCountBW" runat="server" Value="0" />
        <table align="center" cellpadding="0" cellspacing="0" border="0" class="TableBodyBg MainBgColor">
            <tr>
                <!--Header Part Starts-->
                <td class="Header_Height">
                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="Header_LeftTd_Width">
                            </td>
                            <td align="left" class="LoginInfo_Bg">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td class="UserImg_Bg">
                                            <div class="UserImg">
                                            </div>
                                        </td>
                                        <td class="Welcome_Font">
                                            Welcome &nbsp;<asp:Label ID="AccUser" CssClass="Admin_Font" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="Header_Space_Td">
                            </td>
                            <td align="left" class="LogoutInfo_Bg">
                                <asp:LinkButton ID="LinkButtonSignOut" OnClick="LinkButtonSignOut_Click" runat="server">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="UserImg_Bg">
                                                <div class="LogoutImg">
                                                </div>
                                            </td>
                                            <td class="Welcome_Font">
                                                Logout
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                            </td>
                            <td class="Header_RightTd_Width">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!--Header Part Ends-->
            <tr>
                <!--Content Part Starts-->
                <td align="left" class="InnerBgColor" valign="top">
                    <asp:Panel ID="panelContent" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="WidthLeft_Content" valign="top">
                                    <!--Left Menu Starts-->
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="center" class="Bal_Height">
                                                <div class="Title_Font">
                                                    Balance:
                                                    <asp:Label ID="AccBal" runat="server" Text="0"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="PaddingAll10">
                                                <table cellpadding="0" cellspacing="0" border="0" class="LeftMenu_Outer_Padd">
                                                    <tr>
                                                        <!--LeftMenu BWTab Starts-->
                                                        <td align="left" onclick="ShowBWMode()">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="LeftMenu_Selected_Bg"
                                                                id="BWMode_Id">
                                                                <tr>
                                                                    <td align="left" class="LeftMenu_BWImg">
                                                                    </td>
                                                                    <td align="left" class="LeftMenu_Paddleft_InnerContent CurrentBal_Font">
                                                                        <div style="white-space: nowrap;">
                                                                            Black & White Mode</div>
                                                                        <div class="LeftMenu_Descrip_Font PaddingTop_MenuDesc">
                                                                            Select this option to print, scan, copy or fax in black & white mode
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <!--LeftMenu BWTab Ends-->
                                                    <tr>
                                                        <!--LeftMenu ColorTab Starts-->
                                                        <td align="left" class="LeftMenu_PaddingTop" onclick="ShowColorMode()">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="LeftMenu_Unselected_Bg"
                                                                id="ColorMode_Id">
                                                                <tr>
                                                                    <td align="left" class="LeftMenu_ColorImg">
                                                                    </td>
                                                                    <td align="left" class="LeftMenu_Paddleft_InnerContent CurrentBal_Font">
                                                                        <div>
                                                                            Color Mode</div>
                                                                        <div class="LeftMenu_Descrip_Font PaddingTop_MenuDesc">
                                                                            Select this option to print, scan, copy or fax in color mode
                                                                        </div>
                                                                        <div class="Arrow_Position_Tab1" id="Arrow_Tab1">
                                                                            <div class="Arrow_SelectedImg">
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <!--LeftMenu ColorTab Ends-->
                                                    <tr>
                                                        <!--LeftMenu MyAccountTab Starts-->
                                                        <td align="left" class="LeftMenu_PaddingTop" onclick="ShowMyAccount()">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="LeftMenu_Unselected_Bg"
                                                                id="MyAcc_Id">
                                                                <tr>
                                                                    <td align="left" class="LeftMenu_MyAccountImg_Unselected" id="MyAccount_Img">
                                                                    </td>
                                                                    <td align="left" class="LeftMenu_Paddleft_InnerContent CurrentBal_Font">
                                                                        <div>
                                                                            My Account</div>
                                                                        <div class="LeftMenu_Descrip_Font PaddingTop_MenuDesc">
                                                                            Select this option to view or recharge your account
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <!--Left Menu Ends-->
                                <td class="WidthRight_Content PaddingAll10" valign="top">
                                    <!--Right Content Starts-->
                                    <div id="JobMode_Cont_Id">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="RightContent_Bg">
                                            <tr>
                                                <td valign="top" class="Title_Font PaddingBot_JobMode_Font">
                                                    Job Mode
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" align="center">
                                                    <asp:Table ID="TblUserOptions" CellPadding="12" CellSpacing="12" BorderWidth="0"
                                                        runat="server">
                                                        <asp:TableRow>
                                                            <asp:TableCell></asp:TableCell>
                                                            <asp:TableCell></asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell></asp:TableCell>
                                                            <asp:TableCell></asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="MyAcc_Cont_Id" style="display: none;">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="RightContent_Bg">
                                            <tr>
                                                <td valign="top" class="Title_Font PaddingBot_JobMode_Font">
                                                    My Account
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" align="center">
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="center" class="RightSideTab_Bg" id="tablecellMinistatement" runat="server">
                                                                <!--My Account Tab1 Starts-->
                                                                <asp:LinkButton ID="LinkButtonMiniStatement" OnClick="LinkButtonMiniStatement_Click"
                                                                    runat="server">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="center" valign="middle" class="RightTab_Inner_Iconheight">
                                                                        <div class="MiniStmntImg">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="top" class="RightTab_Inner_Descheight">
                                                                        <div class="RightTab_TitleFont">
                                                                            Mini Statement</div>
                                                                        <div class="RightTab_Descrip_Font">
                                                                            Select this option to view your account statement
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <!--My Account Tab1 Ends-->
                                                            <td class="RightTabs_Space_width">
                                                            </td>
                                                            <td align="center" class="RightSideTab_Bg" id="tablecellTopUP" runat="server">
                                                                <!--My Account Tab2 Starts-->
                                                                <asp:LinkButton ID="LinkButtonTopUp" OnClick="LinkButtonTopup_Click" runat="server">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="center" valign="middle" class="RightTab_Inner_Iconheight">
                                                                        <div class="RechargeImg">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="top" class="RightTab_Inner_Descheight">
                                                                        <div class="RightTab_TitleFont">
                                                                            Top up</div>
                                                                        <div class="RightTab_Descrip_Font">
                                                                            Select this option to <b>Top up</b> your account
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <!--My Account Tab2 Ends-->
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <!--Right Content Ends-->
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="panelMessage" runat="server">
                        <asp:Table ID="Table1" CellPadding="0" CellSpacing="0" BorderWidth="0" runat="server" HorizontalAlign="Center" ForeColor="White">

                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center" CssClass="Height_LowBal">
                                    <div class="Lowbal_ErrorMsg_Bg">Low Balance</div>           
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top" CssClass="Lowbal_OuterBg">
                                    <asp:Table ID="Table_1" CellPadding="0" CellSpacing="0" runat="server" HorizontalAlign="Center">

                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="left">
                                                <div class="LowBal_Icon"></div>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="left" CssClass="NormalFont_New">
                                                Your current balance is :
                                                <asp:Label ID="LabelCurrentBalace" CssClass="BalAmt_Font_New" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow> 
                                      
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="left"  VerticalAlign="Top" CssClass="NormalFont_New PaddTop_LowBal">
                                                <asp:Label ID="LabelminimumBalance" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow> 
                                      
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="left"  VerticalAlign="Top" CssClass="NormalFont_New PaddTop_LowBal">
                                                Please top up your account.
                                            </asp:TableCell>
                                        </asp:TableRow>

                                    </asp:Table> 
                                </asp:TableCell>
                            </asp:TableRow>
                                      
                        </asp:Table>
                    </asp:Panel>
                </td>
            </tr>
            <!--Content Part Ends-->
            <tr>
                <!--Footer Part-->
                <td class="Footer_Height" align="right">
                    © SHARP Software Development India Pvt Ltd
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td colspan="2" class="PaddingTop_Header Height_TopHeader">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="right" class="Paddingright_12 Width_UserIcon_Td">
                                <div class="User_Icon">
                                </div>
                            </td>
                            <td align="left" class="Width_User_Text WhiteSpace">
                                <asp:Label ID="LabelWel" CssClass="WelcomeFont" runat="server" Text="Welcome"></asp:Label>&nbsp;
                            </td>
                            <%--<td align="right">
                                <asp:LinkButton ID="ButtonBalance" runat="server" Text="Recharge" CssClass="Bal_Btn"
                                    OnClick="Button_Click"></asp:LinkButton>
                            </td>--%>
                            <td align="right" valign="top" class="PaddingRight_LogoutBtn">
                                <table id="tablelogout" runat="server" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td align="right" class="PaddingTop_LogoutBtn">
                                            <%--<asp:ImageButton ID="ImageButtonLogout" runat="server" OnClick="ImageButtonLogout_Click" />&nbsp;--%>
                                            <asp:LinkButton ID="LinkButtonLogout" runat="server" Text="Logout" CssClass="Bal_Btn"
                                                OnClick="ImageButtonLogout_Click"></asp:LinkButton>&nbsp;
                                        </td>
                                        <%--<td align="right">
                                            <as p:Label ID="LabelLogout" runat="server" CssClass="WelcomeFont" Text="Logout"></asp:Label>
                                        </td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="PaddingTop_BelowHeader">
                </td>
            </tr>
            <tr>
                <td align="right" class="Width_LeftTabs" valign="top">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="right" class="LeftTab_Selected PaddingTop_10" onclick="ShowColor();" id="LeftTab_Color">
                                <div style="display: table;">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right" valign="middle" class="Width_ColorIcon">
                                                <div class="ColorImg_Selected_Icon" id="Color_Img_Selected">
                                                </div>
                                                <div class="ColorImg_Unselected_Icon" id="Color_Img_Unselected" style="display: none;">
                                                </div>
                                            </td>
                                            <td align="left" valign="middle" class="Width_LeftTabText">
                                                <div class="Marginbottom_5">
                                                    <asp:Label ID="LabelColor" CssClass="FontSelected_LeftTab" runat="server" Text="Color"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="PaddingTop_10">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="LeftTab_Unselected PaddingTop_10" onclick="ShowBW();" id="LeftTab_BW">
                                <div style="display: table;">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right" valign="middle" class="Width_ColorIcon">
                                                <div class="BWImg_Unselected_Icon" id="BW_Img_Unselected">
                                                </div>
                                                <div class="BWImg_Selected_Icon" id="BW_Img_Selected" style="display: none;">
                                                </div>
                                            </td>
                                            <td align="left" valign="middle" class="Width_LeftTabText">
                                                <div class="Marginbottom_5">
                                                    <asp:Label ID="LabelBW" CssClass="FontUnselected_LeftTab" runat="server" Text="Black & White"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="PaddingTop_10">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="LeftTab_Unselected PaddingTop_10" onclick="MyBalance();"
                                id="LeftTab_Bal">
                                <div style="display: table;">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right" valign="middle" class="Width_ColorIcon">
                                                <div class="BalanceImg_Unselected_Icon" id="Balance_Img_Unselected">
                                                </div>
                                                <div class="BalanceImg_Selected_Icon" id="Balance_Img_Selected" style="display: none;">
                                                </div>
                                            </td>
                                            <td align="left" valign="middle" class="Width_LeftTabText">
                                                <div class="Marginbottom_5">
                                                    <asp:Label ID="LabelMyBalance" CssClass="FontUnselected_LeftTab" runat="server" Text="My Account"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="Width_RightContent">
                    <div id="Color">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="PaddingLeft_20">
                            <tr>
                                <td valign="middle" class="FontUnselected_LeftTab PaddingLeft_20 Height_ForMode_TD">
                                    <asp:Label ID="Label1" runat="server" Text="Select Job Type to Perform"></asp:Label>
                                </td>
                                <td align="right" class="Paddingright_30">
                                    <asp:Label ID="LabelBal" CssClass="FontUnselected_LeftTab" runat="server" Text="Balance :"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="Content_LeftPart_Width">
                                            </td>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="Content_Tab_Color">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="Height_PrintIcon">
                                                                        <asp:ImageButton ID="ImageButtonPrintColor" runat="server" OnClick="OnPrintColorClicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelPrint" runat="server" Text="Print"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="Content_CenterPart_Width">
                                                        </td>
                                                        <td class="Content_Tab_Color">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="Height_PrintIcon">
                                                                        <asp:ImageButton ID="ImageButtonCopyColor" runat="server" OnClick="OnColorCopyClicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelCopy" runat="server" Text="Copy"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="PaddingTop_25">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Content_Tab_Color">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="Height_PrintIcon">
                                                                        <asp:ImageButton ID="ImageButtonScanColor" runat="server" OnClick="OnColorScanClicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelScan" runat="server" Text="Scan"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="Content_CenterPart_Width">
                                                        </td>
                                                        <td class="Content_Tab_Color">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="Height_PrintIcon">
                                                                        <asp:ImageButton ID="ImageButtonFaxColor" runat="server" OnClick="OnColorFaxClicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelFax" runat="server" Text="Fax"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="Content_LeftPart_Width">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="Black_White" style="display: none;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="PaddingLeft_20">
                            <tr>
                                <td valign="middle" class="FontUnselected_LeftTab PaddingLeft_20 Height_ForMode_TD">
                                    <asp:Label ID="Label2" runat="server" Text="Select Job Type to Perform"></asp:Label>
                                </td>
                                <td align="right" class="Paddingright_30">
                                    <asp:Label ID="LabelBalance" CssClass="FontUnselected_LeftTab" runat="server" Text="Balance :"></asp:Label>
                                    <asp:Label ID="AccBalBW" CssClass="BalanceAmount_Font" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="Content_LeftPart_Width">
                                            </td>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="Content_Tab_BW">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="Height_PrintIcon">
                                                                        <%-- <img src="../App_Themes/Blue/Wide-SVGA/Images/Print_Icon_BW.png" width="56" height="53"
                                                                        alt="" border="0" />--%>
                                                                        <asp:ImageButton ID="ImageButtonPrintBW" runat="server" OnClick="OnPrintBWClicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelPrintBW" runat="server" Text="Print"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <%--<asp:Button ID="Button1" runat="server" OnClick="OnBWCopyClicked" />--%>
                                                        </td>
                                                        <td class="Content_CenterPart_Width">
                                                        </td>
                                                        <td class="Content_Tab_BW">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="Height_PrintIcon">
                                                                        <%-- <img src="../App_Themes/Blue/Wide-SVGA/Images/Copy_Icon_BW.png" width="55" height="54"
                                                                        alt="" border="0" />--%>
                                                                        <asp:ImageButton ID="ImageButtonCopyBW" runat="server" OnClick="OnBWCopyClicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelCopyBW" runat="server" Text="Copy"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <%--<asp:Button ID="Button2" runat="server" OnClick="OnColorCopyClicked" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="PaddingTop_25">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Content_Tab_BW">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="Height_PrintIcon">
                                                                        <%-- <img src="../App_Themes/Blue/Wide-SVGA/Images/Scan_Icon_BW.png" width="68" height="44"
                                                                        alt="" border="0" />--%>
                                                                        <asp:ImageButton ID="ImageButtonScanBW" runat="server" OnClick="OnBWScanClicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelScanBW" runat="server" Text="Scan"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <%--<asp:Button ID="Button3" runat="server" OnClick="OnBWScanClicked" />--%>
                                                        </td>
                                                        <td class="Content_CenterPart_Width">
                                                        </td>
                                                        <td class="Content_Tab_BW">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td class="Height_PrintIcon">
                                                                        <%-- <img src="../App_Themes/Blue/Wide-SVGA/Images/Fax_Icon_BW.png" width="61" height="55"
                                                                        alt="" border="0" />--%>
                                                                        <asp:ImageButton ID="ImageButtonFaxBW" runat="server" OnClick="OnBWFaxClicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelFaxBW" runat="server" Text="Fax"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <%--<asp:Button ID="Button4" runat="server" OnClick="OnColorScanClicked" />--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="Content_LeftPart_Width">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="Balance" style="display: none;">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="PaddingLeft_20">
                            <tr>
                                <td valign="middle" class="FontUnselected_LeftTab PaddingLeft_20 Height_ForMode_TD">
                                    <asp:Label ID="LabelOptionRecharge" runat="server" Text="Select Options for My Account"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="Content_LeftPart_Width">
                                            </td>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="Content_Tab_Color">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="center" class="Height_PrintIcon">
                                                                        <asp:ImageButton ID="ImageButtonMiniStatement" OnClick="ImageButtonMiniStatement_Click"
                                                                            runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelMiniStatement" runat="server" Text="Mini Statement"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="Content_CenterPart_Width">
                                                        </td>
                                                        <td class="Content_Tab_Color">
                                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td align="center" class="Height_PrintIcon">
                                                                        <asp:ImageButton ID="ImageButtonRecharge" runat="server" OnClick="ImageButtonRecharge_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="Height_Printtext">
                                                                        <div class="FontUnselected_LeftTab">
                                                                            <asp:Label ID="LabelRecharge" runat="server" Text="Top UP"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="PaddingTop_25">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="Content_LeftPart_Width">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript" type="text/javascript">

        function ShowColor() {
            document.getElementById("Color").style.display = "inline";
            document.getElementById("Black_White").style.display = "none";
            document.getElementById("Balance").style.display = "none";

            document.getElementById("LeftTab_Color").className = "LeftTab_Selected PaddingTop_10";
            document.getElementById("LeftTab_BW").className = "LeftTab_Unselected PaddingTop_10";
            document.getElementById("LeftTab_Bal").className = "LeftTab_Unselected PaddingTop_10";

            document.getElementById("LabelColor").className = "FontSelected_LeftTab";
            document.getElementById("LabelBW").className = "FontUnselected_LeftTab";
            document.getElementById("LabelMyBalance").className = "FontUnselected_LeftTab";

            document.getElementById("Color_Img_Unselected").style.display = "none";
            document.getElementById("Color_Img_Selected").style.display = "block";

            document.getElementById("BW_Img_Unselected").style.display = "block";
            document.getElementById("BW_Img_Selected").style.display = "none";

            document.getElementById("Balance_Img_Unselected").style.display = "block";
            document.getElementById("Balance_Img_Selected").style.display = "none";
        }

        function ShowBW() {
            document.getElementById("Color").style.display = "none";
            document.getElementById("Black_White").style.display = "inline";
            document.getElementById("Balance").style.display = "none";

            document.getElementById("LeftTab_Color").className = "LeftTab_Unselected PaddingTop_10";
            document.getElementById("LeftTab_BW").className = "LeftTab_Selected PaddingTop_10";
            document.getElementById("LeftTab_Bal").className = "LeftTab_Unselected PaddingTop_10";

            document.getElementById("LabelColor").className = "FontUnselected_LeftTab";
            document.getElementById("LabelBW").className = "FontSelected_LeftTab";
            document.getElementById("LabelMyBalance").className = "FontUnselected_LeftTab";

            document.getElementById("Color_Img_Unselected").style.display = "block";
            document.getElementById("Color_Img_Selected").style.display = "none";

            document.getElementById("BW_Img_Unselected").style.display = "none";
            document.getElementById("BW_Img_Selected").style.display = "block";

            document.getElementById("Balance_Img_Unselected").style.display = "block";
            document.getElementById("Balance_Img_Selected").style.display = "none";
        }

        function MyBalance() {
            document.getElementById("Color").style.display = "none";
            document.getElementById("Black_White").style.display = "none";
            document.getElementById("Balance").style.display = "inline";

            document.getElementById("LeftTab_Color").className = "LeftTab_Unselected PaddingTop_10";
            document.getElementById("LeftTab_BW").className = "LeftTab_Unselected PaddingTop_10";
            document.getElementById("LeftTab_Bal").className = "LeftTab_Selected PaddingTop_10";

            document.getElementById("LabelColor").className = "FontUnselected_LeftTab";
            document.getElementById("LabelBW").className = "FontUnselected_LeftTab";
            document.getElementById("LabelMyBalance").className = "FontSelected_LeftTab";

            document.getElementById("Color_Img_Unselected").style.display = "block";
            document.getElementById("Color_Img_Selected").style.display = "none";

            document.getElementById("BW_Img_Unselected").style.display = "block";
            document.getElementById("BW_Img_Selected").style.display = "none";

            document.getElementById("Balance_Img_Unselected").style.display = "none";
            document.getElementById("Balance_Img_Selected").style.display = "block";
        }       

    </script>
</body>
</html>
