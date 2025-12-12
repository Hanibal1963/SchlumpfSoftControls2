' *************************************************************************************************
' DeleteItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Dialog zur Bestätigung des Löschens eines Elements.
    ''' </summary>
    ''' <remarks>
    ''' Verwendet <see cref="System.Windows.Forms.DialogResult.OK"/> für "Ja" und <see
    ''' cref="System.Windows.Forms.DialogResult.Cancel"/> für "Nein".
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Verwendung des DeleteItemDialog
    ''' Using dlg As New IniFileControl.DeleteItemDialog()
    '''     dlg.ItemValue = "MeinEintrag"
    '''     If dlg.ShowDialog() = DialogResult.OK Then
    '''         ' Element löschen
    '''     End If
    ''' End Using]]></code>
    ''' </example>
    Friend Class DeleteItemDialog : Inherits System.Windows.Forms.Form

#Region "Variablendefinition"

        Private WithEvents ButtonYes As System.Windows.Forms.Button
        Private WithEvents ButtonNo As System.Windows.Forms.Button
        Private WithEvents Label As System.Windows.Forms.Label
        Private ReadOnly components As System.ComponentModel.IContainer
        Private _ItemValue As String = $""

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Wert (Text) des zu löschenden Elements.<br/>
        ''' Wird in der Bestätigungsfrage angezeigt.
        ''' </summary>
        ''' <remarks>
        ''' Beim Setzen wird der UI-Text des Labels aktualisiert.
        ''' </remarks>
        ''' <value>
        ''' Der Elementwert, der im Dialog referenziert wird.
        ''' </value>
        Public Property ItemValue As String
            Get
                Return Me._ItemValue
            End Get
            Set
                Me._ItemValue = Value
                Me.Label.Text = $"Möchten Sie das Element '{Me._ItemValue}' wirklich löschen?"
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Erstellt eine neue Instanz des <see cref="DeleteItemDialog"/> und initialisiert Steuerelemente.
        ''' </summary>
        ''' <remarks>Die UI-Elemente werden in <see cref="InitializeComponent"/> erzeugt.</remarks>
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        ''' <summary>
        ''' Gibt die vom Dialog verwendeten Ressourcen frei.
        ''' </summary>
        ''' <param name="disposing"><c>True</c>, wenn verwaltete Ressourcen freigegeben werden sollen; andernfalls <c>False</c>.</param>
        ''' <remarks>
        ''' Überschreibt <see cref="System.Windows.Forms.Form.Dispose(Boolean)"/> zur Freigabe des Komponenten-Containers.
        ''' </remarks>
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

        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonYes.Click, ButtonNo.Click
            If sender Is Me.ButtonYes Then ' Welcher Button wurde gedrückt?
                Me.DialogResult = System.Windows.Forms.DialogResult.OK ' Ergebnis setzen
            ElseIf sender Is Me.ButtonNo Then
                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel ' Ergebnis setzen
            End If
            Me.Close() ' Dialog schließen
        End Sub

        ' Initialisiert alle Steuerelemente des Dialogs und deren Eigenschaften / Layout.
        ' Methode wird vom Konstruktor aufgerufen. Änderungen sollten über den Designer erfolgen.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
            Me.ButtonYes = New System.Windows.Forms.Button()
            Me.ButtonNo = New System.Windows.Forms.Button()
            Me.Label = New System.Windows.Forms.Label()
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
            TableLayoutPanel.Location = New System.Drawing.Point(149, 40)
            TableLayoutPanel.Name = "TableLayoutPanel"
            TableLayoutPanel.RowCount = 1
            Dim unused = TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            TableLayoutPanel.Size = New System.Drawing.Size(146, 29)
            TableLayoutPanel.TabIndex = 0
            '
            'ButtonYes
            '
            Me.ButtonYes.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ButtonYes.Location = New System.Drawing.Point(3, 3)
            Me.ButtonYes.Name = "ButtonYes"
            Me.ButtonYes.Size = New System.Drawing.Size(67, 23)
            Me.ButtonYes.TabIndex = 0
            Me.ButtonYes.Text = "Ja"
            '
            'ButtonNo
            '
            Me.ButtonNo.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.ButtonNo.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.ButtonNo.Location = New System.Drawing.Point(76, 3)
            Me.ButtonNo.Name = "ButtonNo"
            Me.ButtonNo.Size = New System.Drawing.Size(67, 23)
            Me.ButtonNo.TabIndex = 1
            Me.ButtonNo.Text = "Nein"
            AddHandler Me.ButtonNo.Click, AddressOf Me.Button_Click
            '
            'Label
            '
            Me.Label.Location = New System.Drawing.Point(12, 9)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(283, 20)
            Me.Label.TabIndex = 1
            AddHandler Me.Label.Click, AddressOf Me.Button_Click
            '
            'DeleteItemDialog
            '
            Me.AcceptButton = Me.ButtonYes
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.ButtonNo
            Me.ClientSize = New System.Drawing.Size(307, 81)
            Me.ControlBox = False
            Me.Controls.Add(Me.Label)
            Me.Controls.Add(TableLayoutPanel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "DeleteItemDialog"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Element löschen"
            TableLayoutPanel.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

#End Region

    End Class

End Namespace