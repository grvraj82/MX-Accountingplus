<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardLogOn.aspx.cs" Inherits="AccountingPlusEA.SKY.CardLogOn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form class='osa_fields' title='AccountingPlus' action='CardLogOn.aspx'>
    <img id="id_logo" height="32" width="32" src="../App_Themes/<%=theme %>/FormBrowser/Images/logo.gif" />
    <input id='id_ok' />
    
    <input id="id_cancel" onclick="CardLogOn.aspx" />
    <fieldset title="Please Provide Login Details">
        <input id="CardID" title="Card ID" value="" format="text" type="password" />
        <% if (cardType == "Secure Swipe")
           {
        %>
        <input id="UserPassword" title="Password" value="" type="password" format="password" />
        <%} %>
    </fieldset>
    </form>
</body>
</html>
