<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="EmployeeManagementSystem._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Employee Management System</title>
</head>
<body>
	<form id="form1" runat="server">
	<table width="600px" align="center" style="text-align: center">
		<tr>
			<td colspan="1">
				<img alt="CSC Logo" src="img/csc_logo.gif" style="width: 60px; height: 33px" />
			</td>
			<td colspan="3">
				<h1>
					Employee Management System</h1>
			</td>
		</tr>
		<tr>
			<td colspan="4">
				<asp:Label ID="lblTip" runat="server" Text="Please login first:"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;</td>
			<td colspan="2">
				User Name:</td>
			<td align="left">
				<asp:TextBox ID="txtUser" runat="server" MaxLength="10"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				&nbsp;
			</td>
			<td>
				Password:</td>
			<td align="left">
				<asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="4">
				<asp:Button ID="btnLogin" runat="server" Text="Login" />&nbsp;
				<asp:Button ID="btnReset" runat="server" Text="Reset" />
			</td>
		</tr>
	</table>
	</form>
	<hr />
	<div align="center">
		<p>
			&copy CSC 2012. By Alex</p>
	</div>
</body>
</html>
