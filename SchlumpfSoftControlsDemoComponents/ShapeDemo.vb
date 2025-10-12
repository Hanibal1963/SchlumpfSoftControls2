
Imports SchlumpfSoft.Controls.ShapeControl

Public Class ShapeDemo

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.ComboBox_ShapeModus.SelectedIndex = 0
        Me.ComboBox_LineStart.SelectedIndex = 0
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_ShapeModus.SelectedIndexChanged, ComboBox_LineStart.SelectedIndexChanged
        Dim index As Integer = CType(sender, ComboBox).SelectedIndex
        Select Case True
            Case sender Is ComboBox_ShapeModus ' Form des Shapes schalten
                Select Case index
                    Case 0
                        Me.Shape1.ShapeModus = ShapeModes.HorizontalLine
                    Case 1
                        Me.Shape1.ShapeModus = ShapeModes.VerticalLine
                    Case 2
                        Me.Shape1.ShapeModus = ShapeModes.DiagonalLine
                    Case 3
                        Me.Shape1.ShapeModus = ShapeModes.Rectangle
                    Case 4
                        Me.Shape1.ShapeModus = ShapeModes.FilledRectangle
                    Case 5
                        Me.Shape1.ShapeModus = ShapeModes.Ellipse
                    Case 6
                        Me.Shape1.ShapeModus = ShapeModes.FilledEllipse
                End Select
            Case sender Is ComboBox_LineStart ' Startpunkt der Linie schalten
                Select Case index
                    Case 0
                        Me.Shape1.DiagonalLineModus = DiagonalLineModes.TopLeftToBottomRight
                    Case 1
                        Me.Shape1.DiagonalLineModus = DiagonalLineModes.BottomLeftToTopRight
                End Select
        End Select
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles Button_LineColor.Click, Button_FillColor.Click
        Dim result As DialogResult
        Select Case True
            Case sender Is Button_LineColor ' Linienfarbe wählen
                With Me.ColorDialog1
                    .Color = Me.Shape1.LineColor
                    result = .ShowDialog(Me)
                    If result = DialogResult.OK Then Me.Shape1.LineColor = .Color
                End With
            Case sender Is Button_FillColor ' Füllfarbe wählen
                With Me.ColorDialog1
                    .Color = Me.Shape1.FillColor
                    result = .ShowDialog(Me)
                    If result = DialogResult.OK Then Me.Shape1.FillColor = .Color
                End With
        End Select
    End Sub

    Private Sub NumericUpDown_LineWidth_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_LineWidth.ValueChanged
        Dim selvalue As Decimal = CType(sender, NumericUpDown).Value ' Breite der Linie oder des Rahmens schalten
        Me.Shape1.LineWidth = selvalue
    End Sub

End Class
