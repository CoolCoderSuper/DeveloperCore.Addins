Imports DeveloperCore.Addins
Imports DeveloperCore.Addins.Management

Friend Module Program

    Public Sub Main()
        Addin.AddDependency(Of Writer)()
        Addin.AddDependency(Of CoolThing)()
        Addin.AddDependency(Of IThinker, DumbThinker)()
        Addin.AddDependencyAsSingleton(Of MessageCache)()
        Dim mngr = If(Environment.OSVersion.Platform = PlatformID.Win32NT,
            New FileSystemManager("C:\CodingCool\DeveloperCore\Addins"),
            New FileSystemManager("/home/canadiancoder/addins"))
        mngr.Init(Reflection.Assembly.GetEntryAssembly)
        Dim objAddin As Addin = mngr.Addins.First.Addin
        objAddin.GetService(Of IMessageSender).Send("Hi")
        objAddin.GetService(Of MessageSender).Send("Bye")
        Dim cache As MessageCache = Addin.GetSingleton(Of MessageCache)()
    End Sub

End Module