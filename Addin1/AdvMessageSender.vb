Imports DeveloperCore.Addins
Imports TestApp

<ServiceImpl(GetType(MessageSender))>
Public Class AdvMessageSender
    Inherits MessageSender

    Private ReadOnly _thinker As IThinker
    Private ReadOnly _cache As MessageCache

    Public Sub New(thinker As IThinker, cache As MessageCache)
        _thinker = thinker
        _cache = cache
    End Sub

    Public Overrides Sub Send(m As String)
        Debug.WriteLine(m)
        MyBase.Send(m)
        _cache.Messages.Add(m)
        For i As Integer = 0 To Random.Shared.NextInt64(1, 100)
            _thinker.Think()
        Next
    End Sub

End Class