<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.TabPageAniGif = New System.Windows.Forms.TabPage()
        Me.TabPageColorProgressBar = New System.Windows.Forms.TabPage()
        Me.TabPageDriveWatcher = New System.Windows.Forms.TabPage()
        Me.TabPageExplorerTreeView = New System.Windows.Forms.TabPage()
        Me.TabPageNotifyForm = New System.Windows.Forms.TabPage()
        Me.TabPageSevenSegment = New System.Windows.Forms.TabPage()
        Me.TabPageShape = New System.Windows.Forms.TabPage()
        Me.TabPageTransparentLabel = New System.Windows.Forms.TabPage()
        Me.TabPageWizard = New System.Windows.Forms.TabPage()
        Me.MenuStripMain = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItemDatei = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemBeenden = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemAnsicht = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemAniGifTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemColorProgressBarTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemDriveWatcherTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemExplorerTreeViewTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemNotifyFormTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemSevenSegmentTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemShapeTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemTransparentLabelTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemWizardTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.AniGifDemo1 = New SchlumpfSoftControlsDemo.AniGifDemo()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.TabPageAniGif.SuspendLayout()
        Me.MenuStripMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.TabControl)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(599, 383)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(599, 407)
        Me.ToolStripContainer.TabIndex = 0
        Me.ToolStripContainer.Text = "ToolStripContainer1"
        '
        'ToolStripContainer.TopToolStripPanel
        '
        Me.ToolStripContainer.TopToolStripPanel.Controls.Add(Me.MenuStripMain)
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPageAniGif)
        Me.TabControl.Controls.Add(Me.TabPageColorProgressBar)
        Me.TabControl.Controls.Add(Me.TabPageDriveWatcher)
        Me.TabControl.Controls.Add(Me.TabPageExplorerTreeView)
        Me.TabControl.Controls.Add(Me.TabPageNotifyForm)
        Me.TabControl.Controls.Add(Me.TabPageSevenSegment)
        Me.TabControl.Controls.Add(Me.TabPageShape)
        Me.TabControl.Controls.Add(Me.TabPageTransparentLabel)
        Me.TabControl.Controls.Add(Me.TabPageWizard)
        Me.TabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl.Location = New System.Drawing.Point(0, 0)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(599, 383)
        Me.TabControl.TabIndex = 0
        '
        'TabPageAniGif
        '
        Me.TabPageAniGif.Controls.Add(Me.AniGifDemo1)
        Me.TabPageAniGif.Location = New System.Drawing.Point(4, 22)
        Me.TabPageAniGif.Name = "TabPageAniGif"
        Me.TabPageAniGif.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAniGif.Size = New System.Drawing.Size(591, 357)
        Me.TabPageAniGif.TabIndex = 0
        Me.TabPageAniGif.Text = "AniGif"
        Me.TabPageAniGif.UseVisualStyleBackColor = True
        '
        'TabPageColorProgressBar
        '
        Me.TabPageColorProgressBar.Location = New System.Drawing.Point(4, 22)
        Me.TabPageColorProgressBar.Name = "TabPageColorProgressBar"
        Me.TabPageColorProgressBar.Size = New System.Drawing.Size(591, 357)
        Me.TabPageColorProgressBar.TabIndex = 1
        Me.TabPageColorProgressBar.Text = "ColorProgressBar"
        Me.TabPageColorProgressBar.UseVisualStyleBackColor = True
        '
        'TabPageDriveWatcher
        '
        Me.TabPageDriveWatcher.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDriveWatcher.Name = "TabPageDriveWatcher"
        Me.TabPageDriveWatcher.Size = New System.Drawing.Size(591, 357)
        Me.TabPageDriveWatcher.TabIndex = 2
        Me.TabPageDriveWatcher.Text = "DriveWatcher"
        Me.TabPageDriveWatcher.UseVisualStyleBackColor = True
        '
        'TabPageExplorerTreeView
        '
        Me.TabPageExplorerTreeView.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExplorerTreeView.Name = "TabPageExplorerTreeView"
        Me.TabPageExplorerTreeView.Size = New System.Drawing.Size(591, 357)
        Me.TabPageExplorerTreeView.TabIndex = 3
        Me.TabPageExplorerTreeView.Text = "ExplorerTreeView"
        Me.TabPageExplorerTreeView.UseVisualStyleBackColor = True
        '
        'TabPageNotifyForm
        '
        Me.TabPageNotifyForm.Location = New System.Drawing.Point(4, 22)
        Me.TabPageNotifyForm.Name = "TabPageNotifyForm"
        Me.TabPageNotifyForm.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageNotifyForm.Size = New System.Drawing.Size(591, 357)
        Me.TabPageNotifyForm.TabIndex = 4
        Me.TabPageNotifyForm.Text = "NotifyForm"
        Me.TabPageNotifyForm.UseVisualStyleBackColor = True
        '
        'TabPageSevenSegment
        '
        Me.TabPageSevenSegment.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSevenSegment.Name = "TabPageSevenSegment"
        Me.TabPageSevenSegment.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSevenSegment.Size = New System.Drawing.Size(591, 357)
        Me.TabPageSevenSegment.TabIndex = 5
        Me.TabPageSevenSegment.Text = "SevenSegment"
        Me.TabPageSevenSegment.UseVisualStyleBackColor = True
        '
        'TabPageShape
        '
        Me.TabPageShape.Location = New System.Drawing.Point(4, 22)
        Me.TabPageShape.Name = "TabPageShape"
        Me.TabPageShape.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageShape.Size = New System.Drawing.Size(591, 357)
        Me.TabPageShape.TabIndex = 6
        Me.TabPageShape.Text = "Shape"
        Me.TabPageShape.UseVisualStyleBackColor = True
        '
        'TabPageTransparentLabel
        '
        Me.TabPageTransparentLabel.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTransparentLabel.Name = "TabPageTransparentLabel"
        Me.TabPageTransparentLabel.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTransparentLabel.Size = New System.Drawing.Size(591, 357)
        Me.TabPageTransparentLabel.TabIndex = 7
        Me.TabPageTransparentLabel.Text = "TransparentLabel"
        Me.TabPageTransparentLabel.UseVisualStyleBackColor = True
        '
        'TabPageWizard
        '
        Me.TabPageWizard.Location = New System.Drawing.Point(4, 22)
        Me.TabPageWizard.Name = "TabPageWizard"
        Me.TabPageWizard.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageWizard.Size = New System.Drawing.Size(591, 357)
        Me.TabPageWizard.TabIndex = 8
        Me.TabPageWizard.Text = "Wizard"
        Me.TabPageWizard.UseVisualStyleBackColor = True
        '
        'MenuStripMain
        '
        Me.MenuStripMain.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.MenuStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemDatei, Me.ToolStripMenuItemAnsicht})
        Me.MenuStripMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripMain.Name = "MenuStripMain"
        Me.MenuStripMain.Size = New System.Drawing.Size(599, 24)
        Me.MenuStripMain.TabIndex = 0
        Me.MenuStripMain.Text = "MenuStripMain"
        '
        'ToolStripMenuItemDatei
        '
        Me.ToolStripMenuItemDatei.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemBeenden})
        Me.ToolStripMenuItemDatei.Name = "ToolStripMenuItemDatei"
        Me.ToolStripMenuItemDatei.Size = New System.Drawing.Size(46, 20)
        Me.ToolStripMenuItemDatei.Text = "Datei"
        '
        'ToolStripMenuItemBeenden
        '
        Me.ToolStripMenuItemBeenden.Name = "ToolStripMenuItemBeenden"
        Me.ToolStripMenuItemBeenden.Size = New System.Drawing.Size(120, 22)
        Me.ToolStripMenuItemBeenden.Text = "Beenden"
        '
        'ToolStripMenuItemAnsicht
        '
        Me.ToolStripMenuItemAnsicht.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemAniGifTest, Me.ToolStripMenuItemColorProgressBarTest, Me.ToolStripMenuItemDriveWatcherTest, Me.ToolStripMenuItemExplorerTreeViewTest, Me.ToolStripMenuItemNotifyFormTest, Me.ToolStripMenuItemSevenSegmentTest, Me.ToolStripMenuItemShapeTest, Me.ToolStripMenuItemTransparentLabelTest, Me.ToolStripMenuItemWizardTest})
        Me.ToolStripMenuItemAnsicht.Name = "ToolStripMenuItemAnsicht"
        Me.ToolStripMenuItemAnsicht.Size = New System.Drawing.Size(59, 20)
        Me.ToolStripMenuItemAnsicht.Text = "Ansicht"
        '
        'ToolStripMenuItemAniGifTest
        '
        Me.ToolStripMenuItemAniGifTest.Name = "ToolStripMenuItemAniGifTest"
        Me.ToolStripMenuItemAniGifTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemAniGifTest.Text = "AniGif Test"
        '
        'ToolStripMenuItemColorProgressBarTest
        '
        Me.ToolStripMenuItemColorProgressBarTest.Name = "ToolStripMenuItemColorProgressBarTest"
        Me.ToolStripMenuItemColorProgressBarTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemColorProgressBarTest.Text = "ColorProgressBar Test"
        '
        'ToolStripMenuItemDriveWatcherTest
        '
        Me.ToolStripMenuItemDriveWatcherTest.Name = "ToolStripMenuItemDriveWatcherTest"
        Me.ToolStripMenuItemDriveWatcherTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemDriveWatcherTest.Text = "DriveWatcher Test"
        '
        'ToolStripMenuItemExplorerTreeViewTest
        '
        Me.ToolStripMenuItemExplorerTreeViewTest.Name = "ToolStripMenuItemExplorerTreeViewTest"
        Me.ToolStripMenuItemExplorerTreeViewTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemExplorerTreeViewTest.Text = "ExplorerTreeView Test"
        '
        'ToolStripMenuItemNotifyFormTest
        '
        Me.ToolStripMenuItemNotifyFormTest.Name = "ToolStripMenuItemNotifyFormTest"
        Me.ToolStripMenuItemNotifyFormTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemNotifyFormTest.Text = "NotifyForm Test"
        '
        'ToolStripMenuItemSevenSegmentTest
        '
        Me.ToolStripMenuItemSevenSegmentTest.Name = "ToolStripMenuItemSevenSegmentTest"
        Me.ToolStripMenuItemSevenSegmentTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemSevenSegmentTest.Text = "SevenSegment Test"
        '
        'ToolStripMenuItemShapeTest
        '
        Me.ToolStripMenuItemShapeTest.Name = "ToolStripMenuItemShapeTest"
        Me.ToolStripMenuItemShapeTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemShapeTest.Text = "Shape Test"
        '
        'ToolStripMenuItemTransparentLabelTest
        '
        Me.ToolStripMenuItemTransparentLabelTest.Name = "ToolStripMenuItemTransparentLabelTest"
        Me.ToolStripMenuItemTransparentLabelTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemTransparentLabelTest.Text = "TransparentLabel Test"
        '
        'ToolStripMenuItemWizardTest
        '
        Me.ToolStripMenuItemWizardTest.Name = "ToolStripMenuItemWizardTest"
        Me.ToolStripMenuItemWizardTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemWizardTest.Text = "Wizard Test"
        '
        'AniGifDemo1
        '
        Me.AniGifDemo1.Location = New System.Drawing.Point(8, 6)
        Me.AniGifDemo1.Name = "AniGifDemo1"
        Me.AniGifDemo1.Size = New System.Drawing.Size(575, 343)
        Me.AniGifDemo1.TabIndex = 0
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(599, 407)
        Me.Controls.Add(Me.ToolStripContainer)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStripMain
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMain"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormMain"
        Me.ToolStripContainer.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer.ResumeLayout(False)
        Me.ToolStripContainer.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.TabPageAniGif.ResumeLayout(False)
        Me.MenuStripMain.ResumeLayout(False)
        Me.MenuStripMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents ToolStripContainer As ToolStripContainer
    Private WithEvents MenuStripMain As MenuStrip
    Private WithEvents ToolStripMenuItemDatei As ToolStripMenuItem
    Private WithEvents ToolStripMenuItemAnsicht As ToolStripMenuItem
    Private WithEvents ToolStripMenuItemBeenden As ToolStripMenuItem
    Private WithEvents TabControl As TabControl
    Private WithEvents TabPageAniGif As TabPage
    Private WithEvents ToolStripMenuItemAniGifTest As ToolStripMenuItem
    Private WithEvents TabPageColorProgressBar As TabPage
    Private WithEvents ToolStripMenuItemColorProgressBarTest As ToolStripMenuItem
    Private WithEvents TabPageDriveWatcher As TabPage
    Private WithEvents ToolStripMenuItemDriveWatcherTest As ToolStripMenuItem
    Private WithEvents TabPageExplorerTreeView As TabPage
    Private WithEvents ToolStripMenuItemExplorerTreeViewTest As ToolStripMenuItem
    Private WithEvents TabPageNotifyForm As TabPage
    Private WithEvents ToolStripMenuItemNotifyFormTest As ToolStripMenuItem
    Private WithEvents TabPageSevenSegment As TabPage
    Private WithEvents ToolStripMenuItemSevenSegmentTest As ToolStripMenuItem
    Private WithEvents TabPageShape As TabPage
    Private WithEvents ToolStripMenuItemShapeTest As ToolStripMenuItem
    Private WithEvents TabPageTransparentLabel As TabPage
    Private WithEvents ToolStripMenuItemTransparentLabelTest As ToolStripMenuItem
    Private WithEvents TabPageWizard As TabPage
    Private WithEvents ToolStripMenuItemWizardTest As ToolStripMenuItem
    Friend WithEvents AniGifDemo1 As AniGifDemo
End Class
