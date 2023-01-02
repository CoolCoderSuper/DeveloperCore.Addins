Imports System.Reflection

'TODO: Dependencies
'TODO: Isolation
Public Class Addin

    Public Sub New(assembly As String, hostAsm As Assembly)
        Me.Assembly = assembly
        Dim addinAsm As Assembly = Reflection.Assembly.LoadFrom(assembly)
        Dim addinDescriptor As AddinAttribute = addinAsm.GetCustomAttribute(Of AddinAttribute)
        If addinDescriptor Is Nothing Then Throw New Exception("The assembly does not have the required 'AddinAttribute'")
        Name = addinDescriptor.Name
        Author = addinDescriptor.Author
        Version = addinDescriptor.Version
        Description = addinDescriptor.Description
        HostServices = hostAsm.GetHostServices
        Services = addinAsm.GetServices
        For Each serv As Service In Services
            If Not HostServices.ContainsKey(serv.Service) Then Throw New Exception($"Could not find service {serv.Service}")
            Dim impl As Type = serv.Implementation
            If impl Is Nothing Then Throw New Exception($"Could not find type {serv.Implementation}")
            Dim objService As Type = HostServices(serv.Service)
            If Not objService.IsAssignableFrom(impl) Then Throw New Exception("Invalid type")
        Next
    End Sub

    Private _dctServices As New Dictionary(Of Service, Object)
    Public ReadOnly Property Name As String
    Public ReadOnly Property Version As Version
    Public ReadOnly Property Description As String
    Public ReadOnly Property Author As String
    Public ReadOnly Property Assembly As String
    Public ReadOnly Property Services As List(Of Service)
    Friend ReadOnly Property HostServices As Dictionary(Of String, Type)

    Public Function GetService(Of T)() As T
        Dim servAttr As ServiceAttribute = GetType(T).GetCustomAttribute(Of ServiceAttribute)
        If servAttr Is Nothing Then Throw New Exception("The type does not have the required 'ServiceAttribute'")
        Dim serv As Service = Services.FirstOrDefault(Function(x) x.Service = servAttr.Name)
        If Not Services.Contains(serv) Then Throw New Exception("The service was not implemented in the addin")
        If Not _dctServices.ContainsKey(serv) Then
            Dim objAssembly As Assembly = Reflection.Assembly.LoadFrom(Assembly)
            _dctServices.Add(serv, objAssembly.CreateInstance(serv.Implementation.FullName))
        End If
        Return _dctServices(serv)
    End Function

End Class