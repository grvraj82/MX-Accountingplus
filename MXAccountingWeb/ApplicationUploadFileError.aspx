<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationUploadFileError.aspx.cs"
    Inherits="AccountingPlusWeb.ApplicationUploadFileError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Application FileUpload Error::</title>
     <script language="javascript" type="text/javascript">
    <asp:Literal ID="PageBackgroundUrl" runat="server"></asp:Literal>
        function bgLoad() {
            var detected_os = navigator.userAgent.toLowerCase();
            if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) {
                document.write("<div id='page-background' style='position:absolute; top:0; left:0; width:100%; height:100%;'><img src='" + pageBackgroundUrl + "' width='100%' height='100%'></div>");
                document.write(" <div id='content' style='position:absolute; z-index:1; padding:0px;'>");

            }
            else if ((navigator.appVersion.indexOf('Chrome') != -1) || (navigator.appVersion.indexOf('Safari') != -1) || (navigator.userAgent.indexOf('Opera') >= 0)) {           
                document.write("<div id='page-background' style='position:fixed;z-index:-1; top:0; left:0; width:100%; height:100%;'><img src='" + pageBackgroundUrl + "' width='100%' height='100%'></div>");
                document.write(" <div id='content' style='position:relative; z-index:1; padding:0px;'>");
            }
            else {

                document.write("<div id='page-background' style='position:fixed; top:0; left:0; width:100%; height:100%;'><img src='" + pageBackgroundUrl + "' width='100%' height='100%'></div>");
                document.write(" <div id='content' style='position:relative; z-index:1; padding:0px;'>");

            }
        }
    </script>
</head>
<body class="InsidePageBG">
    <form id="form1" runat="server">
    <div>
        <table align="center" width="75%" cellpadding="4" cellspacing="3">
            <tr>
                <td colspan="2" align="center" class="Grid_topbg">
                    <asp:Label ID="LabelApplicationErrorHeading" runat="server" Text="Application Error Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Image ID="ImageStop" SkinID="StopImage" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top" class="f12b" style="white-space: nowrap">
                    <asp:Label ID="LabelErrorMessageText" runat="server" Text="Error Message"></asp:Label>
                </td>
                <td valign="top">
                    <asp:Label ID="LabelErrorMessage" CssClass="f11b" runat="server" Text="At a Time Maximum of 10,000 Records Should be Imported"
                        ForeColor="#C00000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="f12b" style="white-space: nowrap">
                    <asp:Label ID="LabelSuggestionText" runat="server" Text="Suggestion"></asp:Label>
                </td>
                <td valign="top" class="f12b">
                    <asp:Label ID="LabelSuggestion" runat="server" Text="Please Upload 10,000 Records Only"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
