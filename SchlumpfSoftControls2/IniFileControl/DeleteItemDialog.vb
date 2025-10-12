' *************************************************************************************************
' DeleteItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' 
' Dieser Dialog dient zur Bestätigung, ob ein bestimmtes Element gelöscht werden soll.
' Der anzuzeigende Elementwert wird über die Eigenschaft `ItemValue` gesetzt.
' Im Beschriftungstext (Label) wird der Platzhalter "{0}" durch den aktuellen `ItemValue` ersetzt.
' Hinweis: Der Platzhalter sollte im initialen Label-Text vorhanden sein (z.B. "Möchten Sie '{0}' löschen?").
' *************************************************************************************************

Imports System
Imports System.Windows.Forms

Namespace IniFileControl

    ''' <summary>
    ''' Dialog zur Bestätigung des Löschens eines Elements.
    ''' </summary>
    ''' <remarks>
    ''' Beispielnutzung:
    ''' Dim dlg As New DeleteItemDialog()
    ''' dlg.ItemValue = "MeinEintrag"
    ''' If dlg.ShowDialog() = DialogResult.OK Then
    '''     ' Löschvorgang ausführen
    ''' End If
    ''' </remarks>
    Public Class DeleteItemDialog

        ' Hält den aktuell anzuzeigenden Wert des zu löschenden Elements.
        ' Initialisiert als leere Zeichenfolge.
        Private _itemvalue As String = $""

        ''' <summary>
        ''' Wird ausgelöst, sobald sich der Wert von <see cref="ItemValue"/> ändert.
        ''' Dient dazu, UI-Elemente (hier das Label) synchron zu aktualisieren.
        ''' </summary>
        Private Event PropertyItemValueChanged()

        ''' <summary>
        ''' Gibt den Wert des Elements zurück oder legt ihn fest.
        ''' Beim Setzen wird ein Ereignis ausgelöst, um abhängige UI-Elemente zu aktualisieren.
        ''' </summary>
        Public Property ItemValue As String
            Get
                Return Me._itemvalue
            End Get
            Set
                Me._itemvalue = Value
                ' Nach Wertänderung UI aktualisieren.
                RaiseEvent PropertyItemValueChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gemeinsamer Klick-Handler für die Buttons "Ja" und "Nein".
        ''' Setzt das entsprechende DialogResult und schließt den Dialog.
        ''' </summary>
        ''' <param name="sender">Auslösender Button (ButtonYes oder ButtonNo).</param>
        ''' <param name="e">Ereignisdaten (nicht verwendet).</param>
        Private Sub Button_Click(sender As Object, e As EventArgs) Handles ButtonYes.Click, ButtonNo.Click
            If sender Is Me.ButtonYes Then ' Welcher Button wurde gedrückt?
                Me.DialogResult = DialogResult.OK ' Ergebnis setzen
            ElseIf sender Is Me.ButtonNo Then
                Me.DialogResult = DialogResult.Cancel ' Ergebnis setzen
            End If
            Me.Close() ' Dialog schließen
        End Sub

        ''' <summary>
        ''' Aktualisiert den Label-Text, sobald sich <see cref="ItemValue"/> ändert.
        ''' Ersetzt dabei den Platzhalter "{0}" durch den aktuellen Wert.
        ''' </summary>
        ''' <remarks>
        ''' Wichtig:
        ''' - Der initiale Text des Labels sollte den Platzhalter "{0}" enthalten
        '''   (z.B. "Möchten Sie '{0}' löschen?").
        ''' - Nach dem ersten Ersetzen ist der Platzhalter im Label-Text nicht mehr vorhanden.
        '''   Wenn der Wert mehrfach geändert werden soll, empfiehlt es sich,
        '''   die ursprüngliche Label-Textvorlage zwischenzuspeichern und jeweils neu zu formatieren.
        ''' Hinweis:
        ''' - Das Steuerelement heißt hier "Label". Ein spezifischerer Name (z.B. "LabelPrompt")
        '''   erhöht die Lesbarkeit, ist aber funktional nicht erforderlich.
        ''' </remarks>
        Private Sub IniFileDeleteItemDialog_PropertyItemValueChanged() Handles Me.PropertyItemValueChanged
            Me.Label.Text = Me.Label.Text.Replace("{0}", Me.ItemValue)
        End Sub

    End Class

End Namespace