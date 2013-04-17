Partial Public Class Search
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not Page.IsPostBack Then
			If DBHelper.IsInRole(Page.User.Identity.Name, Role.HR) OrElse DBHelper.IsInRole(Page.User.Identity.Name, Role.ADMIN) Then
				lnkAddUser.Visible = True
			Else
				lnkAddUser.Visible = False
			End If
			btnSearch_Click(e, EventArgs.Empty)
		End If

	End Sub

	Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click

		'Get data from database 
		Dim dt = DBHelper.GetUserList(txtUserName.Text, txtFirstName.Text, _
		txtLastName.Text, txtEmail.Text, txtDepartment.Text, _
		txtProject.Text, txtMobile.Text, txtOfficeAddress.Text)
		'bind gridview control
		gv.DataSource = dt
		gv.DataBind()
	End Sub

	Protected Sub gv_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv.PageIndexChanging
		gv.PageIndex = e.NewPageIndex
		btnSearch_Click(sender, EventArgs.Empty)
	End Sub

	Protected Sub lnkLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkLogout.Click
		FormsAuthentication.SignOut()
		'Server.Transfer(FormsAuthentication.LoginUrl)
		Response.Redirect(FormsAuthentication.LoginUrl)
	End Sub

	Protected Sub lnkAddUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAddUser.Click
		Response.Redirect("Details.aspx")
	End Sub

	Protected Sub lnkEditMyProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkEditMyProfile.Click
		Response.Redirect("Details.aspx?id=" + Page.User.Identity.Name)
	End Sub
End Class