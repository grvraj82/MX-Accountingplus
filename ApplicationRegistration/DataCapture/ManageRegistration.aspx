<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="ManageRegistration.aspx.cs" Inherits="ApplicationRegistration.DataCapture.ManageRegistration" %>
    <asp:Content ID="ManageReg" runat="server" ContentPlaceHolderID="PageContentHolder">
    <script language="javascript" type="text/javascript">
    
    function PrintRegisrtionDetails(id, value, productID)
    {
        targetPage = "../Views/PrintRegistrationDetails.aspx?" + id + "=" + value + "&pid=" +productID;
        window.open(targetPage, "RegistrationDetails", "toolbar=no, addressbar=no, statusbar=no, width=600");
        return false
    }
 
 
    function ConfirmReset()
    {
        if(confirm("The Form data will reset to default data. Do you want to continue?"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    function GetStates(countryID, stateOrOthersID, stateOthersID, selectedStateID)
    {
        
        try
        
        {
            var objCountry = eval("document.forms[0]." + countryID);
            var objSelectedState = eval("document.forms[0]." + selectedStateID);
            var selectedIndex = objCountry.selectedIndex;
            //alert(selectedIndex);
            var states = countries[selectedIndex];
            var statesArray = states.split("!");
            var objStateOrOthers = eval("document.forms[0]." + stateOrOthersID);
            
            objStatesDiv = document.all.divStates;
            if(statesArray.length > 0 && states != "")
            {
                var statesHTML = "<select name='UserState' style='width:305px'>"
                statesHTML += "<option value='0'></option>"
               
                for( var state = 0; state < statesArray.length - 1 ; state++)
                {
                    var stateCodeNameArray = statesArray[state].split("^")
                    if(stateCodeNameArray.length == 2)
                    {
                        var selectedOption = "";
                        if(stateCodeNameArray[0] == objSelectedState.value)
                        {
                            selectedOption = " selected";
                        }
                        var stateOption = new Option(stateCodeNameArray[1], stateCodeNameArray[0])
                         statesHTML += "<option value='" + stateCodeNameArray[0] + "'" + selectedOption + ">" + stateCodeNameArray[1] + "</option>";
                    }
                }
                statesHTML += "</select>"
                objStatesDiv.innerHTML = statesHTML;
                objStateOrOthers.value = "StateDropdownList";
            }
            else
            {
                var objStateOther = eval("document.forms[0]." + stateOthersID);
                
                objStatesDiv.innerHTML = "<input type='textbox' name='UserState' value='"+ objStateOther.value + "' style='width:300px' />";
                
                objStateOrOthers.value = "StateTextBox";
            }
        }
        catch(er){}
        
    }

    function ValidateInputData()
    {
        var baseControlID = "ctl00_PageContentHolder_";
        var objComputerCode = GetControl(baseControlID, "TextBoxClientCode");
        var objSerialKey = GetControl(baseControlID, "TextBoxSerialKey");
        var objFirstName = GetControl(baseControlID, "TextBoxFirstName");
        var objLastName = GetControl(baseControlID, "TextBoxLastName");
        var objEmail = GetControl(baseControlID, "TextBoxEmail");
        var objAddressLine1 = GetControl(baseControlID, "TextBoxAddress1");
        
        var objCountry = GetControl(baseControlID, "DropDownListCountry");
        var objState = GetControl("", "UserState");
        var objCity = GetControl(baseControlID, "TextBoxCity");
        var objPostalCode = GetControl(baseControlID, "TextBoxZipCode");
        var objSharpModel = GetControl(baseControlID, "DropDownListMFPModel");
        var objCompanyName = GetControl(baseControlID, "TextBoxCompanyName");
        
        if(Trim(objComputerCode.value) == "")
        {
            alert("Please Enter value for Computer Code");
            objComputerCode.focus();
            return false;
        }
        if(Trim(objSerialKey.value) == "")
        {
            alert("Please Enter value for Serial Number");
            objSerialKey.focus();
            return false;
        }
        if(Trim(objFirstName.value) == "")
        {
            alert("Please Enter value for First Name");
            objFirstName.focus();
            return false;
        }
        if(Trim(objLastName.value) == "")
        {
            alert("Please Enter value for Last Name");
            objLastName.focus();
            return false;
        }
        
        if(Trim(objEmail.value) == "")
        {
            alert("Please Enter value for Email");
            objEmail.focus();
            return false;
        }
        else
        {
           if(!IsValidEmail(Trim(objEmail.value)))
           {
                alert("Please enter valid Email");
                objEmail.focus();
                return false;
           }
        }
       
        if(Trim(objAddressLine1.value) == "")
        {
            alert("Please Enter value for Address Line1");
            objAddressLine1.focus();
            return false;
        }
        
        if(Trim(objCountry[objCountry.selectedIndex].value) == "-1")
        {
            alert("Please select the Country");
            objCountry.focus();
            return false;
        }
        if(Trim(objState.value) == "" || Trim(objState.value) == "0" )
        {
            alert("Please enter the State");
            objState.focus();
            return false;
        }
        
        if(Trim(objCity.value) == "")
        {
            alert("Please Enter value for City");
            objCity.focus();
            return false;
        }
        
        if(Trim(objPostalCode.value) == "")
        {
            alert("Please Enter value for Postal Code");
            objPostalCode.focus();
            return false;
        }

        if(Trim(objSharpModel[objSharpModel.selectedIndex].value) == "-1")
        {
            alert("Please select the Sharp MFP Model");
            objSharpModel.focus();
            return false;
        }
        if(Trim(objCompanyName.value) == "")
        {
            alert("Please Enter value for Company Name");
            objCompanyName.focus();
            return false;
        } 
        // Check wether the processor count is between 1-9
        
        objProcessorCount = GetControl(baseControlID, "TextBoxProcessorCount");
        
        if(Trim(objProcessorCount.value) != "")
        {
            try
            {
               ProcessorCount = parseInt(Trim(objProcessorCount.value));
               if(ProcessorCount == 0)
               {
                    alert("Please Enter valid data for Processor Count [1-9]");
                    objProcessorCount.focus();
                    return false;
               }
               if(ProcessorCount > 9)
               {
                    alert("Please Enter valid data for Processor Count [1-9]");
                    objProcessorCount.focus();
                    return false;
               }
            }
            catch(er)
            {
                alert("Please Enter valid data for Processor Count [1-9]");
                objProcessorCount.focus();
                return false;

            }
        } 
        //alert(objState.value);
        //return false;
        return true;
        
    }
    
    function Trim(stringToTrim) 
    {
	    return stringToTrim.replace(/^\s+|\s+$/g,"");
    }
    
    function Ltrim(stringToTrim) 
    {
	    return stringToTrim.replace(/^\s+/,"");
    }
    
    function Rtrim(stringToTrim) 
    {
	    return stringToTrim.replace(/\s+$/,"");
    }

    function GetControl(baseControlID, controlID)
    {
        return eval("document.forms[0]." + baseControlID + controlID)
    }
        
    function IsValidEmail(emailAddress) 
    {
     var emailReg = "^[\\w-_\.]*[\\w-_\.]\@[\\w]\.+[\\w]+[\\w]$";
     var regex = new RegExp(emailReg);
     return regex.test(emailAddress);
  }
    function ResetValues()
    {
        if(confirm("The Form data will reset to default data. Do you want to continue?"))
        {
            document.forms[0].reset();
            GetStates('ctl00_PageContentHolder_DropDownListCountry', 'ctl00_PageContentHolder_HiddenFieldStateSource', 'ctl00_PageContentHolder_HiddenFieldStateOthers', 'ctl00_PageContentHolder_HiddenFieldState')
            return true;
        }
        else
        {
            
            return false;
        }
    }
    
    function AdjustHeight()
    {
        GetStates('ctl00_PageContentHolder_DropDownListCountry', 'ctl00_PageContentHolder_HiddenFieldStateSource', 'ctl00_PageContentHolder_HiddenFieldStateOthers', 'ctl00_PageContentHolder_HiddenFieldState');
    }
 
    </script>
        <asp:Label ID="LabelScript" runat="server" Text="Label"></asp:Label>
    <br />
    
    <table border="0" cellpadding="0" cellspacing="0" bgcolor="silver" align="center">
    <tr>
        <td align="left" bgcolor="white">
            <fieldset>
            <legend><asp:Label ID="LabelGroupTitle" CssClass="f11b" runat="server" Text="Product Registration"></asp:Label>&nbsp;</legend>
            
            <asp:Panel ID="PanelRegistrationForm" runat="server">
            <asp:Table ID="TableManageRegistration" CellSpacing="0" CellPadding="3" runat="server" HorizontalAlign="Center" CssClass="f10">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="f12b">
                    <asp:Label ID="LabelActionMessage" runat="server" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:HiddenField ID="HiddenRecordId" runat="server" />
                    <asp:HiddenField ID="HiddenProductId" runat="server" />
                    <asp:Label ID="LabelRequiredFields" runat="server" Text=""></asp:Label></asp:TableCell>
            </asp:TableRow>
                       
            <asp:TableRow>
                <asp:TableCell>
                    Registration Type *
                </asp:TableCell>      
                <asp:TableCell>
                    <asp:DropDownList ID="DropDownListRegistrationType" runat="server" Width="305">
                    </asp:DropDownList>
                </asp:TableCell>         
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    Device Code *
                </asp:TableCell>      
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxClientCode" runat="server" MaxLength="50" Width="300"></asp:TextBox>
                </asp:TableCell>         
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    Serial Number *
                </asp:TableCell>      
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxSerialKey" runat="server" MaxLength="50" Width="300"></asp:TextBox>
                </asp:TableCell>         
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    Activation Code
                </asp:TableCell>      
                <asp:TableCell>
                    <asp:Label ID="LabelActivationCodeDisplay" runat="server" CssClass="f11b"></asp:Label>
                </asp:TableCell>         
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" BackColor="silver" CssClass="f12b">User Information</asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>First Name *</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxFirstName" runat="server" MaxLength="50" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Last Name *</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxLastName" runat="server" MaxLength="50" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Email *</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxEmail" runat="server" MaxLength="50" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Address Line 1 *</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxAddress1" runat="server" MaxLength="250" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Address Line 2</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxAddress2" runat="server" MaxLength="250" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Country *</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="DropDownListCountry" runat="server" Width="305"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>State *</asp:TableCell>
                <asp:TableCell><div id="divStates"></div>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>City *</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxCity" runat="server" MaxLength="50" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Postal Code *</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxZipCode" runat="server" MaxLength="10" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell Wrap="false">Telephone Number</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxPhone" runat="server" MaxLength="15" Width="125"></asp:TextBox>
                &nbsp;Extension <asp:TextBox ID="TextBoxExtension" runat="server" MaxLength="8" Width="102"></asp:TextBox>
                
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                   Fax
                </asp:TableCell>      
                <asp:TableCell>
                    <asp:TextBox ID="TextBoxFax" runat="server" MaxLength="15" Width="300"></asp:TextBox>
                </asp:TableCell>         
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" BackColor="silver" CssClass="f12b">Purchase Information</asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Dealer/Store Name</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxDealerName" MaxLength="50" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Sharp MFP Model *</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="DropDownListMFPModel" runat="server" Width="305"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" BackColor="silver" CssClass="f12b">Company Information</asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Company Name *</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxCompanyName" MaxLength="100" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Job Title</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="DropDownListJobFunction" runat="server" Width="305"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Department</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="DropDownListDepartment" runat="server" Width="305"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Organization</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="DropDownListIndustryType" runat="server" Width="305"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Type Of Business</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="DropDownListOrganizationType" runat="server" Width="305"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            
             <asp:TableRow>
                <asp:TableCell ColumnSpan="2" BackColor="silver" CssClass="f12b">System Information</asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Computer Name</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxComputerName" MaxLength="50" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
           
            <asp:TableRow>
                <asp:TableCell>IP Address</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxIPAddress" MaxLength="50" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Harddisk Id</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxHardDiskId" MaxLength="50" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Mac Address</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxMACAddress" MaxLength="50" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Processor Type</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxProcessorType" MaxLength="30" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Number of Processors</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxProcessorCount" MaxLength="1" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>Operating System</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxOperatingSystem" MaxLength="50" runat="server" Width="300"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
              <asp:TableRow>
                <asp:TableCell ColumnSpan="2" BackColor="silver" CssClass="f12b">Notification</asp:TableCell>
            </asp:TableRow>
             <asp:TableRow>
                <asp:TableCell ColumnSpan="2"><asp:CheckBox ID="CheckBoxNotifications" runat="server" />&nbsp;Send notifications via e-mail regarding future releases, updates and patches.</asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                    <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" Text="Update" Width="85px" />
                    <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="Add" Width="85px" />&nbsp;
                    <asp:Button ID="ButtonReset" runat="server" OnClick="ButtonReset_Click" Text="Reset" Width="85px" Visible="false"/>
                    <input type="button" value="Reset" style="width:85px" OnClick="javascript:ResetValues()"/>&nbsp;
                    <asp:Button ID="ButtonCancel" runat="server" CausesValidation="false" OnClick="ButtonCancel_Click" Text="Cancel" Width="85px" />
                    
                </asp:TableCell>
            </asp:TableRow>
            </asp:Table>
                </asp:Panel>
            <asp:Panel ID="PanelRegistrationResult"  Visible="false" runat="server">
            <asp:Table ID="TableRegistrationResult" Width="100%" CellSpacing="3" CellPadding="3" runat="server">
                <asp:TableRow>
                    <asp:TableCell CssClass="f11b" BackColor="Silver" ColumnSpan="2" HorizontalAlign="Center">Registration Details</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>Product</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelProduct" runat="server" Text=""></asp:Label></asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>Serial Number</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelSerialKey" runat="server" Text=""></asp:Label></asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>Client Code</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelClientCode" runat="server" Text=""></asp:Label></asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>Activation Code</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelActivationCode" runat="server" Text=""></asp:Label></asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>First Name</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelFirstName" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>Last Name</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelLastName" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>Email</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelEmail" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Visible="true">Phone</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelPhone" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Visible="true">Fax</asp:TableCell>
                    <asp:TableCell CssClass="f11b">
                        <asp:Label ID="LabelFax" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                        <asp:Button ID="ButtonOK" runat="server" CausesValidation="false" OnClick="ButtonOK_Click" Text="OK" Width="60px"/>&nbsp;
                        <asp:Button ID="ButtonPrint" runat="server" CausesValidation="false" Text="Print" Width="60px"/>&nbsp;
                        <asp:Button ID="ButtonNew" runat="server" CausesValidation="false" OnClick="ButtonNew_Click" Text="New" Width="60px" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            </asp:Panel>
            <asp:HiddenField ID="HiddenFieldState" runat="server" />
            <asp:HiddenField ID="HiddenFieldStateOthers" runat="server" />
            <asp:HiddenField ID="HiddenFieldStateSource" runat="server" />

            
            </fieldset>
        </td>
    </tr>
    </table>
    <br />
    <br />   
    
    </asp:Content>
