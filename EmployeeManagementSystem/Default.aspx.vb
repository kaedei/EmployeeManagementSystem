Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
		'Check if username/password is correct
		Dim username = txtUser.Text.Trim()
		Dim password = txtPassword.Text.Trim()
		'If username is empty then do nothing
		If (String.IsNullOrEmpty(username)) Then
			lblTip.Text = "Please input your name"
			Return
		End If

		Dim successed = DBHelper.CheckLogin(username, password)
		If successed Then
			'Set authenticated cookies
			FormsAuthentication.SetAuthCookie(username, True)
			'Redirect to Search page
			Response.Redirect("Search.aspx")
		Else
			lblTip.Text = "Please check your input"
		End If

	End Sub

	Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
		txtUser.Text = ""
		txtPassword.Text = ""
	End Sub
End Class