Imports TestApp

Public Class ConsoleMessageSender
    Implements IMessageSender

    Public Property LastMessage As String Implements IMessageSender.LastMessage

    Public Sub Send(m As String) Implements IMessageSender.Send
        LastMessage = m
        Console.WriteLine($"Message: {m}")
    End Sub
End Class