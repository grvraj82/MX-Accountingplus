<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryAnalyzer.aspx.cs"
    Inherits="AccountingPlusWeb.Web.QueryAnalyzer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html id="PageHtml" runat="server">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link href="../Notify/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Notify/jquery.js"></script>
    <script type="text/javascript" src="../Notify/jNotify.jquery.js"></script>
    <script src="../JavaScript/InnerMaster.js" type="text/javascript"></script>
    <link href="../App_Themes/Blue/AppStyle/ApplicationStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel runat="server" ID="PanelLogin">
            <table cellpadding="3" cellspacing="3" border="0" style="margin-top: 10%; margin-left: 40%;
                border: 1px solid #007ca5;">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td width="60px">
                                    <asp:Label ID="Label2" runat="server" Text="User Name"></asp:Label>
                                </td>
                                <td width="5px">
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxUserName" MaxLength="30" runat="server" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td width="60px">
                                    <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
                                </td>
                                <td width="5px">
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" MaxLength="30"
                                        Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="ButtonOk" runat="server" Text="OK" OnClick="ButtonOk_Click" />
                                </td>
                                <td width="5px">
                                </td>
                                <td>
                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelQuery">
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 50px; margin-bottom: 20px;
                border: 1px solid #007ca5;">
                <tr class="Top_menu_bg">
                    <td style="padding-left: 10px;">
                        Query Analyzer
                    </td>
                </tr>
                <tr>
                    <td height="10px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="CenterBG" width="100%">
                            <tr>
                                <td style="padding-left: 20px; padding-right: 20px;">
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top" width="100%">
                                                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="left" class="RowStyle PaddingLeftForTextBox" valign="bottom">
                                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td width="7%">
                                                                                    <asp:Label ID="Label1" runat="server" Text="Query Type :"></asp:Label>
                                                                                </td>
                                                                                <td align="left" width="10%">
                                                                                    <asp:DropDownList ID="DropDownListQueryType" runat="server">
                                                                                        <asp:ListItem Value="0">Select Statement</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Execute Statement</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td width="1%">
                                                                                </td>
                                                                                <td align="left" width="10%">
                                                                                    <asp:Button ID="ButtonExecute" runat="server" CssClass="ButtonSave_CMD" OnClick="ButtonExecute_Click"
                                                                                        TabIndex="12" Text="Execute" />
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td width="62%">
                                                                                </td>
                                                                                <td align="right" width="10%">
                                                                                    <asp:Button ID="Button1" runat="server" CssClass="ButtonSave_CMD" Text="Logout" 
                                                                                        onclick="Button1_Click" />
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr class="Grid_tr">
                                                                    <td align="left" class="RowStyle PaddingLeftForTextBox">
                                                                        <asp:TextBox ID="TextBoxQueryAnalyzer" runat="server" Height="250px" TextMode="MultiLine"
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="RowStyle PaddingLeftForTextBox">
                                                                        <div style="overflow: auto; width: 800px; height: 400px">
                                                                            <asp:GridView ID="GridViewData" runat="server" CellPadding="4" ForeColor="#333333"
                                                                                GridLines="None" Visible="false">
                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                <EditRowStyle BackColor="#2461BF" />
                                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                <RowStyle BackColor="#EFF3FB" />
                                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="padding: 10px 0px 0px 5px;" valign="middle">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
