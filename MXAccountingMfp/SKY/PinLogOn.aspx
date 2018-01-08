<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PinLogOn.aspx.cs" Inherits="AccountingPlusEA.SKY.PinLogOn" %>

<html>
<head>
    <title></title>
</head>
<body>
    <form class='osa_fields' title='AccountingPlus' action='PinLogOn.aspx'>
    <img id="id_logo" height="32" width="32" src="../App_Themes/<%=theme %>/FormBrowser/Images/logo.gif" />
    <input id='id_ok' />
    <input id="id_cancel" onclick="PinLogOn.aspx" />/>
    <fieldset title="Please Provide Login Details">
        <input id="PinNumber" title="PIN" value="" format="text" type="password" />
    </fieldset>
    </form>
</body>
</html>
