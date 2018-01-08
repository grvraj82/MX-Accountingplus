<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="PriceManger.aspx.cs" Inherits="AccountingPlusWeb.Administration.PriceManger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        fnShowPrice();
        Meuselected("Pricing");

        function Onlynumerics(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            return ((k > 47 && k < 58) || k == 46);
        }


        function GetConfirmation() {
            if (confirm(C_SELECTED_PRICE_PROFILE_CONFIRM)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image3" SkinID="HeadingLeft" runat="server" />
            </td>
            <td valign="top" class="CenterBG">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr class="Top_menu_bg">
                        <td class="HeadingMiddleBg" width="2%">
                            <div style="padding: 4px 10px 0px 10px;">
                                <asp:Label ID="LabelHeadingCost" runat="server" Text=""></asp:Label>
                            </div>
                        </td>
                        <td>
                            <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                        </td>
                        <td width="98%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top" align="center" class="CenterBG" height="500">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            <asp:Table ID="TableWarningMessage" Visible="false" CellSpacing="1" CellPadding="3"
                                Width="50%" runat="server" CssClass="Table_bg" border="0" SkinID="Grid">
                                <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                    <asp:TableHeaderCell ID="TableHeaderCellDivName" CssClass="LabelWarningFont" ColumnSpan="2"
                                        HorizontalAlign="Left">Warning</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow CssClass="GridRow">
                                    <asp:TableCell ID="TableCellWarningImage" HorizontalAlign="Center" Width="20%">
                                        <asp:Image ID="ImageWarning" runat="server" SkinID="PermessionsAndLimitsCritical" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell1" HorizontalAlign="Left" Font-Bold="true" Width="80%">
                                           <p  class="LabelLoginFont"> </p>
                                           <p class="LabelWarningFont">There are no Cost Profile(s) created.</p>
                                           <p class="LabelLoginFont"></p>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                        <td align="center" valign="middle">
                            <asp:Panel ID="PanelMainData" runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" width="98%">
                                    <tr>
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table cellpadding="0" cellspacing="0" width="250px">
                                                            <tr style="background-color: White;">
                                                                <td>
                                                                    <asp:TextBox BorderWidth="0" ToolTip=""
                                                                        CssClass="SearchTextBox" Text="*" AutoPostBack="true" OnTextChanged="TextBoxCostProfileSearch_OnTextChanged"
                                                                        ID="TextBoxCostProfileSearch" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButtonSearchCostProfile" OnClick="ImageButtonSearchCostProfile_OnClick"
                                                                        runat="server" SkinID="SearchList" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSearch" runat="server" TargetControlID="TextBoxCostProfileSearch"
                                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="2" ServicePath="~/WebServices/ContextSearch.asmx"
                                                                        CompletionInterval="1000" ServiceMethod="GetCostProfilesForSearch" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButtonCancelSearch" runat="server" ToolTip=""
                                                                        SkinID="CancelSearch" OnClick="ImageButtonCancelSearch_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr class="Grid_tr">
                                                    <td valign="top">
                                                        <asp:HiddenField ID="HiddenFieldSelectedCostProfile" Value="" runat="server" />
                                                        <asp:Table ID="TableCostProfiles" SkinID="Grid" CssClass="Table_bg" CellSpacing="1"
                                                            CellPadding="3" Width="100%" BorderWidth="0" runat="server">
                                                        </asp:Table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="1%" valign="top">
                                            &nbsp;
                                        </td>
                                        <td width="90%" valign="top">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <table cellpadding="2" cellspacing="2">
                                                            <tr>
                                                                <td style="white-space: nowrap">
                                                                 <asp:Label ID="LabelCostDetailsofCostProfile" runat="server" Text=""></asp:Label>
                                                                   
                                                                </td>
                                                                <td align="center">
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LabelSelectedCostProfile" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td>
                                                                    and
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="LabelJobType" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td align="center">
                                                                    :
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="DropDownListJobType" CssClass="FormDropDown_Small" runat="server"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="DropDownListJobType_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:HiddenField ID="HdnJobTypesCount" Value="0" runat="server" />
                                                        <table align="center" cellpadding="0" width="98%" cellspacing="0" border="0" class="TableGridColor">
                                                            <tr class="Grid_tr">
                                                                <td>
                                                                    <asp:Table ID="tblPrices" Width="100%" CellSpacing="1" BorderWidth="0" CellPadding="0"
                                                                        runat="server" CssClass="Table_bg" SkinID="Grid">
                                                                        <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                            <asp:TableHeaderCell CssClass="RowHeader" Width="30px"></asp:TableHeaderCell>
                                                                            <asp:TableHeaderCell runat="server" HorizontalAlign="Left" CssClass="H_title" ID="TableHeaderCellJobtype"
                                                                                Wrap="false"></asp:TableHeaderCell>
                                                                            <asp:TableHeaderCell runat="server" HorizontalAlign="Left" CssClass="H_title" ID="TableHeaderCellPaperSize"
                                                                                Wrap="false"></asp:TableHeaderCell>
                                                                            <asp:TableHeaderCell runat="server" HorizontalAlign="Left" CssClass="H_title" ID="TableHeaderCellcolor"
                                                                                Wrap="false"></asp:TableHeaderCell>
                                                                            <asp:TableHeaderCell runat="server" HorizontalAlign="Left" CssClass="H_title" ID="TableHeaderCellBlack"
                                                                                Wrap="false"></asp:TableHeaderCell>
                                                                        </asp:TableHeaderRow>
                                                                    </asp:Table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="right" width="50%" height="50">
                                                                    <asp:Button ID="BtnUpdate" runat="server" Text="" CssClass="Login_Button" OnClick="BtnUpdate_Click" />
                                                                </td>
                                                                <td align="left" width="50%">
                                                                    <asp:Button ID="ButtonApplyToAll" runat="server" Text="Apply to All Categories" CssClass="Login_Button"
                                                                        OnClick="ButtonApplyToAll_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
