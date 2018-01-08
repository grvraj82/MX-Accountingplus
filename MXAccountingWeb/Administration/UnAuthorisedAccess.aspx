<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Blank.Master"
    CodeBehind="UNAuthorisedAccess.aspx.cs" Inherits="PrintRoverWeb.Administration.UNAuthorisedAccess" %>

<asp:Content ContentPlaceHolderID="PageContent" ID="PcAccessControl" runat="server">
    <table style="margin-top: 10%" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td>
         
                <asp:Image ID="Image1" SkinID="UnAuthorisedAccessAccessDenined" runat="server" />
                <div style="margin-top: 30px" class="MFPLogin_font">
                    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
