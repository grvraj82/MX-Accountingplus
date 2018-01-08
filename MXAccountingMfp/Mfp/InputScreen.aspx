<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputScreen.aspx.cs" ContentType="text/xml" Inherits="AccountingPlusDevice.Mfp.InputScreen" %>

<html>
<body>
<form id='form1' class='osa_input' action="InputScreen.aspx">
<input id='id_ok' />
<input id='id_cancel' type="button" onclick="InputScreen.aspx?Cancel=true"  />
<input id='cardNumber' type='submit' title='Swipe the Card' value='' format='password'/>
</form>
</body>
</html>