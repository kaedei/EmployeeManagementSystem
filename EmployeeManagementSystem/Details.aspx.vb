Partial Public Class Details
	Inherits System.Web.UI.Page


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not Page.IsPostBack Then
			'Load Data and show controls
			Session("id") = Request.Params("id")
			Dim id = Session("id")
			'Get current user's role
			Dim r = DBHelper.GetUserRoleName(Page.User.Identity.Name)

			'Hide controls
			If Not String.IsNullOrEmpty(id) Then
				If id = Page.User.Identity.Name Then
					lblUser.Text = "View my profile"
					HideControls(Role.MANAGER)
					lblBankAccount.Visible = True
					btnShowBankAccount.Visible = True
					txtBankAccount.Enabled = True
				Else
					lblUser.Text = "Profile of " + id
					HideControls(r)
				End If
				'Bind Data
				Dim info = DBHelper.GetInformation(id)
				BindData(info)
			Else
				'New user
				'If the user isnot admin or HR 
				'redirect them to search page
				If r <> Role.ADMIN AndAlso r <> Role.HR Then
					Response.Redirect("Search.aspx")
				End If
				lblUser.Text = "Add a new user"
				txtUserName.Enabled = True
				lnkSalaryHistory.Visible = False
				lnkChangeUserType.Visible = False
				lblBankAccount.Visible = False
				btnShowBankAccount.Visible = False
			End If

		End If
	End Sub

#Region "Hide Controls"
	Private Sub HideControls(ByVal r As Role)
		Select Case r
			Case Role.ADMIN
				lblEmpID.Visible = True
				txtEmpID.Visible = True
				lblManager.Visible = True
				txtManager.Visible = True
				lblGrade.Visible = True
				lstGrade.Visible = True
				lblBankAccount.Visible = True
				btnShowBankAccount.Visible = True
				lnkSalaryHistory.Visible = True
				lnkChangeUserType.Visible = True
				btnSave.Enabled = True
				txtEmpID.Enabled = True
				txtFirstName.Enabled = True
				txtLastName.Enabled = True
				txtDepartment.Enabled = True
				txtProject.Enabled = True
				txtEmail.Enabled = True
				txtMobile.Enabled = True
				txtOfficeAddress.Enabled = True
				txtManager.Enabled = True
				lstGrade.Enabled = True
				txtBankAccount.Enabled = True
			Case Role.HR
				lblEmpID.Visible = True
				txtEmpID.Visible = True
				lblManager.Visible = True
				txtManager.Visible = True
				lblGrade.Visible = True
				lstGrade.Visible = True
				lblBankAccount.Visible = False
				btnShowBankAccount.Visible = False
				lnkSalaryHistory.Visible = True
				lnkChangeUserType.Visible = True
				btnSave.Enabled = True
				txtEmpID.Enabled = True
				txtFirstName.Enabled = True
				txtLastName.Enabled = True
				txtDepartment.Enabled = True
				txtProject.Enabled = True
				txtEmail.Enabled = True
				txtMobile.Enabled = True
				txtOfficeAddress.Enabled = True
				txtManager.Enabled = True
				lstGrade.Enabled = True
				txtBankAccount.Enabled = True
			Case Role.MANAGER
				lblEmpID.Visible = True
				txtEmpID.Visible = True
				lblManager.Visible = True
				txtManager.Visible = True
				lblGrade.Visible = True
				lstGrade.Visible = True
				lblBankAccount.Visible = False
				btnShowBankAccount.Visible = False
				lnkSalaryHistory.Visible = True
				lnkChangeUserType.Visible = False
				btnSave.Enabled = False
				txtEmpID.Enabled = False
				txtFirstName.Enabled = False
				txtLastName.Enabled = False
				txtDepartment.Enabled = False
				txtProject.Enabled = False
				txtEmail.Enabled = False
				txtMobile.Enabled = False
				txtOfficeAddress.Enabled = False
				txtManager.Enabled = False
				lstGrade.Enabled = False
				txtBankAccount.Enabled = False
			Case Role.EMPLOYEE
				lblEmpID.Visible = False
				txtEmpID.Visible = False
				lblManager.Visible = False
				txtManager.Visible = False
				lblGrade.Visible = False
				lstGrade.Visible = False
				lblBankAccount.Visible = False
				btnShowBankAccount.Visible = False
				lnkSalaryHistory.Visible = False
				lnkChangeUserType.Visible = False
				btnSave.Enabled = False
				txtEmpID.Enabled = False
				txtFirstName.Enabled = False
				txtLastName.Enabled = False
				txtDepartment.Enabled = False
				txtProject.Enabled = False
				txtEmail.Enabled = False
				txtMobile.Enabled = False
				txtOfficeAddress.Enabled = False
				txtManager.Enabled = False
				lstGrade.Enabled = False
				txtBankAccount.Enabled = False
		End Select
	End Sub
