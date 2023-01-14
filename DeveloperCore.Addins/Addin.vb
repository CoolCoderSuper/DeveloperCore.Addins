Imports System.Reflection

''' <summary>
''' Represents an addin.
''' </summary>
Public Class Addin

    ''' <summary>
    ''' Creates an instance of an addin.<br/>
    ''' Causes the addin assembly to loaded and services to be detected and validated.
    ''' </summary>
    ''' <param name="assembly">The path to the addin.</param>
    ''' <param name="hostAsm">The host assembly, used to detect available services.</param>
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

    Private ReadOnly _dctServices As New Dictionary(Of Type, Object)

    ''' <summary>
    ''' The name of the addin.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String

    ''' <summary>
    ''' The addins version.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Version As Version

    ''' <summary>
    ''' The addins description.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Description As String

    ''' <summary>
    ''' The addins author.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Author As String

    ''' <summary>
    ''' The location of the assembly.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Assembly As String

    ''' <summary>
    ''' The services that are contained in this addin.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Services As List(Of Type)

    Friend ReadOnly Property HostServices As List(Of Type)

    ''' <summary>
    ''' Gets the service of the specified type.
    ''' </summary>
    ''' <typeparam name="T">The type of the service to load.</typeparam>
    ''' <returns></returns>
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