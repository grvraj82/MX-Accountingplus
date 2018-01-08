<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobStatus.aspx.cs" Inherits="AccountingPlusEA.Mfp.JobStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Job Status ::</title>

    <script language="javascript" type="text/javascript">
        function RedirectTo() 
        {
            location.href = "JobStatus.aspx";
        }

        function IsJobFinished() 
        {
            var returnToPrintJobs = parseInt(document.getElementById("HiddenFieldReturnToPrintJobs").value);
            if(returnToPrintJobs != "0")
            {
                var totalJobs = parseInt(document.getElementById("HiddenTotalJobs").value);
                var totalFinishedJobs = parseInt(document.getElementById("HiddenJobFinishedCount").value);
                if (totalFinishedJobs == totalJobs && (totalJobs > 0 && totalFinishedJobs > 0)) 
                {
                   
                }
                else 
                {
                    setTimeout("RedirectTo()", 10000);
                }
            }
        }
        
        function RefreshPage()
        {
            setTimeout("RedirectTo()", 10000);
        }
        
        function DisplayError(selectedValue) 
        {
            try 
            {
                document.getElementById('divStatus1').style.display = 'block';
                document.getElementById('divStatus2').style.display = 'block';
                document.getElementById('divStatus3').style.display = 'block';
                document.getElementById('divStatus4').style.display = 'block';
                document.getElementById('divStatus5').style.display = 'block';
            }
            catch (Error) 
            {

            }
        }
        function CloseCommunicator() 
        {
            try 
            {
                document.getElementById("divStatus1").style.display = "none";
                document.getElementById("divStatus2").style.display = "none";
                document.getElementById("divStatus3").style.display = "none";
                document.getElementById("divStatus4").style.display = "none";
                document.getElementById("divStatus5").style.display = "none";

            }
            catch (Error) 
            {

            }
        }
        
    </script>

</head>
<body leftmargin="0" topmargin="0" onload="javascript:IsJobFinished()" class="InsidePage_BGcolor">
    <form id="form1" runat="server">
    <asp:HiddenField ID="HiddenFieldReturnToPrintJobs" runat="server" />
    <table width="101%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" valign="top" height="2" colspan="5" class="HR_line_New">
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <asp:Table ID="TableJobList" runat="server" Width="100%" BorderWidth="0" CssClass="Title_bar_bg">
                    <asp:TableRow CssClass="Title_bar_bg">
                        <asp:TableCell Width="2" RowSpan="13" CssClass="Vr_Line_Insidepage">
                        </asp:TableCell>
                        <asp:TableCell Width="50" HorizontalAlign="center" Height="30" VerticalAlign="middle"
                            CssClass="Title_bar_bg">                           
                        </asp:TableCell>
                        <asp:TableCell Width="2" RowSpan="13" CssClass="Vr_Line_Insidepage">
                        </asp:TableCell>
                        <asp:TableCell Width="500" HorizontalAlign="center" VerticalAlign="middle">
                            <asp:Label ID="LabelJobName" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="2" RowSpan="13" CssClass="Vr_Line_Insidepage">
                        </asp:TableCell>
                        <asp:TableCell Width="116" HorizontalAlign="center" VerticalAlign="middle">
                            <asp:Label ID="LabelJobStatus" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="2" RowSpan="13" CssClass="Vr_Line_Insidepage">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Height="2" ColumnSpan="5"
                            CssClass="HR_line_New"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="HiddenJobFinishedCount" Value="0" runat="server" />
                <asp:HiddenField ID="HiddenTotalJobs" Value="0" runat="server" />
                <asp:Label ID="LabelStatus" runat="server" Text="" CssClass="Login_TextFonts"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
