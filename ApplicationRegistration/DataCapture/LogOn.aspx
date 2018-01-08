<%@ Page Language="C#" MasterPageFile="~/AppMaster/LogOn.Master" AutoEventWireup="true" CodeBehind="LogOn.aspx.cs" Inherits="ApplicationRegistration.DataCapture.LogOn"%>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
<script language="javascript" type="text/javascript">
    function ResetMessage(labelID)
    {
      objLabel = document.getElementById(labelID);
      //alert(objLabel);
      objLabel.innerHTML= "";
    }    
    
    function FocusFirstEditableContol()
    {
        document.forms[0].ctl00$PageContentHolder$TextBoxLogOnID.focus();
    }
</script>

<table cellpadding="5" cellspacing="3" border="0" align="center" onkeydown="javascrip:ResetMessage('ctl00_PageContentHolder_LabelActionMessage')">
<tr>
    <td align="left" valign="top">
        <fieldset>
        <legend>
        <table>
            <tr>
                <td><img src="../AppImages/mebereLogOn_icon.gif" /></td>
                <td>
                    LogOn&nbsp;</td>
            </tr>    
        </table> 
        </legend>
        
        <table cellpadding="2" cellspacing="3" border="0" align="center">
        
        <tr>
            <td>
                LogOn ID</td>
            <td>
                <asp:TextBox ID="TextBoxLogOnID" runat="server" MaxLength="30" CssClass="DataCapture"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Password</td>
            <td>
                <asp:TextBox ID="TextBoxLogOnPassword" runat="server" MaxLength="30" TextMode="Password" CssClass="DataCapture"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                &nbsp;<asp:Button ID="ButtonLogOn" runat="server" Text="LogOn" OnClick="ButtonLogOn_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
        </tr>
        </table>
        </fieldset>
    </td>

</tr>


</table>




</asp:Content>
