Imports DeveloperCore.Addins
Imports DeveloperCore.Addins.Management

Friend Module Program

    Public Sub Main()
        'Dim objAddin As New Addin("C:\CodingCool\Code\Projects\DeveloperCore.Addins\Addin1\bin\Debug\net7.0\Addin1.dll", Assembly.GetEntryAssembly)
        Dim mngr As New FileSystemManager("C:\CodingCool\DeveloperCore\Addins")
        mngr.Init(Reflection.Assembly.GetEntryAssembly)
        Dim objAddin As Addin = mngr.Addins.First.Addin
        objAddin.GetService(Of IMessageSender).Send("Hi")
        objAddin.GetService(Of MessageSender).Init()
        objAddin.GetService(Of MessageSender).Send("Bye")
    End Sub

End Module

<Service()>
Public Interface IMessageSender
    Property LastMessage As String

    Sub Send(m As String)

End Interface

<Service()>
Public MustInherit Class MessageSender

    Public MustOverride Sub Init()

    Public Overridable Sub Send(m As String)
        Console.WriteLine(m)
    End Sub

End Class