Imports System.Reflection

Namespace Management

    Public Interface IManager

        Sub Init(hostAsm As Assembly)

        ReadOnly Property Addins As List(Of AddinRef)

        Sub Install(addin As AddinRef)

        Sub Uninstall(addin As AddinRef)

    End Interface

End Namespace