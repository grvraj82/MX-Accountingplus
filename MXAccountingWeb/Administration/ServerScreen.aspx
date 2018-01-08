<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerScreen.aspx.cs" Inherits="AccountingPlusWeb.Administration.ServerScreen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript">

        function StartDigitalWatch() 
        {

            document.getElementById("TimerID").value = self.setInterval("DisplayServerScreen()", 1000);
        }

        function DisplayServerScreen() {

            var year = document.getElementById("drpYear");
            var month = document.getElementById("drpMonth");
            var day = document.getElementById("drpDay");

            var hour = document.getElementById("drpHour");
            var minute = document.getElementById("drpMinute");
            var second = document.getElementById("drpSecond");

            var targetScreenShot = "../ScreenCapture/" + year.value + "/" + month.value + "/" + day.value + "/" + hour.value + "/" + minute.value + "/" + second.value + ".png";

            document.getElementById("ImageServerScreen").src = targetScreenShot;

            // Increment timer
            var nextSecond = parseInt(second.value) + 1;
            //document.getElementById("TimerID").value = nextSecond;
            if (parseInt(nextSecond) < 60) {

                second.selectedIndex = nextSecond
            }
            else {

                second.selectedIndex = 0;

                if (parseInt(minute.selectedIndex + 1) < 60) {
                    minute.selectedIndex = minute.selectedIndex + 1;
                }
                else {
                    minute.selectedIndex = 0;

                    if (parseInt(hour.selectedIndex + 1) < 24) {
                        hour.selectedIndex = hour.selectedIndex + 1;
                    }
                    else {
                        hour.selectedIndex = 0;
                        
                    }
                }
            }
        }

        function PlayOrPause() {

            var playerState = document.getElementById("PlayerState");
            var timerID = document.getElementById("TimerID");
            var playerStateIcon = document.getElementById("PlayerStateIcon");
            if (playerState.value == "0") {
                playerState.value = "1";
                playerStateIcon.src = "../App_Images/player_pause.png";
                if (timerID != "") {
                    self.clearInterval(timerID.value);
                }
                StartDigitalWatch();
            }
            else {
                playerState.value = "0";
                playerStateIcon.src = "../App_Images/player_play.png";
                if (timerID != "") {
                    self.clearInterval(timerID.value);
                }
            }
        }
    </script>
</head>
<body onload="javascript:StartDigitalWatch()">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="drpYear" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                /
                            </td>
                            <td>
                                <asp:DropDownList ID="drpMonth" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                /
                            </td>
                            <td>
                                <asp:DropDownList ID="drpDay" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td width="30px">
                            </td>
                            <td>
                                <asp:DropDownList ID="drpHour" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpMinute" runat="server">
                                </asp:DropDownList>
                            </td>
                             <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSecond" runat="server">
                                </asp:DropDownList>
                            </td>

                            <td width="30px">
                                <a href="javascript:PlayOrPause()"><image src="../App_Images/player_pause.png" id="PlayerStateIcon" border="0" /></a>
                            </td>
                            <td>
                                Speed
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSpeed" runat="server">
                                </asp:DropDownList>
                                <input type="hidden" name="PlayerState" id="PlayerState" value="1" />
                                <input type="hidden" name="TimerID" id="TimerID" value="" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Image ID="ImageServerScreen" ImageUrl="" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
