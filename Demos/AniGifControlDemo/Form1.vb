Imports SchlumpfSoft.Controls.AniGifControl.AniGif

Public Class Form1

    Private _Ani As Integer = 0

    Public Sub New()
        Me.InitializeComponent() ' Dieser Aufruf ist für den Designer erforderlich.
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.ComboBoxAnsicht.SelectedIndex = 0 ' Standardwert für Anzeigemodus
        Me.NumericUpDownFramesPerSecound.Value = Me.AniGif.FramesPerSecond ' Standardwert für benutzerdefinierte Anzeigegeschwindigkeit 
        Me.NumericUpDownZoomFactor.Value = Me.AniGif.ZoomFactor ' Standardwert für Zoom
        Me.CheckBoxAutoplay.Checked = Me.AniGif.AutoPlay ' kein automatischer Start
        Me.ButtonBack.Enabled = False
        Me.ChangeAni()
    End Sub

    Private Sub AniGif_NoAnimation(sender As Object, e As EventArgs) Handles AniGif.NoAnimation
        Dim unused = MsgBox("Das Bild kann nicht animiert werden!", MsgBoxStyle.Information, "AniGifControl")
    End Sub

    Private Sub NumericUpDownValueChanged(sender As Object, e As EventArgs) Handles _
        NumericUpDownZoomFactor.ValueChanged, NumericUpDownFramesPerSecound.ValueChanged
        Dim numericupdownvalue As Integer = CInt(CType(sender, NumericUpDown).Value)
        Select Case True
            ' Benutzerdefinierte Anzeigegeschwindigkeit einstellen
            Case sender Is Me.NumericUpDownFramesPerSecound : Me.AniGif.FramesPerSecond = numericupdownvalue
            ' Zoom ändern
            Case sender Is Me.NumericUpDownZoomFactor : Me.AniGif.ZoomFactor = numericupdownvalue
        End Select
    End Sub

    Private Sub CheckBoxCheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCustomDisplaySpeed.CheckedChanged, CheckBoxAutoplay.CheckedChanged
        Dim checkstate As Boolean = CType(sender, CheckBox).Checked
        Select Case True
            ' Autoplay umschalten
            Case sender Is Me.CheckBoxAutoplay : Me.AniGif.AutoPlay = checkstate
            ' Benutzerdefinierte Geschwindigkeit umschalten
            Case sender Is Me.CheckBoxCustomDisplaySpeed : Me.AniGif.CustomDisplaySpeed = checkstate
        End Select
    End Sub

    Private Sub ComboBoxAnsichtSelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxAnsicht.SelectedIndexChanged
        'Anzeigemodus umschalten
        Select Case CType(sender, ComboBox).SelectedIndex
            Case 0 : Me.AniGif.GifSizeMode = ImageSizeMode.Normal
            Case 1 : Me.AniGif.GifSizeMode = ImageSizeMode.CenterImage
            Case 2 : Me.AniGif.GifSizeMode = ImageSizeMode.Zoom
            Case 3 : Me.AniGif.GifSizeMode = ImageSizeMode.Fill
        End Select
    End Sub

    Private Sub ButtonClick(sender As Object, e As EventArgs) Handles ButtonStop.Click, ButtonStart.Click, ButtonForward.Click, ButtonBack.Click
        Select Case True
            Case sender Is Me.ButtonBack : Me.ShowBackAni() ' Vorherige Animation anzeigen
            Case sender Is Me.ButtonForward : Me.ShowNextAni() ' Nächste Animation anzeigen
            Case sender Is Me.ButtonStop : Me.AniGif.StopAnimation() ' Animation stoppen
            Case sender Is Me.ButtonStart : Me.AniGif.StartAnimation() ' Animation starten
        End Select
        Me.ChangeAni()   ' Animation umschalten
    End Sub

    Private Sub ShowNextAni()
        If Me._Ani < 20 Then Me._Ani += 1 ' Animationsnummer erhöhen solange < 20
        Me.ButtonBack.Enabled = True  ' Zurück-Button aktivieren
        If Me._Ani = 20 Then Me.ButtonForward.Enabled = False ' Vor-Button deaktivieren wenn Animationsnummer = 20
    End Sub

    Private Sub ShowBackAni()
        If Me._Ani > 0 Then Me._Ani -= 1 ' Animationsnummer verringern solange > 0
        Me.ButtonForward.Enabled = True ' Vor-Button aktivieren
        If Me._Ani = 0 Then Me.ButtonBack.Enabled = False ' Zurück-Button deaktivieren wenn Animationsnummer = 0
    End Sub

    Private Sub ChangeAni()
        'Animationsnummer anzeigen
        Select Case Me._Ani
            Case Is <> 0 : Me.LabelAni.Text = $"Animation Nr.: {Me._Ani}"
            Case Else : Me.LabelAni.Text = "Standardanimation"
        End Select
        'Animation schalten
        Select Case Me._Ani
            Case Is <> 0 : Me.AniGif.Gif = CType(My.Resources.ResourceManager.GetObject("Anim" & CStr(100 + Me._Ani - 1)), Bitmap)
            Case Else : Me.AniGif.Gif = Nothing
        End Select
    End Sub

End Class
