Imports Microsoft.Ink
Imports System.Text.RegularExpressions

Public Class Rectangle
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub

    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "Rectangle"
    End Sub


    Protected Overrides Function Recognize() As Boolean
        Return StrokeInfo.StrokeStatistics.StartEndProximity < CLOSED_PROXIMITY _
            AndAlso _
            ( _
                StrokeInfo.StrokeStatistics.StopPoints = 5 OrElse _
                StrokeInfo.StrokeStatistics.StopPoints >= 8 _
            ) _
            AndAlso StrokeInfo.StrokeStatistics.Square > 0.85 _
            AndAlso Math.Abs(StrokeInfo.StrokeStatistics.Left - StrokeInfo.StrokeStatistics.Right) < 0.15 _
            AndAlso Math.Abs(StrokeInfo.StrokeStatistics.Up - StrokeInfo.StrokeStatistics.Down) < 0.15 _
            AndAlso _
            ( _
                ( _
                    StrokeInfo.IsMatch(Vectors.StartTick & _
                    Vectors.Rights & Vectors.Downs & Vectors.Lefts & Vectors.Ups & _
                    Vectors.EndTick, 90, False, False) _
                ) _
                OrElse _
                ( _
                    StrokeInfo.IsMatch(Vectors.StartTick & _
                    Vectors.Lefts & Vectors.Downs & Vectors.Rights & Vectors.Ups & _
                    Vectors.EndTick, 90, False, False) _
                ) _
            )
    End Function

End Class
