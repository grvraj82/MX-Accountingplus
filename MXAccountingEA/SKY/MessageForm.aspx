<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageForm.aspx.cs" Inherits="AccountingPlusEA.SKY.MessageForm" %>

<html>
<body>
    <form class='osa_message' title='' action='MessageForm.aspx'>
    <input id='id_ok' />
     <% if (message == "cardInfoNotFoundRegister")
       {
    %>
    <input id='RegisterID' value="Register" onclick="SelfRegistration.aspx" />
    <%} %>
    
    <p>
        <%=displayMessage %></p>
    <p>
        <%=displayMessageNextLine%></p>
    </form>
</body>
</html>
