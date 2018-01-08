<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountBalance.aspx.cs" Inherits="AccountingPlusEA.Mfp.AccountBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="Browser" content="NetFront" />
    <title></title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">
        var timerCloseCommunicator;



        function ClearTimer() {
            var timeId = document.getElementById("TimerID").value;
            if (timeId != "") {
                self.clearInterval(timeId);
            }
        }

        function LoadPageImages() {
            try {
                timerCloseCommunicator = setTimeout('CloseCommunicator()', 5000);
            }
            catch (Error) {
            }
            ResetTrackCounter();
        }

        function ResetTrackCounter() {
            document.getElementById("ElapsedTime").value = "0";
        }
    </script>
</head>
<style type="text/css">
     <asp:Literal ID="PageBackground" runat="server"></asp:Literal>
</style>
<body leftmargin="0" topmargin="0" scroll="no" style="font-size:xx-large" onload="PageShowingEve(), init(), StartTimer()">
    <div style="display: inline;height:auto" id="PageShowingID" runat="server">
        <form id="UserAccBalance" runat="server" visible="true">
            <input type="hidden" size="4" id="ElapsedTime" value="0" />
            <input type="hidden" size="4" value="" id="TimerID" />
            <asp:HiddenField ID="HiddenFieldIntervalTime" runat="server" Value="0" />
            <input type="hidden" value="1" name="currentPage" id="currentPage" runat="server" />
            <table id="Header" runat="server" style="height:20%;display:inline;background-color:Blue">
                <tr>
                    <td>
                        <p>Account Balance</p>
                    </td>
                </tr>
            </table>
            <table id="Contents" width="100%" align="center" border="0" class="Error_msgTable" cellpadding="0" cellspacing="0">
                <tr class="Error_msgcenter">
                    <td align="center" style="width: 90%;">
                            <asp:Label ID="AccUser" Font-Names="Verdana,Arial" Font-Bold="true"
                                ForeColor="Red" runat="server" Text="Account Balance: Rs."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 90%;">
                            <asp:Label ID="accBalance" Font-Names="Verdana,Arial" Font-Bold="true"
                                ForeColor="Red" runat="server" Text="Account Balance: Rs."></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Footer" runat="server" style="height:20%;display:inline;background-color:Blue">
                <tr>
                    <td>
                        <p>Please wait while we take you to the next page...</p>
                    </td>
                </tr>
            </table>
         </form>
    </div>
    <script type="text/javascript">
        var reloadInterval = 2000;

        function PageShowingEve() {
            setTimeout(PageShowing(), 50000);
        }
        function PageShowing() {
            document.getElementById("PageShowingID").style.display = "inline";
        }
        function init() {
            setTimeout('redirect()', reloadInterval);
        }

        function redirect() {
            var rurl = document.getElementById("currentPage");
            if (rurl.value != null) {
                location.href = rurl.value;
            }
            else {
                location.href = "JobList.aspx";
            }
        }

        function StartTimer() {
            var elapsedTime = parseInt(document.getElementById("ElapsedTime").value);
            elapsedTime = elapsedTime + 1;
            document.getElementById("ElapsedTime").value = elapsedTime;
            var timeOut = document.getElementById("HiddenFieldIntervalTime").value;

            if (elapsedTime >= timeOut) {
                ClearTimer();
            }
        }
    </script>
</body>
</html>
