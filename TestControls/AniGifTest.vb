

Public Class AniGifTest

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'Rahmenstiel
        AniGif1.BorderStyle = System.Windows.Forms.BorderStyle.None
        ComboBoxBorderStyle.SelectedIndex = 0
        'Anzeigeart
        AniGif1.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.SizeMode.Normal
        ComboBoxSizeMode.SelectedIndex = 0
        'Anzeigegeschwindigkeit
        NumericUpDownFramesPerSecund.Value = AniGif1.FramesPerSecond
        'Zoomfaktor
        NumericUpDownZoomfaktor.Value = AniGif1.ZoomFactor
        'Autostart
        CheckBoxAutostart.Checked = AniGif1.AutoPlay
    End Sub

    Private Sub ComboBoxBorderStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxBorderStyle.SelectedIndexChanged
        Select Case ComboBoxBorderStyle.SelectedIndex
            Case 0
                AniGif1.BorderStyle = System.Windows.Forms.BorderStyle.None
            Case 1
                AniGif1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Case 2
                AniGif1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        End Select
    End Sub

    Private Sub ComboBoxSizeMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSizeMode.SelectedIndexChanged
        Select Case ComboBoxSizeMode.SelectedIndex
            Case 0
                AniGif1.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.SizeMode.Normal
            Case 1
                AniGif1.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.SizeMode.CenterImage
            Case 2
                AniGif1.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.SizeMode.Zoom
            Case 3
                AniGif1.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.SizeMode.Fill
        End Select
    End Sub

    Private Sub NumericUpDownFramesPerSecund_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownFramesPerSecund.ValueChanged
        AniGif1.FramesPerSecond = NumericUpDownFramesPerSecund.Value
    End Sub

    Private Sub NumericUpDownZoomfaktor_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownZoomfaktor.ValueChanged
        AniGif1.ZoomFactor = NumericUpDownZoomfaktor.Value
    End Sub

    Private Sub CheckBoxCustomDisplaySpeed_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCustomDisplaySpeed.CheckedChanged
        AniGif1.CustomDisplaySpeed = CheckBoxCustomDisplaySpeed.Checked
    End Sub

    Private Sub CheckBoxAutostart_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAutostart.CheckedChanged
        AniGif1.AutoPlay = CheckBoxAutostart.Checked
    End Sub

End Class
