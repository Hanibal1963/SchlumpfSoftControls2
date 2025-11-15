' *************************************************************************************************
' DeleteItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************


' TODO: Code noch überarbeiten

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
    Public Class DeleteItemDialog : Inherits System.Windows.Forms.Form

        ' Hält den aktuell anzuzeigenden Wert des zu löschenden Elements.
        ' Initialisiert als leere Zeichenfolge.
        Private _ItemValue As String = $""

        ''' <summary>
        ''' Gibt den Wert des Elements zurück oder legt ihn fest.
        ''' Beim Setzen wird ein Ereignis ausgelöst, um abhängige UI-Elemente zu aktualisieren.
        ''' </summary>
        Public Property ItemValue As String
            Get
                Return Me._ItemValue
            End Get
            Set
                Me._ItemValue = Value
                Me.Label.Text = $"Möchten Sie das Element '{Me._ItemValue}' wirklich löschen?"
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
            Me.ButtonYes = New System.Windows.Forms.Button()
            Me.ButtonNo = New System.Windows.Forms.Button()
            Me.Label = New System.Windows.Forms.Label()
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
            TableLayoutPanel.Controls.Add(Me.ButtonYes, 0, 0)
            TableLayoutPanel.Controls.Add(Me.ButtonNo, 1, 0)
            TableLayoutPanel.Location = New System.Drawing.Point(149, 40)
            TableLayoutPanel.Name = "TableLayoutPanel"
            TableLayoutPanel.RowCount = 1
            TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
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

        Private WithEvents ButtonYes As System.Windows.Forms.Button
        Private WithEvents ButtonNo As System.Windows.Forms.Button
        Private WithEvents Label As System.Windows.Forms.Label

        Public Sub New()
            InitializeComponent()
        End Sub

    End Class

End Namespace