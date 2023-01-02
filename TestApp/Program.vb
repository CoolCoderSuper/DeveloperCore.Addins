Imports System.Reflection
Imports DeveloperCore.Addins

Friend Module Program

    Public Sub Main()
        Dim objAddin As New Addin("C:\CodingCool\Code\Projects\DeveloperCore.Addins\Addin1\bin\Debug\net7.0\Addin1.dll", Assembly.GetEntryAssembly)
        objAddin.GetService(Of IMessageSender).Send("Hi")
        objAddin.GetService(Of MessageSender).Send("Bye")
    End Sub

End Module

<Service("MessageSender")>
Public Interface IMessageSender
    Property LastMessage As String

    Sub Send(m As String)

End Interface

<Service("OtherMessageSender")>
Public MustInherit Class MessageSender

    Public MustOverride Sub Init

    Public Overridable Sub Send(m As String)
        Console.WriteLine(m)
    End Sub

End Class