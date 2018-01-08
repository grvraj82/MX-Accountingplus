<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AppMaster/Header.Master" CodeBehind="ManageEmailContent.aspx.cs" Inherits="ApplicationRegistration.AppConfiguration.ManageEmailContent" %>
<asp:Content ID="ManageEmailContentId" ContentPlaceHolderID="PageContentHolder" runat="server">
<script language="javascript" type="text/javascript" src="../AppClientScripts/Editor.js"></script>
<script language="javascript" type="text/javascript">

function AssignEmailContentTo(controlID)
{
    contentContol = eval("document.forms[0]." + controlID);
    contentContol.value = divEmailContent.innerHTML;
}

function test(title)
{
    
    var sText = document.selection.createRange();
//    sText.innerHTML = "<b>Raj</b>"
//    sText.execCommand("InsertInputButton", false, "value='raj'")
    //document.execCommand("inserthtml", false , "<input type='textbox'>")
    
    var oNode=document.createElement("span");
    oNode.unselectable = "off";
    oNode.contentEditable="false";
    oNode.style.background = "green";
    divEmailContent.insertBefore(oNode);
    
    oNode.innerText='<Field:'+ title +'>';
    
    //oNode.style.position='absolute';
    
    <!-- Set the new text as the active element for editing. -->
    oNode.setActive()
    
}


</script>
<br />

