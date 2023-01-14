Imports DeveloperCore.Addins
Imports DeveloperCore.Addins.Management

Friend Module Program

    Public Sub Main()
        Dim mngr As FileSystemManager
        If Environment.OSVersion.Platform = PlatformID.Win32NT Then
            mngr = New FileSystemManager("C:\CodingCool\DeveloperCore\Addins")
        Else
            mngr = New FileSystemManager("/home/canadiancoder/addins")
        End If
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