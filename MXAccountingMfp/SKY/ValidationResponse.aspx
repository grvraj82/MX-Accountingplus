<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidationResponse.aspx.cs" Inherits="AccountingPlusEA.SKY.ValidationResponse" %>

<html>
<head>
    <title></title>
</head>
<body>
    <form class='osa_message' title='AccountingPlus' action='LogOn.aspx'>
    <img id='id_background' height='192' width='277' src='../Images/IDCard.gif' />
    <img id='id_logo' height='32' width='32' src='../Images/logo.gif' />
    <input id='id_ok' />
    <p>
        <%=validationResponse%></p>
    <p>
    </p>
    <p>
        <%=validationSuggestion%></p>
    </form>
</body>
</html>