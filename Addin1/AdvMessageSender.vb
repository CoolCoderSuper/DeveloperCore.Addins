Imports TestApp

Public Class AdvMessageSender
    Inherits MessageSender

    Public Overrides Sub Send(m As String)
        Debug.WriteLine(m)
        MyBase.Send(m)
    End Sub
End Class