<table align="center">
    <tr>
        <td>
            <fieldset>
                <legend><asp:Label ID="LabelGroupTitle" runat="server" Text="Activation Email Configuration" CssClass="f11b"></asp:Label></legend>
                <table align="center">
                    <tr>
                        <td valign="top" style="background-color:Silver">
                          <table>
                            <tr>
                                <td class="f12b" align="center" style="white-space:nowrap">
                                    Configurable E-mail fields
                                </td>
                            </tr>
                          </table>
                          <span contentEditable="true">
                            <asp:Table ID="TableFields" runat="server">
                            </asp:Table>
                          </span>
                        </td>
                        <td valign="top" align="left">
                        <table cellpadding="3" cellspacing="0" border="0" align="center">
                            <tr>
                                <td colspan="2">
                                    &nbsp;&nbsp;<asp:Label ID="LabelActionMessage" CssClass="f11b" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">&nbsp;<asp:Label ID="LabelRequiredFields" runat="server" CssClass="f11"></asp:Label></td>
                            </tr>
                                                
                            <tr>
                                <td style="white-space:nowrap ">
                                    <asp:Label ID="LabelFrom" runat="server" Text="From Address"></asp:Label>
                                    *</td>
                                <td>
                                    <asp:TextBox ID="TextBoxFromAddress" Width="350" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFrom" runat="server" ControlToValidate="TextBoxFromAddress"
                                        Display="None" ErrorMessage="From: Please enter valid Email ID " SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regExpEmailFromAddress" runat="server" ControlToValidate="TextBoxFromAddress"
                                        Display="None" ErrorMessage="From Address : Invalid Email ID" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:Label ID="LabelCC" runat="server" Text="CC Address"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="TextBoxCCAddress" Width="350" runat="server"></asp:TextBox></td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:Label ID="LabelBCC" runat="server" Text="BCC Address"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="TextBoxBCCAddress" Width="350" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="white-space:nowrap ">
                                    <asp:Label ID="LabelSubject" runat="server" Text="Subject"></asp:Label>
                                    *</td>
                                <td>
                                    <asp:TextBox ID="TextBoxSubject" Width="350" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxSubject"
                                        Display="None" ErrorMessage="Subject : Please enter value " SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td rowspan="2" valign="top"><asp:Label ID="LabelEmailContent" runat="server" Text="Content"></asp:Label></td>
                                <td>        
                                    <table border="0" bgcolor="#d4d0c8" align="center">
				                        <tr>
					                        <td style="white-space:nowrap" valign="top">
					                        
					                        &nbsp;<a href="javascript:perform('fontcolor','')"><span id='sfClr' style="WIDTH:10px;HEIGHT:5px" valign="top" onClick="javascript:perform('fontcolor','');">
								                        <img border="0" src="../AppImages/EditorImages/fontcolor.jpg" alt="font Color" onmouseOver="javascript:forms(0).colorFor.value='FontColor'" /></span></a>
					                        &nbsp;<a href="javascript:perform('Bold','')"><img border="0" src="../AppImages/EditorImages/bold.jpg" alt="Bold" /></a>
					                        &nbsp;<a href="javascript:perform('Italic','')"><img border="0" src="../AppImages/EditorImages/italic.jpg" alt="Italic" /></a>
					                        &nbsp;<a href="javascript:perform('UnderLine','')"><img border="0" src="../AppImages/EditorImages/underline.jpg" alt="UnderLine" /></a>
					                        &nbsp;<a href="javascript:perform('InsImage','')"><img border="0" src="../AppImages/EditorImages/img.jpg" alt="Insert Image" height="22" width="22" /></a>
					                        &nbsp;<a href="javascript:perform('Anchor','')"><img border="0" src="../AppImages/EditorImages/link.jpg" alt="Anchor" height="22" width="22" /></a>
					                        &nbsp;<a href="javascript:perform('alignLeft','')"><img border="0" src="../AppImages/EditorImages/alignLeft.jpg" alt="Align Left" /></a>
					                        &nbsp;<a href="javascript:perform('alignCenter','')"><img border="0" src="../AppImages/EditorImages/alignCenter.jpg" alt="Align Center" /></a>
					                        <!--
					                        <td style="width: 31px"><a href="javascript:perform('Paste','')"><img border="0" src="../AppImages/EditorImages/paste.gif" alt="Paste" /></a>
					                        -->
					                        &nbsp;<a href="javascript:perform('alignRight','')"><img border="0" src="../AppImages/EditorImages/alignRight.jpg" alt="Align Right" /></a>
					                        &nbsp;<a href="javascript:perform('Justifyfull','')"><img border="0" src="../AppImages/EditorImages/justifyall.gif" alt="JUSTIFY FULL" /></a>
					                        &nbsp;<a href="javascript:perform('unorder','')"><img border="0" src="../AppImages/EditorImages/uolist.jpg" alt="Unorder List" /></a>
					                        &nbsp;<a href="javascript:perform('order','')"><img border="0" src="../AppImages/EditorImages/olist.jpg" alt="Order List" /></a>
					                        &nbsp;<a href="javascript:perform('Outdent','')"><img border="0" src="../AppImages/EditorImages/lindent.gif" alt="Move object Left side" /></a>
					                        &nbsp;<a href="javascript:perform('Indent','')"><img border="0" src="../AppImages/EditorImages/rindent.gif" alt="Move object Right side" /></a>
					                        <!--
					                        <td><a href="javascript:perform('Cut','')"><img border="0" src="../AppImages/EditorImages/cut.gif" alt="Cut" /></a></td>
					                        <td><a href="javascript:perform('Copy','')"><img border="0" src="../AppImages/EditorImages/copy.gif" alt="Copy" /></a></td>
				                            -->
				                            <br />
						                        <select name="fontfmly" onChange="perform('fontFmly','')" style="FONT-SIZE:10px;COLOR:blue;FONT-FAMILY:verdana;WIDTH:160">
							                        <option value="Arial" selected>Arial</option>
							                        <option value="Book Antiqua">Book Antiqua</option>
							                        <option value="Bookman Old Style">Bookman Old Style</option>
							                        <option value="Century Gothic">Century Gothic</option>
							                        <option value="Comic Sans MS">Comic Sans MS</option>
							                        <option value="Courier New">Courier New</option>
							                        <option value="Garamond">Garamond</option>
							                        <option value="Impact">Impact</option>
							                        <option value="Lucida Console">Lucida Console</option>
							                        <option value="Marlette">Marlette</option>
							                        <option value="MS Outlook">MS Outlook</option>
							                        <option value="Symbol">Symbol</option>
							                        <option value="Tahoma">Tahoma</option>
							                        <option value="Times New Roman">Times New Roman</option>
							                        <option value="Trebuchet MS">Trebuchet MS</option>
							                        <option value="Verdana">Verdana</option>
							                        <option value="Webdings">Webdings</option>
							                        <option value="WingDings">WingDings</option>
						                        </select>
						                        <select name="fontSize" onChange="perform('fontsize','')" style="FONT-SIZE:10px;COLOR:blue;FONT-FAMILY:verdana">
							                        <option value="1" selected>8</option>
							                        <option value="2">9</option>
							                        <option value="3">10</option>
							                        <option value="4">11</option>
							                        <option value="5">12</option>
							                        <option value="6">13</option>
							                        <option value="7">14</option>
						                        </select>&nbsp;&nbsp;
						                              <input type="hidden" name="posX" style="width: 1px"> <input type="hidden" name="posY" style="width: 1px">
						                        <input type="hidden" name="colorFor" style="width: 3px"> <input type="hidden" name="Fntclr" style="width: 1px">
						                        <input type="hidden" size="12" name="Bgclr" style="width: 0px"> <input type="hidden" size="12" name="insideTable" style="width: 0px">
					                        </td>
				                        <td align="left">
		                                <div id="colorPicker" style="VISIBILITY:hidden;POSITION:absolute;cursor:hand">
			                                <img height="196" alt="" isMap src="../AppImages/EditorImages/cpicker.gif" width="292" useMap="#map_webpal" border="0">
			                                <map name="map_webpal">
				                                <area onclick="SendColor('#330000');return false" shape="RECT" coords="2,2,18,18" href="">
				                                <area onclick="SendColor('#333300');return false" shape="RECT" coords="18,2,34,18" href="javascript:{;}">
				                                <area onclick="SendColor('#336600');return false" shape="RECT" coords="34,2,50,18" href="javascript:{;}">
				                                <area onclick="SendColor('#339900');return false" shape="RECT" coords="50,2,66,18" href="javascript:{;}">
				                                <area onclick="SendColor('#33CC00');return false" shape="RECT" coords="66,2,82,18" href="javascript:{;}">
				                                <area onclick="SendColor('#33FF00');return false" shape="RECT" coords="82,2,98,18" href="javascript:{;}">
				                                <area onclick="SendColor('#66FF00');return false" shape="RECT" coords="98,2,114,18" href="javascript:{;}">
				                                <area onclick="SendColor('#66CC00');return false" shape="RECT" coords="114,2,130,18" href="javascript:{;}">
				                                <area onclick="SendColor('#669900');return false" shape="RECT" coords="130,2,146,18" href="javascript:{;}">
				                                <area onclick="SendColor('#666600');return false" shape="RECT" coords="146,2,162,18" href="javascript:{;}">
				                                <area onclick="SendColor('#663300');return false" shape="RECT" coords="162,2,178,18" href="javascript:{;}">
				                                <area onclick="SendColor('#660000');return false" shape="RECT" coords="178,2,194,18" href="javascript:{;}">
				                                <area onclick="SendColor('#FF0000');return false" shape="RECT" coords="194,2,210,18" href="javascript:{;}">
				                                <area onclick="SendColor('#FF3300');return false" shape="RECT" coords="210,2,226,18" href="javascript:{;}">
				                                <area onclick="SendColor('#FF6600');return false" shape="RECT" coords="226,2,242,18" href="javascript:{;}">
				                                <area onclick="SendColor('#FF9900');return false" shape="RECT" coords="242,2,258,18" href="javascript:{;}">
				                                <area onclick="SendColor('#FFCC00');return false" shape="RECT" coords="258,2,274,18" href="javascript:{;}">
				                                <area onclick="SendColor('#FFFF00');return false" shape="RECT" coords="274,2,290,18" href="javascript:{;}">
				                                <!--- Row 2 ---><area onclick="SendColor('#330033');return false" shape="RECT" coords="2,18,18,34" href="javascript:{;}">
				                                <area onclick="SendColor('#333333');return false" shape="RECT" coords="18,18,34,34" href="javascript:{;}">
				                                <area onclick="SendColor('#336633');return false" shape="RECT" coords="34,18,50,34" href="javascript:{;}">
				                                <area onclick="SendColor('#339933');return false" shape="RECT" coords="50,18,66,34" href="javascript:{;}">
				                                <area onclick="SendColor('#33CC33');return false" shape="RECT" coords="66,18,82,34" href="javascript:{;}">
				                                <area onclick="SendColor('#33FF33');return false" shape="RECT" coords="82,18,98,34" href="javascript:{;}">
				                                <area onclick="SendColor('#66FF33');return false" shape="RECT" coords="98,18,114,34" href="javascript:{;}">
				                                <area onclick="SendColor('#66CC33');return false" shape="RECT" coords="114,18,130,34" href="javascript:{;}">
				                                <area onclick="SendColor('#669933');return false" shape="RECT" coords="130,18,146,34" href="javascript:{;}">
				                                <area onclick="SendColor('#666633');return false" shape="RECT" coords="146,18,162,34" href="javascript:{;}">
				                                <area onclick="SendColor('#663333');return false" shape="RECT" coords="162,18,178,34" href="javascript:{;}">
				                                <area onclick="SendColor('#660033');return false" shape="RECT" coords="178,18,194,34" href="javascript:{;}">
				                                <area onclick="SendColor('#FF0033');return false" shape="RECT" coords="194,18,210,34" href="javascript:{;}">
				                                <area onclick="SendColor('#FF3333');return false" shape="RECT" coords="210,18,226,34" href="javascript:{;}">
				                                <area onclick="SendColor('#FF6633');return false" shape="RECT" coords="226,18,242,34" href="javascript:{;}">
				                                <area onclick="SendColor('#FF9933');return false" shape="RECT" coords="242,18,258,34" href="javascript:{;}">
				                                <area onclick="SendColor('#FFCC33');return false" shape="RECT" coords="258,18,274,34" href="javascript:{;}">
				                                <area onclick="SendColor('#FFFF33');return false" shape="RECT" coords="274,18,290,34" href="javascript:{;}">
				                                <!--- Row 3 --->
				                                <area onclick="SendColor('#330066');return false" shape="RECT" coords="2,34,18,50" href="javascript:{;}">
				                                <area onclick="SendColor('#333366');return false" shape="RECT" coords="18,34,34,50" href="javascript:{;}">
				                                <area onclick="SendColor('#336666');return false" shape="RECT" coords="34,34,50,50" href="javascript:{;}">
				                                <area onclick="SendColor('#339966');return false" shape="RECT" coords="50,34,66,50" href="javascript:{;}">
				                                <area onclick="SendColor('#33CC66');return false" shape="RECT" coords="66,34,82,50" href="javascript:{;}">
				                                <area onclick="SendColor('#33FF66');return false" shape="RECT" coords="82,34,98,50" href="javascript:{;}">
				                                <area onclick="SendColor('#66FF66');return false" shape="RECT" coords="98,34,114,50" href="javascript:{;}">
				                                <area onclick="SendColor('#66CC66');return false" shape="RECT" coords="114,34,130,50" href="javascript:{;}">
				                                <area onclick="SendColor('#669966');return false" shape="RECT" coords="130,34,146,50" href="javascript:{;}">
				                                <area onclick="SendColor('#666666');return false" shape="RECT" coords="146,34,162,50" href="javascript:{;}">
				                                <area onclick="SendColor('#663366');return false" shape="RECT" coords="162,34,178,50" href="javascript:{;}">
				                                <area onclick="SendColor('#660066');return false" shape="RECT" coords="178,34,194,50" href="javascript:{;}">
				                                <area onclick="SendColor('#FF0066');return false" shape="RECT" coords="194,34,210,50" href="javascript:{;}">
				                                <area onclick="SendColor('#FF3366');return false" shape="RECT" coords="210,34,226,50" href="javascript:{;}">
				                                <area onclick="SendColor('#FF6666');return false" shape="RECT" coords="226,34,242,50" href="javascript:{;}">
				                                <area onclick="SendColor('#FF9966');return false" shape="RECT" coords="242,34,258,50" href="javascript:{;}">
				                                <area onclick="SendColor('#FFCC66');return false" shape="RECT" coords="258,34,274,50" href="javascript:{;}">
				                                <area onclick="SendColor('#FFFF66');return false" shape="RECT" coords="274,34,290,50" href="javascript:{;}">
				                                <!--- Row 4 --->
				                                <area onclick="SendColor('#330099');return false" shape="RECT" coords="2,50,18,66" href="javascript:{;}">
				                                <area onclick="SendColor('#333399');return false" shape="RECT" coords="18,50,34,66" href="javascript:{;}">
				                                <area onclick="SendColor('#336699');return false" shape="RECT" coords="34,50,50,66" href="javascript:{;}">
				                                <area onclick="SendColor('#339999');return false" shape="RECT" coords="50,50,66,66" href="javascript:{;}">
				                                <area onclick="SendColor('#33CC99');return false" shape="RECT" coords="66,50,82,66" href="javascript:{;}">
				                                <area onclick="SendColor('#33FF99');return false" shape="RECT" coords="82,50,98,66" href="javascript:{;}">
				                                <area onclick="SendColor('#66FF99');return false" shape="RECT" coords="98,50,114,66" href="javascript:{;}">
				                                <area onclick="SendColor('#66CC99');return false" shape="RECT" coords="114,50,130,66" href="javascript:{;}">
				                                <area onclick="SendColor('#669999');return false" shape="RECT" coords="130,50,146,66" href="javascript:{;}">
				                                <area onclick="SendColor('#666699');return false" shape="RECT" coords="146,50,162,66" href="javascript:{;}">
				                                <area onclick="SendColor('#663399');return false" shape="RECT" coords="162,50,178,66" href="javascript:{;}">
				                                <area onclick="SendColor('#660099');return false" shape="RECT" coords="178,50,194,66" href="javascript:{;}">
				                                <area onclick="SendColor('#FF0099');return false" shape="RECT" coords="194,50,210,66" href="javascript:{;}">
				                                <area onclick="SendColor('#FF3399');return false" shape="RECT" coords="210,50,226,66" href="javascript:{;}">
				                                <area onclick="SendColor('#FF6699');return false" shape="RECT" coords="226,50,242,66" href="javascript:{;}">
				                                <area onclick="SendColor('#FF9999');return false" shape="RECT" coords="242,50,258,66" href="javascript:{;}">
				                                <area onclick="SendColor('#FFCC99');return false" shape="RECT" coords="258,50,274,66" href="javascript:{;}">
				                                <area onclick="SendColor('#FFFF99');return false" shape="RECT" coords="274,50,290,66" href="javascript:{;}">
				                                <!--- Row 5 --->
				                                <area onclick="SendColor('#3300CC');return false" shape="RECT" coords="2,66,18,82" href="javascript:{;}">
				                                <area onclick="SendColor('#3333CC');return false" shape="RECT" coords="18,66,34,82" href="javascript:{;}">
				                                <area onclick="SendColor('#3366CC');return false" shape="RECT" coords="34,66,50,82" href="javascript:{;}">
				                                <area onclick="SendColor('#3399CC');return false" shape="RECT" coords="50,66,66,82" href="javascript:{;}">
				                                <area onclick="SendColor('#33CCCC');return false" shape="RECT" coords="66,66,82,82" href="javascript:{;}">
				                                <area onclick="SendColor('#33FFCC');return false" shape="RECT" coords="82,66,98,82" href="javascript:{;}">
				                                <area onclick="SendColor('#66FFCC');return false" shape="RECT" coords="98,66,114,82" href="javascript:{;}">
				                                <area onclick="SendColor('#66CCCC');return false" shape="RECT" coords="114,66,130,82" href="javascript:{;}">
				                                <area onclick="SendColor('#6699CC');return false" shape="RECT" coords="130,66,146,82" href="javascript:{;}">
				                                <area onclick="SendColor('#6666CC');return false" shape="RECT" coords="146,66,162,82" href="javascript:{;}">
				                                <area onclick="SendColor('#6633CC');return false" shape="RECT" coords="162,66,178,82" href="javascript:{;}">
				                                <area onclick="SendColor('#6600CC');return false" shape="RECT" coords="178,66,194,82" href="javascript:{;}">
				                                <area onclick="SendColor('#FF00CC');return false" shape="RECT" coords="194,66,210,82" href="javascript:{;}">
				                                <area onclick="SendColor('#FF33CC');return false" shape="RECT" coords="210,66,226,82" href="javascript:{;}">
				                                <area onclick="SendColor('#FF66CC');return false" shape="RECT" coords="226,66,242,82" href="javascript:{;}">
				                                <area onclick="SendColor('#FF99CC');return false" shape="RECT" coords="242,66,258,82" href="javascript:{;}">
				                                <area onclick="SendColor('#FFCCCC');return false" shape="RECT" coords="258,66,274,82" href="javascript:{;}">
				                                <area onclick="SendColor('#FFFFCC');return false" shape="RECT" coords="274,66,290,82" href="javascript:{;}">
				                                <!--- Row 6 --->
				                                <area onclick="SendColor('#3300FF');return false" shape="RECT" coords="2,82,18,98" href="javascript:{;}">
				                                <area onclick="SendColor('#3333FF');return false" shape="RECT" coords="18,82,34,98" href="javascript:{;}">
				                                <area onclick="SendColor('#3366FF');return false" shape="RECT" coords="34,82,50,98" href="javascript:{;}">
				                                <area onclick="SendColor('#3399FF');return false" shape="RECT" coords="50,82,66,98" href="javascript:{;}">
				                                <area onclick="SendColor('#33CCFF');return false" shape="RECT" coords="66,82,82,98" href="javascript:{;}">
				                                <area onclick="SendColor('#33FFFF');return false" shape="RECT" coords="82,82,98,98" href="javascript:{;}">
				                                <area onclick="SendColor('#66FFFF');return false" shape="RECT" coords="98,82,114,98" href="javascript:{;}">
				                                <area onclick="SendColor('#66CCFF');return false" shape="RECT" coords="114,82,130,98" href="javascript:{;}">
				                                <area onclick="SendColor('#6699FF');return false" shape="RECT" coords="130,82,146,98" href="javascript:{;}">
				                                <area onclick="SendColor('#6666FF');return false" shape="RECT" coords="146,82,162,98" href="javascript:{;}">
				                                <area onclick="SendColor('#6633FF');return false" shape="RECT" coords="162,82,178,98" href="javascript:{;}">
				                                <area onclick="SendColor('#6600FF');return false" shape="RECT" coords="178,82,194,98" href="javascript:{;}">
				                                <area onclick="SendColor('#FF00FF');return false" shape="RECT" coords="194,82,210,98" href="javascript:{;}">
				                                <area onclick="SendColor('#FF33FF');return false" shape="RECT" coords="210,82,226,98" href="javascript:{;}">
				                                <area onclick="SendColor('#FF66FF');return false" shape="RECT" coords="226,82,242,98" href="javascript:{;}">
				                                <area onclick="SendColor('#FF99FF');return false" shape="RECT" coords="242,82,258,98" href="javascript:{;}">
				                                <area onclick="SendColor('#FFCCFF');return false" shape="RECT" coords="258,82,274,98" href="javascript:{;}">
				                                <area onclick="SendColor('#FFFFFF');return false" shape="RECT" coords="274,82,290,98" href="javascript:{;}">
				                                <!--- Row 7 --->
				                                <area onclick="SendColor('#0000FF');return false" shape="RECT" coords="2,98,18,114" href="javascript:{;}">
				                                <area onclick="SendColor('#0033FF');return false" shape="RECT" coords="18,98,34,114" href="javascript:{;}">
				                                <area onclick="SendColor('#0066FF');return false" shape="RECT" coords="34,98,50,114" href="javascript:{;}">
				                                <area onclick="SendColor('#0099FF');return false" shape="RECT" coords="50,98,66,114" href="javascript:{;}">
				                                <area onclick="SendColor('#00CCFF');return false" shape="RECT" coords="66,98,82,114" href="javascript:{;}">
				                                <area onclick="SendColor('#00FFFF');return false" shape="RECT" coords="82,98,98,114" href="javascript:{;}">
				                                <area onclick="SendColor('#99FFFF');return false" shape="RECT" coords="98,98,114,114" href="javascript:{;}">
				                                <area onclick="SendColor('#99CCFF');return false" shape="RECT" coords="114,98,130,114" href="javascript:{;}">
				                                <area onclick="SendColor('#9999FF');return false" shape="RECT" coords="130,98,146,114" href="javascript:{;}">
				                                <area onclick="SendColor('#9966FF');return false" shape="RECT" coords="146,98,162,114" href="javascript:{;}">
				                                <area onclick="SendColor('#9933FF');return false" shape="RECT" coords="162,98,178,114" href="javascript:{;}">
				                                <area onclick="SendColor('#9900FF');return false" shape="RECT" coords="178,98,194,114" href="javascript:{;}">
				                                <area onclick="SendColor('#CC00FF');return false" shape="RECT" coords="194,98,210,114" href="javascript:{;}">
				                                <area onclick="SendColor('#CC33FF');return false" shape="RECT" coords="210,98,226,114" href="javascript:{;}">
				                                <area onclick="SendColor('#CC66FF');return false" shape="RECT" coords="226,98,242,114" href="javascript:{;}">
				                                <area onclick="SendColor('#CC99FF');return false" shape="RECT" coords="242,98,258,114" href="javascript:{;}">
				                                <area onclick="SendColor('#CCCCFF');return false" shape="RECT" coords="258,98,274,114" href="javascript:{;}">
				                                <area onclick="SendColor('#CCFFFF');return false" shape="RECT" coords="274,98,290,114" href="javascript:{;}">
				                                <!--- Row 8 --->
				                                <area onclick="SendColor('#0000CC');return false" shape="RECT" coords="2,114,18,130" href="javascript:{;}">
				                                <area onclick="SendColor('#0033CC');return false" shape="RECT" coords="18,114,34,130" href="javascript:{;}">
				                                <area onclick="SendColor('#0066CC');return false" shape="RECT" coords="34,114,50,130" href="javascript:{;}">
				                                <area onclick="SendColor('#0099CC');return false" shape="RECT" coords="50,114,66,130" href="javascript:{;}">
				                                <area onclick="SendColor('#00CCCC');return false" shape="RECT" coords="66,114,82,130" href="javascript:{;}">
				                                <area onclick="SendColor('#00FFCC');return false" shape="RECT" coords="82,114,98,130" href="javascript:{;}">
				                                <area onclick="SendColor('#99FFCC');return false" shape="RECT" coords="98,114,114,130" href="javascript:{;}">
				                                <area onclick="SendColor('#99CCCC');return false" shape="RECT" coords="114,114,130,130" href="javascript:{;}">
				                                <area onclick="SendColor('#9999CC');return false" shape="RECT" coords="130,114,146,130" href="javascript:{;}">
				                                <area onclick="SendColor('#9966CC');return false" shape="RECT" coords="146,114,162,130" href="javascript:{;}">
				                                <area onclick="SendColor('#9933CC');return false" shape="RECT" coords="162,114,178,130" href="javascript:{;}">
				                                <area onclick="SendColor('#9900CC');return false" shape="RECT" coords="178,114,194,130" href="javascript:{;}">
				                                <area onclick="SendColor('#CC00CC');return false" shape="RECT" coords="194,114,210,130" href="javascript:{;}">
				                                <area onclick="SendColor('#CC33CC');return false" shape="RECT" coords="210,114,226,130" href="javascript:{;}">
				                                <area onclick="SendColor('#CC66CC');return false" shape="RECT" coords="226,114,242,130" href="javascript:{;}">
				                                <area onclick="SendColor('#CC99CC');return false" shape="RECT" coords="242,114,258,130" href="javascript:{;}">
				                                <area onclick="SendColor('#CCCCCC');return false" shape="RECT" coords="258,114,274,130" href="javascript:{;}">
				                                <area onclick="SendColor('#CCFFCC');return false" shape="RECT" coords="274,114,290,130" href="javascript:{;}">
				                                <!--- Row 9 --->
				                                <area onclick="SendColor('#000099');return false" shape="RECT" coords="2,130,18,146" href="javascript:{;}">
				                                <area onclick="SendColor('#003399');return false" shape="RECT" coords="18,130,34,146" href="javascript:{;}">
				                                <area onclick="SendColor('#006699');return false" shape="RECT" coords="34,130,50,146" href="javascript:{;}">
				                                <area onclick="SendColor('#009999');return false" shape="RECT" coords="50,130,66,146" href="javascript:{;}">
				                                <area onclick="SendColor('#00CC99');return false" shape="RECT" coords="66,130,82,146" href="javascript:{;}">
				                                <area onclick="SendColor('#00FF99');return false" shape="RECT" coords="82,130,98,146" href="javascript:{;}">
				                                <area onclick="SendColor('#99FF99');return false" shape="RECT" coords="98,130,114,146" href="javascript:{;}">
				                                <area onclick="SendColor('#99CC99');return false" shape="RECT" coords="114,130,130,146" href="javascript:{;}">
				                                <area onclick="SendColor('#999999');return false" shape="RECT" coords="130,130,146,146" href="javascript:{;}">
				                                <area onclick="SendColor('#996699');return false" shape="RECT" coords="146,130,162,146" href="javascript:{;}">
				                                <area onclick="SendColor('#993399');return false" shape="RECT" coords="162,130,178,146" href="javascript:{;}">
				                                <area onclick="SendColor('#990099');return false" shape="RECT" coords="178,130,194,146" href="javascript:{;}">
				                                <area onclick="SendColor('#CC0099');return false" shape="RECT" coords="194,130,210,146" href="javascript:{;}">
				                                <area onclick="SendColor('#CC3399');return false" shape="RECT" coords="210,130,226,146" href="javascript:{;}">
				                                <area onclick="SendColor('#CC6699');return false" shape="RECT" coords="226,130,242,146" href="javascript:{;}">
				                                <area onclick="SendColor('#CC9999');return false" shape="RECT" coords="242,130,258,146" href="javascript:{;}">
				                                <area onclick="SendColor('#CCCC99');return false" shape="RECT" coords="258,130,274,146" href="javascript:{;}">
				                                <area onclick="SendColor('#CCFF99');return false" shape="RECT" coords="274,130,290,146" href="javascript:{;}">
				                                <!--- Row 10 --->
				                                <area onclick="SendColor('#000066');return false" shape="RECT" coords="2,146,18,162" href="javascript:{;}">
				                                <area onclick="SendColor('#003366');return false" shape="RECT" coords="18,146,34,162" href="javascript:{;}">
				                                <area onclick="SendColor('#006666');return false" shape="RECT" coords="34,146,50,162" href="javascript:{;}">
				                                <area onclick="SendColor('#009966');return false" shape="RECT" coords="50,146,66,162" href="javascript:{;}">
				                                <area onclick="SendColor('#00CC66');return false" shape="RECT" coords="66,146,82,162" href="javascript:{;}">
				                                <area onclick="SendColor('#00FF66');return false" shape="RECT" coords="82,146,98,162" href="javascript:{;}">
				                                <area onclick="SendColor('#99FF66');return false" shape="RECT" coords="98,146,114,162" href="javascript:{;}">
				                                <area onclick="SendColor('#99CC66');return false" shape="RECT" coords="114,146,130,162" href="javascript:{;}">
				                                <area onclick="SendColor('#999966');return false" shape="RECT" coords="130,146,146,162" href="javascript:{;}">
				                                <area onclick="SendColor('#996666');return false" shape="RECT" coords="146,146,162,162" href="javascript:{;}">
				                                <area onclick="SendColor('#993366');return false" shape="RECT" coords="162,146,178,162" href="javascript:{;}">
				                                <area onclick="SendColor('#990066');return false" shape="RECT" coords="178,146,194,162" href="javascript:{;}">
				                                <area onclick="SendColor('#CC0066');return false" shape="RECT" coords="194,146,210,162" href="javascript:{;}">
				                                <area onclick="SendColor('#CC3366');return false" shape="RECT" coords="210,146,226,162" href="javascript:{;}">
				                                <area onclick="SendColor('#CC6666');return false" shape="RECT" coords="226,146,242,162" href="javascript:{;}">
				                                <area onclick="SendColor('#CC9966');return false" shape="RECT" coords="242,146,258,162" href="javascript:{;}">
				                                <area onclick="SendColor('#CCCC66');return false" shape="RECT" coords="258,146,274,162" href="javascript:{;}">
				                                <area onclick="SendColor('#CCFF66');return false" shape="RECT" coords="274,146,290,162" href="javascript:{;}">
				                                <!--- Row 11 --->
				                                <area onclick="SendColor('#000033');return false" shape="RECT" coords="2,162,18,178" href="javascript:{;}">
				                                <area onclick="SendColor('#003333');return false" shape="RECT" coords="18,162,34,178" href="javascript:{;}">
				                                <area onclick="SendColor('#006633');return false" shape="RECT" coords="34,162,50,178" href="javascript:{;}">
				                                <area onclick="SendColor('#009933');return false" shape="RECT" coords="50,162,66,178" href="javascript:{;}">
				                                <area onclick="SendColor('#00CC33');return false" shape="RECT" coords="66,162,82,178" href="javascript:{;}">
				                                <area onclick="SendColor('#00FF33');return false" shape="RECT" coords="82,162,98,178" href="javascript:{;}">
				                                <area onclick="SendColor('#99FF33');return false" shape="RECT" coords="98,162,114,178" href="javascript:{;}">
				                                <area onclick="SendColor('#99CC33');return false" shape="RECT" coords="114,162,130,178" href="javascript:{;}">
				                                <area onclick="SendColor('#999933');return false" shape="RECT" coords="130,162,146,178" href="javascript:{;}">
				                                <area onclick="SendColor('#996633');return false" shape="RECT" coords="146,162,162,178" href="javascript:{;}">
				                                <area onclick="SendColor('#993333');return false" shape="RECT" coords="162,162,178,178" href="javascript:{;}">
				                                <area onclick="SendColor('#990033');return false" shape="RECT" coords="178,162,194,178" href="javascript:{;}">
				                                <area onclick="SendColor('#CC0033');return false" shape="RECT" coords="194,162,210,178" href="javascript:{;}">
				                                <area onclick="SendColor('#CC3333');return false" shape="RECT" coords="210,162,226,178" href="javascript:{;}">
				                                <area onclick="SendColor('#CC6633');return false" shape="RECT" coords="226,162,242,178" href="javascript:{;}">
				                                <area onclick="SendColor('#CC9933');return false" shape="RECT" coords="242,162,258,178" href="javascript:{;}">
				                                <area onclick="SendColor('#CCCC33');return false" shape="RECT" coords="258,162,274,178" href="javascript:{;}">
				                                <area onclick="SendColor('#CCFF33');return false" shape="RECT" coords="274,162,290,178" href="javascript:{;}">
				                                <!--- Row 12 --->
				                                <area onclick="SendColor('#000000');return false" shape="RECT" coords="2,178,18,194" href="javascript:{;}">
				                                <area onclick="SendColor('#003300');return false" shape="RECT" coords="18,178,34,194" href="javascript:{;}">
				                                <area onclick="SendColor('#006600');return false" shape="RECT" coords="34,178,50,194" href="javascript:{;}">
				                                <area onclick="SendColor('#009900');return false" shape="RECT" coords="50,178,66,194" href="javascript:{;}">
				                                <area onclick="SendColor('#00CC00');return false" shape="RECT" coords="66,178,82,194" href="javascript:{;}">
				                                <area onclick="SendColor('#00FF00');return false" shape="RECT" coords="82,178,98,194" href="javascript:{;}">
				                                <area onclick="SendColor('#99FF00');return false" shape="RECT" coords="98,178,114,194" href="javascript:{;}">
				                                <area onclick="SendColor('#99CC00');return false" shape="RECT" coords="114,178,130,194" href="javascript:{;}">
				                                <area onclick="SendColor('#999900');return false" shape="RECT" coords="130,178,146,194" href="javascript:{;}">
				                                <area onclick="SendColor('#996600');return false" shape="RECT" coords="146,178,162,194" href="javascript:{;}">
				                                <area onclick="SendColor('#993300');return false" shape="RECT" coords="162,178,178,194" href="javascript:{;}">
				                                <area onclick="SendColor('#990000');return false" shape="RECT" coords="178,178,194,194" href="javascript:{;}">
				                                <area onclick="SendColor('#CC0000');return false" shape="RECT" coords="194,178,210,194" href="javascript:{;}">
				                                <area onclick="SendColor('#CC3300');return false" shape="RECT" coords="210,178,226,194" href="javascript:{;}">
				                                <area onclick="SendColor('#CC6600');return false" shape="RECT" coords="226,178,242,194" href="javascript:{;}">
				                                <area onclick="SendColor('#CC9900');return false" shape="RECT" coords="242,178,258,194" href="javascript:{;}">
				                                <area onclick="SendColor('#CCCC00');return false" shape="RECT" coords="258,178,274,194" href="javascript:{;}">
				                                <area onclick="SendColor('#CCFF00');return false" shape="RECT" coords="274,178,290,194" href="javascript:{;}">
			                                </map>
		                                </div>
	                                </td>
	                                </tr>
                        	        
	                                </table>
	                                 <div oncontextmenu="return false" id="divEmailContent" contentEditable="true" style="BORDER-RIGHT: thin groove; BORDER-TOP: thin groove; OVERFLOW: auto; BORDER-LEFT: thin groove; WIDTH: 562px; BORDER-BOTTOM: thin groove; HEIGHT: 250px; BACKGROUND-COLOR: white; font-face: verdana; scrollbar-color: red" align="left"></div>

	                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="HiddenFieldEmailContent" runat="server" />
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" />
                                </td>
                            </tr> 
                            <tr>
                                <td colspan="2" style="text-align:center">
                                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" />&nbsp;<asp:Button ID="ButtonCancel"
                                        runat="server" Text="Cancel" OnClick="ButtonCancel_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                        </td>
                     </tr>
                </table>
            </fieldset>
        </td>
    </tr>
</table>
<asp:Label ID="LabelClientScript" runat="server" Text="ClientScript"></asp:Label>
<br />
</asp:Content>