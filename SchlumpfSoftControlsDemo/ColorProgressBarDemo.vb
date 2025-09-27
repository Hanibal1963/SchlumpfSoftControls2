Public Class ColorProgressBarDemo

    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowGliss.CheckedChanged, CheckBoxShowBorder.CheckedChanged
        Select Case True
            Case sender Is CheckBoxShowGliss
                ColorProgressBar1.IsGlossy = CheckBoxShowGliss.Checked
            Case sender Is CheckBoxShowBorder
                ColorProgressBar1.ShowBorder = CheckBoxShowBorder.Checked
        End Select
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles ButtonEmptyColor.Click, ButtonBorderColor.Click, ButtonBarColor.Click
        Select Case True
            Case sender Is ButtonEmptyColor
                If ColorDialog1.ShowDialog(Me) = DialogResult.OK Then
                    ColorProgressBar1.EmptyColor = ColorDialog1.Color
                End If
            Case sender Is ButtonBorderColor
                If ColorDialog1.ShowDialog(Me) = DialogResult.OK Then
                    ColorProgressBar1.BorderColor = ColorDialog1.Color
                End If
            Case sender Is ButtonBarColor
                If ColorDialog1.ShowDialog(Me) = DialogResult.OK Then
                    ColorProgressBar1.BarColor = ColorDialog1.Color
                End If
        End Select
    End Sub

    Private Sub NumericUpDown_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownProgressValue.ValueChanged
        ColorProgressBar1.Value = CInt(NumericUpDownProgressValue.Value)
    End Sub

End Class
