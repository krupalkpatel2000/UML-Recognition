Public Class Question
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub

    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "Question"
    End Sub

    Protected Overrides Function Recognize() As Boolean
        Return _
            StrokeInfo.StrokeStatistics.StartEndProximity > CLOSED_PROXIMITY _
            AndAlso StrokeInfo.IsMatch(Vectors.StartTick & "(" & Vectors.Lefts & ")*" & _
                Vectors.Ups & Vectors.Rights & Vectors.Downs & Vectors.Lefts & _
                Vectors.Downs & Vectors.EndTick)
    End Function

End Class
