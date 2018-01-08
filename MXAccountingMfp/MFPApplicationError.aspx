<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MFPApplicationError.aspx.cs" Inherits="AccountingPlusDevice.MFPApplicationError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="Browser" content="NetFront" />
    <asp:ContentPlaceHolder ID="head" runat="server">
        <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
    </asp:ContentPlaceHolder>
</head>
<body class="LoginBG">
    <form id="form1" runat="server">
    <div>
      <table align="center" width="75%" cellpadding="4" cellspacing="3">
            <tr>
                <td colspan="2" align="center" class="Grid_topbg">
                    <asp:Label ID="LabelMFPApplicationErrorHeading" runat="server" 
                        Text="MFP Application Error Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <img height="128px" width="128px" src="Images/Stop.png"alt="Error" />
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
                    <asp:Label ID="LabelSuggestion" runat="server" Text="Please contact Administrator"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
