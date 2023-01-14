''' <summary>
''' Provides meta-data about the addin.
''' </summary>
<AttributeUsage(AttributeTargets.Assembly)>
Public Class AddinAttribute
    Inherits Attribute

    Public Sub New(name As String, version As String, description As String, author As String)
        Me.Name = name
        Me.Version = System.Version.Parse(version)
        Me.Description = description
        Me.Author = author
    End Sub

    Public ReadOnly Property Name As String
    Public ReadOnly Property Version As Version
    Public ReadOnly Property Description As String
    Public ReadOnly Property Author As String

End Class