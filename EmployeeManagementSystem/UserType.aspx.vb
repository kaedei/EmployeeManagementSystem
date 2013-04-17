Partial Public Class UserType
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not Page.IsPostBack Then
			'Load Data
			'User name
			Dim id = Session("id")
			If id Is Nothing OrElse String.IsNullOrEmpty(id.ToString()) Then
				Response.Redirect("Search.aspx")
			End If

			Dim username = id.ToString()
			'Current login user. Check his access right
			If DBHelper.IsInRole(Page.User.Identity.Name, Role.ADMIN) OrElse _
			 DBHelper.IsInRole(Page.User.Identity.Name, Role.HR) Then
				'User Type
				Dim r = DBHelper.GetUserRoleID(username)
				lstRole.SelectedIndex = r
				'Disabled
				chkDisabled.Checked = DBHelper.GetUserDisabled(username)
			Else
				Response.Redirect("Details.aspx?id=" + username)
			End If
		End If
	End Sub

	Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
		Dim username = Session("id").ToString()
		'save Role
		DBHelper.UpdateUserRole(username, lstRole.SelectedIndex)
		'save Disabled
		DBHelper.UpdateUserDisabled(username, chkDisabled.Checked)
		'Redirect to details page
		Response.Redirect("Details.aspx?id=" + username)
	End Sub

	Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		Response.Redirect("Details.aspx?id=" + Session("id").ToString())
	End Sub
End Class