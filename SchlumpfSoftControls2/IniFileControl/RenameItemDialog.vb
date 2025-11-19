' *************************************************************************************************
' RenameItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Dialog zum Umbenennen eines Elements. Zeigt den alten Wert an und ermöglicht die Eingabe eines neuen Werts.
    ''' </summary>
    ''' <remarks>
    ''' Der Dialog validiert die Eingabe (keine leeren oder nur aus Leerzeichen bestehenden Namen) und stellt das Ergebnis über <see cref="NewItemValue"/> bereit.
    ''' </remarks>
    Friend Class RenameItemDialog : Inherits System.Windows.Forms.Form

#Region "Variablendefinition"

        ''' <summary>
        ''' Container für die vom Designer erzeugten Komponenten.
        ''' </summary>
        Private ReadOnly components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Button zum Bestätigen der Eingabe ("Ja"). Ist deaktiviert bis eine gültige Eingabe vorliegt.
        ''' </summary>
        Private WithEvents ButtonYes As System.Windows.Forms.Button

        ''' <summary>
        ''' Button zum Abbrechen des Dialogs ("Nein").
        ''' </summary>
        Private WithEvents ButtonNo As System.Windows.Forms.Button

        ''' <summary>
        ''' Label zur Anzeige des Hinweistextes inklusive des alten Element-Namens.
        ''' </summary>
        Private WithEvents Label As System.Windows.Forms.Label

        ''' <summary>
        ''' TextBox zur Eingabe des neuen Element-Namens.
        ''' </summary>
        Private WithEvents TextBox As System.Windows.Forms.TextBox

        ''' <summary>
        ''' Hält den ursprünglichen (alten) Wert, der im Dialog angezeigt wird.
        ''' </summary>
        Private _OldItemValue As String = $"" ' Hält den ursprünglichen (alten) Wert, der im Dialog angezeigt wird.

        ''' <summary>
        ''' Hält den vom Benutzer eingegebenen (neuen) Wert, der beim Bestätigen übernommen wird.
        ''' </summary>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Automatisch generierte Eigenschaft verwenden", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
        Private _NewItemValue As String = $"" ' Hält den vom Benutzer eingegebenen (neuen) Wert, der beim Bestätigen übernommen wird.

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert den Dialog und setzt Initialzustände der Steuerelemente.
        ''' </summary>
        Public Sub New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Designer erforderlich.
            Me.ButtonYes.Enabled = False  ' Bestätigen-Button deaktiviert, bis gültiger Text eingegeben wurde.
        End Sub

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Stellt den alten Wert bereit, der im Label-Text (Platzhalter {0}) angezeigt wird.
        ''' Das Setzen löst ein Ereignis aus, damit die UI den Platzhalter ersetzen kann.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        Public Property OldItemValue As String
            Get
                Return Me._OldItemValue
            End Get
            Set
                Me._OldItemValue = Value
                Me.Label.Text = $"Element '{Me._OldItemValue}' umbenennen in:"
            End Set
        End Property

        ''' <summary>
        ''' Stellt den vom Benutzer bestätigten neuen Wert bereit.
        ''' Wird beim Klick auf "Ja" aus dem Textfeld übernommen.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        Public Property NewItemValue As String
            Get
                Return Me._NewItemValue
            End Get
            Set
                Me._NewItemValue = Value
            End Set
        End Property

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Übernimmt den aktuellen Text aus dem Eingabefeld als neuen Wert.
        ''' </summary>
        Private Sub SetNewItemValue()
            Me._NewItemValue = Me.TextBox.Text
        End Sub

        ''' <summary>
        ''' Gemeinsamer Click-Handler für "Ja" und "Nein".
        ''' - Bei "Ja": übernimmt den Text aus der TextBox in NewItemValue und schließt mit DialogResult.Yes.
        ''' - Bei "Nein": schließt mit DialogResult.No.
        ''' </summary>
        ''' <param name="sender">Auslösender Button (Ja oder Nein).</param>
        ''' <param name="e">Ereignisdaten.</param>
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonYes.Click, ButtonNo.Click
            ' Prüfen, welcher Button ausgelöst hat.
            If sender Is Me.ButtonYes Then
                Me.SetNewItemValue() ' Neuen Wert aus der TextBox in die Property übernehmen.
                Me.DialogResult = System.Windows.Forms.DialogResult.Yes ' Dialog mit positivem Ergebnis schließen.
            ElseIf sender Is Me.ButtonNo Then
                Me.DialogResult = System.Windows.Forms.DialogResult.No ' Abbruch: Dialog mit negativem Ergebnis schließen.
            End If
            Me.Close() ' Dialog schließen (Modal-Result ist bereits gesetzt).
        End Sub

        ''' <summary>
        ''' Aktiviert/Deaktiviert den "Ja"-Button abhängig davon, ob ein sinnvoller Text eingegeben wurde.
        ''' Leere Eingaben oder nur Leerzeichen sind nicht zulässig.
        ''' </summary>
        ''' <param name="sender">Die TextBox, deren Inhalt geprüft wird.</param>
        ''' <param name="e">Ereignisdaten.</param>
        Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox.TextChanged
            ' TextBox leer oder enthält nur Whitespace?
            If String.IsNullOrWhiteSpace(CType(sender, System.Windows.Forms.TextBox).Text) Then
                Me.ButtonYes.Enabled = False ' Bestätigungs-Button deaktivieren, solange der Text nicht gültig ist.
            Else
                Me.ButtonYes.Enabled = True ' Gültige Eingabe: Bestätigungs-Button aktivieren.
            End If
        End Sub

        ''' <summary>
        ''' Initialisiert und konfiguriert die Steuerelemente des Dialogs. 
        ''' Hinweis: 
        ''' Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        ''' Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        ''' Das Bearbeiten ist mit dem Windows Form-Designer möglich. 
        ''' </summary>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
            Me.ButtonYes = New System.Windows.Forms.Button()
            Me.ButtonNo = New System.Windows.Forms.Button()
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
            Dim unused2 = TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Dim unused1 = TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            TableLayoutPanel.Controls.Add(Me.ButtonYes, 0, 0)
            TableLayoutPanel.Controls.Add(Me.ButtonNo, 1, 0)
            TableLayoutPanel.Location = New System.Drawing.Point(188, 68)
            TableLayoutPanel.Name = "TableLayoutPanel"
            TableLayoutPanel.RowCount = 1
            Dim unused = TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            TableLayoutPanel.Size = New System.Drawing.Size(146, 29)
            TableLayoutPanel.TabIndex = 0
            '
            'ButtonYes
            '
            Me.ButtonYes.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ButtonYes.DialogResult = System.Windows.Forms.DialogResult.Yes
            Me.ButtonYes.Location = New System.Drawing.Point(3, 3)
            Me.ButtonYes.Name = "ButtonYes"
            Me.ButtonYes.Size = New System.Drawing.Size(67, 23)
            Me.ButtonYes.TabIndex = 0
            Me.ButtonYes.Text = "Ja"
            '
            'ButtonNo
            '
            Me.ButtonNo.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ButtonNo.DialogResult = System.Windows.Forms.DialogResult.No
            Me.ButtonNo.Location = New System.Drawing.Point(76, 3)
            Me.ButtonNo.Name = "ButtonNo"
            Me.ButtonNo.Size = New System.Drawing.Size(67, 23)
            Me.ButtonNo.TabIndex = 1
            Me.ButtonNo.Text = "Nein"
            '
            'Label
            '
            Me.Label.Location = New System.Drawing.Point(15, 9)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(314, 17)
            Me.Label.TabIndex = 1
            '
            'TextBox
            '
            Me.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TextBox.Location = New System.Drawing.Point(15, 38)
            Me.TextBox.Name = "TextBox"
            Me.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.TextBox.Size = New System.Drawing.Size(316, 20)
            Me.TextBox.TabIndex = 2
            Me.TextBox.WordWrap = False
            '
            'RenameItemDialog
            '
            Me.AcceptButton = Me.ButtonYes
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.ButtonNo
            Me.ClientSize = New System.Drawing.Size(341, 109)
            Me.ControlBox = False
            Me.Controls.Add(Me.TextBox)
            Me.Controls.Add(Me.Label)
            Me.Controls.Add(TableLayoutPanel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "RenameItemDialog"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Element umbenennen"
            TableLayoutPanel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

#Region "überschriebene Methoden"

        'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        ''' <summary>
        ''' Gibt verwaltete Ressourcen frei und bereinigt die Komponentenliste, falls vorhanden.
        ''' </summary>
        ''' <param name="disposing">True zum Freigeben verwalteter Ressourcen; False andernfalls.</param>
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