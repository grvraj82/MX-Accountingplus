<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="PrintJobStatus.aspx.cs" Inherits="AccountingPlusWeb.Administration.PrintJobStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        Meuselected("JobList");
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" align="center" border="0" class="table_border_org" cellpadding="0"
        cellspacing="0" height="550">
        <tr>
            <td valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" height="33" width="100%">
                    <tr class="Top_menu_bg">
                        <td valign="top">
                            <asp:Table ID="Table1" runat="server" CellPadding="2" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell ID="tabelCellLabelUserSource" align="center" valign="middle" runat="server"
                                        Visible="true" Height="33">
                                        <asp:Label ID="LabelPrintJobStatus" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;:
                                    </asp:TableCell>
                                     <asp:TableCell ID="TableCell1" align="center" valign="middle" runat="server"
                                        Visible="true" Height="33">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="PrintJobStatusBackPage" 
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="" PostBackUrl="~/Administration/JobList.aspx" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table width="98%" align="center" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                <tr class="Grid_tr">
                                    <td>
                                        <asp:Label ID="LabelStatusMessage" runat="server" Text="" SkinID="TotalResource"></asp:Label>&nbsp;:
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
