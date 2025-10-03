' *************************************************************************************************
' AddItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace IniFileControl

    ''' <summary>
    ''' Dialogfenster zum Erfassen eines neuen Wertes (z. B. für einen neuen INI-Item).
    ''' Der Dialog aktiviert den OK-Button nur bei nicht-leerer Eingabe und gibt
    ''' den eingegebenen Text über die Eigenschaft <see cref="NewItemValue"/> zurück.
    ''' </summary>
    Public Class AddItemDialog

        ' Hält den vom Benutzer bestätigten neuen Wert.
        ' Initialisiert mit leerer Zeichenfolge. Wird beim Klick auf "OK" aus der TextBox übernommen.
        Private _NewItemValue As String = $""

        ''' <summary>
        ''' Gibt den neuen Wert zurück oder legt ihn fest.
        ''' </summary>
        ''' <remarks>
        ''' Der Wert wird normalerweise nicht direkt gesetzt, sondern beim Bestätigen (OK)
        ''' aus dem Textfeld übernommen. Ein externer Set ist möglich, z. B. um einen
        ''' Startwert anzuzeigen.
        ''' </remarks>
        ''' <returns>Der neu eingegebene und über OK bestätigte Text.</returns>
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
        ''' Erstellt den Dialog und initialisiert Steuerelemente.
        ''' </summary>
        ''' <remarks>
        ''' Die Steuerelemente (z. B. <c>ButtonOK</c>, <c>ButtonCancel</c>, <c>TextBox</c>)
        ''' werden im Designer angelegt. Hier wird u. a. der OK-Button deaktiviert,
        ''' bis eine gültige Eingabe vorliegt.
        ''' </remarks>
        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()

            ' Nach der Designer-Initialisierung: OK-Button standardmäßig deaktivieren,
            ' damit nur valide Eingaben bestätigt werden können.
            Me.ButtonOK.Enabled = False
        End Sub

        ''' <summary>
        ''' Gemeinsamer Klick-Handler für OK- und Abbrechen-Button.
        ''' OK: Wert übernehmen und Dialog mit DialogResult.OK schließen.
        ''' Abbrechen: Dialog mit DialogResult.Cancel schließen (ohne Übernahme).
        ''' </summary>
        ''' <param name="sender">Auslösendes Steuerelement (OK oder Cancel).</param>
        ''' <param name="e">Ereignisargumente (nicht verwendet).</param>
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonOK.Click, ButtonCancel.Click
            If sender Is Me.ButtonOK Then ' Welcher Button wurde geklickt?
                Me.SetNewItemValue()       ' Neuen Wert aus der TextBox in das Feld übernehmen.
                Me.DialogResult = DialogResult.OK ' Ergebnis auf OK setzen.
            ElseIf sender Is Me.ButtonCancel Then
                Me.DialogResult = DialogResult.Cancel ' Ergebnis auf Cancel setzen.
            End If

            Me.Close() ' Dialog schließen (Rückgabe über DialogResult).
        End Sub

        ''' <summary>
        ''' Übernimmt den neuen Wert aus der TextBox in das interne Feld.
        ''' </summary>
        ''' <remarks>
        ''' Es erfolgt keine Trimmung oder Validierung – Leerzeichen am Anfang/Ende bleiben bewusst erhalten.
        ''' Validierung/Erzwingung nicht-leerer Eingabe erfolgt im TextChanged-Handler.
        ''' </remarks>
        Private Sub SetNewItemValue()
            Me._NewItemValue = Me.TextBox.Text
        End Sub

        ''' <summary>
        ''' Wird aufgerufen, wenn sich der Text im Eingabefeld ändert.
        ''' Steuert die Aktivierung des OK-Buttons abhängig von der Eingabe.
        ''' </summary>
        ''' <param name="sender">Die TextBox, deren Inhalt sich geändert hat.</param>
        ''' <param name="e">Ereignisargumente (nicht verwendet).</param>
        Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox.TextChanged
            ' Textbox leer oder nur Leerzeichen?
            If String.IsNullOrWhiteSpace(CType(sender, TextBox).Text) Then
                Me.ButtonOK.Enabled = False ' Button deaktivieren, wenn Eingabe nicht sinnvoll ist.
            Else
                Me.ButtonOK.Enabled = True  ' Ansonsten Button aktivieren.
            End If
        End Sub

    End Class

End Namespace