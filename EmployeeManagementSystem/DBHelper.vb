Imports System.Data.SqlClient

Public Class DBHelper

	'DB Connection cache
	Private Shared _websecconnection As SqlConnection
	Private Shared _empdataconnection As SqlConnection


	''' <summary>
	''' Get a database connection from pool
	''' </summary>
	''' <param name="db">Database name</param>
	''' <returns></returns>
	''' <remarks></remarks>
	Private Shared Function GetConnection(ByVal db As Database) As SqlConnection

		Select Case db
			Case Database.EmployeeData
				Dim connstring = ConfigurationManager.ConnectionStrings("EmpDataConnectionString").ConnectionString
				If _empdataconnection Is Nothing Then
					_empdataconnection = New SqlConnection()
				End If
				If (_empdataconnection.State <> ConnectionState.Open) Then
					_empdataconnection.ConnectionString = connstring
				End If
				Return _empdataconnection
			Case Database.WebSecurity
				Dim connstring = ConfigurationManager.ConnectionStrings("WebSecConnectionString").ConnectionString
				If _websecconnection Is Nothing Then
					_websecconnection = New SqlConnection()
				End If
				If _websecconnection.State <> ConnectionState.Open Then
					_websecconnection.ConnectionString = connstring
				End If
				Return _websecconnection
		End Select

		Return Nothing
	End Function


	''' <summary>
	''' Check for login
	''' </summary>
	''' <param name="user">User name</param>
	''' <param name="pw">Password</param>
	''' <remarks>Return True if login successed</remarks>
	Public Shared Function CheckLogin(ByVal user As String, ByVal pw As String) As Boolean
		Try
			'Get connection string from web.config

			Using connection = GetConnection(Database.WebSecurity)

				'Open Connection
				If connection.State <> ConnectionState.Open Then
					connection.Open()
				End If

				'Create SQL Command (use stored procedure)
				Dim command As New SqlCommand("SP_CHECK_LOGIN", connection)
				command.CommandType = CommandType.StoredProcedure

				'Add parameters
				command.Parameters.Add("@UserName", SqlDbType.NChar, 10)
				command.Parameters("@UserName").Value = user
				command.Parameters.Add("@Password", SqlDbType.NChar, 20)
				command.Parameters("@Password").Value = pw
				command.Parameters.Add("@Successed", SqlDbType.Int)
				command.Parameters("@Successed").Direction = ParameterDirection.Output

				'Run SP
				command.ExecuteNonQuery()
				Dim successed = Int32.Parse(command.Parameters("@Successed").Value)

				Return successed > 0
			End Using
		Catch
			Return False
		End Try

	End Function

	''' <summary>
	''' Search for users
	''' </summary>
	''' <param name="username">User name</param>
	''' <param name="firstname">First name</param>
	''' <param name="lastname">Last name</param>
	''' <param name="email">Email</param>
	''' <param name="department">Department</param>
	''' <param name="project">Project</param>
	''' <param name="mobile">Mobile phone number</param>
	''' <param name="officeaddress">Office address</param>
	''' <returns>A DataTable object</returns>
	''' <remarks></remarks>
	Public Shared Function GetUserList(ByVal username As String, ByVal firstname As String, _
	 ByVal lastname As String, ByVal email As String, ByVal department As String, _
	 ByVal project As String, ByVal mobile As String, ByVal officeaddress As String) As DataTable
		'create database connection
		Dim connection = GetConnection(Database.EmployeeData)
		If connection.State <> ConnectionState.Open Then
			connection.Open()
		End If

		'Create command
		Dim cmd = New SqlCommand("SP_SEARCH_USER", connection)
		cmd.CommandType = CommandType.StoredProcedure
		'add parameters
		cmd.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserName").Value = username

		cmd.Parameters.Add("@FirstName", SqlDbType.NChar, 20)
		cmd.Parameters("@FirstName").Value = firstname

		cmd.Parameters.Add("@LastName", SqlDbType.NChar, 20)
		cmd.Parameters("@LastName").Value = lastname

		cmd.Parameters.Add("@Email", SqlDbType.NChar, 20)
		cmd.Parameters("@Email").Value = email

		cmd.Parameters.Add("@Department", SqlDbType.NChar, 20)
		cmd.Parameters("@Department").Value = department

		cmd.Parameters.Add("@Project", SqlDbType.NChar, 20)
		cmd.Parameters("@Project").Value = project

		cmd.Parameters.Add("@Mobile", SqlDbType.NChar, 20)
		cmd.Parameters("@Mobile").Value = mobile

		cmd.Parameters.Add("@OfficeAddress", SqlDbType.NChar, 20)
		cmd.Parameters("@OfficeAddress").Value = officeaddress

		'Create dataset
		Dim ds = New DataSet()
		'create dataadapter
		Dim adapter = New SqlDataAdapter(cmd)
		'Fill dataset
		adapter.Fill(ds)
		Return ds.Tables(0)
	End Function

	''' <summary>
	''' Check if the user is in a specific User Role (like ADMIN,HR)
	''' </summary>
	''' <param name="username">User Name</param>
	''' <param name="r">User Role Name</param>
	''' <returns>If the user is in role, return True, otherwise False</returns>
	''' <remarks></remarks>
	Public Shared Function IsInRole(ByVal username As String, ByVal r As Role)
		'Get Connection
		Dim connection = GetConnection(Database.WebSecurity)
		If (connection.State <> ConnectionState.Open) Then
			connection.Open()
		End If

		Dim cmd = New SqlCommand("SP_IS_IN_ROLES", connection)
		cmd.CommandType = CommandType.StoredProcedure

		cmd.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserName").Value = username

		cmd.Parameters.Add("@UserRoleName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserRoleName").Value = r.ToString()

		cmd.Parameters.Add("@InRoles", SqlDbType.Int)
		cmd.Parameters("@InRoles").Direction = ParameterDirection.Output

		cmd.ExecuteNonQuery()
		Dim inroles = Int32.Parse(cmd.Parameters("@InRoles").Value)
		Return inroles > 0
	End Function

	''' <summary>
	''' Get the Role of an user (like ADMIN, HR)
	''' </summary>
	''' <param name="username">User Name</param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Shared Function GetUserRoleName(ByVal username As String) As Role
		'Get Connection
		Dim connection = GetConnection(Database.WebSecurity)
		If (connection.State <> ConnectionState.Open) Then
			connection.Open()
		End If

		Dim cmd = New SqlCommand("SP_GET_USERROLE", connection)
		cmd.CommandType = CommandType.StoredProcedure

		cmd.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserName").Value = username

		cmd.Parameters.Add("@UserRoleName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserRoleName").Direction = ParameterDirection.Output

		cmd.ExecuteNonQuery()
		Dim rolename = cmd.Parameters("@UserRoleName").Value.ToString()

		Dim r As Role
		Select Case rolename.Trim()
			Case "ADMIN"
				r = Role.ADMIN
			Case "MANAGER"
				r = Role.MANAGER
			Case "HR"
				r = Role.HR
			Case Else
				r = Role.EMPLOYEE
		End Select

		Return r
	End Function

	''' <summary>
	''' Get user's information
	''' </summary>
	''' <param name="username">User Name</param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Shared Function GetInformation(ByVal username As String) As UserInformation
		Dim info As New UserInformation()
		'Get Connection
		Dim connection = GetConnection(Database.EmployeeData)
		If (connection.State <> ConnectionState.Open) Then
			connection.Open()
		End If

		'Get informations 
		Dim cmd = New SqlCommand("SP_GET_INFORMATION_ADV", connection)
		cmd.CommandType = CommandType.StoredProcedure

		cmd.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserName").Value = username

		Using reader = cmd.ExecuteReader()
			If reader.HasRows Then
				reader.Read()
				'fill into UserInformation object
				info.EmpID = reader.GetString(0).Trim()
				info.FirstName = reader.GetString(1).Trim()
				info.LastName = reader.GetString(2).Trim()
				info.Department = reader.GetString(3).Trim()
				info.Project = reader.GetString(4).Trim()
				info.Email = reader.GetString(5).Trim()
				info.Mobile = reader.GetString(6).Trim()
				info.OfficeAddress = reader.GetString(7).Trim()
				info.Grade = reader.GetInt32(8)
				info.Manager = reader.GetString(9).Trim()
			End If
		End Using
		Return info
	End Function

	''' <summary>
	''' Get User's bank account number
	''' </summary>
	''' <param name="username"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Shared Function GetBankAccount(ByVal username As String) As String
		'Get Connection
		Dim connection = GetConnection(Database.EmployeeData)
		If (connection.State <> ConnectionState.Open) Then
			connection.Open()
		End If

		'Get informations 
		Dim cmd = New SqlCommand("SP_GET_BANKACCOUNT", connection)
		cmd.CommandType = CommandType.StoredProcedure

		cmd.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserName").Value = username

		cmd.Parameters.Add("@BankAccount", SqlDbType.NChar, 20)
		cmd.Parameters("@BankAccount").Direction = ParameterDirection.Output

		cmd.ExecuteNonQuery()
		Dim account = cmd.Parameters("@BankAccount").Value
		If (account IsNot Nothing) Then
			Return account.ToString().Trim()
		Else
			Return ""
		End If
	End Function

	''' <summary>
	''' Update User's bank account number
	''' </summary>
	''' <param name="username"></param>
	''' <param name="newbankaccount"></param>
	''' <remarks></remarks>
	Public Shared Sub UpdateBankAccount(ByVal username As String, ByVal newbankaccount As String)
		'Get Connection
		Dim connection = GetConnection(Database.EmployeeData)
		If (connection.State <> ConnectionState.Open) Then
			connection.Open()
		End If

		'Get informations 
		Dim cmd = New SqlCommand("SP_UPDATE_BANKACCOUNT", connection)
		cmd.CommandType = CommandType.StoredProcedure

		cmd.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserName").Value = username

		cmd.Parameters.Add("@BankAccount", SqlDbType.NChar, 20)
		cmd.Parameters("@BankAccount").Value = newbankaccount

		cmd.ExecuteNonQuery()
	End Sub


	''' <summary>
	''' Update User's information
	''' </summary>
	''' <remarks></remarks>
	Public Shared Sub UpdateInformation(ByVal username As String, ByVal empID As String, ByVal firstName As String, _
	 ByVal lastName As String, ByVal department As String, ByVal project As String, _
	 ByVal email As String, ByVal mobile As String, ByVal officeAddress As String, _
	 ByVal manager As String, ByVal grade As Integer)
		'create database connection
		Dim connection = GetConnection(Database.EmployeeData)
		If connection.State <> ConnectionState.Open Then
			connection.Open()
		End If

		'Create command
		Dim cmd = New SqlCommand("SP_UPDATE_INFORMATION", connection)
		cmd.CommandType = CommandType.StoredProcedure
		'add parameters
		cmd.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserName").Value = username

		cmd.Parameters.Add("@EmpID", SqlDbType.NChar, 20)
		cmd.Parameters("@EmpID").Value = empID

		cmd.Parameters.Add("@FirstName", SqlDbType.NChar, 20)
		cmd.Parameters("@FirstName").Value = firstName

		cmd.Parameters.Add("@LastName", SqlDbType.NChar, 20)
		cmd.Parameters("@LastName").Value = lastName

		cmd.Parameters.Add("@Department", SqlDbType.NChar, 20)
		cmd.Parameters("@Department").Value = department

		cmd.Parameters.Add("@Project", SqlDbType.NChar, 20)
		cmd.Parameters("@Project").Value = project

		cmd.Parameters.Add("@Email", SqlDbType.NChar, 20)
		cmd.Parameters("@Email").Value = email

		cmd.Parameters.Add("@Mobile", SqlDbType.NChar, 20)
		cmd.Parameters("@Mobile").Value = mobile

		cmd.Parameters.Add("@OfficeAddress", SqlDbType.NChar, 20)
		cmd.Parameters("@OfficeAddress").Value = officeAddress

		cmd.Parameters.Add("@Manager", SqlDbType.NChar, 10)
		cmd.Parameters("@Manager").Value = manager

		cmd.Parameters.Add("@Grade", SqlDbType.Int)
		cmd.Parameters("@Grade").Value = grade

		'Run stored procedure
		cmd.ExecuteNonQuery()

	End Sub

	''' <summary>
	''' Add a new user
	''' </summary>
	''' <remarks>
	''' The data will be write into WebSec and EmpData dbs. 
	''' The default password is same as user name.
	''' The default Role is EMPLOYEE
	''' </remarks>
	Public Shared Sub AddUserInformation(ByVal username As String, ByVal empID As String, ByVal firstName As String, _
	ByVal lastName As String, ByVal department As String, ByVal project As String, _
	ByVal email As String, ByVal mobile As String, ByVal officeAddress As String, _
	ByVal manager As String, ByVal grade As Integer)
		'create database connection(Web Security)
		Dim connectionSec = GetConnection(Database.WebSecurity)
		If connectionSec.State <> ConnectionState.Open Then
			connectionSec.Open()
		End If

		'Create command
		Dim cmdSec = New SqlCommand("SP_ADD_USER", connectionSec)
		cmdSec.CommandType = CommandType.StoredProcedure
		'add parameters
		cmdSec.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmdSec.Parameters("@UserName").Value = username
		cmdSec.Parameters.Add("@Password", SqlDbType.NChar, 20)
		'Default password is same as user name
		cmdSec.Parameters("@Password").Value = username
		'Default Employee type is EMPLOYEE
		cmdSec.Parameters.Add("@UserRoleID", SqlDbType.Int)
		cmdSec.Parameters("@UserRoleID").Value = Role.EMPLOYEE
		'Add new user in WebSec
		cmdSec.ExecuteNonQuery()


		'create database connection(Employee Data)
		Dim connection = GetConnection(Database.EmployeeData)
		If connection.State <> ConnectionState.Open Then
			connection.Open()
		End If

		'Create command
		Dim cmd = New SqlCommand("SP_ADD_INFORMATION", connection)
		cmd.CommandType = CommandType.StoredProcedure
		'add parameters
		cmd.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmd.Parameters("@UserName").Value = username

		cmd.Parameters.Add("@EmpID", SqlDbType.NChar, 20)
		cmd.Parameters("@EmpID").Value = empID

		cmd.Parameters.Add("@FirstName", SqlDbType.NChar, 20)
		cmd.Parameters("@FirstName").Value = firstName

		cmd.Parameters.Add("@LastName", SqlDbType.NChar, 20)
		cmd.Parameters("@LastName").Value = lastName

		cmd.Parameters.Add("@Department", SqlDbType.NChar, 20)
		cmd.Parameters("@Department").Value = department

		cmd.Parameters.Add("@Project", SqlDbType.NChar, 20)
		cmd.Parameters("@Project").Value = project

		cmd.Parameters.Add("@Email", SqlDbType.NChar, 20)
		cmd.Parameters("@Email").Value = email

		cmd.Parameters.Add("@Mobile", SqlDbType.NChar, 20)
		cmd.Parameters("@Mobile").Value = mobile

		cmd.Parameters.Add("@OfficeAddress", SqlDbType.NChar, 20)
		cmd.Parameters("@OfficeAddress").Value = officeAddress

		cmd.Parameters.Add("@Manager", SqlDbType.NChar, 10)
		cmd.Parameters("@Manager").Value = manager

		cmd.Parameters.Add("@Grade", SqlDbType.Int)
		cmd.Parameters("@Grade").Value = grade

		'Run stored procedure
		cmd.ExecuteNonQuery()

	End Sub

	Public Shared Sub UpdateUserRole(ByVal username As String, ByVal r As Role)
		'create database connection(Web Security)
		Dim connectionSec = GetConnection(Database.WebSecurity)
		If connectionSec.State <> ConnectionState.Open Then
			connectionSec.Open()
		End If

		'Create command
		Dim cmdSec = New SqlCommand("SP_SET_USERROLEID", connectionSec)
		cmdSec.CommandType = CommandType.StoredProcedure
		'add parameters
		cmdSec.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmdSec.Parameters("@UserName").Value = username
		cmdSec.Parameters.Add("@UserRoleID", SqlDbType.Int)
		Dim i As Int32 = r
		cmdSec.Parameters("@UserRoleID").Value = i

		cmdSec.ExecuteNonQuery()

	End Sub

	Public Shared Sub UpdateUserDisabled(ByVal username As String, ByVal disabled As Boolean)
		'create database connection(Web Security)
		Dim connectionSec = GetConnection(Database.WebSecurity)
		If connectionSec.State <> ConnectionState.Open Then
			connectionSec.Open()
		End If

		'Create command
		Dim cmdSec = New SqlCommand("SP_SET_DISABLE", connectionSec)
		cmdSec.CommandType = CommandType.StoredProcedure
		'add parameters
		cmdSec.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmdSec.Parameters("@UserName").Value = username
		cmdSec.Parameters.Add("@Disabled", SqlDbType.Int)
		If disabled Then
			cmdSec.Parameters("@Disabled").Value = 1
		Else
			cmdSec.Parameters("@Disabled").Value = Nothing
		End If

		cmdSec.ExecuteNonQuery()

	End Sub

	Public Shared Function GetUserRoleID(ByVal username As String) As Integer
		'create database connection(Web Security)
		Dim connectionSec = GetConnection(Database.WebSecurity)
		If connectionSec.State <> ConnectionState.Open Then
			connectionSec.Open()
		End If

		'Create command
		Dim cmdSec = New SqlCommand("SP_GET_USERROLEID", connectionSec)
		cmdSec.CommandType = CommandType.StoredProcedure
		'add parameters
		cmdSec.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmdSec.Parameters("@UserName").Value = username
		cmdSec.Parameters.Add("@UserRoleID", SqlDbType.Int)
		cmdSec.Parameters("@UserRoleID").Direction = ParameterDirection.Output

		cmdSec.ExecuteNonQuery()

		Dim id = Int32.Parse(cmdSec.Parameters("@UserRoleID").Value)

		Return id

	End Function

	Public Shared Function GetUserDisabled(ByVal username As String) As Boolean
		'create database connection(Web Security)
		Dim connectionSec = GetConnection(Database.WebSecurity)
		If connectionSec.State <> ConnectionState.Open Then
			connectionSec.Open()
		End If

		'Create command
		Dim cmdSec = New SqlCommand("SP_GET_DISABLE", connectionSec)
		cmdSec.CommandType = CommandType.StoredProcedure
		'add parameters
		cmdSec.Parameters.Add("@UserName", SqlDbType.NChar, 10)
		cmdSec.Parameters("@UserName").Value = username
		cmdSec.Parameters.Add("@Disabled", SqlDbType.Int)
		cmdSec.Parameters("@Disabled").Direction = ParameterDirection.Output

		cmdSec.ExecuteNonQuery()

		Dim id = cmdSec.Parameters("@Disabled").Value

		If id Is DBNull.Value Then
			Return False
		Else
			Return True
		End If
	End Function

End Class


Public Enum Database
	EmployeeData
	WebSecurity
End Enum

Public Enum Role
	ADMIN
	EMPLOYEE
	MANAGER
	HR
End Enum