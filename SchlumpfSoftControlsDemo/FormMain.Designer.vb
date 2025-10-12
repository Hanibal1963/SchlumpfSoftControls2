Imports SchlumpfSoftControlsDemoComponents

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
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
        Me.ToolStripContainer = New System.Windows.Forms.ToolStripContainer()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.TabPageAniGif = New System.Windows.Forms.TabPage()
        Me.AniGifDemo1 = New SchlumpfSoftControlsDemoComponents.AniGifDemo()
        Me.TabPageColorProgressBar = New System.Windows.Forms.TabPage()
        Me.ColorProgressBarDemo2 = New SchlumpfSoftControlsDemoComponents.ColorProgressBarDemo()
        Me.TabPageDriveWatcher = New System.Windows.Forms.TabPage()
        Me.DriveWatcherDemo1 = New SchlumpfSoftControlsDemoComponents.DriveWatcherDemo()
        Me.TabPageExplorerTreeView = New System.Windows.Forms.TabPage()
        Me.ExplorerTreeViewDemo1 = New SchlumpfSoftControlsDemoComponents.ExplorerTreeViewDemo()
        Me.TabPageNotifyForm = New System.Windows.Forms.TabPage()
        Me.NotifyFormDemo1 = New SchlumpfSoftControlsDemoComponents.NotifyFormDemo()
        Me.TabPageSevenSegment = New System.Windows.Forms.TabPage()
        Me.SevenSegmentDemo2 = New SchlumpfSoftControlsDemoComponents.SevenSegmentDemo()
        Me.TabPageShape = New System.Windows.Forms.TabPage()
        Me.ShapeDemo1 = New SchlumpfSoftControlsDemoComponents.ShapeDemo()
        Me.TabPageTransparentLabel = New System.Windows.Forms.TabPage()
        Me.TransparentLabelDemo1 = New SchlumpfSoftControlsDemoComponents.TransparentLabelDemo()
        Me.TabPageWizard = New System.Windows.Forms.TabPage()
        Me.WizardDemo1 = New SchlumpfSoftControlsDemoComponents.WizardDemo()
        Me.TabPageIniFile = New System.Windows.Forms.TabPage()
        Me.IniFileDemo1 = New SchlumpfSoftControlsDemoComponents.IniFileDemo()
        Me.TabPageExtRTF = New System.Windows.Forms.TabPage()
        Me.ExtendedRTFDemo1 = New SchlumpfSoftControlsDemoComponents.ExtendedRTFDemo()
        Me.TabPageFileSearch = New System.Windows.Forms.TabPage()
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
        Me.ToolStripMenuItemIniFileTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemExtendedRTFTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemFileSearchTest = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColorProgressBarDemo1 = New SchlumpfSoftControlsDemoComponents.ColorProgressBarDemo()
        Me.SevenSegmentDemo1 = New SchlumpfSoftControlsDemoComponents.SevenSegmentDemo()
        Me.FileSearchDemo1 = New SchlumpfSoftControlsDemoComponents.FileSearchDemo()
        Me.ToolStripContainer.ContentPanel.SuspendLayout()
        Me.ToolStripContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.TabPageAniGif.SuspendLayout()
        Me.TabPageColorProgressBar.SuspendLayout()
        Me.TabPageDriveWatcher.SuspendLayout()
        Me.TabPageExplorerTreeView.SuspendLayout()
        Me.TabPageNotifyForm.SuspendLayout()
        Me.TabPageSevenSegment.SuspendLayout()
        Me.TabPageShape.SuspendLayout()
        Me.TabPageTransparentLabel.SuspendLayout()
        Me.TabPageWizard.SuspendLayout()
        Me.TabPageIniFile.SuspendLayout()
        Me.TabPageExtRTF.SuspendLayout()
        Me.TabPageFileSearch.SuspendLayout()
        Me.MenuStripMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer
        '
        '
        'ToolStripContainer.ContentPanel
        '
        Me.ToolStripContainer.ContentPanel.Controls.Add(Me.TabControl)
        Me.ToolStripContainer.ContentPanel.Size = New System.Drawing.Size(830, 561)
        Me.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer.Name = "ToolStripContainer"
        Me.ToolStripContainer.Size = New System.Drawing.Size(830, 585)
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
        Me.TabControl.Controls.Add(Me.TabPageIniFile)
        Me.TabControl.Controls.Add(Me.TabPageExtRTF)
        Me.TabControl.Controls.Add(Me.TabPageFileSearch)
        Me.TabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl.Location = New System.Drawing.Point(0, 0)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(830, 561)
        Me.TabControl.TabIndex = 0
        '
        'TabPageAniGif
        '
        'Me.TabPageAniGif.Controls.Add(Me.AniGifDemo1)
        Me.TabPageAniGif.Location = New System.Drawing.Point(4, 22)
        Me.TabPageAniGif.Name = "TabPageAniGif"
        Me.TabPageAniGif.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAniGif.Size = New System.Drawing.Size(822, 535)
        Me.TabPageAniGif.TabIndex = 0
        Me.TabPageAniGif.Text = "AniGif"
        Me.TabPageAniGif.UseVisualStyleBackColor = True
        '
        'AniGifDemo1
        '
        Me.AniGifDemo1.Location = New System.Drawing.Point(8, 6)
        Me.AniGifDemo1.Name = "AniGifDemo1"
        Me.AniGifDemo1.Size = New System.Drawing.Size(575, 278)
        Me.AniGifDemo1.TabIndex = 0
        '
        'TabPageColorProgressBar
        '
        'Me.TabPageColorProgressBar.Controls.Add(Me.ColorProgressBarDemo2)
        Me.TabPageColorProgressBar.Location = New System.Drawing.Point(4, 22)
        Me.TabPageColorProgressBar.Name = "TabPageColorProgressBar"
        Me.TabPageColorProgressBar.Size = New System.Drawing.Size(822, 535)
        Me.TabPageColorProgressBar.TabIndex = 1
        Me.TabPageColorProgressBar.Text = "ColorProgressBar"
        Me.TabPageColorProgressBar.UseVisualStyleBackColor = True
        '
        'ColorProgressBarDemo2
        '
        Me.ColorProgressBarDemo2.Location = New System.Drawing.Point(17, 20)
        Me.ColorProgressBarDemo2.Name = "ColorProgressBarDemo2"
        Me.ColorProgressBarDemo2.Size = New System.Drawing.Size(351, 174)
        Me.ColorProgressBarDemo2.TabIndex = 0
        '
        'TabPageDriveWatcher
        '
        'Me.TabPageDriveWatcher.Controls.Add(Me.DriveWatcherDemo1)
        Me.TabPageDriveWatcher.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDriveWatcher.Name = "TabPageDriveWatcher"
        Me.TabPageDriveWatcher.Size = New System.Drawing.Size(822, 535)
        Me.TabPageDriveWatcher.TabIndex = 2
        Me.TabPageDriveWatcher.Text = "DriveWatcher"
        Me.TabPageDriveWatcher.UseVisualStyleBackColor = True
        '
        'DriveWatcherDemo1
        '
        Me.DriveWatcherDemo1.Location = New System.Drawing.Point(17, 13)
        Me.DriveWatcherDemo1.Name = "DriveWatcherDemo1"
        Me.DriveWatcherDemo1.Size = New System.Drawing.Size(481, 271)
        Me.DriveWatcherDemo1.TabIndex = 0
        '
        'TabPageExplorerTreeView
        '
        'Me.TabPageExplorerTreeView.Controls.Add(Me.ExplorerTreeViewDemo1)
        Me.TabPageExplorerTreeView.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExplorerTreeView.Name = "TabPageExplorerTreeView"
        Me.TabPageExplorerTreeView.Size = New System.Drawing.Size(822, 535)
        Me.TabPageExplorerTreeView.TabIndex = 3
        Me.TabPageExplorerTreeView.Text = "ExplorerTreeView"
        Me.TabPageExplorerTreeView.UseVisualStyleBackColor = True
        '
        'ExplorerTreeViewDemo1
        '
        Me.ExplorerTreeViewDemo1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExplorerTreeViewDemo1.Location = New System.Drawing.Point(0, 0)
        Me.ExplorerTreeViewDemo1.Name = "ExplorerTreeViewDemo1"
        Me.ExplorerTreeViewDemo1.Size = New System.Drawing.Size(822, 535)
        Me.ExplorerTreeViewDemo1.TabIndex = 0
        '
        'TabPageNotifyForm
        '
        'Me.TabPageNotifyForm.Controls.Add(Me.NotifyFormDemo1)
        Me.TabPageNotifyForm.Location = New System.Drawing.Point(4, 22)
        Me.TabPageNotifyForm.Name = "TabPageNotifyForm"
        Me.TabPageNotifyForm.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageNotifyForm.Size = New System.Drawing.Size(822, 535)
        Me.TabPageNotifyForm.TabIndex = 4
        Me.TabPageNotifyForm.Text = "NotifyForm"
        Me.TabPageNotifyForm.UseVisualStyleBackColor = True
        '
        'NotifyFormDemo1
        '
        Me.NotifyFormDemo1.Location = New System.Drawing.Point(8, 6)
        Me.NotifyFormDemo1.Name = "NotifyFormDemo1"
        Me.NotifyFormDemo1.Size = New System.Drawing.Size(395, 260)
        Me.NotifyFormDemo1.TabIndex = 0
        '
        'TabPageSevenSegment
        '
        'Me.TabPageSevenSegment.Controls.Add(Me.SevenSegmentDemo2)
        Me.TabPageSevenSegment.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSevenSegment.Name = "TabPageSevenSegment"
        Me.TabPageSevenSegment.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSevenSegment.Size = New System.Drawing.Size(822, 535)
        Me.TabPageSevenSegment.TabIndex = 5
        Me.TabPageSevenSegment.Text = "SevenSegment"
        Me.TabPageSevenSegment.UseVisualStyleBackColor = True
        '
        'SevenSegmentDemo2
        '
        Me.SevenSegmentDemo2.Location = New System.Drawing.Point(18, 20)
        Me.SevenSegmentDemo2.Name = "SevenSegmentDemo2"
        Me.SevenSegmentDemo2.Size = New System.Drawing.Size(318, 248)
        Me.SevenSegmentDemo2.TabIndex = 0
        '
        'TabPageShape
        '
        'Me.TabPageShape.Controls.Add(Me.ShapeDemo1)
        Me.TabPageShape.Location = New System.Drawing.Point(4, 22)
        Me.TabPageShape.Name = "TabPageShape"
        Me.TabPageShape.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageShape.Size = New System.Drawing.Size(822, 535)
        Me.TabPageShape.TabIndex = 6
        Me.TabPageShape.Text = "Shape"
        Me.TabPageShape.UseVisualStyleBackColor = True
        '
        'ShapeDemo1
        '
        Me.ShapeDemo1.Location = New System.Drawing.Point(8, 18)
        Me.ShapeDemo1.Name = "ShapeDemo1"
        Me.ShapeDemo1.Size = New System.Drawing.Size(608, 320)
        Me.ShapeDemo1.TabIndex = 0
        '
        'TabPageTransparentLabel
        '
        'Me.TabPageTransparentLabel.Controls.Add(Me.TransparentLabelDemo1)
        Me.TabPageTransparentLabel.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTransparentLabel.Name = "TabPageTransparentLabel"
        Me.TabPageTransparentLabel.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTransparentLabel.Size = New System.Drawing.Size(822, 535)
        Me.TabPageTransparentLabel.TabIndex = 7
        Me.TabPageTransparentLabel.Text = "TransparentLabel"
        Me.TabPageTransparentLabel.UseVisualStyleBackColor = True
        '
        'TransparentLabelDemo1
        '
        Me.TransparentLabelDemo1.Location = New System.Drawing.Point(19, 25)
        Me.TransparentLabelDemo1.Name = "TransparentLabelDemo1"
        Me.TransparentLabelDemo1.Size = New System.Drawing.Size(349, 214)
        Me.TransparentLabelDemo1.TabIndex = 0
        '
        'TabPageWizard
        '
        'Me.TabPageWizard.Controls.Add(Me.WizardDemo1)
        Me.TabPageWizard.Location = New System.Drawing.Point(4, 22)
        Me.TabPageWizard.Name = "TabPageWizard"
        Me.TabPageWizard.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageWizard.Size = New System.Drawing.Size(822, 535)
        Me.TabPageWizard.TabIndex = 8
        Me.TabPageWizard.Text = "Wizard"
        Me.TabPageWizard.UseVisualStyleBackColor = True
        '
        'WizardDemo1
        '
        Me.WizardDemo1.Location = New System.Drawing.Point(8, 16)
        Me.WizardDemo1.Name = "WizardDemo1"
        Me.WizardDemo1.Size = New System.Drawing.Size(570, 300)
        Me.WizardDemo1.TabIndex = 0
        '
        'TabPageIniFile
        '
        'Me.TabPageIniFile.Controls.Add(Me.IniFileDemo1)
        Me.TabPageIniFile.Location = New System.Drawing.Point(4, 22)
        Me.TabPageIniFile.Name = "TabPageIniFile"
        Me.TabPageIniFile.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageIniFile.Size = New System.Drawing.Size(822, 535)
        Me.TabPageIniFile.TabIndex = 9
        Me.TabPageIniFile.Text = "IniFile"
        Me.TabPageIniFile.UseVisualStyleBackColor = True
        '
        'IniFileDemo1
        '
        Me.IniFileDemo1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IniFileDemo1.Location = New System.Drawing.Point(3, 3)
        Me.IniFileDemo1.Name = "IniFileDemo1"
        Me.IniFileDemo1.Size = New System.Drawing.Size(816, 529)
        Me.IniFileDemo1.TabIndex = 0
        '
        'TabPageExtRTF
        '
        'Me.TabPageExtRTF.Controls.Add(Me.ExtendedRTFDemo1)
        Me.TabPageExtRTF.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExtRTF.Name = "TabPageExtRTF"
        Me.TabPageExtRTF.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExtRTF.Size = New System.Drawing.Size(822, 535)
        Me.TabPageExtRTF.TabIndex = 10
        Me.TabPageExtRTF.Text = "ExtendedRTF"
        Me.TabPageExtRTF.UseVisualStyleBackColor = True
        '
        'ExtendedRTFDemo1
        '
        Me.ExtendedRTFDemo1.Location = New System.Drawing.Point(8, 16)
        Me.ExtendedRTFDemo1.Name = "ExtendedRTFDemo1"
        Me.ExtendedRTFDemo1.Size = New System.Drawing.Size(806, 500)
        Me.ExtendedRTFDemo1.TabIndex = 0
        '
        'TabPageFileSearch
        '
        'Me.TabPageFileSearch.Controls.Add(Me.FileSearchDemo1)
        Me.TabPageFileSearch.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFileSearch.Name = "TabPageFileSearch"
        Me.TabPageFileSearch.Size = New System.Drawing.Size(822, 535)
        Me.TabPageFileSearch.TabIndex = 11
        Me.TabPageFileSearch.Text = "FileSearch"
        '
        'MenuStripMain
        '
        Me.MenuStripMain.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.MenuStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemDatei, Me.ToolStripMenuItemAnsicht})
        Me.MenuStripMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripMain.Name = "MenuStripMain"
        Me.MenuStripMain.Size = New System.Drawing.Size(830, 24)
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
        Me.ToolStripMenuItemAnsicht.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemAniGifTest, Me.ToolStripMenuItemColorProgressBarTest, Me.ToolStripMenuItemDriveWatcherTest, Me.ToolStripMenuItemExplorerTreeViewTest, Me.ToolStripMenuItemNotifyFormTest, Me.ToolStripMenuItemSevenSegmentTest, Me.ToolStripMenuItemShapeTest, Me.ToolStripMenuItemTransparentLabelTest, Me.ToolStripMenuItemWizardTest, Me.ToolStripMenuItemIniFileTest, Me.ToolStripMenuItemExtendedRTFTest, Me.ToolStripMenuItemFileSearchTest})
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
        'ToolStripMenuItemIniFileTest
        '
        Me.ToolStripMenuItemIniFileTest.Name = "ToolStripMenuItemIniFileTest"
        Me.ToolStripMenuItemIniFileTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemIniFileTest.Text = "IniFile"
        '
        'ToolStripMenuItemExtendedRTFTest
        '
        Me.ToolStripMenuItemExtendedRTFTest.Name = "ToolStripMenuItemExtendedRTFTest"
        Me.ToolStripMenuItemExtendedRTFTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemExtendedRTFTest.Text = "ExtendedRTF Test"
        '
        'ToolStripMenuItemFileSearchTest
        '
        Me.ToolStripMenuItemFileSearchTest.Name = "ToolStripMenuItemFileSearchTest"
        Me.ToolStripMenuItemFileSearchTest.Size = New System.Drawing.Size(188, 22)
        Me.ToolStripMenuItemFileSearchTest.Text = "FileSearch Test"
        '
        'ColorProgressBarDemo1
        '
        Me.ColorProgressBarDemo1.Location = New System.Drawing.Point(8, 15)
        Me.ColorProgressBarDemo1.Name = "ColorProgressBarDemo1"
        Me.ColorProgressBarDemo1.Size = New System.Drawing.Size(359, 124)
        Me.ColorProgressBarDemo1.TabIndex = 0
        '
        'SevenSegmentDemo1
        '
        Me.SevenSegmentDemo1.Location = New System.Drawing.Point(22, 15)
        Me.SevenSegmentDemo1.Name = "SevenSegmentDemo1"
        Me.SevenSegmentDemo1.Size = New System.Drawing.Size(320, 245)
        Me.SevenSegmentDemo1.TabIndex = 0
        '
        'FileSearchDemo1
        '
        Me.FileSearchDemo1.Location = New System.Drawing.Point(16, 20)
        Me.FileSearchDemo1.Name = "FileSearchDemo1"
        Me.FileSearchDemo1.Size = New System.Drawing.Size(746, 489)
        Me.FileSearchDemo1.TabIndex = 0
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 585)
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
        Me.TabPageColorProgressBar.ResumeLayout(False)
        Me.TabPageDriveWatcher.ResumeLayout(False)
        Me.TabPageExplorerTreeView.ResumeLayout(False)
        Me.TabPageNotifyForm.ResumeLayout(False)
        Me.TabPageSevenSegment.ResumeLayout(False)
        Me.TabPageShape.ResumeLayout(False)
        Me.TabPageTransparentLabel.ResumeLayout(False)
        Me.TabPageWizard.ResumeLayout(False)
        Me.TabPageIniFile.ResumeLayout(False)
        Me.TabPageExtRTF.ResumeLayout(False)
        Me.TabPageFileSearch.ResumeLayout(False)
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
    Private WithEvents AniGifDemo1 As AniGifDemo
    Private WithEvents ColorProgressBarDemo1 As ColorProgressBarDemo
    Friend WithEvents DriveWatcherDemo1 As DriveWatcherDemo
    Friend WithEvents NotifyFormDemo1 As NotifyFormDemo
    Friend WithEvents ExplorerTreeViewDemo1 As ExplorerTreeViewDemo
    Friend WithEvents SevenSegmentDemo1 As SevenSegmentDemo
    Friend WithEvents ShapeDemo1 As ShapeDemo
    Friend WithEvents TransparentLabelDemo1 As TransparentLabelDemo
    Friend WithEvents ColorProgressBarDemo2 As ColorProgressBarDemo
    Friend WithEvents SevenSegmentDemo2 As SevenSegmentDemo
    Friend WithEvents WizardDemo1 As WizardDemo
    Private WithEvents TabPageIniFile As TabPage
    Private WithEvents ToolStripMenuItemIniFileTest As ToolStripMenuItem
    Private WithEvents IniFileDemo1 As IniFileDemo
    Private WithEvents TabPageExtRTF As TabPage
    Private WithEvents ToolStripMenuItemExtendedRTFTest As ToolStripMenuItem
    Private WithEvents ExtendedRTFDemo1 As ExtendedRTFDemo
    Private WithEvents ToolStripMenuItemFileSearchTest As ToolStripMenuItem
    Private WithEvents TabPageFileSearch As TabPage
    Friend WithEvents FileSearchDemo1 As FileSearchDemo
End Class
