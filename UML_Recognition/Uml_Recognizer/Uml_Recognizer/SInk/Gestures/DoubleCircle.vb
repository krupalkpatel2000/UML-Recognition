Imports Microsoft.Ink
Imports System.Text.RegularExpressions

Public Class DoubleCircle
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub

    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "DoubleCircle"
    End Sub


    Protected Overrides Function Recognize() As Boolean
        Return _
            StrokeInfo.StrokeStatistics.StartEndProximity < CLOSED_PROXIMITY _
            AndAlso _
            ( _
                ( _
                    StrokeInfo.StrokeStatistics.StopPoints >= 2 _
                    AndAlso StrokeInfo.StrokeStatistics.StopPoints <= 5 _
                ) _
                OrElse StrokeInfo.StrokeStatistics.StopPoints >= 8 _
            ) _
            AndAlso StrokeInfo.StrokeStatistics.Square > 0.35 _
            AndAlso StrokeInfo.StrokeStatistics.Square < 0.65 _
            AndAlso StrokeInfo.IsMatch(Vectors.StartTick & Vectors.Rights & _
                Vectors.Downs & Vectors.Lefts & Vectors.Ups & Vectors.Rights & _
                Vectors.Downs & Vectors.Lefts & Vectors.Ups & Vectors.EndTick, _
                90, True, True)
    End Function

End Class
