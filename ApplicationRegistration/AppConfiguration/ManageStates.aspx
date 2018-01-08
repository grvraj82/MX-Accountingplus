<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="ManageStates.aspx.cs" Inherits="ApplicationRegistration.Settings.ManageStates" %>
<asp:Content ID="ManageStatesContent" ContentPlaceHolderID="PageContentHolder" runat="server">

<script language="javascript" type="text/javascript">
    function SetGridWidth()
    {
        document.getElementById("PanelCountries").style.width=screen.width-110;
    }
    
    function UpdateState(id)
    {
        document.forms[0].HiddenStateId.value = id;
    }
    
     function isStateSelected()
    {
        thisForm = document.forms[0];
        var countries = thisForm.RadioButtonState.length;
	    selectedCount = 0;
	    if(countries > 0)
	    {
	        for(var item = 0 ; item < countries ; item++)
	        {
	            if(thisForm.RadioButtonState[item].checked)
	            {
	                selectedCount++
	                return true;
	            }
	        }
	    }
	    else
	    {
	        if(thisForm.RadioButtonState.checked)
	        {
	            selectedCount++
	            return true;
	        }
	    }
	     
	    if(selectedCount == 0)
	    {
            alert("Please select the State");
            return false;
        }
    }
    
    function confirmStateDeletion()
    {
        if(isStateSelected())
        {
            if(!confirm("Do you want to delete the selected State?"))
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
            <td class="" style="height:35">&nbsp;&nbsp;<asp:Label ID="LabelPageTitle" runat="server" Text="States of" CssClass="f11b"></asp:Label>&nbsp;<asp:DropDownList
                ID="DropDownListCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged" CssClass="f11b">
            </asp:DropDownList>&nbsp;<asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click"
                Text="Back" /></td>
        </tr>
        <tr>
            <td style="white-space:nowrap" valign="middle">
                <br />
                &nbsp;&nbsp;
                <asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/AppImages/Add.jpg" CausesValidation="False" OnClick="ImageButtonAdd_Click"/>&nbsp;&nbsp;<asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg" CausesValidation="False" OnClick="ImageButtonEdit_Click" OnClientClick="return isStateSelected()" />
                <asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg"
                    OnClick="ImageButtonDelete_Click" OnClientClick="return confirmStateDeletion()" />
                &nbsp; &nbsp;&nbsp;<input type="hidden" name="HiddenStateId" value="" style="width: 1px" />
                <asp:Panel ID="PanelManageState" runat="server" Visible="False">&nbsp;&nbsp;
                    <asp:Label ID="LabelState" runat="server" Text="State Name" CssClass="f9b"></asp:Label>&nbsp;*
                    <asp:TextBox ID="TextBoxStateName" runat="server" MaxLength="50"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                        ID="requiredFieldStateName" runat="server" ControlToValidate="TextBoxStateName" Display="None"
                        ErrorMessage="Please enter valid State Name" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regularExpressionValidatorStateName" runat="server" ControlToValidate="TextBoxStateName"
                        Display="None" ErrorMessage="Please enter valid State Name" SetFocusOnError="True"
                        ValidationExpression="^[a-zA-Z0-9 , ']+$" Width="1px">*</asp:RegularExpressionValidator>
                    <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" Width="75px" /><asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" Width="75px" />&nbsp;
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" Width="75px" CausesValidation="False" />
                    <asp:HiddenField ID="HiddenStateId" runat="server" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </asp:Panel>
                
                <br />
                &nbsp;&nbsp;<asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>
                <div style="overflow:auto;width:895px;height:420px" id="PanelCountries">
                    <asp:Table ID="TableStates" runat="server" CellSpacing="1" CellPadding="4" GridLines="Both" HorizontalAlign="center"  Width="98%" BorderWidth="0" BackColor="white">
                    </asp:Table>
                </div>
            </td>
        </tr>
        </table>

</asp:Content>
