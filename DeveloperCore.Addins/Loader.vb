Imports System.Reflection

'TODO: Cleanup exceptions
Public Class Loader

    Public Shared Function Load(json As String, objApp As App) As Addin
        Dim objAddin As Addin = Text.Json.JsonSerializer.Deserialize(Of Addin)(json)
        objAddin.App = objApp
        Dim objAssembly As Assembly = Assembly.LoadFrom(objAddin.Assembly)
        For Each serv As Service In objAddin.Services
            Dim impl As Type = objAssembly.GetType(serv.Implementation, False)
            If Not objApp.Services.Any(Function(x) x.Name = serv.Service) Then Throw New Exception($"Could not find service {serv.Service}")
            If impl Is Nothing Then Throw New TypeAccessException($"Could not find type: {serv.Implementation}")
            If Not impl.IsClass Then Throw New TypeAccessException($"The type {serv.Implementation} was found but was not a class")
            Dim objService As AppService = objApp.Services.First(Function(x) x.Name = serv.Service)
            If Not objService.Interface.IsAssignableFrom(impl) Then Throw New Exception("Invalid type")
        Next
        Return objAddin
    End Function

End Class