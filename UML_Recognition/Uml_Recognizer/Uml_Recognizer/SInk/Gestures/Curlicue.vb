Imports Microsoft.Ink
Imports System.Text.RegularExpressions

Public Class Curlicue
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub

    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "Curlicue"
    End Sub

    Protected Overrides Function Recognize() As Boolean
        Return _
            StrokeInfo.StrokeStatistics.StartEndProximity > CLOSED_PROXIMITY _
            AndAlso StrokeInfo.IsMatch(Vectors.StartTick + Vectors.RightUps + _
                Vectors.LeftUps + Vectors.LeftDowns + Vectors.RightDowns + _
                Vectors.RightUps + "(LU,|L,|U,)*" & Vectors.End, 0, False, False)
    End Function

End Class
