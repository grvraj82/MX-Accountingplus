<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectGroup.aspx.cs" Inherits="AccountingPlusEA.SKY.SelectGroup" %>

<html>
<head>
</head>
<body>
    <form class='osa_menu' title='AccountingPlus' action='SelectGroup.aspx'>
    <img id='id_logo' height='32' width='32' src='../App_Themes/<%=theme %>/FormBrowser/Images/logo.gif' />
    <fieldset title='Please select Cost Center'>
        <%=AssignedGroups %>
    </fieldset>
    </form>
</body>
</html>