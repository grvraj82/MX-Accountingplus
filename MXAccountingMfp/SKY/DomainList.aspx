<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DomainList.aspx.cs" Inherits="AccountingPlusDevice.SKY.DomainList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title></title>
</head>
<body>
    <form class='osa_options' title='Domains list' action='DomainList.aspx'>
    <input id="id_ok" />
    <input id="id_cancel" />
    <select id="DomainList" title="Please select an Domain:">
        <%=domainList%>
    </select>
    </form>
</body>
</html>
