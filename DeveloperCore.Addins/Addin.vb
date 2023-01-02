Imports System.Reflection

Public Class Addin
    Private _dctServices As New Dictionary(Of Service, Object)
    Public Property Name As String
    Public Property Version As String
    Public Property Description As String
    Public Property Author As String
    Public Property Assembly As String
    Public Property Services As List(Of Service)
    Public Property App As App

    Public Function GetService(Of T)() As T
        Dim objAppServ As AppService = App.Services.FirstOrDefault(Function(x) x.Interface Is GetType(T))
        If objAppServ Is Nothing Then throw New Exception("Invalid service")
        Dim serv As Service = Services.FirstOrDefault(Function(x) x.Service = objAppServ.Name)
        If Not Services.Contains(serv) Then Throw New Exception("Invalid service")
        If Not _dctServices.ContainsKey(serv) Then
            Dim objAssembly As Assembly = Reflection.Assembly.LoadFrom(Assembly)
            _dctServices.Add(serv, objAssembly.CreateInstance(serv.Implementation))
        End If
        Return _dctServices(serv)
    End Function

End Class