Imports System.Threading

Public Class DumbThinker
    Implements IThinker
    Private _count As Integer = 0
    Public Sub Think() Implements IThinker.Think
        If _count = 10 Then
            Console.WriteLine("Brain is dead")
        Else
            _count += 1
            Console.Write("I'm dumb, but I'm thinking")
            Thread.Sleep(1000)
            Console.Write(".")
            Thread.Sleep(1000)
            Console.Write(".")
            Thread.Sleep(1000)
            Console.WriteLine(".")
        End If
    End Sub
End Class