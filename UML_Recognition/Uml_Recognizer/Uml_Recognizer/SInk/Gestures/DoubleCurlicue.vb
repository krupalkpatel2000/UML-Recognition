Imports Microsoft.Ink
Imports System.Text.RegularExpressions

Public Class DoubleCurlicue
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub

    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "DoubleCurlicue"
    End Sub


    Protected Overrides Function Recognize() As Boolean
        Return _
            StrokeInfo.StrokeStatistics.StartEndProximity > CLOSED_PROXIMITY _
            AndAlso StrokeInfo.StrokeStatistics.Diagonal > 0.4 _
            AndAlso StrokeInfo.IsMatch(Vectors.StartTick & Vectors.RightUps & _
                Vectors.LeftUps & Vectors.LeftDowns & Vectors.RightDowns & _
                Vectors.RightUps & Vectors.LeftUps & Vectors.LeftDowns & _
                Vectors.RightDowns & Vectors.RightUps & "(LU,|L,|U,)*" & _
                Vectors.End, 0, False, False)
    End Function

End Class
