Imports Microsoft.Ink
Imports System.Text.RegularExpressions

Public Class Square
    Inherits CustomGesture

    Public Sub New()
        MyClass.New(Nothing)
    End Sub

    Public Sub New(ByVal strokeInfo As StrokeInfo)
        MyBase.New(strokeInfo)
        Name = "Square"
    End Sub


    Protected Overrides Function Recognize() As Boolean
        Dim rectangle As New Rectangle(StrokeInfo)

        With StrokeInfo.StrokeStatistics
            Return StrokeInfo.MaxDifference(New Double() {.Right, .Left, .Up, .Down}) < 0.1 _
                AndAlso rectangle.IsMatch
        End With

    End Function

End Class
