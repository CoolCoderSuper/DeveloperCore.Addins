Imports System.Reflection
Imports System.Runtime.CompilerServices

Friend Module Extensions

    <Extension>
    Public Function GetHostServices(asm As Assembly) As Dictionary(Of String, Type)
        Return asm.GetTypes().Where(Function(x) x.CustomAttributes.Any(Function(y) y.AttributeType Is GetType(ServiceAttribute))).ToDictionary(Function(x) x.GetCustomAttribute(Of ServiceAttribute)(True).Name, Function(x) x)
    End Function

    <Extension>
    Public Function GetServices(asm As Assembly) As List(Of Service)
        Return asm.GetTypes().Where(Function(x) x.CustomAttributes.Any(Function(y) y.AttributeType Is GetType(ServiceImplAttribute))).Select(Function(x) New Service With {.Service = x.GetCustomAttribute(Of ServiceImplAttribute)(True).Name, .Implementation = x}).ToList
    End Function

End Module