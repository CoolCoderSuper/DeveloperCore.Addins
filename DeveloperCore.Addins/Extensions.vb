Imports System.Reflection
Imports System.Runtime.CompilerServices

Friend Module Extensions

    <Extension>
    Public Function GetHostServices(asm As Assembly) As List(Of Type)
        Return asm.GetTypes().Where(Function(x) x.CustomAttributes.Any(Function(y) y.AttributeType Is GetType(ServiceAttribute))).ToList
    End Function

    <Extension>
    Public Function GetServices(asm As Assembly) As List(Of Type)
        Return asm.GetTypes().Where(Function(x) x.CustomAttributes.Any(Function(y) y.AttributeType Is GetType(ServiceImplAttribute))).ToList
    End Function

End Module