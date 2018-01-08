<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintRegistrationDetails.aspx.cs" Inherits="ApplicationRegistration.Views.PrintRegistrationDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>:: Product Registration Details ::</title>
    <script language="javascript" type="text/javascript">
    function PrintPage()
    {
        window.print();
    }
    
    </script>
    <link href="../AppStyle/ApplicationStyle.css" rel="stylesheet" type="text/css" />
</head>
<body onload="javascript:PrintPage()">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="1" bgcolor="silver" align="center">
            <tr>
                <td bgcolor="white" align="left">
                     <asp:Table ID="TableRegistrationResult" Width="500" CellSpacing="0" CellPadding="3" runat="server">
                        <asp:TableRow>
                            <asp:TableCell CssClass="f11b" BackColor="Silver" ColumnSpan="2">Registration Details</asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>Product</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelProduct" runat="server" ForeColor="green" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>Serial Number</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelSerialKey" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>Client Code</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelClientCode" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>Activation Code</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelActivationCode" ForeColor="green" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        
                         <asp:TableRow>
                                <asp:TableCell ColumnSpan="2" BackColor="silver" CssClass="f12b">Personnel Information</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>First Name</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelFirstName" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>Last Name</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelLastName" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Company</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelCompany" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Email</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelEmail" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell Visible="true">Phone</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelPhone" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell Visible="true">Fax</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelFax" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                                <asp:TableCell ColumnSpan="2" BackColor="silver" CssClass="f12b">System Information</asp:TableCell>
                        </asp:TableRow>
                       <asp:TableRow>
                            <asp:TableCell Visible="true">IP Address</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelIPAddress" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell Visible="true">MAC Address</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelMACAddress" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow Visible="false">
                            <asp:TableCell Visible="true">Computer Name</asp:TableCell>
                            <asp:TableCell CssClass="f11b">
                                <asp:Label ID="LabelComputerName" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
               </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
