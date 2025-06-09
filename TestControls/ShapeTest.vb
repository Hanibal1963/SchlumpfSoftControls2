

Imports SchlumpfSoft.Controls.ShapeControl

Public Class ShapeTest

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        PictureBox1.Image = My.Resources.Papa_Schlumpf_08
        ComboBox1.SelectedIndex = 0
        NumericUpDown1.Value = 1
        Shape1.LineWidth = 1
        Shape1.ShapeModus = ShapeModes.HorizontalLine
        ComboBox2.SelectedIndex = 0
        Shape1.DiagonalLineModus = DiagonalLineModes.BottomLeftToTopRight
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case CType(sender, ComboBox).SelectedIndex
            Case 0 : Shape1.ShapeModus = ShapeModes.HorizontalLine
            Case 1 : Shape1.ShapeModus = ShapeModes.VerticalLine
            Case 2 : Shape1.ShapeModus = ShapeModes.DiagonalLine
            Case 3 : Shape1.ShapeModus = ShapeModes.Rectangle
            Case 4 : Shape1.ShapeModus = ShapeModes.Ellipse
            Case 5 : Shape1.ShapeModus = ShapeModes.FilledRectangle
            Case 6 : Shape1.ShapeModus = ShapeModes.FilledEllipse
        End Select
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Select Case CType(sender, ComboBox).SelectedIndex
            Case 0 : Shape1.DiagonalLineModus = DiagonalLineModes.BottomLeftToTopRight
            Case 1 : Shape1.DiagonalLineModus = DiagonalLineModes.TopLeftToBottomRight
        End Select
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        Shape1.LineWidth = CType(sender, NumericUpDown).Value
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ColorDialog1.Color = Shape1.LineColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            Shape1.LineColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ColorDialog1.Color = Shape1.FillColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            Shape1.FillColor = ColorDialog1.Color
        End If
    End Sub

End Class
