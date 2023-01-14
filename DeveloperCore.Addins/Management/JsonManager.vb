Imports System.Reflection

Namespace Management

    ''' <summary>
    ''' Implementation <see cref="IManager"/> using a JSON file.
    ''' </summary>
    Public Class JsonManager
        Implements IManager

        Private ReadOnly _path As String
        Private _hostAsm As Assembly

        Public Sub New(path As String)
            _path = path
        End Sub

        Public ReadOnly Property Addins As List(Of AddinRef) Implements IManager.Addins
            Get
                Dim l As New List(Of AddinRef)
                For Each addinPath As String In Parse()
                    l.Add(New AddinRef(addinPath, _hostAsm))
                Next
                Return l
            End Get
        End Property

        Public Sub Init(hostAsm As Assembly) Implements IManager.Init
            _hostAsm = hostAsm
        End Sub

        Public Sub Install(addin As AddinRef) Implements IManager.Install
            Dim l As List(Of String) = Parse()
            l.Add(addin.Path)
            Save(l)
        End Sub

        Public Sub Uninstall(addin As AddinRef) Implements IManager.Uninstall
            Dim l As List(Of String) = Parse()
            l.Remove(addin.Path)
            Save(l)
        End Sub

        Private Function Parse() As List(Of String)
            Return Text.Json.JsonSerializer.Deserialize(Of List(Of String))(IO.File.ReadAllText(_path))
        End Function

        Private Sub Save(l As List(Of String))
            IO.File.WriteAllText(_path, Text.Json.JsonSerializer.Serialize(l))
        End Sub

    End Class

End Namespace