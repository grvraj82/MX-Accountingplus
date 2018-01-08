<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageHelp.aspx.cs" Inherits="ApplicationRegistration.Help.PageHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>:: Help ::</title>
</head>
<link href="../AppStyle/ApplicationStyle.css" rel="stylesheet" type="text/css" />
<body onload="javascript:window.focus()">
    <form id="form1" runat="server">
    <div>
    <table width="98%" style="text-align:right">
        <tr>
            <td></td>
            <td style="text-align:right" class="f11b">Build : 
                <asp:Label ID="LabelBuildVersion" runat="server" Text="BuildVersion"></asp:Label></td>
        </tr>
        <!--
        <tr>
            <td style="text-align:left" colspan="2">
            <fieldset>
                <legend class="f11b">Help&nbsp;</legend>
            
   
            </fieldset>
            </td>
        </tr>
        -->
    </table>
    </div>
    </form>
</body>
</html>
