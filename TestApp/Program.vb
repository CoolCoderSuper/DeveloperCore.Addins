Imports DeveloperCore.Addins

Friend Module Program

    Public Sub Main()
        Dim objApp As New App With {.Services = New List(Of AppService) From {New AppService With {.Name = "MessageSender", .[Interface] = GetType(IMessageSender)}, New AppService With {.Name = "OtherMessageSender", .[Interface] = GetType(MessageSender)}}}
        Dim objAddin As Addin = Loader.Load(IO.File.ReadAllText("Addin.json"), objApp)
        objAddin.GetService(Of IMessageSender).Send("Hi")
        objAddin.GetService(Of MessageSender).Send("Bye ")
    End Sub

End Module

Public Interface IMessageSender
    Property LastMessage As String

    Sub Send(m As String)

End Interface

Public MustInherit Class MessageSender

    Public Overridable Sub Send(m As String)
        Console.WriteLine(m)
    End Sub

End Class