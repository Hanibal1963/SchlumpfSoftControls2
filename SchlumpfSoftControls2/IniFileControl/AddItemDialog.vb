' *************************************************************************************************
' AddItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Dialogfenster zur Eingabe eines neuen Textwertes (z. B. für einen neuen
    ''' INI-Schlüssel oder -Eintrag). Der OK-Button ist nur aktiviert, wenn die Eingabe
    ''' nicht leer ist oder lediglich aus Leerzeichen besteht. Der bestätigte Text wird
    ''' über die Eigenschaft <see cref="NewItemValue"/> bereitgestellt.
    ''' </summary>
    ''' <remarks>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Der Dialog setzt den OK-Button initial auf
    ''' deaktiviert.</description>
    '''  </item>
    '''  <item>
    '''   <description>Bei Änderungen im Texteingabefeld wird die Gültigkeit geprüft und
    ''' der OK-Button entsprechend aktiviert/deaktiviert.</description>
    '''  </item>
    '''  <item>
    '''   <description>Beim Bestätigen mit OK wird der Text aus dem Eingabefeld in <see
    ''' cref="NewItemValue"/> übernommen und <see
    ''' cref="System.Windows.Forms.Form.DialogResult"/> auf <see
    ''' cref="System.Windows.Forms.DialogResult.OK"/> gesetzt.</description>
    '''  </item>
    ''' </list>
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: Dialog anzeigen und den bestätigten Wert übernehmen
    ''' Dim dlg As New IniFileControl.AddItemDialog()
    ''' ' Optional: Startwert vorgeben
    ''' dlg.NewItemValue = "VorschlagName"
    '''  
    ''' If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
    '''     Dim neuerName As String = dlg.NewItemValue
    '''     ' Weiterverarbeiten des neuen Namens (z. B. INI-Item hinzufügen)
    ''' End If]]></code>
    ''' </example>
    Public Class AddItemDialog : Inherits System.Windows.Forms.Form

#Region "Variablen"

        Private WithEvents Label As System.Windows.Forms.Label
        Private WithEvents TextBox As System.Windows.Forms.TextBox
        Private WithEvents ButtonOK As System.Windows.Forms.Button
        Private WithEvents ButtonCancel As System.Windows.Forms.Button
        Private ReadOnly components As System.ComponentModel.IContainer ' Wird vom Windows Form-Designer benötigt.

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Gibt den neuen Wert zurück oder legt ihn fest.
        ''' </summary>
        ''' <remarks>
        ''' Der Wert wird standardmäßig beim Bestätigen (OK) aus dem Textfeld
        ''' übernommen.<br/>
        ''' Das Setzen von außen ist möglich, um einen Startwert im Textfeld anzuzeigen.
        ''' </remarks>
        ''' <value>
        ''' Der über OK bestätigte Textwert.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Startwert setzen und später den bestätigten Wert auslesen
        ''' Dim dlg As New IniFileControl.AddItemDialog()
        ''' dlg.NewItemValue = "Startwert"
        '''  
        ''' If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '''     Dim wert As String = dlg.NewItemValue
        '''     ' Nutzung des bestätigten Wertes
        ''' End If]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        Public Property NewItemValue As String = $""

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz des Dialogs und setzt den OK-Button auf
        ''' deaktiviert.
        ''' </summary>
        ''' <remarks>
        ''' Ruft <see cref="InitializeComponent"/> auf und deaktiviert anschließend den
        ''' OK-Button, bis eine gültige Eingabe erfolgt ist. Der OK-Button wird automatisch
        ''' aktiviert, sobald der Text im Eingabefeld nicht leer ist und nicht nur aus
        ''' Leerzeichen besteht.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Dialog instanziieren, optional Startwert setzen und anzeigen
        ''' Dim dlg As New IniFileControl.AddItemDialog()
        ''' ' Optional: Starttext für das Eingabefeld
        ''' dlg.NewItemValue = "VorschlagName"
        '''  
        ''' ' Dialog modal anzeigen
        ''' If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '''     ' Bestätigten Wert auslesen
        '''     Dim neuerName As String = dlg.NewItemValue
        '''     ' Weiterverarbeitung ...
        ''' End If
        '''  
        ''' ' Nach Verwendung Ressourcen freigeben
        ''' dlg.Dispose()]]></code>
        ''' </example>
        Public Sub New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Designer erforderlich.
            Me.ButtonOK.Enabled = False ' OK-Button standardmäßig deaktivieren damit nur valide Eingaben bestätigt werden können.
        End Sub

        ''' <summary>
        ''' Gibt verwaltete Ressourcen des Dialogs frei.
        ''' </summary>
        ''' <remarks>
        ''' Überschreibt <see cref="System.Windows.Forms.Form.Dispose(Boolean)"/> und sorgt dafür, dass die vom Designer erzeugten Komponenten korrekt bereinigt werden, wenn <paramref name="disposing"/> <c>True</c> ist. Anschließend wird die Basisklasse aufgerufen.
        ''' </remarks>
        ''' <param name="disposing"><c>True</c>, wenn sowohl verwaltete als auch unverwalte Ressourcen freigegeben werden sollen; <c>False</c>, wenn nur unverwalte Ressourcen freigegeben werden.</param>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Sichere Verwendung mit Using-Block (automatisches Dispose)
        ''' Using dlg As New IniFileControl.AddItemDialog()
        '''     dlg.NewItemValue = "Startwert"
        '''     If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '''         Dim wert As String = dlg.NewItemValue
        '''         ' Nutzung des bestätigten Wertes ...
        '''     End If
        ''' End Using
        '''  
        ''' ' Alternativ ohne Using-Block:
        ''' Dim dlg2 As New IniFileControl.AddItemDialog()
        ''' Try
        '''     If dlg2.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '''         Dim wert2 As String = dlg2.NewItemValue
        '''     End If
        ''' Finally
        '''     dlg2.Dispose()
        ''' End Try]]></code>
        ''' </example>
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

#Region "interne Methoden"

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

        Private Sub SetNewItemValue()
            Me.NewItemValue = Me.TextBox.Text
        End Sub

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

    End Class

End Namespace