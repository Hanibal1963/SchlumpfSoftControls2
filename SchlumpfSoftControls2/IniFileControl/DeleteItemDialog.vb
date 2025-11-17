' *************************************************************************************************
' DeleteItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

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
    Public Class DeleteItemDialog : Inherits System.Windows.Forms.Form

#Region "Variablendefinition"

        ''' <summary>
        ''' Bestätigungs-Schaltfläche für das Löschen ("Ja"). Löst bei Klick den Dialogabschluss mit <see cref="System.Windows.Forms.DialogResult.OK"/> aus.
        ''' </summary>
        Private WithEvents ButtonYes As System.Windows.Forms.Button

        ''' <summary>
        ''' Abbruch-Schaltfläche ("Nein"). Löst bei Klick den Dialogabschluss mit <see cref="System.Windows.Forms.DialogResult.Cancel"/> aus.
        ''' </summary>
        Private WithEvents ButtonNo As System.Windows.Forms.Button

        ''' <summary>
        ''' Anzeige-Label für die Bestätigungsfrage, inkl. des aktuell gesetzten Elementwertes.
        ''' </summary>
        Private WithEvents Label As System.Windows.Forms.Label

        ''' <summary>
        ''' Container für untergeordnete Komponenten. Wird vom Windows Forms-Designer verwaltet und beim Dispose freigegeben.
        ''' </summary>
        ''' <remarks>ReadOnly, da nur während der Initialisierung gesetzt.</remarks>
        Private ReadOnly components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Aktuell anzuzeigender Wert des zu löschenden Elements. Initialwert ist eine leere Zeichenfolge.
        ''' </summary>
        ''' <remarks>Wird über die Eigenschaft <see cref="ItemValue"/> gesetzt.</remarks>
        Private _ItemValue As String = $""

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Wert (Text) des zu löschenden Elements. Wird in der Bestätigungsfrage angezeigt.
        ''' </summary>
        ''' <value>Der Elementwert, der im Dialog referenziert wird.</value>
        ''' <remarks>Beim Setzen wird der UI-Text des Labels aktualisiert.</remarks>
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

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Gemeinsamer Klick-Handler für die Schaltflächen "Ja" und "Nein".
        ''' </summary>
        ''' <param name="sender">Der auslösende Button (<see cref="ButtonYes"/> oder <see cref="ButtonNo"/>).</param>
        ''' <param name="e">Ereignisdaten (werden hier nicht verwendet).</param>
        ''' <remarks>
        ''' Setzt je nach gedrückter Schaltfläche das entsprechende <see cref="System.Windows.Forms.DialogResult"/> und schließt das Formular.
        ''' </remarks>
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonYes.Click, ButtonNo.Click
            If sender Is Me.ButtonYes Then ' Welcher Button wurde gedrückt?
                Me.DialogResult = System.Windows.Forms.DialogResult.OK ' Ergebnis setzen
            ElseIf sender Is Me.ButtonNo Then
                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel ' Ergebnis setzen
            End If
            Me.Close() ' Dialog schließen
        End Sub

        ''' <summary>
        ''' Initialisiert alle Steuerelemente des Dialogs und deren Eigenschaften / Layout.
        ''' </summary>
        ''' <remarks>
        ''' Methode wird vom Konstruktor aufgerufen. Änderungen sollten über den Designer erfolgen.
        ''' </remarks>
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

#Region "überschriebene Methoden"

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

    End Class

End Namespace