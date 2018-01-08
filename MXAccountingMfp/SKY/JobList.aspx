<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="AccountingPlusEA.SKY.JobList" %>

<html>
<head>
    <title></title>
</head>
<body>
    <form class='osa_filefolder' title='' action='JobList.aspx'>
    <select id='options' class="menu_fill" title='Print Documents' multiple="true">
        <%=jobList%>
    </select>
    <input id="MFPMode" type="submit" value="MFP Mode" />
    <input id="Logout" type="submit" value="Logout" />
    <input id="Delete" type="submit" value="Delete" />
    <input id='id_ok' value="Print" />
    </form>
</body>
</html>
