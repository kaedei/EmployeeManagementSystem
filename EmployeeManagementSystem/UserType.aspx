<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserType.aspx.vb" Inherits="EmployeeManagementSystem.UserType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Change User's Type</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center" align="center">
    
    	<asp:Label ID="Label1" runat="server" Text="Change User's Type"></asp:Label>
		<br />
		<br />
		<asp:Label ID="Label2" runat="server" Text="Change to: "></asp:Label>
		<asp:DropDownList ID="lstRole" runat="server">
			<asp:ListItem Value="0">Administrator</asp:ListItem>
			<asp:ListItem Value="1">Employee</asp:ListItem>
			<asp:ListItem Value="2">Manager</asp:ListItem>
			<asp:ListItem Value="3">HR</asp:ListItem>
		</asp:DropDownList>
		<br />
		<asp:CheckBox ID="chkDisabled" runat="server" Text="This User is disabled" />
		<br />
		<asp:Button ID="btnSave" runat="server" Text="Save" />
		<asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    
    </div>
    </form>
</body>
</html>
