Public Class ColorProgressTest

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        ColorProgressBar1.ProgressMaximumValue = 100
        NumericUpDown1.Maximum = 100
        ColorProgressBar1.Value = 0
        NumericUpDown1.Value = 0
        ColorProgressBar1.IsGlossy = True
        CheckBox1.CheckState = CheckState.Checked
        ColorProgressBar1.ShowBorder = True
        CheckBox2.CheckState = CheckState.Checked
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        ' Aktualisiert den Wert der Fortschrittsanzeige, wenn der NumericUpDown-Wert geändert wird.
        ColorProgressBar1.Value = CInt(NumericUpDown1.Value)
    End Sub

    Private Sub CheckBox1_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckStateChanged
        Select Case CType(sender, CheckBox).CheckState
            Case CheckState.Checked
                ColorProgressBar1.IsGlossy = True
            Case CheckState.Unchecked
                ColorProgressBar1.IsGlossy = False
        End Select
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Select Case CType(sender, CheckBox).CheckState
            Case CheckState.Checked
                ColorProgressBar1.ShowBorder = True
            Case CheckState.Unchecked
                ColorProgressBar1.ShowBorder = False
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ColorDialog1.Color = ColorProgressBar1.BorderColor
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            ColorProgressBar1.BorderColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ColorDialog1.Color = ColorProgressBar1.BarColor
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            ColorProgressBar1.BarColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ColorDialog1.Color = ColorProgressBar1.EmptyColor
        If ColorDialog1.ShowDialog = DialogResult.OK Then
            ColorProgressBar1.EmptyColor = ColorDialog1.Color
        End If
    End Sub

End Class
