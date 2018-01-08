<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationErrorMessage.aspx.cs"
    Inherits="AccountingPlusWeb.ApplicationErrorMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal ID="LiteralApplicationName" runat="server"></asp:Literal>
    </title>
   
    

    <script src="JavaScript/resizing_background.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

    function AdjustHeight()
    {
        var availheight=screen.availHeight;
        var availwidth=screen.availWidth;
        var imageObject= document.getElementById("HeightAdjustImage");

        imageObject.width = availwidth-25;
        imageObject.style.paddingLeft="30px";
    }

    </script>

</head>
<body leftmargin="0" topmargin="0" onresize="rbResize()">

    <script language="javascript" type="text/javascript">rbOpen(false);</script>

    <div style="height: 10px">
    </div>
    <form id="form1" runat="server">
    <div style="z-index: 1; position: absolute; top: 2%; left: 2%;">
        <table align="center" width="75%" cellpadding="4" cellspacing="3" id="HeightAdjustImage">
            <tr>
                <td colspan="2" align="center" class="Grid_topbg">
                    <asp:Label ID="LabelApplicationErrorHeading" runat="server" Text="Application Error Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Image Height="128px" Width="128px" SkinID="ApplicationErrorMessageStop" ID="Image1" runat="server" />
                    
                </td>
            </tr>
            <tr>
                <td valign="top" class="f12b" style="white-space: nowrap">
                    <asp:Label ID="LabelErrorMessageText" runat="server" Text="Error Message"></asp:Label>
                </td>
                <td valign="top">
                    <asp:Label ID="LabelErrorMessage" CssClass="f11b" runat="server" Text="Label" ForeColor="#C00000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="f12b" style="white-space: nowrap">
                    <asp:Label ID="LabelErrorSourceText" runat="server" Text="Error Source"></asp:Label>
                </td>
                <td valign="top">
                    <asp:Label ID="LabelErrorSource" CssClass="f11b" runat="server" Text="Label" ForeColor="Maroon"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="f12b" style="white-space: nowrap">
                    <asp:Label ID="LabelTraceText" runat="server" Text="Trace"></asp:Label>
                </td>
                <td valign="top">
                    <asp:Label ID="LabelStackTrace" CssClass="f11b" runat="server" Text="Label" ForeColor="Maroon"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="f12b" style="white-space: nowrap">
                    <asp:Label ID="LabelAction" runat="server" Text="Action"></asp:Label>
                </td>
                <td valign="top" class="f12b">
                    <asp:Label ID="LabelNone" runat="server" Text="None"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="f12b" style="white-space: nowrap">
                    <asp:Label ID="LabelSuggestionText" runat="server" Text="Suggestion"></asp:Label>
                </td>
                <td valign="top" class="f12b">
                    <asp:Label ID="LabelSuggestion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script language="javascript" type="text/javascript">rbClose("App_Themes/Blue/Images/Bg.jpg");</script>

    <script language="javascript" type="text/javascript">rbInit();</script>

    <script language="javascript" type="text/javascript">AdjustHeight();</script>

</body>
</html>
