Imports Microsoft.Ink
Imports System.Text.RegularExpressions

Public Class CheckMark
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub


    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "CheckMark"
    End Sub

    Protected Overrides Function Recognize() As Boolean
        Return _
            StrokeInfo.StrokeStatistics.StartEndProximity > CLOSED_PROXIMITY _
            AndAlso _
            ( _
                StrokeInfo.StrokeStatistics.StopPoints = 3 _
                OrElse StrokeInfo.StrokeStatistics.StopPoints = 2 _
                OrElse StrokeInfo.StrokeStatistics.StopPoints >= 8 _
            ) _
            AndAlso StrokeInfo.StrokeStatistics.RightUp + StrokeInfo.StrokeStatistics.Up >= 0.6 _
            AndAlso StrokeInfo.StrokeStatistics.RightUp + StrokeInfo.StrokeStatistics.Up <= 0.85 _
            AndAlso StrokeInfo.StrokeStatistics.RightDown + StrokeInfo.StrokeStatistics.Down > 0.15 _
            AndAlso StrokeInfo.StrokeStatistics.RightDown + StrokeInfo.StrokeStatistics.Down < 0.4 _
            AndAlso StrokeInfo.IsMatch(Vectors.StartTick & Vectors.RightDowns & _
                Vectors.RightUps & Vectors.EndTick, 0, False, False)
    End Function

End Class
