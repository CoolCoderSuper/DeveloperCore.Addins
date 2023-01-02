Imports System.Reflection

'TODO: Cleanup exceptions
'TODO: More validation
Public Class Loader

    Public Shared Function Load(json As String, hostAsm As Assembly) As Addin
        Dim objAddin As Addin = Text.Json.JsonSerializer.Deserialize(Of Addin)(json)
        objAddin.HostServices = hostAsm.GetServices
        Dim addinAsm As Assembly = Assembly.LoadFrom(objAddin.Assembly)
        For Each serv As Service In objAddin.Services
            Dim impl As Type = addinAsm.GetType(serv.Implementation, False)
            If Not objAddin.HostServices.ContainsKey(serv.Service) Then Throw New Exception($"Could not find service {serv.Service}")
            If impl Is Nothing Then Throw New TypeAccessException($"Could not find type: {serv.Implementation}")
            If Not impl.IsClass Then Throw New TypeAccessException($"The type {serv.Implementation} was found but was not a class")
            Dim objService As Type = objAddin.HostServices(serv.Service)
            If Not objService.IsAssignableFrom(impl) Then Throw New Exception("Invalid type")
        Next
        Return objAddin
    End Function

End Class