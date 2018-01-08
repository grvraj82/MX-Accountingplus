<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="DiscoveryStatus.aspx.cs" Inherits="PrintRoverWeb.Administration.DiscoveryStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <meta http-equiv="refresh" content="5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">

        fnShowCellMFPs();
        Meuselected("Device");

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div align="center">
        <br>
        <br>
        <br>
        <b>
            <asp:Label ID="LabelDicoverProgress" runat="server" Text=""></asp:Label></b>
        <br>
        <br>
        <br>
        <asp:Image ID="Image1" SkinID="DiscoveryStatusProcessing" runat="server" />
        <br>
        <asp:Label ID="StatusMessage" runat="server"></asp:Label>
    </div>
</asp:Content>
