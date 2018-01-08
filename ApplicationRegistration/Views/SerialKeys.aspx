<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master"CodeBehind="SerialKeys.aspx.cs" Inherits="ApplicationRegistration.Views.SerialKeys" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
    <script language="javascript" type="text/javascript">
           

        function SetGridWidth()
        {
            document.getElementById("PanelSerialKeys").style.width=screen.width-110;
            document.getElementById("PanelSerialKeys").style.height = screen.height-410;
        }

        function isSerialKeySelected()
        {
            thisForm = document.forms[0];
            var SerialKeys = thisForm.SRLKEY_ID.length;
	        selectedCount = 0;
	        if(SerialKeys > 0)
	        {
	            for(var item = 0 ; item < SerialKeys ; item++)
	            {
	                //alert(item)
	                if(thisForm.SRLKEY_ID[item].checked)
	                {
	                    selectedCount++
	                    return true;
	                }
	            }
	        }
	        else
	        {
	            if(thisForm.SRLKEY_ID.checked)
	            {
	                selectedCount++
	                return true;
	            }
	        }
    	     
	        if(selectedCount == 0)
	        {
                alert("Please select the Serial Number");
                return false;
            }
        }
        
        function confirmSerialKeyDeletion()
        {
            if(isSerialKeySelected())
            {
                if(!confirm("Do you want to delete the selected Serial Number?"))
                {
                    return false;
                }
            }
            else
            {
                 return false;
            }
        }
        
    </script>
 
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <table cellpadding="2" cellspacing="2" border="0" width="100%">
                <tr>
                    <td align="left" colspan="2"><asp:Panel ID="PanelControls" runat="server">&nbsp;
                <asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/AppImages/Add.jpg" CausesValidation="False" OnClick="ImageButtonAdd_Click"/>&nbsp;<asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg" CausesValidation="False" OnClick="ImageButtonEdit_Click" OnClientClick="return isSerialKeySelected()" />
                <asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg" OnClick="ImageButtonDelete_Click" OnClientClick="return confirmSerialKeyDeletion()" />
                   </asp:Panel></td>
                   <td align="left" valign="middle"><asp:Label
                    ID="LabelSerialKey" runat="server" CssClass="f11b"></asp:Label></td>
                    <td align="right">
                        <asp:Panel ID="PanelNavigation" runat="server">
                       <table>
                          <tr>
                            <td style="white-space:nowrap">
                                <asp:Label ID="LabelTotalRecords" runat="server" Text="Total Records"></asp:Label>:
                                <asp:Label ID="LabelTotalRecordCount" runat="server" CssClass="f11b" Text=""></asp:Label>&nbsp;
                                <asp:Label ID="LabelPageSize" runat="server" Text="Page Size"></asp:Label>&nbsp;<asp:DropDownList ID="DropDownListPageSize" runat="server" AutoPostBack="True" CssClass="f11b" OnSelectedIndexChanged="DropDownListPageSize_SelectedIndexChanged" CausesValidation="True">
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem Selected="True">20</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>500</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="white-space:nowrap">
                                <asp:ImageButton ID="ImageButtonFirstPage" runat="server" ImageUrl="~/AppImages/pg_first.gif" OnClick="ImageButtonFirstPage_Click" /></td>
                            <td style="white-space:nowrap">
                                <asp:ImageButton ID="ImageButtonPreviousPage" runat="server" ImageUrl="~/AppImages/pg_prev.gif" OnClick="ImageButtonPreviousPage_Click" /></td>
                            <td style="white-space:nowrap">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Page"></asp:Label>
                                    <asp:Label ID="LabelCurrentPageNumber" runat="server" CssClass="f11b" ForeColor="#004000"></asp:Label>&nbsp;of 
                                <asp:Label ID="LabelPageNumbers" runat="server" CssClass="f11b" ForeColor="#400000"></asp:Label>
                                    &nbsp;
                                </b>
                                </td>
                            <td style="white-space:nowrap">
                                <asp:ImageButton ID="ImageButtonNextPage" runat="server" ImageUrl="~/AppImages/pg_next.gif" OnClick="ImageButtonNextPage_Click" /></td>
                            <td style="white-space:nowrap">
                                <asp:ImageButton ID="ImageButtonLastPage" runat="server" ImageUrl="~/AppImages/pg_last.gif" OnClick="ImageButtonLastPage_Click" /></td>
                          </tr>          
                        </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div style="overflow:scroll;width:895px;height:100px" id="PanelSerialKeys">
                <asp:Table ID="TableSerialKeys" runat="server" CellSpacing="1" CellPadding="4" GridLines="Both" HorizontalAlign="center"  Width="100%" BorderWidth="0" BackColor="Silver"></asp:Table>
           </div>
         </td>
     </tr>
    </table>
</asp:Content>