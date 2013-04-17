<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Details.aspx.vb" Inherits="EmployeeManagementSystem.Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Details</title>
</head>
<body>
	<form id="form1" runat="server">
	<div style="width: 100%">
		<table align="center">
			<tr>
				<td style="font-weight: bold; font-size: x-large">
					<asp:Label ID="lblUser" runat="server" Text="Edit/View details of User X"></asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					<a href="Search.aspx">&lt;&lt;Back</a>
				</td>
			</tr>
		</table>
	</div>
	<div style="width: 100%">
		<table align="center">
			<tr>
				<td>
					<asp:Label ID="lblUserName" runat="server" Text="User Name:"></asp:Label>
				</td>
				<td>
					<asp:TextBox ID="txtUserName" runat="server" Enabled="False"></asp:TextBox>
				</td>
				<td>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
						ControlToValidate="txtUserName" ErrorMessage="Please input user name" 
						SetFocusOnError="True"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblEmpID" runat="server" Text="Employee ID:"></asp:Label>
				</td>
				<td>
					<asp:TextBox ID="txtEmpID" runat="server" MaxLength="10"></asp:TextBox>
				</td>
				<td>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpID"
						ErrorMessage="EmployeeID is required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td>
					First Name:
				</td>
				<td>
					<asp:TextBox ID="txtFirstName" runat="server" MaxLength="20"></asp:TextBox>
				</td>
				<td>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName"
						ErrorMessage="First Name is required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td>
					Last Name:
				</td>
				<td>
					<asp:TextBox ID="txtLastName" runat="server" MaxLength="20"></asp:TextBox>
				</td>
				<td>
					&nbsp;
				</td>
			</tr>
			<tr>
				<td>
					Department:
				</td>
				<td>
					<asp:TextBox ID="txtDepartment" runat="server" MaxLength="20"></asp:TextBox>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td>
					Project:
				</td>
				<td>
					<asp:TextBox ID="txtProject" runat="server" MaxLength="20"></asp:TextBox>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td>
					E-mail:
				</td>
				<td>
					<asp:TextBox ID="txtEmail" runat="server" MaxLength="20"></asp:TextBox>
				</td>
				<td>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
						ControlToValidate="txtEmail" 
						ErrorMessage="Please input correct&lt;br/&gt; email address" 
						ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td>
					Mobile:
				</td>
				<td>
					<asp:TextBox ID="txtMobile" runat="server" MaxLength="20"></asp:TextBox>
				</td>
				<td>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
						ControlToValidate="txtMobile" 
						ErrorMessage="Please input correct&lt;br/&gt; mobile number" 
						ValidationExpression="[\d\+\-]{3,20}"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td>
					Office Address:
				</td>
				<td>
					<asp:TextBox ID="txtOfficeAddress" runat="server" MaxLength="20"></asp:TextBox>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblManager" runat="server" Text="Manager:"></asp:Label>
				</td>
				<td>
					<asp:TextBox ID="txtManager" runat="server" MaxLength="10"></asp:TextBox>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblGrade" runat="server" Text="Grade:"></asp:Label>
				</td>
				<td>
					<asp:DropDownList ID="lstGrade" runat="server">
						<asp:ListItem>0</asp:ListItem>
						<asp:ListItem>1</asp:ListItem>
						<asp:ListItem>2</asp:ListItem>
						<asp:ListItem>3</asp:ListItem>
						<asp:ListItem>4</asp:ListItem>
						<asp:ListItem>5</asp:ListItem>
						<asp:ListItem>6</asp:ListItem>
						<asp:ListItem>7</asp:ListItem>
						<asp:ListItem>8</asp:ListItem>
					</asp:DropDownList>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblBankAccount" runat="server" Text="Bank Account:"></asp:Label>
				</td>
				<td colspan="2">
					<asp:TextBox ID="txtBankAccount" runat="server" MaxLength="20" Visible="False"></asp:TextBox>
					<asp:Button ID="btnShowBankAccount" runat="server" Text="Show" />
					<asp:Button ID="btnUpdateBankAccount" runat="server" Text="Update" Visible="False" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:HyperLink ID="lnkSalaryHistory" runat="server" Target="_blank">Salary History</asp:HyperLink>
				</td>
				<td>
					<asp:HyperLink ID="lnkChangeUserType" runat="server" 
						NavigateUrl="~/UserType.aspx">Change User Type</asp:HyperLink>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td align="right">
					<asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" />
				</td>
				<td>
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" />
				</td>
				<td>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
