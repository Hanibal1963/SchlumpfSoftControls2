Imports SchlumpfSoft.Controls.NotifyFormControl

Public Class Form1

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.ComboBox_Design.SelectedIndex = 0
        Me.ComboBox_Style.SelectedIndex = 0
        Me.NumericUpDown_ShowTime.Value = CDec(Me.NotifyForm1.ShowTime / 1000)  ' Startwert für Anzeigezeit
        Me.TextBox_Title.Text = Me.NotifyForm1.Title ' Startwert für Titelzeile
        Me.TextBox_Message.Text = Me.NotifyForm1.Message ' Startwert für Meldungstext

    End Sub

    Private Sub ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Style.SelectedIndexChanged, ComboBox_Design.SelectedIndexChanged
        Dim selindex As Integer = CType(sender, ComboBox).SelectedIndex  ' Auswahl merken
        Select Case True
            Case sender Is Me.ComboBox_Design
                Select Case selindex ' Design ändern
                    Case 0
                        Me.NotifyForm1.Design = NotifyForm.NotifyFormDesign.Bright ' Helles Desing
                    Case 1
                        Me.NotifyForm1.Design = NotifyForm.NotifyFormDesign.Colorful ' Farbiges Desing
                    Case 2
                        Me.NotifyForm1.Design = NotifyForm.NotifyFormDesign.Dark ' Dunkles Desing
                End Select
            Case sender Is Me.ComboBox_Style
                Select Case selindex  ' Style ändern
                    Case 0
                        Me.NotifyForm1.Style = NotifyForm.NotifyFormStyle.Information  ' Infosymbol
                    Case 1
                        Me.NotifyForm1.Style = NotifyForm.NotifyFormStyle.Question ' Fragesymbol
                    Case 2
                        Me.NotifyForm1.Style = NotifyForm.NotifyFormStyle.CriticalError ' Fehlersymbol
                    Case 3
                        Me.NotifyForm1.Style = NotifyForm.NotifyFormStyle.Exclamation  ' Hinweissymbol
                End Select
        End Select
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Title.TextChanged, TextBox_Message.TextChanged
        Dim text As String = CType(sender, TextBox).Text
        Select Case True
            Case sender Is Me.TextBox_Title
                Me.NotifyForm1.Title = text ' Titelzeile ändern
            Case sender Is Me.TextBox_Message
                Me.NotifyForm1.Message = text  ' Mitteilungstext ändern
        End Select
    End Sub

    Private Sub NumericUpDown_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown_ShowTime.ValueChanged
        Me.NotifyForm1.ShowTime = CInt(CType(sender, NumericUpDown).Value * 1000)
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles Button_Show.Click
        Me.NotifyForm1.Show()
    End Sub

End Class
