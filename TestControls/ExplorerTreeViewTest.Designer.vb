<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExplorerTreeViewTest
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ExplorerTreeView1 = New SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ExplorerTreeView1
        '
        Me.ExplorerTreeView1.BackColor = System.Drawing.SystemColors.Window
        Me.ExplorerTreeView1.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExplorerTreeView1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ExplorerTreeView1.Indent = 19
        Me.ExplorerTreeView1.ItemHeight = 16
        Me.ExplorerTreeView1.LineColor = System.Drawing.Color.Black
        Me.ExplorerTreeView1.Location = New System.Drawing.Point(19, 16)
        Me.ExplorerTreeView1.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.ExplorerTreeView1.Name = "ExplorerTreeView1"
        Me.ExplorerTreeView1.ShowLines = True
        Me.ExplorerTreeView1.ShowPlusMinus = True
        Me.ExplorerTreeView1.ShowRootLines = True
        Me.ExplorerTreeView1.Size = New System.Drawing.Size(322, 410)
        Me.ExplorerTreeView1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.Location = New System.Drawing.Point(16, 442)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(538, 57)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Label1"
        '
        'ExplorerTreeViewTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ExplorerTreeView1)
        Me.Name = "ExplorerTreeViewTest"
        Me.Size = New System.Drawing.Size(576, 516)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExplorerTreeView1 As SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView
    Friend WithEvents Label1 As Label
End Class
