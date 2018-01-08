<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster/Header.Master" AutoEventWireup="true"
    CodeBehind="Redistributor.aspx.cs" Inherits="ApplicationRegistration.DataCapture.Redistributor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContentHolder" runat="server">
    <script language="javascript" type="text/javascript">

        function SetGridWidth() {
            document.getElementById("PanelUserDetails").style.width = screen.width - 110;
            document.getElementById("PanelUserDetails").style.height = screen.height - 410;
        }

        function isUserSelected() {
            try {
                var thisForm = document.forms[0];
                var users = thisForm.REDIST_ID.length;
                var selectedCount = 0;

                if (users > 0) {
                    for (var item = 0; item < users; item++) {
                        if (thisForm.REDIST_ID[item].checked) {
                            selectedCount++
                            return true;
                        }
                    }
                }
                else {
                    if (thisForm.REDIST_ID.checked) {
                        selectedCount++
                        return true;
                    }
                }

                if (selectedCount == 0) {
                    alert("Please select the Redistributor");
                    return false;
                }

            }
            catch (Error) {
                alert("Please select the Redistributor");
                return false;
            }
        }

        function UpdateUserDetails() {
            if (isUserSelected()) {
                if (GetSeletedCount() > 1) {
                    for (var i = 0; i < document.getElementById('aspnetForm').elements.length; i++) {
                        document.getElementById('aspnetForm').elements[i].checked = false;
                    }
                    alert("Please select only one Redistributor");
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function GetSeletedCount() {
            var thisForm = document.forms[0];
            var users = thisForm.REDIST_ID.length;
            var selectedCount = 0;
            if (users > 0) {
                for (var item = 0; item < users; item++) {
                    if (thisForm.REDIST_ID[item].checked) {
                        selectedCount++
                    }
                }
            }
            else {
                if (thisForm.REDIST_ID.checked) {
                    selectedCount++
                }
            }
            return selectedCount;

        }

        function confirmUserDeletion() {
            if (isUserSelected()) {
                if (!confirm("Do you want to delete the selected Redistributor? ")) {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        
    </script>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="height: 30px" valign="middle">
                <asp:Label ID="LabelUsers" runat="server" Visible="false" Text="Users" Font-Bold="True"
                    Font-Size="12pt"></asp:Label>&nbsp;
                <asp:ImageButton ID="ImageButtonAdd" runat="server" ImageUrl="~/AppImages/Add.jpg"
                    CausesValidation="False" onclick="ImageButtonAdd_Click" />&nbsp;
                <asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="~/AppImages/Edit.jpg"
                    CausesValidation="False"  
                    onclick="ImageButtonEdit_Click" />
                <asp:ImageButton ID="ImageButtonDelete" runat="server" CausesValidation="False" ImageUrl="~/AppImages/Delete.jpg"
                    OnClientClick="return confirmUserDeletion()" 
                    onclick="ImageButtonDelete_Click" />
                <asp:ImageButton ID="ImageButtonAssignUser" runat="server" CausesValidation="False"
                    ImageUrl="~/AppImages/Assign User.jpg" onclick="ImageButtonAssignUser_Click" />
                <asp:ImageButton ID="ImageButtonAssignProducts" runat="server" CausesValidation="False"
                    ImageUrl="~/AppImages/Assign Product.jpg" 
                    onclick="ImageButtonAssignProducts_Click"  />
                <asp:ImageButton ID="ImageButtonAssignLimits" runat="server" CausesValidation="False"
                    ImageUrl="~/AppImages/Assign Limits.jpg" onclick="ImageButtonAssignLimits_Click" 
                    style="height: 21px" />
                <br />
                <asp:Label ID="LabelActionMessage" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: scroll; width: 895px; width: 100px" id="PanelUserDetails">
                    <asp:Table ID="TableRedist" runat="server" CellSpacing="1" CellPadding="4" GridLines="Both"
                        HorizontalAlign="center" Width="100%" BorderWidth="0" BackColor="Silver">
                        <asp:TableHeaderRow ID="TableHeaderRow1" runat="server" CssClass="tableHeaderRow">
                            <asp:TableHeaderCell Width="20px" ID="TableHeaderCell1" runat="server"></asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell3" runat="server" Wrap="False" HorizontalAlign="Center">ID</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell4" runat="server" Wrap="False" HorizontalAlign="Center">Name</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell5" runat="server" Wrap="False" HorizontalAlign="Center">Address Line1</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell6" runat="server" Wrap="False" HorizontalAlign="Center">Address Line2</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell7" runat="server" Wrap="False" HorizontalAlign="Center">City</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell8" runat="server" Wrap="False" HorizontalAlign="Center">State</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell9" runat="server" Wrap="False" HorizontalAlign="Center">Country</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell10" runat="server" Wrap="False" HorizontalAlign="Center">Postal Code</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell11" runat="server" Wrap="False" HorizontalAlign="Center">Telephone Number</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell12" runat="server" Wrap="False" HorizontalAlign="Center">Email</asp:TableHeaderCell>
                            <asp:TableHeaderCell ID="TableHeaderCell2" runat="server" Wrap="False" HorizontalAlign="Center">Is Access Enabled?</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
