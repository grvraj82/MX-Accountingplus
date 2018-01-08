<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="ReportIndex.aspx.cs" Inherits="ApplicationRegistration.Reports.ReportIndex" %>

<asp:Content ID="ReportsIndex" ContentPlaceHolderID="PageContentHolder" runat="server">
<script language="javascript" type="text/javascript">
    function SetGridWidth()
    {
        try
        {
            document.getElementById("ctl00_PageContentHolder_PanelRegistrationDetails").style.width=screen.width-110;
        }
        catch(er){}
    }
    
    function CheckControl(controlName)
    {
        controlObject = eval("document.forms[0]." + controlName);
        controlObject.checked = true;
    }
    
    function IsValidDate(dateStr, format)
    {

        //check if 2nd parameter contains valid value or not
        //if not valid then set default format = "MDY"
        if (format == null) { format = "MDY" }
        format = format.toUpperCase();
        if (format.length != 3) { format = "MDY" }
        if ( (format.indexOf("M") == -1) ||
        (format.indexOf("D") == -1) ||
        (format.indexOf("Y") == -1)
        )
        { format = "MDY" }

        if (format.substring(0, 1) == "Y")
        { // If the year is first
        var reg1 = /^\d{2}(\-|\/|\.)\d{1,2}\1\d{1,2}$/
        var reg2 = /^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$/
        }
        else if (format.substring(1, 2) == "Y")
        { // If the year is second
        var reg1 = /^\d{1,2}(\-|\/|\.)\d{2}\1\d{1,2}$/
        var reg2 = /^\d{1,2}(\-|\/|\.)\d{4}\1\d{1,2}$/
        }
        else
        { // The year must be third
        var reg1 = /^\d{1,2}(\-|\/|\.)\d{1,2}\1\d{2}$/
        var reg2 = /^\d{1,2}(\-|\/|\.)\d{1,2}\1\d{4}$/
        }

        // If it doesn’t conform to the right format
        //(with either a 2 digit year or 4 digit year), fail
        if ( (reg1.test(dateStr) == false) &&
        (reg2.test(dateStr) == false)
        )
        { return false; }

        // Split into 3 parts based on what the divider was
        var parts = dateStr.split(RegExp.$1);

        // Check to see if the 3 parts end up making a valid date
        //extract month part
        if (format.substring(0, 1) == "M") { var mm = parts[0]; }
        else if (format.substring(1, 2) == "M") { var mm = parts[1]; }
        else { var mm = parts[2]; }

        //extract day part
        if (format.substring(0, 1) == "D") { var dd = parts[0]; }
        else if (format.substring(1, 2) == "D") { var dd = parts[1]; }
        else { var dd = parts[2]; }

        //extract Year part
        if (format.substring(0, 1) == "Y") { var yy = parts[0]; }
        else if (format.substring(1, 2) == "Y") { var yy = parts[1]; }
        else { var yy = parts[2]; }

        //if year is in 2 digit
        //00-49 are assumed to be 21st century and 50-99 are assumed to be 20th century
        if (parseFloat(yy) <= 50)
        { yy = (parseFloat(yy) + 2000).toString(); }
        if (parseFloat(yy) <= 99) { yy = (parseFloat(yy) + 1900).toString(); }

        var dt = new Date(parseFloat(yy), parseFloat(mm)-1, parseFloat(dd), 0, 0, 0, 0);
        if (parseFloat(dd) != dt.getDate()) { return false; }
        if (parseFloat(mm)-1 != dt.getMonth()) { return false; }
        return true;

     }

     function DataValidation()
     {
        if(!IsValidDate(document.forms[0], "MDY"))
        {
            alert("Please enter valid From Date.");
            return false;
        }
        
        if(!IsValidDate(document.forms[0], "MDY"))
        {
            alert("Please enter valid From Date.");
            return false;
        }
     }

