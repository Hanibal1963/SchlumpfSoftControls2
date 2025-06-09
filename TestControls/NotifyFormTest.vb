
Imports SchlumpfSoft.Controls.NotifyFormControl

Public Class NotifyFormTest

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        ComboBox1.SelectedIndex = 0
        NotifyForm1.Design = NotifyFormDesign.Bright
        ComboBox2.SelectedIndex = 0
        NotifyForm1.Style = NotifyFormStyle.Information
        NumericUpDown1.Minimum = 0
        NumericUpDown1.Maximum = 10000
        TextBox1.Text = $"Titel"
        NotifyForm1.Title = TextBox1.Text
        TextBox2.Text = $"Mitteilung"
        NotifyForm1.Message = TextBox2.Text
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case CType(sender, ComboBox).SelectedIndex
            Case 0 : NotifyForm1.Design = NotifyFormDesign.Bright
            Case 1 : NotifyForm1.Design = NotifyFormDesign.Dark
            Case 2 : NotifyForm1.Design = NotifyFormDesign.Colorful
        End Select
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Select Case CType(sender, ComboBox).SelectedIndex
            Case 0 : NotifyForm1.Style = NotifyFormStyle.Information
            Case 1 : NotifyForm1.Style = NotifyFormStyle.Question
            Case 2 : NotifyForm1.Style = NotifyFormStyle.Exclamation
            Case 3 : NotifyForm1.Style = NotifyFormStyle.CriticalError
        End Select
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        NotifyForm1.ShowTime = CInt(CType(sender, NumericUpDown).Value)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        NotifyForm1.Title = CType(sender, TextBox).Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        NotifyForm1.Message = CType(sender, TextBox).Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        NotifyForm1.Show()
    End Sub

End Class
