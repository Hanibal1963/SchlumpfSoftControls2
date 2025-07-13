<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExplorerTreeViewTest
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.ExplorerTreeView1 = New SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView()
        Me.SuspendLayout()
        '
        'ExplorerTreeView1
        '
        Me.ExplorerTreeView1.Location = New System.Drawing.Point(31, 29)
        Me.ExplorerTreeView1.Name = "ExplorerTreeView1"
        Me.ExplorerTreeView1.Size = New System.Drawing.Size(340, 437)
        Me.ExplorerTreeView1.TabIndex = 0
        '
        'ExplorerTreeViewTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ExplorerTreeView1)
        Me.Name = "ExplorerTreeViewTest"
        Me.Size = New System.Drawing.Size(444, 507)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExplorerTreeView1 As SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView
End Class
