Imports System.Xml
Imports System.Xml.Xsl
Imports System.IO

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'Load From XML file(template)
		Dim XmlContent = File.ReadAllText(Server.MapPath("/Data/template.xml"))
		'Get UserName and Bank Account
		XmlContent = XmlContent.Replace("{0}", Request.Params("user"))
		'’XmlContent = XmlContent.Replace("{1}", Request.Params("account"))

		'Create new xml/xsl tranformer
		Dim transform = New System.Xml.Xsl.XslCompiledTransform()
		'Load xml file
		Dim XmlDoc = New System.Xml.XmlDocument()
		XmlDoc.LoadXml(XmlContent)
		'Load xslt file
		transform.Load(Server.MapPath("/Data/Salary.xslt"))
		'Transform and output
		transform.Transform(XmlDoc, Nothing, Response.Output)
	End Sub

End Class