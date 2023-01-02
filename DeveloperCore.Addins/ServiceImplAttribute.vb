<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Interface)>
Public Class ServiceImplAttribute
    Inherits Attribute
    Public ReadOnly Property ServiceType As Type

    Public Sub New(servType As Type)
        ServiceType = servType
    End Sub

End Class