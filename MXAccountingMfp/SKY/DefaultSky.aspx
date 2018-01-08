<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultSky.aspx.cs" Inherits="AccountingPlusEA.SKY.DefaultSky" %>

<html>
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form class='osa_message' title='AccountingPlus' action='LogOn.aspx'>
    <img id="id_logo" height="32" width="32" src="../App_Themes/<%=theme %>/FormBrowser/Images/logo.gif" />
    <img id='id_background' height='192' width='277' src='../App_Themes/<%=theme %>/FormBrowser/Images/IDCard.gif' />
    <input id='id_ok' />
    <p>
        Please press OK button and flash</p>
    <p>
        your card to activate the machine</p>
    <p>
        Unauthorized persons are not allowed</p>
    <p>
        to access this machine!</p>
    </form>
</body>
</html>
