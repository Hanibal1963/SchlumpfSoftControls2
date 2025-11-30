
Public Class Form1

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        Me.InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.NumericUpDownProgressValue.Value = 0
        Me.NumericUpDownProgressValue.Minimum = 0
        Me.NumericUpDownProgressValue.Maximum = 100
        Me.NumericUpDownProgressValue.Increment = 10
        Me.ColorProgressBar1.Value = 0
        Me.ColorProgressBar1.ProgressMaximumValue = 100
    End Sub

    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles _
        CheckBoxShowGliss.CheckedChanged, CheckBoxShowBorder.CheckedChanged
        Select Case True
            Case sender Is Me.CheckBoxShowGliss : Me.ColorProgressBar1.IsGlossy = Me.CheckBoxShowGliss.Checked
            Case sender Is Me.CheckBoxShowBorder : Me.ColorProgressBar1.ShowBorder = Me.CheckBoxShowBorder.Checked
        End Select
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles ButtonEmptyColor.Click, ButtonBorderColor.Click, ButtonBarColor.Click
        Select Case True
            Case sender Is Me.ButtonEmptyColor : Me.ChangeEmptyColor()
            Case sender Is Me.ButtonBorderColor : Me.ChangeBorderColor()
            Case sender Is Me.ButtonBarColor : Me.ChangeBarColor()
        End Select
    End Sub

    Private Sub ChangeBarColor()
        If Me.ColorDialog1.ShowDialog(Me) = DialogResult.OK Then
            Me.ColorProgressBar1.BarColor = Me.ColorDialog1.Color
        End If
    End Sub

    Private Sub ChangeBorderColor()
        If Me.ColorDialog1.ShowDialog(Me) = DialogResult.OK Then
            Me.ColorProgressBar1.BorderColor = Me.ColorDialog1.Color
        End If
    End Sub

    Private Sub ChangeEmptyColor()
        If Me.ColorDialog1.ShowDialog(Me) = DialogResult.OK Then
            Me.ColorProgressBar1.EmptyColor = Me.ColorDialog1.Color
        End If
    End Sub

    Private Sub NumericUpDown_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownProgressValue.ValueChanged
        Me.ColorProgressBar1.Value = CInt(Me.NumericUpDownProgressValue.Value)
    End Sub

End Class
