<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyBalance.aspx.cs" Inherits="AccountingPlusEA.SKY.MyBalance" %>

<html>
<body>
    <form class='osa_list' title='AccountingPlus' action='MyBalance.aspx?uid=<%=UserID %>&usysid=<%=UserIdentityID %>&gid=<%=GroupID %>'>
    <img id='id_logo' height='32' width='32' src='../Images/logo.gif' />
    <input id="id_ok" />
        <ul title="">
            <%= UserDetails%>
            <%= BalanceReport %>
        </ul>
    </form>
</body>
</html>