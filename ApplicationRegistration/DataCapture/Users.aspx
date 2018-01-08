<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="Users.aspx.cs" Inherits="ApplicationRegistration.DataCapture.Users" %>
    <asp:Content ID="UsersGrid" runat="server" ContentPlaceHolderID="PageContentHolder">
    <script language="javascript" type="text/javascript">
     
    function SetGridWidth()
    {
        document.getElementById("PanelUserDetails").style.width=screen.width-110;
        document.getElementById("PanelUserDetails").style.height = screen.height-410;
    }
    
    function isUserSelected()
    {
        thisForm = document.forms[0];
        var users = thisForm.USR_ID.length;
	    selectedCount = 0;
	    if(users > 0)
	    {
	        for(var item = 0 ; item < users ; item++)
	        {
	            //alert(item)
	            if(thisForm.USR_ID[item].checked)
	            {
	                selectedCount++
	                return true;
	            }
	        }
	    }
	    else
	    {
	        if(thisForm.USR_ID.checked)
	        {
	            selectedCount++
	            return true;
	        }
	    }
	     
	    if(selectedCount == 0)
	    {
            alert("Please select the User");
            return false;
        }
    }
    
    function confirmUserDeletion()
    {
        if(isUserSelected())
        {
            if(!confirm("Do you want to delete the selected User? \n\nNote:User with the same User ID can not be created again"))
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
            <td style="height:30px" valign="middle">
                <asp:Label ID="LabelUsers" runat="server" Visible="false" Text="Users" Font-Bold="True" Font-Size="12pt"></asp:Label>&nbsp;
                <asp:ImageButton ID="ImageButtonAdd" runat="server"  ImageUrl="~/AppImages/Add.jpg" CausesValidation="False" OnClick="ImageButtonAdd_Click"/>&nbsp;<asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg" CausesValidation="False" OnClick="ImageButtonEdit_Click" OnClientClick="return isUserSelected()" />
                <asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg"
                    OnClick="ImageButtonDelete_Click" OnClientClick="return confirmUserDeletion()" />
                <asp:ImageButton ID="ImageButtonXLS" runat="server" CausesValidation="False" ImageUrl="~/AppImages/xls.gif"
                    OnClick="ImageButtonXLS_Click" /><br />
                <asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label>

            </td>
        </tr>
                    
        <tr>
            <td>
                <div style="overflow:scroll;width:895px;width:100px" id="PanelUserDetails">
                    <asp:Table ID="TableUsers" runat="server" CellSpacing="1" CellPadding="4" GridLines="Both" HorizontalAlign="center"  Width="100%" BorderWidth="0" BackColor="Silver">
                            <asp:TableHeaderRow ID="TableHeaderRow1" runat="server" CssClass="tableHeaderRow">
                                <asp:TableHeaderCell Width="20px" ID="TableHeaderCell1" runat="server"></asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell3" runat="server" Wrap="False" HorizontalAlign="Center">ID</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell4" runat="server" Wrap="False" HorizontalAlign="Center">Name</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell13" runat="server" Wrap="False" HorizontalAlign="Center">Company Name</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell5" runat="server" Wrap="False" HorizontalAlign="Center">Address Line1</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell6" runat="server" Wrap="False" HorizontalAlign="Center">Address Line2</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell7" runat="server" Wrap="False" HorizontalAlign="Center">City</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell8" runat="server" Wrap="False" HorizontalAlign="Center">State</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell9" runat="server" Wrap="False" HorizontalAlign="Center">Country</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell10" runat="server" Wrap="False" HorizontalAlign="Center">Postal Code</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell11" runat="server" Wrap="False" HorizontalAlign="Center">Telephone Number</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell12" runat="server" Wrap="False" HorizontalAlign="Center">Email</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell2" runat="server" Wrap="False" HorizontalAlign="Center">Access Enabled</asp:TableHeaderCell>

                            </asp:TableHeaderRow>
                    </asp:Table>                       
            </div>
        </td>
    </tr>
    </table>
</asp:Content>

