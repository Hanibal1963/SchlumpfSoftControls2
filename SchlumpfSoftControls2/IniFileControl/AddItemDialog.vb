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
    Public Class AddItemDialog : Inherits System.Windows.Forms.Form

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



        'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Wird vom Windows Form-Designer benötigt.
        Private components As System.ComponentModel.IContainer

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
            Me.ButtonOK = New System.Windows.Forms.Button()
            Me.ButtonCancel = New System.Windows.Forms.Button()
            Me.Label = New System.Windows.Forms.Label()
            Me.TextBox = New System.Windows.Forms.TextBox()
            TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
            TableLayoutPanel.SuspendLayout()
            Me.SuspendLayout()
            '
            'TableLayoutPanel
            '
            TableLayoutPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            TableLayoutPanel.ColumnCount = 2
            TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            TableLayoutPanel.Controls.Add(Me.ButtonOK, 0, 0)
            TableLayoutPanel.Controls.Add(Me.ButtonCancel, 1, 0)
            TableLayoutPanel.Location = New System.Drawing.Point(188, 55)
            TableLayoutPanel.Name = "TableLayoutPanel"
            TableLayoutPanel.RowCount = 1
            TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            TableLayoutPanel.Size = New System.Drawing.Size(146, 29)
            TableLayoutPanel.TabIndex = 0
            '
            'ButtonOK
            '
            Me.ButtonOK.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ButtonOK.Location = New System.Drawing.Point(3, 3)
            Me.ButtonOK.Name = "ButtonOK"
            Me.ButtonOK.Size = New System.Drawing.Size(67, 23)
            Me.ButtonOK.TabIndex = 0
            Me.ButtonOK.Text = "OK"
            '
            'ButtonCancel
            '
            Me.ButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.ButtonCancel.Location = New System.Drawing.Point(76, 3)
            Me.ButtonCancel.Name = "ButtonCancel"
            Me.ButtonCancel.Size = New System.Drawing.Size(67, 23)
            Me.ButtonCancel.TabIndex = 1
            Me.ButtonCancel.Text = "Abbrechen"
            '
            'Label
            '
            Me.Label.AutoSize = True
            Me.Label.Location = New System.Drawing.Point(12, 9)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(232, 13)
            Me.Label.TabIndex = 1
            Me.Label.Text = "Geben Sie den Name für das neue Element ein:"
            '
            'TextBox
            '
            Me.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TextBox.Location = New System.Drawing.Point(15, 25)
            Me.TextBox.Name = "TextBox"
            Me.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.TextBox.Size = New System.Drawing.Size(316, 20)
            Me.TextBox.TabIndex = 2
            Me.TextBox.WordWrap = False
            '
            'AddItemDialog
            '
            Me.AcceptButton = Me.ButtonOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.ButtonCancel
            Me.ClientSize = New System.Drawing.Size(339, 96)
            Me.ControlBox = False
            Me.Controls.Add(Me.TextBox)
            Me.Controls.Add(Me.Label)
            Me.Controls.Add(TableLayoutPanel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "AddItemDialog"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Element hinzufügen"
            TableLayoutPanel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Private WithEvents Label As System.Windows.Forms.Label
        Private WithEvents TextBox As System.Windows.Forms.TextBox
        Private WithEvents ButtonOK As System.Windows.Forms.Button
        Private WithEvents ButtonCancel As System.Windows.Forms.Button


    End Class

End Namespace