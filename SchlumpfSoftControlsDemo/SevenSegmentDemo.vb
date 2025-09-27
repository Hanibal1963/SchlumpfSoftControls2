
Public Class SevenSegmentDemo
    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        ' Anzahl der eingebaren Zeichen der Textboxen begrenzen
        Me.TextBoxSingle.MaxLength = 1
        Me.TextBoxMulti.MaxLength = Me.MultiDigit1.DigitCount
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxSingle.TextChanged, TextBoxMulti.TextChanged
        Dim str As String = CType(sender, TextBox).Text
        Select Case True
            Case sender Is Me.TextBoxSingle
                Me.SingleDigit1.DigitValue = str
            Case sender Is Me.TextBoxMulti
                Me.MultiDigit1.Value = str
        End Select
    End Sub

End Class
