Imports System.Reflection

'TODO: Dependencies
'TODO: Isolation
'TODO: Addin manager
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
        For Each serv As Type In Services
            Dim objService As Type = serv.GetCustomAttribute(Of ServiceImplAttribute).ServiceType
            If Not HostServices.Contains(objService) Then Throw New Exception($"Could not find service {serv.Name}")
            If Not objService.IsAssignableFrom(serv) Then Throw New Exception("Invalid type")
        Next
    End Sub

    Private _dctServices As New Dictionary(Of Type, Object)
    Public ReadOnly Property Name As String
    Public ReadOnly Property Version As Version
    Public ReadOnly Property Description As String
    Public ReadOnly Property Author As String
    Public ReadOnly Property Assembly As String
    Public ReadOnly Property Services As List(Of Type)
    Friend ReadOnly Property HostServices As List(Of Type)

    Public Function GetService(Of T)() As T
        Dim servAttr As ServiceAttribute = GetType(T).GetCustomAttribute(Of ServiceAttribute)
        If servAttr Is Nothing Then Throw New Exception("The type does not have the required 'ServiceAttribute'")
        Dim serv As Type = Services.FirstOrDefault(Function(x) x.GetCustomAttribute(Of ServiceImplAttribute).ServiceType Is GetType(T))
        If Not Services.Contains(serv) Then Throw New Exception("The service was not implemented in the addin")
        If Not _dctServices.ContainsKey(serv) Then
            Dim objAssembly As Assembly = Reflection.Assembly.LoadFrom(Assembly)
            _dctServices.Add(serv, objAssembly.CreateInstance(serv.FullName))
        End If
        Return _dctServices(serv)
    End Function

End Class