Imports System.Reflection

'TODO: Share common resources across addins
'TODO: Properly dispose stuff
Namespace Management

    Public Interface IManager

        Sub Init(hostAsm As Assembly)

        ReadOnly Property Addins As List(Of AddinRef)

        Sub Install(addin As AddinRef)

        Sub Uninstall(addin As AddinRef)

    End Interface

End Namespace