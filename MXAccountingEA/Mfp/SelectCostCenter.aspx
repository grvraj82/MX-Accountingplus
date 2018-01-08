<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCostCenter.aspx.cs"
    Inherits="AccountingPlusEA.Mfp.SelectCostCenter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="Browser" content="NetFront" />
    <title>Select Cost Center</title>
    <asp:Literal ID="LiteralCssStyle" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">
        function costCenterButtonClick(costCenterID) {
            document.sound(0);
            document.getElementById('HiddenFieldSelectedColumn').value = "0";
            document.getElementById('HiddenFieldCostCenterID').value = costCenterID;
            document.forms[0].submit();
            ResetTrackCounter();
        }

        function costCenterButtonClickPL(costCenterID) {
            document.sound(0);
            document.getElementById('HiddenFieldSelectedColumn').value = 1;
            document.getElementById('HiddenFieldCostCenterID').value = costCenterID;
            document.forms[0].submit();
            ResetTrackCounter();
        }

        function CloseCommunicator() {
            try {
                document.sound(0);
                document.getElementById("TableRowCommunicator").style.display = "none";
                //document.getElementById("TableCostCenterControls").style.display = "inline";
            }
            catch (Error) {
            }
            ResetTrackCounter();
        }

        function TrackUserInteraction() {
            var timerId = self.setInterval("StartTimer()", 1000);
            document.getElementById("TimerID").value = timerId;
        }

        function StartTimer() {
            var elapsedTime = parseInt(document.getElementById("ElapsedTime").value);
            elapsedTime = elapsedTime + 1;
            document.getElementById("ElapsedTime").value = elapsedTime;
            var timeOut = document.getElementById("HiddenFieldIntervalTime").value;
            var osaICCard = document.getElementById("HiddenFieldOsaICCard").value;
            if (elapsedTime >= timeOut) {
                ClearTimer();
                if (osaICCard == "True") {
                    location.href = "Refresh.aspx?osa=true";
                }
//                else {
//                    location.href = "Refresh.aspx?osa=true";
//                }
                //location.href = "../Mfp/LogOn.aspx";
            }
        }

        function ResetTrackCounter() {
            document.getElementById("ElapsedTime").value = "0";
        }

        function ClearTimer() {
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
<body leftmargin="0" topmargin="0" scroll="auto" class="InsideBG" onload="PageShowingEve(),TrackUserInteraction()">
    <div style="display: inline; width: 500px; left: 30px; z-index: 1; position: absolute"
        id="PageLoadingID" class="InsidePage_BGcolor">
        <table cellpadding="0" cellspacing="0" border="0" width="300" height="200">
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
        <form id="jobList" runat="server">
        <input type="hidden" size="4" id="ElapsedTime" value="0" />
        <input type="hidden" size="4" value="" id="TimerID" />
        <asp:HiddenField ID="HiddenFieldIntervalTime" runat="server" Value="0" />
        <asp:HiddenField runat="server" ID="HiddenFieldCostCenterID" Value=""></asp:HiddenField>
        <asp:HiddenField runat="server" ID="HiddenFieldSelectedColumn" Value=""></asp:HiddenField>
         <asp:HiddenField ID="HiddenFieldOsaICCard" Value="" runat="server" />
        <input type="hidden" value="1" name="currentPage" id="currentPage" runat="server" />
        <asp:HiddenField ID="HiddenRecordsPerPage" Value="5" runat="server" />
        <asp:HiddenField ID="HiddenTotalJobs" Value="0" runat="server" />
        <asp:HiddenField ID="HiddenFieldPrintText" runat="server" />
        <div runat="server" id="Communicator">
            <asp:Panel ID="TableRowCommunicator" Visible="false" CssClass="CommunicatorPannel"
                Width="100%" runat="server">
                <table width="100%" align="center" border="0" class="Error_msgTable" cellpadding="0"
                    cellspacing="0">
                    <tr class="Error_msgcenter">
                        <td align="center" style="width: 90%;">
                            <asp:Label ID="LabelCommunicatorMessage" Font-Names="Verdana,Arial" Font-Bold="true"
                                ForeColor="Red" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" valign="middle" width="10%">
                            <asp:Image ID="imageErrorClose" runat="server" onclick="CloseCommunicator()" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" height="368" id="TablePage" runat="server"
            width="103%" >
            <tr >
                <td height="50" align="left" valign="top">
                    <!-- Title Bar-->
                    <asp:Table ID="Table1" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0"
                        Height="50" border="0" runat="server">
                        <asp:TableRow>
                            <asp:TableCell CssClass="Inside_TOPTitleFontBold">
                                &nbsp;<asp:Label ID="LabelJobListPageTitle" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:LinkButton ID="LinkButtonCancel" OnClick="LinkButtonCancel_Click" runat="server">
                                    <asp:Table ID="TbleCancelButton" Width="82%" CellPadding="0" CellSpacing="0" Height="38"
                                        runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Left">
                                            </asp:TableCell>
                                            <asp:TableCell Width="75%" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Button_center">
                                                <div class="Login_TextFonts">
                                                    <asp:Label ID="LabelCancel" runat="server" Text="Cancel"></asp:Label>
                                                </div>
                                            </asp:TableCell>
                                            <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Button_Right">
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:LinkButton>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td height="2" class="HR_line_New">
                </td>
            </tr>
            <!--End of the Title Bar-->
            <tr id="TableCostCenterControls" runat="server">
                <td align="left" valign="top">
                    <!-- Table for Data Loading...-->
                    <table id="TablePrintJobs" width="100%" border="0" cellpadding="0" cellspacing="0"
                        runat="server">
                        <tr>
                            <td width="90%" align="center" valign="top">
                                <asp:Table ID="TableCostCenterList" runat="server" Height="42" border="0" CellPadding="0"
                                    CellSpacing="0" Width="100%">
                                    <asp:TableHeaderRow CssClass="Title_bar_bg" Height="42">
                                        <asp:TableHeaderCell ID="HeaderCellSlNo" Wrap="false" Width="40">
                                        </asp:TableHeaderCell>
                                        <asp:TableCell ID="TableCell7" Width="4" runat="server" class="Vr_Line_Insidepage"
                                            RowSpan="1000">
                                        </asp:TableCell>
                                        <asp:TableHeaderCell ID="HeaderCellCostCenterName" Wrap="false" Text="Cost Center Name"
                                            Width="40%">
                                        </asp:TableHeaderCell>
                                        <asp:TableCell ID="TableCell1" Width="4" runat="server" class="Vr_Line_Insidepage"
                                            RowSpan="1000">
                                        </asp:TableCell>
                                        <asp:TableHeaderCell ID="TableHeaderCellAccess" Wrap="false" Text="Access ?" Width="20%">
                                        </asp:TableHeaderCell>
                                        <asp:TableCell ID="TableCell3" Width="4" runat="server" class="Vr_Line_Insidepage"
                                            RowSpan="1000">
                                        </asp:TableCell>
                                        <asp:TableHeaderCell ID="TableHeaderCellPermissions" Wrap="false" Text="Permissions & Limits"
                                            Width="35%">
                                        </asp:TableHeaderCell>
                                        <asp:TableCell ID="TableCell2" Width="2" runat="server" class="Vr_Line_Insidepage"
                                            RowSpan="1000">
                                        </asp:TableCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Height="2" ColumnSpan="7"
                                            CssClass="HR_line_New">
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <!-- End of the Table for Data Loading...-->
                </td>
            </tr>
            <tr>
                <td height="2" class="HR_Line">
                </td>
            </tr>
        </table>
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
