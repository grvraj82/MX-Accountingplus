<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="ApplicationRegistration.DataCapture.Roles"%>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">

<script language="javascript" type="text/javascript">
    
    
    function isRoleSelected()
    {
        thisForm = document.forms[0];
        var Roles = thisForm.ROLE_ID.length;
	    selectedCount = 0;
	    if(Roles > 0)
	    {
	        for(var item = 0 ; item < Roles ; item++)
	        {
	            //alert(item)
	            if(thisForm.ROLE_ID[item].checked)
	            {
	                selectedCount++
	                return true;
	            }
	        }
	    }
	    else
	    {
	        if(thisForm.ROLE_ID.checked)
	        {
	            selectedCount++
	            return true;
	        }
	    }
	     
	    if(selectedCount == 0)
	    {
            alert("Please select the Role");
            return false;
        }
    }
    
    function confirmRoleDeletion()
    {
        if(isRoleSelected())
        {
            if(!confirm("Do you want to delete the selected Role?"))
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
            <td valign="middle" style="height:30px;white-space:nowrap">
                <asp:Label ID="LabelRoles" runat="server" Visible="false" Text="Roles" Font-Bold="True" Font-Size="12pt"></asp:Label>&nbsp;
                <asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/AppImages/Add.jpg" CausesValidation="False" OnClick="ImageButtonAdd_Click"/>&nbsp;<asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg" CausesValidation="False" OnClick="ImageButtonEdit_Click" OnClientClick="return isRoleSelected()" />
                <asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg"
                    OnClick="ImageButtonDelete_Click" OnClientClick="return confirmRoleDeletion()" />
                <asp:ImageButton ID="ImageButtonAssignRoles" runat="server" ImageUrl="~/AppImages/Assign Roles.gif"
                    OnClick="ImageButtonAssignRoles_Click" />&nbsp;
                <asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>
            
                <asp:Table ID="TableRoles" runat="server" CellSpacing="1" CellPadding="3" GridLines="Both" HorizontalAlign="center"  Width="100%" BorderWidth="0" BackColor="Silver">
                    <asp:TableHeaderRow ID="TableHeaderRow1" runat="server" CssClass="tableHeaderRow">
                        <asp:TableHeaderCell Width="20px" ID="TableHeaderCell1" runat="server"></asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell3" runat="server" Wrap="false" HorizontalAlign="Center" Width="50px">ID</asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell4" runat="server" Wrap="false" HorizontalAlign="Center" Width="100px">Role</asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell13" runat="server" Wrap="false" HorizontalAlign="Center" Width="100px">Category</asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell5" runat="server" Wrap="false" HorizontalAlign="Center" Width="300px">Description</asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell6" runat="server" Wrap="false" HorizontalAlign="Center" Width="50px">Active</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            
            </td>
        </tr>
    </table>
 </asp:Content>
