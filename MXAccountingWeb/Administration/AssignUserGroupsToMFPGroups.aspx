<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="AssignUserGroupsToMFPGroups.aspx.cs" Inherits="AccountingPlusWeb.Administration.AssignUserGroupsToMFPGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        Meuselected("UserID");
        
        function ChkandUnchk() {
            if (document.getElementById('chkALL').checked) {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = true;
                }
            }
            else {
                for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                    document.getElementById('aspnetForm').elements[i].checked = false;
                }
            }
        }

        function togall(refcheckbox) {

            var thisForm = document.forms[0];
            var users = thisForm.__COSTCENTERID.length;
            var selectedCount = 0;
            if (thisForm.__COSTCENTERID[refcheckbox - 1].checked) {

                thisForm.__COSTCENTERID[refcheckbox - 1].checked = false;
            }
            else {
                thisForm.__COSTCENTERID[refcheckbox - 1].checked = true;
            }

        }
    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="table_border_org">
        <tr>
            <td height="25" align="left" valign="top">
                <table cellpadding="0" cellspacing="0" width="100%" border="0" class="Top_menu_bg">
                    <tr>
                        <td height="33" align="left" valign="middle" width="25%">
                            <table cellpadding="0" cellspacing="0" width="75%" border="0">
                                <tr>
                                    <td width="4%" align="center" valign="middle">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server"  SkinID="AssignUserGroupsToMFPGroupsBackPage"
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="" PostBackUrl="~/Administration/ManageUsers.aspx" />
                                    </td>
                                    <td width="1%" class="Menu_split">
                                    </td>
                                    <td width="4%" align="left" valign="middle">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" CausesValidation="true" ImageAlign="Middle" SkinID="AssignUserGroupsToMFPGroupssave"
                                         ToolTip="" OnClick="ImageButtonSave_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split">
                                    </td>
                                    <td width="4%" align="left" valign="middle">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="AssignUserGroupsToMFPGroupsReset" 
                                            ImageAlign="Middle" ToolTip="" OnClick="ImageButtonReset_Click" />
                                    </td>
                                    <td width="1%" class="Menu_split">
                                    </td>
                                    <td width="10%" class="f10b" height="35" align="right">
                                        <asp:Label ID="LabelGroups" runat="server" Text=""></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td valign="middle" align="left">
                                        <asp:DropDownList ID="DropDownListDeviceGroups" AutoPostBack="true" OnSelectedIndexChanged="DropDownListDeviceGroups_SelectedIndexChanged"
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="Grid_tr">
            <td class="CenterBG" valign="top" height="500">
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table align="center" cellpadding="0" cellspacing="0" border="0" class="" width="98%">
                                <tr>
                                    <td colspan="2" valign="top">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" height="30">
                                            <tr height="2">
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr align="center" height="30">
                                                <td>
                                                    <table width="70%" border="0" cellpadding="0" cellspacing="0" class="TableGridColor">
                                                        <tr class="Grid_tr">
                                                            <td>
                                                                <asp:Table EnableViewState="false" ID="TableUserGroups" CellSpacing="1" CellPadding="3"
                                                                    Width="100%" BorderWidth="0" runat="server" CssClass="Table_bg" SkinID="Grid">
                                                                    <asp:TableHeaderRow CssClass="Table_HeaderBG">
                                                                        <asp:TableHeaderCell Width="30" HorizontalAlign="Left"  >
                                                                            <input id="chkALL" onclick="ChkandUnchk()" type="checkbox" />
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell Wrap="false" HorizontalAlign="Left" >
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCellGroupName" Wrap="false" HorizontalAlign="Left"  CssClass="H_title"> 
                                                                        </asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell ID="TableHeaderCellIsGroupEnabled" Wrap="false" HorizontalAlign="Left"  CssClass="H_title"></asp:TableHeaderCell>
                                                                    </asp:TableHeaderRow>
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="10">
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Button ID="ButtonSave" CssClass="Login_Button" runat="server" Text="" TabIndex="11"
                                                        OnClick="ButtonSave_Click" />
                                                    <asp:Button ID="ButtonCancel" CssClass="Cancel_button" CausesValidation="false" runat="server"
                                                        Text="" TabIndex="12" PostBackUrl="~/Administration/ManageUsers.aspx" />
                                                    <asp:Button ID="ButtonReset" CssClass="Cancel_button" CausesValidation="false" runat="server"
                                                        Text="" TabIndex="13" OnClick="ButtonReset_Click" />
                                                </td>
                                            </tr>
                                            <tr height="2">
                                                <td>
                                                    &nbsp;
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
