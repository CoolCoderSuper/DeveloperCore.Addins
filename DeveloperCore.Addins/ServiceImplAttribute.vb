''' <summary>
''' Indicates that this class implements a service.
''' </summary>
<AttributeUsage(AttributeTargets.Class)>
Public Class ServiceImplAttribute
    Inherits Attribute
    Public ReadOnly Property ServiceType As Type

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="servType">The service that was implemented.</param>
    Public Sub New(servType As Type)
        ServiceType = servType
    End Sub

End Class