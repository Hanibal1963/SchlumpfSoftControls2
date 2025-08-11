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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
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
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(16, 468)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(538, 57)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Label1"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(527, 434)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(27, 22)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(19, 434)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(502, 20)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.WordWrap = False
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "suche Pfad"
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'ExplorerTreeViewTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ExplorerTreeView1)
        Me.Name = "ExplorerTreeViewTest"
        Me.Size = New System.Drawing.Size(566, 542)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ExplorerTreeView1 As SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView
    Friend WithEvents Label1 As Label
    Private WithEvents Button1 As Button
    Private WithEvents TextBox1 As TextBox
    Private WithEvents FolderBrowserDialog1 As FolderBrowserDialog
End Class
