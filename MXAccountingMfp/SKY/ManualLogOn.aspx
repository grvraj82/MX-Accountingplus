<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManualLogOn.aspx.cs" Inherits="AccountingPlusEA.SKY.ManualLogOn" %>

<html>
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form class='osa_fields' title='AccountingPlus' action='ManualLogOn.aspx'>
    <img id="id_logo" height="32" width="32" src="../App_Themes/<%=theme %>/FormBrowser/Images/logo.gif" />
    <% if (userSource != "AD")
       {
    %>
    <input id='id_ok' />
    <%} %>
    <input id="id_cancel" onclick="ManualLogOn.aspx" />
    <fieldset title="Please Provide Login Details">
        <input id="UserName" title="User Name" value="" format="text" />
        <input id="UserPassword" title="Password" value="" type="password" format="password" />
        <% if (userSource != "DB")
           {
        %>
        <% if (domainsCount > 0)
           {
        %>
        <input id="TextDomainName" title="Domain Name" type="submit" value="Click on Domain Name button to Select Domain" />
        <%} %>
        <% else
            {
        %>
        <input id="TextDomainNotConfigured" title="Domain Name" value="Domains are not configured"
            format="button" disabled="true" />
        <%} %>
        <%} %>
    </fieldset>
    </form>
</body>
</html>
