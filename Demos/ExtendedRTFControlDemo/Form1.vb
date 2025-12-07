Public Class Form1

    ' Zentraler Event-Handler für alle Format-Schaltflächen und Menüeinträge.
    ' Über "sender" wird ermittelt, welche Aktion auf die aktuelle Auswahl im RTFTB angewendet werden soll.
    ' Das Muster "Select Case True" fasst mehrere If/Else-Verzweigungen kompakt zusammen.
    Private Sub ToolStripButtonClick(sender As Object, e As EventArgs) Handles _
        ToolStripButtonUnderline.Click, ToolStripButtonToggleBullets.Click, ToolStripButtonTextRight.Click, ToolStripButtonTextLeft.Click,
        ToolStripButtonTextCenter.Click, ToolStripButtonStrikeout.Click, ToolStripButtonLeftIndentUp.Click, ToolStripButtonLeftIndentDown.Click,
        ToolStripButtonItalic.Click, ToolStripButtonForeColor.Click, ToolStripButtonFontSizeUp.Click, ToolStripButtonFontSizeDown.Click,
        ToolStripButtonFontFormat.Click, ToolStripButtonDelFormat.Click, ToolStripButtonBold.Click, ToolStripButtonBackColor.Click

        ' Welche Schaltfläche wurde geklickt?
        Select Case True

            Case sender Is Me.ToolStripButtonBold
                ' markierten Text fett oder nicht fett
                Me.RTFTB.ToggleBold()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                Me.SetBoldCheckedState()

            Case sender Is Me.ToolStripButtonItalic
                ' markierten Text kursiv oder nicht kursiv
                Me.RTFTB.ToggleItalic()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                Me.SetItalicCheckedState()

            Case sender Is Me.ToolStripButtonUnderline
                ' markierten Text unterstrichen oder nicht unterstrichen
                Me.RTFTB.ToggleUnderline()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                Me.SetUnderlineCheckedState()

            Case sender Is Me.ToolStripButtonStrikeout
                ' markierten Text durchgestrichen oder nicht durchgestrichen
                Me.RTFTB.ToggleStrikeout()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                Me.SetStrikeoutCheckedState()

            Case sender Is Me.ToolStripButtonToggleBullets
                ' Aufzählungszeichen ein- oder ausschalten
                Me.RTFTB.ToggleBullet()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                Me.SetBulletCheckedState()

            Case sender Is Me.ToolStripButtonFontSizeUp
                ' markierte Schriftgröße um 1 Punkt vergrößern, max. 72
                ' Begrenzung schützt vor zu großen Schriftgraden
                If Me.RTFTB.SelectionFontSize < 72 Then
                    Dim newSize As Single = CSng(Me.RTFTB.SelectionFontSize + 1)
                    If newSize > 72 Then newSize = 72
                    Me.RTFTB.SelectionFontSize = newSize
                End If

            Case sender Is Me.ToolStripButtonFontSizeDown
                ' markierte Schriftgröße um 1 Punkt verkleinern, min. 8
                ' Begrenzung schützt vor zu kleinen Schriftgraden
                If Me.RTFTB.SelectionFontSize > 8 Then
                    Dim newSize As Single = CSng(Me.RTFTB.SelectionFontSize - 1)
                    If newSize < 8 Then newSize = 8
                    Me.RTFTB.SelectionFontSize = newSize
                End If

            Case sender Is Me.ToolStripButtonForeColor
                ' Farbe für markierten Text auswählen
                ' Der Dialog wird mit der aktuellen Vordergrundfarbe der Auswahl vorbelegt.
                Using colorDialog As New ColorDialog()
                    colorDialog.Color = Me.RTFTB.SelectionForeColor
                    If colorDialog.ShowDialog(Me) = DialogResult.OK Then
                        Me.RTFTB.SelectionColor = colorDialog.Color
                    End If
                End Using

            Case sender Is Me.ToolStripButtonBackColor
                ' Hintergrundfarbe für markierten Text auswählen
                ' Der Dialog wird mit der aktuellen Hintergrundfarbe der Auswahl vorbelegt.
                Using colorDialog As New ColorDialog()
                    colorDialog.Color = Me.RTFTB.SelectionBackColor
                    If colorDialog.ShowDialog(Me) = DialogResult.OK Then
                        Me.RTFTB.SelectionBackColor = colorDialog.Color
                    End If
                End Using

            Case sender Is Me.ToolStripButtonDelFormat
                ' alle Formatierungen des markierten Textes entfernen
                Me.RTFTB.ClearFormatting()
                ' Nach dem Entfernen alle UI-Checkboxen anhand der Selektion aktualisieren
                Me.SetBoldCheckedState()
                Me.SetItalicCheckedState()
                Me.SetUnderlineCheckedState()
                Me.SetStrikeoutCheckedState()
                Me.SetBulletCheckedState()
                Me.SetAlignmentCheckedState()

            Case sender Is Me.ToolStripButtonLeftIndentUp
                ' linken Einzug um 10 Pixel vergrößern (RichTextBox-Indents sind pixelbasiert)
                Me.RTFTB.SelectionIndent += 10

            Case sender Is Me.ToolStripButtonLeftIndentDown
                ' linken Einzug um 10 Pixel verkleinern, min. 0 (kein negativer Einzug)
                Me.RTFTB.SelectionIndent = Math.Max(0, Me.RTFTB.SelectionIndent - 10)

            Case sender Is Me.ToolStripButtonTextLeft
                ' Text linksbündig
                Me.RTFTB.SelectionAlignment = HorizontalAlignment.Left
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                Me.SetAlignmentCheckedState()

            Case sender Is Me.ToolStripButtonTextCenter
                ' Text zentriert
                Me.RTFTB.SelectionAlignment = HorizontalAlignment.Center
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                Me.SetAlignmentCheckedState()

            Case sender Is Me.ToolStripButtonTextRight
                ' Text rechtsbündig
                Me.RTFTB.SelectionAlignment = HorizontalAlignment.Right
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                Me.SetAlignmentCheckedState()

            Case sender Is Me.ToolStripButtonFontFormat
                ' Schriftart auswählen
                ' Der Dialog wird mit der aktuellen Auswahl-Schrift vorbelegt.
                Using fontDialog As New FontDialog()
                    fontDialog.Font = Me.RTFTB.SelectionFont
                    If fontDialog.ShowDialog(Me) = DialogResult.OK Then
                        Me.RTFTB.SelectionFont = fontDialog.Font
                    End If
                End Using

        End Select

    End Sub

    ' Event-Handler, der aufgerufen wird, wenn sich die Textauswahl im RTFTB ändert.
    Private Sub RTFTB_SelectionChanged(sender As Object, e As EventArgs) Handles RTFTB.SelectionChanged
        ' Wenn sich die Textauswahl ändert, müssen die UI-Checkboxen den neuen Status der Auswahl widerspiegeln.
        Me.SetBoldCheckedState()
        Me.SetItalicCheckedState()
        Me.SetUnderlineCheckedState()
        Me.SetStrikeoutCheckedState()
        Me.SetBulletCheckedState()
        Me.SetAlignmentCheckedState()
    End Sub

    ' Synchronisiert den Checked-Status  für Textausrichtung (Alignment).
    Private Sub SetAlignmentCheckedState()
        Select Case Me.RTFTB.SelectionAlignment
            Case HorizontalAlignment.Left
                Me.SetLeftAlignmentCheckedState()
            Case HorizontalAlignment.Center
                Me.SetCenterAlignmentCheckedState()
            Case HorizontalAlignment.Right
                Me.SetRightAlignmentCheckedState()
        End Select
    End Sub

    ' Synchronisiert den Checked-Status für rechtsbündige Textausrichtung (Right).
    Private Sub SetRightAlignmentCheckedState()
        Me.ToolStripButtonTextLeft.Checked = False
        Me.ToolStripButtonTextCenter.Checked = False
        Me.ToolStripButtonTextRight.Checked = True
    End Sub

    ' Synchronisiert den Checked-Status für zentrierte Textausrichtung (Center).
    Private Sub SetCenterAlignmentCheckedState()
        Me.ToolStripButtonTextLeft.Checked = False
        Me.ToolStripButtonTextCenter.Checked = True
        Me.ToolStripButtonTextRight.Checked = False
    End Sub

    ' Synchronisiert den Checked-Status für linksbündige Textausrichtung (Left).
    Private Sub SetLeftAlignmentCheckedState()
        Me.ToolStripButtonTextLeft.Checked = True
        Me.ToolStripButtonTextCenter.Checked = False
        Me.ToolStripButtonTextRight.Checked = False
    End Sub

    ' Synchronisiert den Checked-Status  für Aufzählungszeichen (Bullet).
    Private Sub SetBulletCheckedState()
        Me.ToolStripButtonToggleBullets.Checked = Me.RTFTB.SelectionBullet
    End Sub

    ' Synchronisiert den Checked-Status für Durchstreichung (Strikeout).
    Private Sub SetStrikeoutCheckedState()
        Me.ToolStripButtonStrikeout.Checked = CBool(Me.RTFTB.SelectionStrikeout)
    End Sub

    ' Synchronisiert den Checked-Status für Unterstreichung (Underline).
    Private Sub SetUnderlineCheckedState()
        Me.ToolStripButtonUnderline.Checked = CBool(Me.RTFTB.SelectionUnderline)
    End Sub

    ' Synchronisiert den Checked-Status für Kursivschrift (Italic).
    Private Sub SetItalicCheckedState()
        Me.ToolStripButtonItalic.Checked = CBool(Me.RTFTB.SelectionItalic)
    End Sub

    ' Synchronisiert den Checked-Status für Fettschrift (Bold).
    Private Sub SetBoldCheckedState()
        Me.ToolStripButtonBold.Checked = CBool(Me.RTFTB.SelectionBold)
    End Sub

End Class
