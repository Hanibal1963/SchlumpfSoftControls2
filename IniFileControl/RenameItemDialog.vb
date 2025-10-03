' *************************************************************************************************
' RenameItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' 
' Zweck dieses Dialogs:
' - Bietet eine einfache UI, um den Wert (z. B. Namen/Schlüssel) eines Items umzubenennen.
' - Der Button "Ja" wird nur aktiviert, wenn im Textfeld ein nicht-leerer Text steht.
' - Beim Setzen von OldItemValue wird ein Ereignis ausgelöst, um Platzhalter im Label-Text zu ersetzen.
'
' Erwartetes UI (Designer):
' - Label:      Enthält einen Text mit Platzhalter "{0}" für den alten Wert (z. B. "Alten Namen '{0}' ändern in:")
' - TextBox:    Eingabefeld für den neuen Wert
' - ButtonYes:  Bestätigt die Änderung und setzt DialogResult = Yes
' - ButtonNo:   Bricht ab und setzt DialogResult = No
'
' Typische Verwendung:
'   Dim dlg As New RenameItemDialog() With {.OldItemValue = "Alt"}
'   If dlg.ShowDialog() = DialogResult.Yes Then
'       Dim neuerWert = dlg.NewItemValue
'       ' ... neuen Wert verwenden ...
'   End If
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace IniFileControl

    Friend Class RenameItemDialog

        ' Hält den ursprünglichen (alten) Wert, der im Dialog angezeigt wird.
        Private _OldItemValue As String = $"asarasa"
        ' Hält den vom Benutzer eingegebenen (neuen) Wert, der beim Bestätigen übernommen wird.
        Private _NewItemValue As String = $""

        ' Ereignis wird ausgelöst, wenn sich OldItemValue ändert (siehe Property-Setter unten).
        ' Hinweis: Der Ereignisname ist bewusst unverändert gelassen, um bestehende Designer- oder Code-Verknüpfungen nicht zu brechen.
        Private Event PropertyoldItemValueChanged()

        ''' <summary>
        ''' Initialisiert den Dialog und setzt Initialzustände der Steuerelemente.
        ''' </summary>
        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()

            ' Initialzustand: Bestätigen-Button deaktiviert, bis gültiger Text eingegeben wurde.
            Me.ButtonYes.Enabled = False
        End Sub

        ''' <summary>
        ''' Stellt den alten Wert bereit, der im Label-Text (Platzhalter {0}) angezeigt wird.
        ''' Das Setzen löst ein Ereignis aus, damit die UI den Platzhalter ersetzen kann.
        ''' </summary>
        <Browsable(True)>
        Public Property OldItemValue As String
            Get
                Return Me._OldItemValue
            End Get
            Set
                Me._OldItemValue = Value
                ' UI-Update anstoßen (z. B. Label-Text aktualisieren).
                RaiseEvent PropertyoldItemValueChanged()
            End Set
        End Property

        ''' <summary>
        ''' Stellt den vom Benutzer bestätigten neuen Wert bereit.
        ''' Wird beim Klick auf "Ja" aus dem Textfeld übernommen.
        ''' </summary>
        <Browsable(True)>
        Public Property NewItemValue As String
            Get
                Return Me._NewItemValue
            End Get
            Set
                Me._NewItemValue = Value
            End Set
        End Property

        ''' <summary>
        ''' Gemeinsamer Click-Handler für "Ja" und "Nein".
        ''' - Bei "Ja": übernimmt den Text aus der TextBox in NewItemValue und schließt mit DialogResult.Yes.
        ''' - Bei "Nein": schließt mit DialogResult.No.
        ''' </summary>
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonYes.Click, ButtonNo.Click
            ' Prüfen, welcher Button ausgelöst hat.
            If sender Is Me.ButtonYes Then
                ' Neuen Wert aus der TextBox in die Property übernehmen.
                Me.SetNewItemValue()
                ' Dialog mit positivem Ergebnis schließen.
                Me.DialogResult = DialogResult.Yes
            ElseIf sender Is Me.ButtonNo Then
                ' Abbruch: Dialog mit negativem Ergebnis schließen.
                Me.DialogResult = DialogResult.No
            End If

            ' Dialog schließen (Modal-Result ist bereits gesetzt).
            Me.Close()
        End Sub

        ''' <summary>
        ''' Übernimmt den aktuellen Text aus dem Eingabefeld als neuen Wert.
        ''' </summary>
        Private Sub SetNewItemValue()
            Me._NewItemValue = Me.TextBox.Text
        End Sub

        ''' <summary>
        ''' Aktiviert/Deaktiviert den "Ja"-Button abhängig davon, ob ein sinnvoller Text eingegeben wurde.
        ''' Leere Eingaben oder nur Leerzeichen sind nicht zulässig.
        ''' </summary>
        Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox.TextChanged
            ' TextBox leer oder enthält nur Whitespace?
            If String.IsNullOrWhiteSpace(CType(sender, TextBox).Text) Then
                ' Bestätigungs-Button deaktivieren, solange der Text nicht gültig ist.
                Me.ButtonYes.Enabled = False
            Else
                ' Gültige Eingabe: Bestätigungs-Button aktivieren.
                Me.ButtonYes.Enabled = True
            End If
        End Sub

        ''' <summary>
        ''' Aktualisiert den Label-Text, sobald sich OldItemValue ändert.
        ''' Erwartet, dass der Label-Text einen "{0}"-Platzhalter enthält, der durch den alten Wert ersetzt wird.
        ''' </summary>
        Private Sub IniFileRenameItemDialog_PropertyoldItemValueChanged() Handles Me.PropertyoldItemValueChanged
            ' Den Platzhalter "{0}" im Label-Text durch den alten Wert ersetzen.
            Me.Label.Text = Me.Label.Text.Replace("{0}", Me._OldItemValue)
        End Sub

    End Class

End Namespace