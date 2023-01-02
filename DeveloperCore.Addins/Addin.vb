Imports System.Reflection

Public Class Addin
    Private _dctServices As New Dictionary(Of Service, Object)
    Public Property Name As String
    Public Property Version As String
    Public Property Description As String
    Public Property Author As String
    Public Property Assembly As String
    Public Property Services As List(Of Service)
    Friend Property HostServices As Dictionary(Of String, Type)

    'TODO: Better exceptions
    'TODO: Think of validation
    Public Function GetService(Of T)() As T
        Dim servName As String = HostServices.FirstOrDefault(Function(x) x.Value Is GetType(T)).Key
        Dim serv As Service = Services.FirstOrDefault(Function(x) x.Service = servName)
        If Not Services.Contains(serv) Then Throw New Exception("Invalid service")
        If Not _dctServices.ContainsKey(serv) Then
            Dim objAssembly As Assembly = Reflection.Assembly.LoadFrom(Assembly)
            _dctServices.Add(serv, objAssembly.CreateInstance(serv.Implementation))
        End If
        Return _dctServices(serv)
    End Function

End Class