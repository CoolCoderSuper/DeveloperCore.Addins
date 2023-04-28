Imports DeveloperCore.Addins
Imports TestApp

<ServiceImpl(GetType(IMessageSender))>
Public Class ConsoleMessageSender
    Implements IMessageSender

    Private ReadOnly _writer As Writer
    Private ReadOnly _cache As MessageCache
    Private ReadOnly _thing As CoolThing

    Public Sub New(thing As CoolThing, writer As Writer, cache As MessageCache)
        _writer = writer
        _cache = cache
        _thing = thing
    End Sub

    Public Sub Send(m As String) Implements IMessageSender.Send
        _writer.Write($"Message: {m}")
        _cache.Messages.Add(m)
        _thing.Go()
    End Sub
End Class