Imports System.Reflection

Namespace Management

    ''' <summary>
    ''' Allows the user to manage addins.
    ''' </summary>
    Public Interface IManager

        ''' <summary>
        ''' Performs any initialization necessary.
        ''' </summary>
        ''' <param name="hostAsm"></param>
        Sub Init(hostAsm As Assembly)

        ''' <summary>
        ''' The addins available to the manager.
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property Addins As List(Of AddinRef)

        ''' <summary>
        ''' Installs an addin.
        ''' </summary>
        ''' <param name="addin">The addin to install.</param>
        Sub Install(addin As AddinRef)

        ''' <summary>
        ''' Uninstalls an addin.
        ''' </summary>
        ''' <param name="addin">The addin to uninstall.</param>
        Sub Uninstall(addin As AddinRef)

    End Interface

End Namespace