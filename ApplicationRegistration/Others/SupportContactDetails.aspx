<%@ Page Language="C#" MasterPageFile="~/AppMaster/LogOn.Master" AutoEventWireup="true" CodeBehind="SupportContactDetails.aspx.cs" Inherits="ApplicationRegistration.Others.SupportContactDetails" %>
<asp:Content ID="SupportContacts" ContentPlaceHolderID="PageContentHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function SetGridWidth()
        {
            document.getElementById("PanelContactDetails").style.width=screen.width-110;
            document.getElementById("PanelContactDetails").style.height = screen.height-410;
        }
        
        function IsContactSelected()
        {
            thisForm = document.forms[0];
            var users = thisForm.REC_ID.length;
            selectedCount = 0;
            if(users > 0)
            {
                for(var item = 0 ; item < users ; item++)
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
                alert("Please select Support Center");
                return false;
            }
        }
        
        function ConfirmContactDeletion()
        {
            if(IsContactSelected())
            {
                if(!confirm("Do you want to delete the selected Support Center?"))
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
    <br />
    <p class="f14b">Contact Details</p>
    <asp:Panel ID="PanelMenu" runat="server">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td><asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/AppImages/Add.jpg" CausesValidation="False" OnClick="ImageButtonAdd_Click"/></td>
                <td><asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg" CausesValidation="False" OnClick="ImageButtonEdit_Click" OnClientClick="return IsContactSelected()" /></td>
                <td><asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg" OnClick="ImageButtonDelete_Click" OnClientClick="return ConfirmContactDeletion()" /></td>
            </tr>
        </table>
    </asp:Panel>
    <div style="overflow:scroll;width:895px;width:100px" id="PanelContactDetails">
        <asp:Table ID="TableSupportContacts" runat="server" CellSpacing="1" CellPadding="4" GridLines="Both" HorizontalAlign="center"  Width="100%" BorderWidth="0" BackColor="Silver">
            <asp:TableHeaderRow CssClass="tableHeaderRow">
                 <asp:TableHeaderCell></asp:TableHeaderCell>
                <asp:TableHeaderCell Width="25%">Contact Centre</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="25%">Contact Number</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="50%">Contact Address</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>