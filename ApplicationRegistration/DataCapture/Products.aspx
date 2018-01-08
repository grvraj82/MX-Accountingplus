<%@ Page Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ApplicationRegistration.DataCapture.Products" %>
<asp:Content ID="ProductList" ContentPlaceHolderID="PageContentHolder" runat="server">
<script language="javascript" type="text/javascript">
    function SetGridWidth()
    {
        document.getElementById("PanelProductDetails").style.width = screen.width-110;
        document.getElementById("PanelProductDetails").style.height = screen.height-410;
        
    }
    function isProductSelected()
    {
        thisForm = document.forms[0];
        var products = thisForm.PRDCT_ID.length;
	    selectedCount = 0;
	    if(products > 0)
	    {
	        for(var item = 0 ; item < products ; item++)
	        {
	            //alert(item)
	            if(thisForm.PRDCT_ID[item].checked)
	            {
	                selectedCount++
	                return true;
	            }
	        }
	    }
	    else
	    {
	        if(thisForm.PRDCT_ID.checked)
	        {
	            selectedCount++
	            return true;
	        }
	    }
	     
	    //alert(selectedCount)
	    if(selectedCount == 0)
	    {
            alert("Please select the Product");
            return false;
        }
    }
    
    function confirmProductDeletion()
    {
        if(isProductSelected())
        {
            if(!confirm("Do you want to delete the selected product?"))
            {
                return false;
            }
        }
        else
        {
             return false;
        }
    }
   
    
    function SetAsSeletedProduct(productId, hiddenFieldProductId)
    {
        productIdField = eval("document.forms[0]." + hiddenFieldProductId)
        productIdField.value = productId;
        document.forms[0].submit();
    }
    </script>

      <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="height:30px;white-space:nowrap" valign="middle">
                <asp:Panel ID="PanelControls" runat="server">
                <asp:Label ID="LabelProducts" Visible="false" runat="server" Text="Products" Font-Bold="True" Font-Size="12pt"></asp:Label>&nbsp;
                <asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/AppImages/Add.jpg" CausesValidation="False" OnClick="ImageButtonAdd_Click"/>
                <asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg" CausesValidation="False" OnClick="ImageButtonEdit_Click" OnClientClick="return isProductSelected()" />
                <asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg"
                    OnClick="ImageButtonDelete_Click" OnClientClick="return confirmProductDeletion()" />
                <asp:ImageButton ID="ImageButtonXLS" runat="server" CausesValidation="False" ImageUrl="~/AppImages/xls.gif"
                    OnClick="ImageButtonXLS_Click" />&nbsp; &nbsp; &nbsp;<br />
                <asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label>
                <asp:HiddenField ID="HiddenFieldProductId" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        
        <tr>
            <td>
               <div style="overflow:scroll;width:895px;height:100px" id="PanelProductDetails">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:Table ID="TableProducts" runat="server" CellSpacing="1" HorizontalAlign="left" width="100%" CellPadding="3" GridLines="Both" BorderWidth="0" BackColor="Silver">
                                <asp:TableHeaderRow ID="TableHeaderRow1" runat="server" CssClass="tableHeaderRow">
                                <asp:TableHeaderCell ID="TableHeaderCell1" runat="server"></asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell3" runat="server" Wrap="false" HorizontalAlign="Center">Product Name</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell4" runat="server" Wrap="false" HorizontalAlign="Center">ID</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell5" runat="server" Wrap="false" HorizontalAlign="Center">Logo</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell6" runat="server" Wrap="false" HorizontalAlign="Center">Version</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell7" runat="server" Wrap="false" HorizontalAlign="Center">Build</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell8" runat="server" Wrap="false" HorizontalAlign="Center">Family</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell9" runat="server" Wrap="false" HorizontalAlign="Center">Company</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell10" runat="server" Wrap="false" HorizontalAlign="Center">Registration Access ID</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell11" runat="server" Wrap="false" HorizontalAlign="Center">Registration Access Password</asp:TableHeaderCell>
                                <asp:TableHeaderCell ID="TableHeaderCell12" runat="server" Wrap="false" HorizontalAlign="Center">Registration Allowed</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                        </td>
                    </tr>
                  </table>
                </div>
              </td>
           </tr>
       </table>
</asp:Content>