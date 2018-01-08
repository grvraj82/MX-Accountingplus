<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="RegistrationDetails.aspx.cs" Inherits="ApplicationRegistration.Views.RegistrationDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
<script language="javascript" type="text/javascript">
    function SetGridWidth()
    {
        document.getElementById("PanelRegistrationDetails").style.width=screen.width-110;
        document.getElementById("PanelRegistrationDetails").style.height=screen.height-410;
    }
    
    function isRegistrationRecordSelected()
        {
            thisForm = document.forms[0];
            var registrations = thisForm.REC_ID.length;
	        selectedCount = 0;
	        if(registrations > 0)
	        {
	            for(var item = 0 ; item < registrations ; item++)
	            {
	                //alert(item)
	                if(thisForm.REC_ID[item].checked)
	                {
	                    selectedCount++
	                    return true;
	                }
	            }
	        }
	        else
	        {
	            if(thisForm.REC_ID.checked)
	            {
	                selectedCount++
	                return true;
	            }
	        }
    	     
	        if(selectedCount == 0)
	        {
                alert("Please select the Registration record");
                return false;
            }
        }
        
        function confirmRegistrationDeletion()
        {
            if(isRegistrationRecordSelected())
            {
                if(!confirm("Do you want to delete the selected Registration?"))
                {
                    return false;
                }
            }
            else
            {
                 return false;
            }
        }
        
        function PrintRegistrationDetails(id, selectedProduct)
        {
            if(isRegistrationRecordSelected())
            {
                thisForm = document.forms[0];
                registrations = thisForm.REC_ID.length;
	            selectedCount = 0;
	            selectedItem = "";
	            if(registrations > 0)
	            {
	                for(var item = 0 ; item < registrations ; item++)
	                {
	                    //alert(item)
	                    if(thisForm.REC_ID[item].checked)
	                    {
	                        selectedItem = thisForm.REC_ID[item].value;
	                        break;
	                    }
	                }
	            }
	            else
	            {
	                selectedItem = thisForm.REC_ID.value;
	            }
	           
	            targetPage = "../Views/PrintRegistrationDetails.aspx?" + id + "=" + selectedItem + "&pid=" +selectedProduct;
                
                window.open(targetPage, "RegistrationDetails", "toolbar=no, addressbar=no, statusbar=no, width=600");
            }
            return false
        }
</script>

     <table width="100%" cellpadding="0" cellspacing="0" border="0">
       
        <tr>
            <td style="white-space:nowrap;" valign="middle">&nbsp; &nbsp;
            <asp:Panel ID="PanelControls" runat="server">
                    &nbsp;&nbsp;<asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/AppImages/Add.jpg" CausesValidation="False" OnClick="ImageButtonAdd_Click"/>&nbsp;<asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg" CausesValidation="False" OnClick="ImageButtonEdit_Click" OnClientClick="return isRegistrationRecordSelected()" />
                    <asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg"
                        OnClick="ImageButtonDelete_Click" OnClientClick="return confirmRegistrationDeletion()" />
                    <asp:ImageButton ID="ImageButtonPrint" runat="server" ImageUrl="~/AppImages/Print.gif" />
                </asp:Panel>
            
            </td>
            <td style="white-space:nowrap;" valign="middle"><asp:Label ID="LabelSerialKey" runat="server" Text="Serial Number : " Visible="False" CssClass="f11b"></asp:Label>
                    <asp:Label ID="LabelSerialKeyValue" runat="server" Visible="False" Font-Bold="True" CssClass="f11b"></asp:Label></td>
            <td align="right" style="white-space:nowrap">
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
                </table></asp:Panel>
            </td>
        </tr>
    
        <tr>
            <td colspan="3">
                <div style="overflow:scroll;width:895px;height:100px" id="PanelRegistrationDetails">
                    <asp:Table ID="TableRegistrationDetails" runat="server" CellSpacing="1" CellPadding="4" GridLines="Both" HorizontalAlign="center"  Width="100%" BorderWidth="0" BackColor="Silver" EnableViewState="False">
                    </asp:Table>
                </div>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        SetGridWidth();
    </script>
</asp:Content>