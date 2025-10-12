Namespace IniFileControl

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class RenameItemDialog
        Inherits System.Windows.Forms.Form

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
            TableLayoutPanel.Controls.Add(Me.ButtonYes, 0, 0)
            TableLayoutPanel.Controls.Add(Me.ButtonNo, 1, 0)
            TableLayoutPanel.Location = New System.Drawing.Point(188, 68)
            TableLayoutPanel.Name = "TableLayoutPanel"
            TableLayoutPanel.RowCount = 1
            TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
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
            Me.Label.AutoSize = True
            Me.Label.Location = New System.Drawing.Point(12, 9)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(255, 26)
            Me.Label.TabIndex = 1
            Me.Label.Text = "Wollen Sie das Element ""{0}"" wirklich umbenennen?" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "neuer Wert:"
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
            'IniFileRenameItemDialog
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
            Me.Name = "IniFileRenameItemDialog"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Element umbenennen"
            TableLayoutPanel.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents ButtonYes As System.Windows.Forms.Button
        Private WithEvents ButtonNo As System.Windows.Forms.Button
        Private WithEvents Label As System.Windows.Forms.Label
        Private WithEvents TextBox As System.Windows.Forms.TextBox
    End Class


End Namespace