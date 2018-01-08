<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/InnerPage.Master" AutoEventWireup="true" CodeBehind="CurrencySettings.aspx.cs" Inherits="AccountingPlusWeb.Administration.CurrencySettings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ClientMessages" runat="server">

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
 <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
 <script type="text/javascript" language="javascript">

     fnShowCellSettings();
     Meuselected("Settings");


    </script>
   
    <style type="text/css">
        .FormTextBox_bg
        {
            margin-left: 0px;
        }
    </style>
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <%--<div style="height: 10px;"> &nbsp;</div>--%>
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
                                            <asp:Label ID="LabelHeadingJobConfig" runat="server" Text="Currency Settings"></asp:Label></div>
                                    </td>
                                    <td>
                                        <asp:Image ID="Image7" SkinID="HeadingRight" runat="server" />
                                    </td>
                                    <td id="Tablecellback" runat="server" width="8%" visible="false" align="left" valign="middle"
                                        style="display: none">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="SettingsImageButtonBack"
                                            CausesValidation="False" ToolTip=""  />
                                    </td>
                                    <td id="Tablecellimage" runat="server" visible="false" width="1%" style="display: none">
                                        <asp:Image ID="Image3" runat="server" SkinID="ManageusersimgSplit" />
                                    </td>
                                    <td width="7%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonSave" runat="server" SkinID="SettingsImageButtonSave"
                                            CausesValidation="true" ImageAlign="Middle" ToolTip=""  />
                                    </td>
                                    <td width="1%" class="Menu_split" style="display: none">
                                        <asp:Image ID="Image5" runat="server" SkinID="ManageusersimgSplit" />
                                    </td>
                                    <td width="7%" align="left" valign="middle" style="display: none">
                                        <asp:ImageButton ID="ImageButtonReset" runat="server" CausesValidation="False" SkinID="SettingsImageButtonReset"
                                            ImageAlign="Middle" ToolTip=""  />
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
                                        <asp:Label ID="LabelUserJobs" SkinID="LabelLogon" runat="server" Text="Currency Settings"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="47%" height="30">
                                        <asp:Label ID="LabelCurrencySymbol" runat="server" Text="Currency Symbol Type" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownListCurrencySymbol" CssClass="Dropdown_CSS"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCurrencySymbol__SelectedIndexChanged">
                                            <asp:ListItem Text="Text" Value="text"></asp:ListItem>
                                            <asp:ListItem Text="Image" Value="image"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id=trSymbolText runat="server" visible="false">
                                    <td align="right" width="47%" height="30">
                                        <asp:Label ID="LabelSymbolText" runat="server"  Text="Currency Symbol Text" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox id="TextboxSumbolText" CssClass="FormTextBox_bg" MaxLength="4" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id=trSymbolImage runat="server" visible="false">
                                    <td align="right" width="47%" height="30">
                                        <asp:Label ID="LabelSymbolImage" runat="server" Text="Currency Symbol Image" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:FileUpload ID="FileUpload1" runat="server">
                                        </asp:FileUpload>
                                        <asp:Image ID="ImageCurrency" runat="server" Visible="false" width="16px" Height="16px" runat="server" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" width="47%" height="30">
                                        <asp:Label ID="Label1" runat="server" Text="Position" class="f10b"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownListAppend" CssClass="Dropdown_CSS"
                                            runat="server"  >
                                            <asp:ListItem Text="Left" Value="LHS"></asp:ListItem>
                                            <asp:ListItem Text="Right" Value="RHS"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr height="10px">
                                    <td colspan="2" align="center">
                                        <table width="50%">
                                            <tr>
                                                <td width="10%">
                                                    &nbsp;
                                                </td>
                                                <td align="right" width="50%">
                                                    <asp:Button ID="ButtonUpdate" runat="server" CssClass="Login_Button" OnClick="ButtonUpdate_Click"
                                                        Text="Update" Visible="True" />
                                                </td>
                                                <td align="left">
                                                    <asp:Button runat="server" ID="ButtonReset" Text="Reset" CssClass="Login_Button"
                                                        OnClientClick="this.form.reset();return false;" />
                                                    <asp:Button ID="ButtonCancel" runat="server" CssClass="Cancel_button" Text="Cancel" Visible="false"
                                                        OnClick="ButtonCancel_Click" CausesValidation="false" />
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
    </table>
</asp:Content>
