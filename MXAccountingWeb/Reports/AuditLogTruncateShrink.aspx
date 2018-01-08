<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="AuditLogTruncateShrink.aspx.cs" Inherits="AccountingPlusWeb.Reports.AuditLogTruncateShrink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">
        fnShowAuditLog();
        Meuselected("AuditLog");        
    </script>
    <asp:ScriptManager EnableScriptGlobalization="True" ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" height="550">
        <tr>
            <td align="right" valign="top" style="width: 1px">
                <asp:Image ID="Image4" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="left">
                            <table cellpadding="0" cellspacing="0" width="50%" border="0">
                                <tr>
                                    <td class="HeadingMiddleBg" width="10%">
                                        <div style="padding: 4px 10px 0px 10px;">
                                            <asp:Label ID="LabelHeadingTruncateAuditLogShrink" runat="server" Text="Clear Audit Log"></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image7" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td width="85%" align="left" valign="middle">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr height="2">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <table width="90%" class="table_border_org" cellpadding="0" cellspacing="0" border="0">
                                <tr class="Top_menu_bg">
                                    <td class="f10b" height="35" colspan="2" align="left">
                                        &nbsp;
                                        <asp:Label ID="LabelTruncateAuditLog" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="5">
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td colspan="2">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
                                            <tr class="Grid_tr">
                                                <td align="center">
                                                    <table width="90%" cellpadding="0" cellspacing="0" border="0">
                                                        <%--<tr class="Grid_tr">
                                                        <td align="center">

                                                            <table width="100%" cellpadding="1" cellspacing="3" border="0" class="Table_bg">
                                                                <tr>
                                                                    <td width="30" class="Grid_topbg1"> 
                                                                    Td1
                                                                    </td>
                                                                    <td align="left" class="H_title""> 
                                                                    Td2
                                                                    </td>
                                                                    <td align="left" class="H_title">
                                                                        Td3
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>--%>
                                                        <tr>
                                                            <td align="right" width="47%" height="35">
                                                                <asp:Label ID="LabelClearAuditLog" runat="server" Text="" class="f10b"></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <asp:CheckBox ID="CheckBoxClearAuditLog" runat="server" Checked="true" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="47%" height="35">
                                                                <asp:Label ID="LabelShrinkDB" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                                            </td>
                                                            <td align="left">
                                                                <asp:CheckBox ID="CheckBoxShrinkDB" runat="server" Checked="true" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="47%" height="35">
                                                                <asp:Button ID="ButtonClear" runat="server" Text="" OnClick="ButtonClear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
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
