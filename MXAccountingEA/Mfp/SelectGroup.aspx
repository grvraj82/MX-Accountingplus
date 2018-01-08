<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/LogOn.Master" AutoEventWireup="true"
    CodeBehind="SelectGroup.aspx.cs" Inherits="AccountingPlusEA.SelectGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogOnControls" runat="server">
    <script language="javascript" type="text/javascript">
        function costCenterButtonClick(costCenterID) {
            document.getElementById('ctl00_LogOnControls_HiddenFieldCostCenterID').value = costCenterID;
            window.location.href = "SelectGroup.aspx?CC=" + costCenterID + "";
        }
    </script>
    <asp:HiddenField runat="server" ID="HiddenFieldCostCenterID" Value=""></asp:HiddenField>
    <asp:Table ID="TableManualLogOn" runat="server" CellPadding="0" Height="250" CellSpacing="0"
        Width="85%" border="0">
        <asp:TableRow ID="TableRowLogOnControls" runat="server" Visible="true">
            <asp:TableCell Width="100%" HorizontalAlign="Center" VerticalAlign="top">
                <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Auto">
                    <asp:Table ID="TableLogOnControls" runat="server" Width="100%" CellPadding="3" CellSpacing="3"
                        border="0" Height="150px">
                    </asp:Table>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRowCommunicator" runat="server" Visible="false">
            <asp:TableCell Width="100%" HorizontalAlign="Center" VerticalAlign="top">
                <asp:Table ID="TableCommunicator" EnableViewState="false" runat="server" Width="50%"
                    Height="229" HorizontalAlign="Center" CellPadding="0" CellSpacing="0" border="0">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Height="115">
                            <asp:Label ID="LabelCommunicatorNote" runat="server" Text="" CssClass="Login_Error_msg"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="32%" HorizontalAlign="Center" VerticalAlign="middle" Height="112">
                            <asp:LinkButton ID="LinkButtonOk" OnClick="LinkButtonOk_Click" runat="server">
                                <asp:Table ID="Table4" runat="server" CellPadding="0" CellSpacing="0" Height="38"
                                    Width="45%" border="0">
                                    <asp:TableRow>
                                        <asp:TableCell Width="4%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Left">
                                        </asp:TableCell>
                                        <asp:TableCell Width="90%" HorizontalAlign="Center" VerticalAlign="middle" CssClass="Button_center">
                                            <div class="Login_TextFonts">
                                                <asp:Label ID="LabelOK" runat="server" Text="OK"></asp:Label>
                                            </div>
                                        </asp:TableCell>
                                        <asp:TableCell Width="3%" HorizontalAlign="Left" VerticalAlign="middle" CssClass="Button_Right">
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
