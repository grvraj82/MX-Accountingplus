<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="ManageMasterData.aspx.cs" Inherits="ApplicationRegistration.AppConfiguration.ManageMasterData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
<script language="javascript" type="text/javascript">
    
    function UpdateItem(controlId, id)
    {
        control = eval("document.forms[0]." + controlId );
        control.value = id;
    }
    
     function isItemSelected()
    {
        thisForm = document.forms[0];
        var countries = thisForm.RadioButtonItem.length;
	    selectedCount = 0;
	    if(countries > 0)
	    {
	        for(var item = 0 ; item < countries ; item++)
	        {
	            if(thisForm.RadioButtonItem[item].checked)
	            {
	                selectedCount++
	                return true;
	            }
	        }
	    }
	    else
	    {
	        if(thisForm.RadioButtonItem.checked)
	        {
	            selectedCount++
	            return true;
	        }
	    }
	     
	    if(selectedCount == 0)
	    {
            alert("Please select the Item");
            return false;
        }
    }
    
    function confirmItemDeletion()
    {
        if(isItemSelected())
        {
            if(!confirm("Do you want to delete the selected Item?"))
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
      <table width="100%" cellpadding="0" cellspacing="0" border="0" align="center">
        <tr>
            <td class="" style="height:35">&nbsp;&nbsp;<asp:Label ID="LabelLookup" runat="server" Text="Lookup" CssClass="f11b"></asp:Label>&nbsp;<asp:DropDownList
                ID="DropDownListMasterList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMasterList_SelectedIndexChanged" CssClass="f11b">
            </asp:DropDownList>&nbsp;<asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click"
                Text="Back" /></td>
        </tr>
        <tr>
            <td style="white-space:nowrap" valign="middle">
                <br />
                &nbsp;&nbsp;
                <asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/AppImages/Add.jpg" CausesValidation="False" OnClick="ImageButtonAdd_Click"/>&nbsp;&nbsp;<asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg" CausesValidation="False" OnClick="ImageButtonEdit_Click" OnClientClick="return isItemSelected()" />
                <asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg"
                    OnClick="ImageButtonDelete_Click" OnClientClick="return confirmItemDeletion()" />&nbsp;<br />
                &nbsp;&nbsp;
                &nbsp; 
                    <asp:HiddenField ID="HiddenItemId" runat="server" />
                    &nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
                <asp:Panel ID="PanelManageData" runat="server" Visible="False">&nbsp;&nbsp;
                    <asp:Label ID="LabelItem" runat="server" Text="Name" CssClass="f11b"></asp:Label>&nbsp;*
                    <asp:TextBox ID="TextBoxItemName" runat="server" MaxLength="250" CssClass="f11b" Width="283px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                        ID="requiredFieldItemName" runat="server" ControlToValidate="TextBoxItemName" Display="None"
                        ErrorMessage="Please enter valid data" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                    <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" Width="75px" /><asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" Width="75px" />&nbsp;
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" Width="75px" CausesValidation="False" />
                    &nbsp; &nbsp;
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </asp:Panel>
                
            </td>
        </tr>
        
        <tr>
            <td>
                    <asp:Table ID="TableItems" runat="server" CellSpacing="1" CellPadding="4" GridLines="Both" HorizontalAlign="center"  Width="98%" BorderWidth="0" BackColor="white" EnableViewState="False">
                    </asp:Table>
            </td>
        </tr>
        </table>

</asp:Content>