' *************************************************************************************************
' ExtendedRTFDemo.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Public Class ExtendedRTFDemo

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
                RTFTB.ToggleBold()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                SetBoldCheckedState()

            Case sender Is Me.ToolStripButtonItalic
                ' markierten Text kursiv oder nicht kursiv
                RTFTB.ToggleItalic()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                SetItalicCheckedState()

            Case sender Is Me.ToolStripButtonUnderline
                ' markierten Text unterstrichen oder nicht unterstrichen
                RTFTB.ToggleUnderline()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                SetUnderlineCheckedState()

            Case sender Is Me.ToolStripButtonStrikeout
                ' markierten Text durchgestrichen oder nicht durchgestrichen
                RTFTB.ToggleStrikeout()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                SetStrikeoutCheckedState()

            Case sender Is Me.ToolStripButtonToggleBullets
                ' Aufzählungszeichen ein- oder ausschalten
                RTFTB.ToggleBullet()
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                SetBulletCheckedState()

            Case sender Is Me.ToolStripButtonFontSizeUp
                ' markierte Schriftgröße um 1 Punkt vergrößern, max. 72
                ' Begrenzung schützt vor zu großen Schriftgraden
                If RTFTB.SelectionFontSize < 72 Then
                    Dim newSize As Single = CSng(RTFTB.SelectionFontSize + 1)
                    If newSize > 72 Then newSize = 72
                    RTFTB.SelectionFontSize = newSize
                End If

            Case sender Is Me.ToolStripButtonFontSizeDown
                ' markierte Schriftgröße um 1 Punkt verkleinern, min. 8
                ' Begrenzung schützt vor zu kleinen Schriftgraden
                If RTFTB.SelectionFontSize > 8 Then
                    Dim newSize As Single = CSng(RTFTB.SelectionFontSize - 1)
                    If newSize < 8 Then newSize = 8
                    RTFTB.SelectionFontSize = newSize
                End If

            Case sender Is Me.ToolStripButtonForeColor
                ' Farbe für markierten Text auswählen
                ' Der Dialog wird mit der aktuellen Vordergrundfarbe der Auswahl vorbelegt.
                Using colorDialog As New ColorDialog()
                    colorDialog.Color = RTFTB.SelectionForeColor
                    If colorDialog.ShowDialog(Me) = DialogResult.OK Then
                        RTFTB.SelectionColor = colorDialog.Color
                    End If
                End Using

            Case sender Is Me.ToolStripButtonBackColor
                ' Hintergrundfarbe für markierten Text auswählen
                ' Der Dialog wird mit der aktuellen Hintergrundfarbe der Auswahl vorbelegt.
                Using colorDialog As New ColorDialog()
                    colorDialog.Color = RTFTB.SelectionBackColor
                    If colorDialog.ShowDialog(Me) = DialogResult.OK Then
                        RTFTB.SelectionBackColor = colorDialog.Color
                    End If
                End Using

            Case sender Is Me.ToolStripButtonDelFormat
                ' alle Formatierungen des markierten Textes entfernen
                RTFTB.ClearFormatting()
                ' Nach dem Entfernen alle UI-Checkboxen anhand der Selektion aktualisieren
                SetBoldCheckedState()
                SetItalicCheckedState()
                SetUnderlineCheckedState()
                SetStrikeoutCheckedState()
                SetBulletCheckedState()
                SetAlignmentCheckedState()

            Case sender Is Me.ToolStripButtonLeftIndentUp
                ' linken Einzug um 10 Pixel vergrößern (RichTextBox-Indents sind pixelbasiert)
                RTFTB.SelectionIndent += 10

            Case sender Is Me.ToolStripButtonLeftIndentDown
                ' linken Einzug um 10 Pixel verkleinern, min. 0 (kein negativer Einzug)
                RTFTB.SelectionIndent = Math.Max(0, RTFTB.SelectionIndent - 10)

            Case sender Is Me.ToolStripButtonTextLeft
                ' Text linksbündig
                RTFTB.SelectionAlignment = HorizontalAlignment.Left
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                SetAlignmentCheckedState()

            Case sender Is Me.ToolStripButtonTextCenter
                ' Text zentriert
                RTFTB.SelectionAlignment = HorizontalAlignment.Center
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                SetAlignmentCheckedState()

            Case sender Is Me.ToolStripButtonTextRight
                ' Text rechtsbündig
                RTFTB.SelectionAlignment = HorizontalAlignment.Right
                ' UI-Status (Häkchen) nach der Aktion synchronisieren
                SetAlignmentCheckedState()

            Case sender Is Me.ToolStripButtonFontFormat
                ' Schriftart auswählen
                ' Der Dialog wird mit der aktuellen Auswahl-Schrift vorbelegt.
                Using fontDialog As New FontDialog()
                    fontDialog.Font = RTFTB.SelectionFont
                    If fontDialog.ShowDialog(Me) = DialogResult.OK Then
                        RTFTB.SelectionFont = fontDialog.Font
                    End If
                End Using

        End Select

    End Sub

    ' Event-Handler, der aufgerufen wird, wenn sich die Textauswahl im RTFTB ändert.
    Private Sub RTFTB_SelectionChanged(sender As Object, e As EventArgs) Handles RTFTB.SelectionChanged
        ' Wenn sich die Textauswahl ändert, müssen die UI-Checkboxen den neuen Status der Auswahl widerspiegeln.
        SetBoldCheckedState()
        SetItalicCheckedState()
        SetUnderlineCheckedState()
        SetStrikeoutCheckedState()
        SetBulletCheckedState()
        SetAlignmentCheckedState()
    End Sub

    ' Synchronisiert den Checked-Status  für Textausrichtung (Alignment).
    Private Sub SetAlignmentCheckedState()
        Select Case RTFTB.SelectionAlignment
            Case HorizontalAlignment.Left
                SetLeftAlignmentCheckedState()
            Case HorizontalAlignment.Center
                SetCenterAlignmentCheckedState()
            Case HorizontalAlignment.Right
                SetRightAlignmentCheckedState()
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
        Me.ToolStripButtonToggleBullets.Checked = RTFTB.SelectionBullet
    End Sub

    ' Synchronisiert den Checked-Status für Durchstreichung (Strikeout).
    Private Sub SetStrikeoutCheckedState()
        Me.ToolStripButtonStrikeout.Checked = CBool(RTFTB.SelectionStrikeout)
    End Sub

    ' Synchronisiert den Checked-Status für Unterstreichung (Underline).
    Private Sub SetUnderlineCheckedState()
        Me.ToolStripButtonUnderline.Checked = CBool(RTFTB.SelectionUnderline)
    End Sub

    ' Synchronisiert den Checked-Status für Kursivschrift (Italic).
    Private Sub SetItalicCheckedState()
        Me.ToolStripButtonItalic.Checked = CBool(RTFTB.SelectionItalic)
    End Sub

    ' Synchronisiert den Checked-Status für Fettschrift (Bold).
    Private Sub SetBoldCheckedState()
        Me.ToolStripButtonBold.Checked = CBool(RTFTB.SelectionBold)
    End Sub

End Class
