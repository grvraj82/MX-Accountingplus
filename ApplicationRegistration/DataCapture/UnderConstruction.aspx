<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="UnderConstruction.aspx.cs" Inherits="ApplicationRegistration.DataCapture.UnderConstruction" %>
 <asp:Content ID="Users" runat="server" ContentPlaceHolderID="PageContentHolder">
 <script language="javascript" type="text/javascript">
    function SetGridWidth()
    {
        document.getElementById("PanelUC").style.width=screen.width-110;
    }
</script>
 <br /><br />
  <div style="overflow:auto;width:895px;height:420px" id="PanelUC">
 <table align="center" width="100%" border="0">
    <tr>
        <td  align="center"><img src="../AppImages/Underconstruction.png" /></td>
    </tr>
 </table>
 </div>
 </asp:Content>