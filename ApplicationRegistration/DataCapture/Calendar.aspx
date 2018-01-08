<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="ApplicationRegistration.DataCapture.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>:: Calendar ::</title>
    <link href="../AppStyle/ApplicationStyle.css" rel="stylesheet" type="text/css" />
</head>
<body class="Cal" MS_POSITIONING="GridLayout" style="background-color:#FFCC66" topmargin="0" leftmargin="0">
		<form id="SharpCalendar" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td><asp:calendar id="CalDateSelection" runat="server" EnableViewState="False" BorderStyle="Ridge" ShowGridLines="True" BorderColor="#FFCC66" Font-Names="Verdana" Font-Size="8pt" Height="210px" ForeColor="#663399" Width="310px" BackColor="#FFFFCC" BorderWidth="4px" OnSelectionChanged="CalDateSelection_SelectionChanged" OnVisibleMonthChanged="CalDateSelection_VisibleMonthChanged">
							<TodayDayStyle ForeColor="White" BackColor="#FFCC66"></TodayDayStyle>
							<SelectorStyle BackColor="#FFCC66"></SelectorStyle>
							<DayStyle Wrap="False"></DayStyle>
							<NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
							<DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle>
							<SelectedDayStyle Font-Bold="True" BackColor="#FFCC66"></SelectedDayStyle>
							<TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#3F5095"></TitleStyle>
							<OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
						</asp:calendar>
                    </td>
				</tr>
				<tr>
					<td align="center" style="height: 20px"><asp:dropdownlist id="DropDownListMonth" Runat="server" CssClass="f11b" Width="120" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMonth_SelectedIndexChanged"></asp:dropdownlist>&nbsp;<asp:dropdownlist id="DropDownListYear" Runat="server" CssClass="f11b" AutoPostBack="True" OnSelectedIndexChanged="DropDownListYear_SelectedIndexChanged"></asp:dropdownlist>&nbsp;</td>
				</tr>
			</table><asp:Label ID="LabelScript" runat="server"></asp:Label>
		</form>
	</body></html>
