Imports Microsoft.Ink
Imports System.Text.RegularExpressions

Public Class Triangle
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub

    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "Triangle"
    End Sub


    Protected Overrides Function Recognize() As Boolean
        Return _
            StrokeInfo.StrokeStatistics.StartEndProximity < CLOSED_PROXIMITY _
            AndAlso _
            ( _
                ( _
                    StrokeInfo.StrokeStatistics.StopPoints >= 3 _
                    AndAlso StrokeInfo.Stroke.PolylineCusps.Length >= 3 _
                ) _
                OrElse StrokeInfo.StrokeStatistics.StopPoints >= 8 _
            ) _
            AndAlso StrokeInfo.IsMatch(Vectors.StartTick & _
                Vectors.LeftDowns & Vectors.Rights & Vectors.LeftUps & _
                Vectors.EndTick, 120, False, False)
    End Function

End Class
