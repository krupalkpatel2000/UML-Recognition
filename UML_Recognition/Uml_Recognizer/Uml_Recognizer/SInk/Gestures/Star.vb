Imports Microsoft.Ink
Imports System.Text.RegularExpressions

Public Class Star
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub

    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "Star"
    End Sub


    Protected Overrides Function Recognize() As Boolean
        Return _
            StrokeInfo.StrokeStatistics.StartEndProximity < CLOSED_PROXIMITY _
            AndAlso _
            ( _
                StrokeInfo.StrokeStatistics.StopPoints = 6 _
                OrElse StrokeInfo.StrokeStatistics.StopPoints >= 8 _
            ) _
            AndAlso StrokeInfo.IsMatch(Vectors.StartTick & Vectors.Rights & _
                Vectors.CornerTick & Vectors.LeftDowns & Vectors.CornerTick & _
                Vectors.Ups & Vectors.CornerTick & Vectors.Downs & _
                Vectors.CornerTick & Vectors.Lefts & Vectors.End, 72, True, True)

    End Function

End Class
