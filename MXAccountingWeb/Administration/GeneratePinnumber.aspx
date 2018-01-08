<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="GeneratePinnumber.aspx.cs" Inherits="AccountingPlusWeb.Administration.GeneratePinnumber" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <script language="javascript" type="text/javascript">

        fnShowCellUsers();
        Meuselected("UserID");

    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <div style="border: 0px solid black; width: 100%; display: none;">
        <div id="divStatus" style="width: 20%;">
        </div>
    </div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="right" valign="top">
                <asp:Image ID="Image3" SkinID="HeadingLeft" runat="server" />
            </td>
            <td width="100%" align="left" valign="top" colspan="3">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg">
                        <td class="HeadingMiddleBg" style="width: 10%">
                            <div style="padding: 4px 10px 0px 10px;">
                                <asp:Label ID="LabelUserManagement" runat="server" Text="Generate Pin"></asp:Label></div>
                        </td>
                        <td>
                            <asp:Image ID="Image4" SkinID="HeadingRight" runat="server" />
                        </td>
                        <td width="2%">
                        </td>
                        <td width="1%" align="left" valign="middle">
                            <asp:Label ID="LabelDummy" runat="server" Text=""></asp:Label>
                        </td>
                        <td width="4%" align="left" valign="middle">
                            <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="AddUsersImageButtonBack"
                                CausesValidation="False" ImageAlign="Middle" ToolTip="" OnClick="ImageButtonBack_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="2">
            <td>
                &nbsp;
            </td>
            <td width="100%" height="500px" class="CenterBG" valign="top">
                <table>
                    <tr>
                        <td height="10px">
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Grid_tr">
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0" align="center">
                                <tr>
                                    <td align="center">
                                        <table cellpadding="0" cellspacing="0" border="0" class="table_border_org" width="50%">
                                            <tr>
                                                <td colspan="2" align="left" valign="top">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                                        <tr class="Top_menu_bg">
                                                            <td width="50%" align="left" valign="middle">
                                                                &nbsp;<asp:Label ID="Label1" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                            </td>
                                                            <%-- <td align="right" width="30%" valign="middle">
                                                                <asp:Image ID="Image1" runat="server" SkinID="LogonImgRequired" Style="padding-right: 5px;" />
                                                            </td>--%>
                                                            <td align="left" width="20%">
                                                                <asp:Label ID="LabelRequiredField" runat="server" Text="" SkinID="Normal_FontLabel_bold"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellpadding="3" cellspacing="1" border="0" width="100%" class="Table_bg" align="center"
                                                        style="background-color: Gray">
                                                        <tr class="Table_HeaderBG">
                                                        <td></td>
                                                            <td class="H_title" style="white-space: nowrap">
                                                                User Source
                                                            </td>
                                                            <td class="H_title" style="white-space: nowrap">
                                                                Total Users
                                                            </td>
                                                            <td class="H_title" style="white-space: nowrap">
                                                                Number of users with Pin Numbers
                                                            </td>
                                                            <td class="H_title" style="white-space: nowrap">
                                                                Number of users without Pin Numbers
                                                            </td>
                                                        </tr>
                                                        <tr class="GridRow">
                                                        <td>
                                                            <asp:CheckBox ID="CheckBoxDB" runat="server" />
                                                        </td>
                                                            <td align="left">
                                                                DataBase
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelTotalUser" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelTotalUserWithPin" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelTotalUserWithoutPin" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="GridRow">
                                                           <td>
                                                            <asp:CheckBox ID="CheckBoxAD" runat="server" />
                                                        </td>
                                                            <td align="left">
                                                                ActiveDirectory
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelTotalUserAD" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelTotalUserWithPinAD" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelTotalUserWithoutPinAD" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="GridRow">
                                                            <td colspan="5">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr valign="middle" >
                                                                        <td align="right"  >
                                                                            <asp:Button ID="ButtonGenerate" runat="server" Text="Generate" OnClick="ButtonGenerate_Click" />
                                                                        </td>
                                                                        <td width="20px"></td>
                                                                        <td   >
                                                                            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel " OnClick="ButtonCancel_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="LabelActionMessage" runat="server" Text=""></asp:Label>
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
