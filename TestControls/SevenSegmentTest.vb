

Public Class SevenSegmentTest

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        TextBox1.MaxLength = 1
        TextBox1.Text = String.Empty
        SingleDigit1.ForeColor = Color.Green
        TextBox2.MaxLength = 10
        TextBox2.Text = String.Empty
        MultiDigit1.ForeColor = Color.Green
        MultiDigit1.DigitCount = 10
        MultiDigit1.DigitPadding = New Padding(5, 0, 5, 0)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        SingleDigit1.DigitValue = CType(sender, TextBox).Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        MultiDigit1.Value = CType(sender, TextBox).Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ColorDialog1.Color = SingleDigit1.ForeColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            SingleDigit1.ForeColor = ColorDialog1.Color
            MultiDigit1.ForeColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ColorDialog1.Color = SingleDigit1.InactiveColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            SingleDigit1.InactiveColor = ColorDialog1.Color
            MultiDigit1.InactiveColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ColorDialog1.Color = SingleDigit1.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            SingleDigit1.BackColor = ColorDialog1.Color
            MultiDigit1.BackColor = ColorDialog1.Color
        End If
    End Sub

End Class
