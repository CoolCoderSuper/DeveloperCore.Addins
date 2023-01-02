Imports System.Reflection
Imports System.Runtime.CompilerServices

Friend Module Extensions

    <Extension>
    Public Function GetServices(asm As Assembly) As Dictionary(Of String, Type)
        Return asm.GetTypes().Where(Function(x) x.CustomAttributes.Any(Function(y) y.AttributeType Is GetType(ServiceAttribute))).ToDictionary(Function(x) x.GetCustomAttribute(Of ServiceAttribute)(True).Name, Function(x) x)
    End Function

End Module