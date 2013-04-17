<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Search.aspx.vb" Inherits="EmployeeManagementSystem.Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link href="css/user.css" rel="stylesheet" type="text/css" />
	<title>Search Page</title>
</head>
<body>
	<form id="form1" runat="server">
	<div style="width: 100%">
		<table align="center" cellpadding="2" cellspacing="2">
			<tr>
				<td colspan="3" style="font-weight: bold; font-size: x-large">
					Hello,&nbsp;<asp:LoginName ID="LoginName1" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:LinkButton ID="lnkEditMyProfile" runat="server">View My Profile</asp:LinkButton>
				</td>
				<td>
					<asp:LinkButton ID="lnkAddUser" runat="server">Add a new User</asp:LinkButton>
				</td>
				<td>
					<asp:LinkButton ID="lnkLogout" runat="server">Log out</asp:LinkButton>
				</td>
			</tr>
		</table>
	</div>
	<hr />
	<div style="width: 100%">
		<table style="width: 600px; text-align: center" align="center">
			<tr>
				<td colspan="4" style="font-weight: bold; font-size: x-large">
					Search in Employee List
				</td>
			</tr>
			<tr>
				<td>
					User Name:
				</td>
				<td>
					<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
				</td>
				<td>
					Department:
				</td>
				<td>
					<asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					First Name:
				</td>
				<td>
					<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
				</td>
				<td>
					Project:
				</td>
				<td>
					<asp:TextBox ID="txtProject" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					Last Name:
				</td>
				<td>
					<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
				</td>
				<td>
					Mobile Phone:
				</td>
				<td>
					<asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					E-mail:
				</td>
				<td>
					<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
				</td>
				<td>
					Office Address:
				</td>
				<td>
					<asp:TextBox ID="txtOfficeAddress" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<asp:Button ID="btnSearch" runat="server" Text="Search" />
				</td>
			</tr>
		</table>
	</div>
	<hr />
	<div align="center">
		<asp:GridView ID="gv" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333"
			GridLines="None" Caption="Search Result" PageSize="5" AutoGenerateColumns="False">
			<RowStyle BackColor="#EFF3FB" />
			<Columns>
				<asp:HyperLinkField DataTextField="UserName" HeaderText="User Name" 
					DataNavigateUrlFormatString="Details.aspx?id={0}" DataNavigateUrlFields="UserName" />
				<asp:BoundField DataField="FirstName" HeaderText="First Name" />
				<asp:BoundField DataField="LastName" HeaderText="Last Name" />
				<asp:BoundField DataField="Email" HeaderText="E-mail" />
				<asp:BoundField DataField="Department" HeaderText="Department" />
				<asp:BoundField DataField="Project" HeaderText="Project" />
				<asp:BoundField DataField="Mobile" HeaderText="Mobile Phone" />
				<asp:BoundField DataField="OfficeAddress" HeaderText="Office Address" />
			</Columns>
			<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
			<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
			<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
			<EditRowStyle BackColor="#2461BF" />
			<AlternatingRowStyle BackColor="White" />
		</asp:GridView>
	</div>
	</form>
</body>
</html>
