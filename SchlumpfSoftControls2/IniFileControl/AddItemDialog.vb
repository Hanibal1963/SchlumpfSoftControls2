' *************************************************************************************************
' AddItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

'Imports System.
'Imports System.ComponentModel.
'Imports System.Windows.Forms.

Namespace IniFileControl

    ''' <summary>
    ''' Dialogfenster zum Erfassen eines neuen Wertes (z. B. für einen neuen INI-Item).
    ''' Der Dialog aktiviert den OK-Button nur bei nicht-leerer Eingabe und gibt
    ''' den eingegebenen Text über die Eigenschaft <see cref="NewItemValue"/> zurück.
    ''' </summary>
    Public Class AddItemDialog : Inherits System.Windows.Forms.Form

#Region "Variablendefinition"

        ''' <summary>
        ''' Beschriftung für die Eingabeaufforderung (Name des neuen Elements).
        ''' </summary>
        Private WithEvents Label As System.Windows.Forms.Label

        ''' <summary>
        ''' Texteingabefeld für den neuen Elementnamen.
        ''' </summary>
        Private WithEvents TextBox As System.Windows.Forms.TextBox

        ''' <summary>
        ''' OK-Schaltfläche zum Bestätigen der Eingabe.
        ''' Aktiviert sich nur bei nicht-leerer Eingabe.
        ''' </summary>
        Private WithEvents ButtonOK As System.Windows.Forms.Button

        ''' <summary>
        ''' Abbrechen-Schaltfläche zum Verwerfen der Eingabe und Schließen des Dialogs.
        ''' </summary>
        Private WithEvents ButtonCancel As System.Windows.Forms.Button

        ''' <summary>
        ''' Vom Windows Forms Designer verwaltete Komponentenliste.
        ''' </summary>
        Private ReadOnly components As System.ComponentModel.IContainer ' Wird vom Windows Form-Designer benötigt.

#End Region

#Region "Eigenschftsdefinitionen"

        ''' <summary>
        ''' Gibt den neuen Wert zurück oder legt ihn fest.
        ''' </summary>
        ''' <remarks>
        ''' Der Wert wird normalerweise nicht direkt gesetzt, sondern beim Bestätigen (OK)
        ''' aus dem Textfeld übernommen. Ein externer Set ist möglich, z. B. um einen
        ''' Startwert anzuzeigen.
        ''' </remarks>
        ''' <returns>Der neu eingegebene und über OK bestätigte Text.</returns>
        <System.ComponentModel.Browsable(True)>
        Public Property NewItemValue As String = $""

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz des Dialogs und setzt den OK-Button auf deaktiviert.
        ''' </summary>
        ''' <remarks>
        ''' Ruft <see cref="InitializeComponent"/> auf und deaktiviert anschließend den OK-Button,
        ''' bis eine gültige Eingabe erfolgt ist.
        ''' </remarks>
        Public Sub New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Designer erforderlich.
            Me.ButtonOK.Enabled = False ' OK-Button standardmäßig deaktivieren damit nur valide Eingaben bestätigt werden können.
        End Sub


#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Gemeinsamer Klick-Handler für OK- und Abbrechen-Button.
        ''' OK: Wert übernehmen und Dialog mit <see cref="System.Windows.Forms.DialogResult.OK"/> schließen.
        ''' Abbrechen: Dialog mit <see cref="System.Windows.Forms.DialogResult.Cancel"/> schließen (ohne Übernahme).
        ''' </summary>
        ''' <param name="sender">Auslösendes Steuerelement (OK oder Cancel).</param>
        ''' <param name="e">Ereignisargumente (nicht verwendet).</param>
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonOK.Click, ButtonCancel.Click

            Select Case True
                Case sender Is Me.ButtonOK
                    Me.SetNewItemValue()  ' Neuen Wert aus der TextBox in das Feld übernehmen.
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK ' Ergebnis auf OK setzen.

                Case sender Is Me.ButtonCancel
                    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel ' Ergebnis auf Cancel setzen.

            End Select

            Me.Close() ' Dialog schließen (Rückgabe über DialogResult).

        End Sub

        ''' <summary>
        ''' Übernimmt den neuen Wert aus der TextBox in die Eigenschaft <see cref="NewItemValue"/>.
        ''' </summary>
        ''' <remarks>
        ''' Es erfolgt keine Trimmung oder Validierung – Leerzeichen am Anfang/Ende bleiben bewusst erhalten.
        ''' Die Validierung erfolgt über <see cref="TextBox_TextChanged"/>.
        ''' </remarks>
        Private Sub SetNewItemValue()
            Me.NewItemValue = Me.TextBox.Text
        End Sub

        ''' <summary>
        ''' Ereignishandler bei Änderungen des Textes im Eingabefeld.
        ''' Aktiviert oder deaktiviert den OK-Button abhängig davon, ob eine nicht-leere Eingabe vorliegt.
        ''' </summary>
        ''' <param name="sender">Die TextBox, deren Inhalt sich geändert hat.</param>
        ''' <param name="e">Ereignisargumente (nicht verwendet).</param>
        Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox.TextChanged
            ' Textbox leer oder nur Leerzeichen?
            If String.IsNullOrWhiteSpace(CType(sender, System.Windows.Forms.TextBox).Text) Then
                Me.ButtonOK.Enabled = False ' Button deaktivieren, wenn Eingabe nicht sinnvoll ist.
            Else
                Me.ButtonOK.Enabled = True  ' Ansonsten Button aktivieren.
            End If
        End Sub

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        ''' <summary>
        ''' Initialisiert und konfiguriert alle vom Designer verwalteten Steuerelemente des Dialogs.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode wird automatisch vom Konstruktor aufgerufen und sollte nicht manuell geändert werden.
        ''' </remarks>
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
            TableLayoutPanel.Anchor = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            TableLayoutPanel.ColumnCount = 2
            Dim unused = TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Dim unused1 = TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            TableLayoutPanel.Controls.Add(Me.ButtonOK, 0, 0)
            TableLayoutPanel.Controls.Add(Me.ButtonCancel, 1, 0)
            TableLayoutPanel.Location = New System.Drawing.Point(188, 55)
            TableLayoutPanel.Name = "TableLayoutPanel"
            TableLayoutPanel.RowCount = 1
            Dim unused2 = TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
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
            AddHandler Me.ButtonOK.Click, AddressOf Me.Button_Click
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
            AddHandler Me.ButtonCancel.Click, AddressOf Me.Button_Click
            '
            'Label
            '
            Me.Label.AutoSize = True
            Me.Label.Location = New System.Drawing.Point(15, 9)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(232, 13)
            Me.Label.TabIndex = 1
            Me.Label.Text = "Geben Sie den Name für das neue Element ein:"
            '
            'TextBox
            '
            Me.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TextBox.Location = New System.Drawing.Point(12, 25)
            Me.TextBox.Name = "TextBox"
            Me.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.TextBox.Size = New System.Drawing.Size(316, 20)
            Me.TextBox.TabIndex = 2
            Me.TextBox.WordWrap = False
            AddHandler Me.TextBox.TextChanged, AddressOf Me.TextBox_TextChanged
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

#End Region

#Region "überschriebene Methoden"

        'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        ''' <summary>
        ''' Gibt verwaltete Ressourcen frei und bereinigt die Komponentenliste, sofern vorhanden.
        ''' </summary>
        ''' <param name="disposing">True, wenn verwaltete Ressourcen freigegeben werden sollen; andernfalls False.</param>
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

#End Region

    End Class

End Namespace