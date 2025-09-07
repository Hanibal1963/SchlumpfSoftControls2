Namespace ExplorerTreeViewControl

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class ExplorerTreeView
        Inherits System.Windows.Forms.UserControl

        'Wird vom Windows Form-Designer benötigt.
        Private components As System.ComponentModel.IContainer

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.DW = New SchlumpfSoft.Controls.DriveWatcherControl.DriveWatcher(Me.components)
            Me.IL = New System.Windows.Forms.ImageList(Me.components)
            Me.TV = New System.Windows.Forms.TreeView()
            Me.SuspendLayout()
            '
            'DW
            '
            '
            'IL
            '
            Me.IL.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
            Me.IL.ImageSize = New System.Drawing.Size(16, 16)
            Me.IL.TransparentColor = System.Drawing.Color.Transparent
            '
            'TV
            '
            Me.TV.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TV.HideSelection = False
            Me.TV.ImageIndex = 0
            Me.TV.ImageList = Me.IL
            Me.TV.Location = New System.Drawing.Point(0, 0)
            Me.TV.Name = "TV"
            Me.TV.SelectedImageIndex = 0
            Me.TV.Size = New System.Drawing.Size(313, 404)
            Me.TV.TabIndex = 0
            '
            'ExplorerTreeView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.TV)
            Me.Name = "ExplorerTreeView"
            Me.Size = New System.Drawing.Size(313, 404)
            Me.ResumeLayout(False)

        End Sub

        Private WithEvents DW As SchlumpfSoft.Controls.DriveWatcherControl.DriveWatcher
        Private WithEvents IL As System.Windows.Forms.ImageList
        Private WithEvents TV As System.Windows.Forms.TreeView
    End Class

End Namespace