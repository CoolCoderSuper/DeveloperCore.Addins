Imports DeveloperCore.Addins

<Service()>
Public MustInherit Class MessageSender

    Public Overridable Sub Send(m As String)
        Console.WriteLine(m)
    End Sub

End Class
