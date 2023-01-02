Imports DeveloperCore.Addins
Imports TestApp

<ServiceImpl(GetType(MessageSender))>
Public Class AdvMessageSender
    Inherits MessageSender

    Public Overrides Sub Init()

    End Sub

    Public Overrides Sub Send(m As String)
        Debug.WriteLine(m)
        MyBase.Send(m)
    End Sub

End Class