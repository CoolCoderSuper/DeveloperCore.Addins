Partial Public Class Addin
    Private Shared _dependencies As New List(Of AddinDependency)
    Private Shared _singletonCache As New Dictionary(Of AddinDependency, Object)

    ''' <summary>
    ''' Adds a dependency to inject into addins.
    ''' </summary>
    ''' <typeparam name="T">The type of the dependency, must have an empty constructor.</typeparam>
    Public Shared Sub AddDependency(Of T)()
        _dependencies.Add(New AddinDependency With {.IsSingleton = False, .ImplementationType = GetType(T), .PlacementType = GetType(T)})
    End Sub

    ''' <summary>
    ''' Adds a dependency to inject into addins.
    ''' </summary>
    ''' <typeparam name="T">The type that will be available in the service constructor, must be assignable from <typeparamref name="Y"/>.</typeparam>
    ''' <typeparam name="Y">The type of the dependency, must have an empty constructor and must be assignable to <typeparamref name="T"/>.</typeparam>
    Public Shared Sub AddDependency(Of T, Y As T)()
        _dependencies.Add(New AddinDependency With {.IsSingleton = False, .ImplementationType = GetType(Y), .PlacementType = GetType(T)})
    End Sub

    ''' <summary>
    ''' Adds a dependency as a singleton to inject into addins.<br/>
    ''' This will create an instance of the dependency and cache it for the lifetime of the application.
    ''' </summary>
    ''' <typeparam name="T">The type of the dependency, must have an empty constructor.</typeparam>
    Public Shared Sub AddDependencyAsSingleton(Of T)()
        Dim d As AddinDependency = New AddinDependency With {.IsSingleton = True, .ImplementationType = GetType(T), .PlacementType = GetType(T)}
        _dependencies.Add(d)
        GetDependency(d)
    End Sub

    ''' <summary>
    ''' Adds a dependency as a singleton to inject into addins.<br/>
    ''' This will create an instance of the dependency and cache it for the lifetime of the application.
    ''' </summary>
    ''' <typeparam name="T">The type that will be available in the service constructor, must be assignable from <typeparamref name="Y"/>.</typeparam>
    ''' <typeparam name="Y">The type of the dependency, must have an empty constructor and must be assignable to <typeparamref name="T"/>.</typeparam>
    Public Shared Sub AddDependencyAsSingleton(Of T, Y As T)()
        Dim d As AddinDependency = New AddinDependency With {.IsSingleton = True, .ImplementationType = GetType(Y), .PlacementType = GetType(T)}
        _dependencies.Add(d)
        GetDependency(d)
    End Sub

    Friend Shared Function GetDependency(dependency As AddinDependency) As Object
        If dependency.IsSingleton Then
            If _singletonCache.ContainsKey(dependency) Then Return _singletonCache(dependency)
            Dim obj = Activator.CreateInstance(dependency.ImplementationType)
            _singletonCache.Add(dependency, obj)
            Return obj
        Else
            Return Activator.CreateInstance(dependency.ImplementationType)
        End If
    End Function

    ''' <summary>
    ''' Gets the currently cached singleton instance of the specified type.
    ''' </summary>
    ''' <typeparam name="T">The type of the singleton.</typeparam>
    ''' <returns></returns>
    Public Shared Function GetSingleton(Of T)() As T
        Return _singletonCache(_dependencies.First(Function(x) x.PlacementType = GetType(T)))
    End Function
End Class