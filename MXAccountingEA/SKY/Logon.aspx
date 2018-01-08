<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="AccountingPlusEA.SKY.Logon" %>

<html>
<head>
    <title></title>
</head>
<body>
    <form class='osa_input' action='LogOn.aspx'>
    <img id='id_background' height='192' width='277' src='../App_Themes/<%=theme %>/FormBrowser/Images/IDCard.gif' />
    <img id='id_logo' height='32' width='32' src='../App_Themes/<%=theme %>/FormBrowser/Images/logo.gif' />
     <input id='id_ok' />
    <input id="id_cancel" onclick="DefaultSky.aspx?CC=True" />
    <% if (displayMessage == "Card")
       {
    %>
    <input id='CardID' format='password' type='submit' title='Flash your card on the reader'
        value='' />
    <%} %>

    <% 
        if (displayMessage == "Password")
        {
    %>
    <input id='Password' format='password' type='submit' title='Enter your Password'
        value='' />
    <%} %>
    </form>
</body>
</html>
