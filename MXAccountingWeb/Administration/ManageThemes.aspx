<%@ Page Language="C#" MasterPageFile="../MasterPages/InnerPage.Master" AutoEventWireup="true"
    CodeBehind="ManageThemes.aspx.cs" Inherits="PrintRoverWeb.Administration.ManageThemes"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
 function EditTheme() {
            if (IsthemeSelected()) {
                if (GetSeletedCount() > 1) {
                    jNotify('Please select only one Theme')
                    return false;
                }
            }
            else {
                return false;
            }

        }
         function IsthemeSelected() {
            var thisForm = document.forms[0];
            var users = thisForm.__ThemeID.length;
            var selectedCount = 0;

            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__ThemeID[item].checked) {
                        selectedCount++
                        return true;
                    }
                }
            }
            else {
                if (thisForm.__ThemeID.checked) {
                    selectedCount++
                    return true;
                }
            }

            if (selectedCount == 0) {
                jNotify('Please select the Theme')
                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.__ThemeID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.__ThemeID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.__ThemeID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;
        }
        
        function ApplySelectedTheme(ThemeName)
        {
        
        }


    </script>

    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
     <div style=" height:10px;">&nbsp;</div>
    <table width="98%" align="center" border="0" cellpadding="0" cellspacing="0" height="500">
        <tr>
           
            <td width="72%" valign="top">
                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="Setting_tableBorder">
                    <tr>
                        <td height="35" class="Top_menu_bg" align="center">
                            &nbsp;<asp:Label ID="LabelHeadingThemes" runat="server" Text="Themes"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Table EnableViewState="false" ID="TableThemes" CellSpacing="1" CellPadding="0" Width="100%" BorderWidth="0"
                                runat="server" CssClass="">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