</script>
<br />
<asp:Panel ID="PanelReport" runat="server">
<table border="0" cellpadding="3" cellspacing="0" align="center" >
<tr>
    <td>
    <fieldset>
    <legend><asp:Label ID="LabelGroupTitle" runat="server" CssClass="f11b" Text="Product Registration Reporting"></asp:Label>&nbsp;</legend>
    
       
        <table  align="center" border="0" cellpadding="3" cellspacing="0" >
            <tr>
                <td colspan="3" align="center">&nbsp;<asp:Label ID="LabelActionMessage" runat="server" CssClass="f12b" ForeColor="Blue" Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" class="f12b">
                    <span style="color: #3300ff">Filter Criteria</span></td>
            </tr>
            
            
            <tr><td colspan="3" class="f11b" style="white-space:nowrap"><asp:RadioButton ID="RadioButtonDateRange" runat="server" GroupName="RadioButtonReportOn" Checked="true" CssClass="f11b" Text="Registration Date" />
                From&nbsp;<asp:TextBox ID="TextBoxDateFrom" runat="server" Width="110px" MaxLength="10"></asp:TextBox><asp:Image ID="ImageDateFrom" runat="server" ImageUrl="~/AppImages/caledar.gif" Height="17px" />
                    <asp:RegularExpressionValidator ID="regExpDateFrom" runat="server" ControlToValidate="TextBoxDateFrom"
                        Display="None" ErrorMessage="Please enter valid From Date in mm/dd/yyyy format"
                        SetFocusOnError="True" ValidationExpression="(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/](19|20)\d\d" Visible="False"></asp:RegularExpressionValidator>&nbsp;To&nbsp;<asp:TextBox ID="TextBoxDateTo" runat="server" Width="110px" MaxLength="10"></asp:TextBox><asp:Image ID="ImageDateTo" runat="server" ImageUrl="~/AppImages/caledar.gif" Height="17px" /><asp:RegularExpressionValidator ID="regExpDateTo" runat="server" ControlToValidate="TextBoxDateTo"
                        Display="None" ErrorMessage="Please enter valid To Date in mm/dd/yyyy format"
                        SetFocusOnError="True" ValidationExpression="(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/](19|20)\d\d"></asp:RegularExpressionValidator></td>
              
            </tr>
            
            <tr>
                <td valign="bottom" class="f11b" colspan="3"><asp:RadioButton ID="RadioButtonFilterOn" runat="server" GroupName="RadioButtonReportOn" CssClass="f11b" /><asp:Label ID="LabelDataColumn" runat="server" CssClass="f11b" Text="Data Column"></asp:Label>
                    <asp:DropDownList ID="DropDownListFilterOn" runat="server" CssClass="f11b">
                    </asp:DropDownList>&nbsp;
                    <asp:TextBox ID="TextBoxFilterValue" runat="server" Width="200px" MaxLength="250"></asp:TextBox></td>

            </tr>
            <tr>
                <td colspan="3" class="f12b" style="height: 20px"></td>
            </tr>
            
            <tr>
                <td colspan="3" class="f12b" style="height: 20px">
                    <span style="color: #3333ff">Report Format</span></td>
            </tr>
            
            <tr>
                <td colspan="3">
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButtonOnscreen" Checked="True" runat="server" CssClass="f11b" GroupName="ReportFormat"
                        Text="On Screen" />&nbsp; [Filter criteria resulting &lt;= 
                    <asp:Label ID="LabelMaxDisplayCount" runat="server" CssClass="f11b"></asp:Label>]&nbsp;
                    <asp:ImageButton
                        ID="ImageButtonPrint" runat="server" ImageUrl="~/AppImages/ColorPrinter.gif" OnClick="ImageButtonPrint_Click" Visible="False" />
                    <asp:HiddenField ID="HiddenPrintPreview" runat="server" Value="yes" />
                    <input type="hidden" name="HiddenPrintPriview" value="yes" />
                </td>
            </tr>
            
            <tr>
                <td colspan="3">
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButtonCSV" runat="server" CssClass="f11b" GroupName="ReportFormat"
                        Text="CSV File" />&nbsp; [Filter criteria resulting &lt;=
                    <asp:Label ID="LabelMaxCsvDisplayCount" runat="server" CssClass="f11b"></asp:Label>]
                    &nbsp;&nbsp;<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Label ID="LabelQueryResult" runat="server" Text="" CssClass="f11b" ForeColor="green"></asp:Label></td>
            </tr>
            
            <tr>
                <td colspan="3" class="f11b">
                    </td>
            </tr>
            
            <tr>
                <td align="center" colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                    <asp:Button ID="ButtonGenerateReport" runat="server" Text="Generate Report" OnClick="ButtonGenerateReport_Click" />&nbsp;
                </td>
            </tr>

        </table>
    </fieldset>
    </td>
</tr>
</table> 
</asp:Panel>
<table align="center">
    <tr>
        <td>&nbsp;&nbsp;<asp:Label ID="LabelResultCount" runat="server" CssClass="f12b" ForeColor="Blue"></asp:Label></td>
    </tr>
    <tr>
    <td>
        <asp:Panel ID="PanelSerialKeyDetails" runat="server" Visible="false">
        <br />
        <fieldset>
        <legend class="f11b">Serial Number Information&nbsp</legend>
        <table width="400">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="f11b">Serial Number</td>
                            <td>
                                <asp:Label ID="LabelSerialKey" CssClass="f11b" runat="server" Text=""></asp:Label></td>
                        </tr>
                        
                        <tr>
                            <td class="f11b">Total Licenses</td>
                            <td>
                                <asp:Label ID="LabelTotalLicenses" CssClass="f11b" runat="server" Text=""></asp:Label></td>
                        </tr>
                        
                        <tr>
                            <td class="f11b">Used Licenses</td>
                            <td>
                                <asp:Label ID="LabelUsedLicenses" CssClass="f11b" runat="server" Text=""></asp:Label></td>
                        </tr>
                        
                        <tr>
                            <td class="f11b">Remaining Licenses</td>
                            <td>
                                <asp:Label ID="LabelRemainingLicenses" CssClass="f11b" runat="server" Text=""></asp:Label></td>
                        </tr>
                        
                    </table>
                 </td>
            </tr>
        </table>
     </fieldset>
    <br />
    </asp:Panel>
    </td>
</tr>
<tr>
    <td>
         <asp:Panel ID="PanelRegistrationDetails" runat="server" Visible="false">
            <asp:Table ID="TableRegistrationDetails" runat="server" CellSpacing="1" CellPadding="4" GridLines="Both" HorizontalAlign="center"  Width="100%" BorderWidth="0" BackColor="Silver">
            </asp:Table>
         </asp:Panel>
    </td>
</tr>
</table>
<br />
</asp:Content>