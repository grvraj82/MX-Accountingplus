<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true"
    CodeBehind="AssignRedistLimits.aspx.cs" Inherits="ApplicationRegistration.DataCapture.AssignRedistLimits"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" align="left" width="50%" class="table_border_org">
    <tr>
    <td colspan="2" class="f11b">Assign Limits to Redistributors</td>
</tr>
        <tr  height="33px" align="center">
            <td >
                &nbsp;Redistributor :
                <asp:DropDownList ID="DropDownListRedist" runat="server" AutoPostBack="True" CssClass="DataCapture"
                    OnSelectedIndexChanged="DropDownListRedist_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
             <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Button ID="Button1" runat="server" Text="OK" OnClick="ButtonOk_Click" Width="62px" />&nbsp;
                            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="ButtonCancel_Click"
                                Width="62px" />
                        </td>
            </tr> </table>
                <table align="left" cellpadding="0" width="100%" cellspacing="0" border="0" class="table_border_org">
                    <tr>
                        <td>
                            <asp:Table ID="TableLimits" runat="server" CellSpacing="1" CellPadding="3" GridLines="Both"
                                HorizontalAlign="center" Width="98%" BorderWidth="0" BackColor="Silver">
                                <asp:TableHeaderRow ID="trlimitGridColumnHeader" runat="server" BackColor="#EFEFEF">
                                    <asp:TableHeaderCell Wrap="false" Width="30"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCell1">Products</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Wrap="false" ID="TableHeaderCell2">Limits</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                             <asp:HiddenField ID="HdnProductCount" Value="0" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                 </td>
        </tr>
    </table>
</asp:Content>
