Imports System.Reflection

Namespace Management

    Public Class AddinRef

        Private ReadOnly _hostAsm As Assembly

        Public Sub New(path As String, hostAsm As Assembly)
            Me.Path = path
            _hostAsm = hostAsm
        End Sub

        Public Property Path As String

        Public ReadOnly Property Addin As Addin
            Get
                Return New Addin(Path, _hostAsm)
            End Get
        End Property

    End Class

End Namespace