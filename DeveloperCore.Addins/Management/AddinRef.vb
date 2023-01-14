Imports System.Reflection

Namespace Management

    ''' <summary>
    ''' Represents an <see cref="Addin"/> to a <see cref="IManager"/>.
    ''' </summary>
    Public Class AddinRef

        Private ReadOnly _hostAsm As Assembly

        Public Sub New(path As String, hostAsm As Assembly)
            Me.Path = path
            _hostAsm = hostAsm
        End Sub

        ''' <summary>
        ''' The path to the addin.
        ''' </summary>
        ''' <returns></returns>
        Public Property Path As String

        ''' <summary>
        ''' Gets the referenced <see cref="Addin"/>, causing it to be loaded.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Addin As Addin
            Get
                Return New Addin(Path, _hostAsm)
            End Get
        End Property

    End Class

End Namespace