Imports System.IO
Imports System.Reflection

Namespace Management

    Public Class FileSystemManager
        Implements IManager

        Private _hostAsm As Assembly
        Private ReadOnly _path As String

        Public Sub New(path As String)
            _path = path
        End Sub

        Public Sub Init(hostAsm As Assembly) Implements IManager.Init
            _hostAsm = hostAsm
        End Sub

        Public ReadOnly Property Addins As List(Of AddinRef) Implements IManager.Addins
            Get
                Dim l As New List(Of AddinRef)
                For Each addinPath As String In Directory.GetFiles(_path, "*.dll")
                    l.Add(New AddinRef(addinPath, _hostAsm))
                Next
                Return l
            End Get
        End Property

        Public Sub Install(addin As AddinRef) Implements IManager.Install
            File.Copy(addin.Path, Path.Combine(_path, Path.GetFileName(addin.Path)))
        End Sub

        Public Sub Uninstall(addin As AddinRef) Implements IManager.Uninstall
            File.Delete(addin.Path)
        End Sub

    End Class

End Namespace