#End Region

#Region "Bind Data"

	Public Sub BindData(ByVal info As UserInformation)
		txtUserName.Text = IIf(Session("id") Is Nothing, "", Session("id"))
		txtEmpID.Text = info.EmpID
		txtFirstName.Text = info.FirstName
		txtLastName.Text = info.LastName
		txtDepartment.Text = info.Department
		txtProject.Text = info.Project
		txtEmail.Text = info.Email
		txtMobile.Text = info.Mobile
		txtOfficeAddress.Text = info.OfficeAddress
		txtManager.Text = info.Manager
		lstGrade.SelectedIndex = info.Grade
		'URL of bank system
		lnkSalaryHistory.NavigateUrl = "http://localhost:55556/GetSalary.aspx?user=" + txtUserName.Text
	End Sub

#End Region


	''' <summary>
	''' Cancel this operation and return to search page
	''' </summary>
	Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		Response.Redirect("Search.aspx")
	End Sub


	''' <summary>
	''' Show bank account number of user
	''' </summary>
	Protected Sub btnShowBankAccount_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowBankAccount.Click
		'show textbox and 'update' button
		txtBankAccount.Visible = True
		btnUpdateBankAccount.Visible = True
		btnShowBankAccount.Visible = False
		'Get user name from session
		Dim id = Session("id")
		If (id IsNot Nothing) Then
			'show bank account number
			txtBankAccount.Text = DBHelper.GetBankAccount(id.ToString())
		End If

	End Sub

	''' <summary>
	''' Update user's bank account number
	''' </summary>
	Protected Sub btnUpdateBankAccount_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateBankAccount.Click
		'Get user id from session
		Dim id = Session("id")
		If (id IsNot Nothing) Then
			DBHelper.UpdateBankAccount(id.ToString(), txtBankAccount.Text)
			MsgBox("Bank account number has been updated successfully")
		End If
	End Sub

	Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
		'Check if is 'New' or 'Edit'
		Dim id = Session("id")
		If id Is Nothing Then	'New User
			DBHelper.AddUserInformation(txtUserName.Text, txtEmpID.Text, txtFirstName.Text, _
			txtLastName.Text, txtDepartment.Text, _
			txtProject.Text, txtEmail.Text, _
			txtMobile.Text, txtOfficeAddress.Text, _
			txtManager.Text, lstGrade.SelectedIndex)
			'Redirect to Search page
			Response.Redirect("Search.aspx")

		Else 'Edit User
			'Update User Information
			DBHelper.UpdateInformation(id, txtEmpID.Text, txtFirstName.Text, _
			 txtLastName.Text, txtDepartment.Text, _
			 txtProject.Text, txtEmail.Text, _
			 txtMobile.Text, txtOfficeAddress.Text, _
			 txtManager.Text, lstGrade.SelectedIndex)
			'Redirect to Search page
			Response.Redirect("Search.aspx")
		End If
	End Sub

End Class