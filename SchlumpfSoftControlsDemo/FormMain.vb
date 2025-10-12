' *************************************************************************************************
' FormMain.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Public Class FormMain

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.Text = Application.ProductName & " - " & Application.ProductVersion
        Me.TabControl.SelectedTab = Me.TabPageAniGif
    End Sub

    Private Sub ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles ToolStripMenuItemBeenden.Click, ToolStripMenuItemAniGifTest.Click, ToolStripMenuItemColorProgressBarTest.Click,
        ToolStripMenuItemDriveWatcherTest.Click, ToolStripMenuItemExplorerTreeViewTest.Click, ToolStripMenuItemNotifyFormTest.Click,
        ToolStripMenuItemSevenSegmentTest.Click, ToolStripMenuItemShapeTest.Click, ToolStripMenuItemTransparentLabelTest.Click,
        ToolStripMenuItemWizardTest.Click, ToolStripMenuItemIniFileTest.Click, ToolStripMenuItemExtendedRTFTest.Click,
        ToolStripMenuItemFileSearchTest.Click

        Select Case True
            Case sender Is ToolStripMenuItemBeenden
                Me.Close()
            Case sender Is ToolStripMenuItemAniGifTest
                Me.TabControl.SelectedTab = Me.TabPageAniGif
            Case sender Is ToolStripMenuItemColorProgressBarTest
                Me.TabControl.SelectedTab = Me.TabPageColorProgressBar
            Case sender Is ToolStripMenuItemDriveWatcherTest
                Me.TabControl.SelectedTab = Me.TabPageDriveWatcher
            Case sender Is ToolStripMenuItemExplorerTreeViewTest
                Me.TabControl.SelectedTab = Me.TabPageExplorerTreeView
            Case sender Is ToolStripMenuItemNotifyFormTest
                Me.TabControl.SelectedTab = Me.TabPageNotifyForm
            Case sender Is ToolStripMenuItemSevenSegmentTest
                Me.TabControl.SelectedTab = Me.TabPageSevenSegment
            Case sender Is ToolStripMenuItemShapeTest
                Me.TabControl.SelectedTab = Me.TabPageShape
            Case sender Is ToolStripMenuItemTransparentLabelTest
                Me.TabControl.SelectedTab = Me.TabPageTransparentLabel
            Case sender Is ToolStripMenuItemWizardTest
                Me.TabControl.SelectedTab = Me.TabPageWizard
            Case sender Is ToolStripMenuItemIniFileTest
                Me.TabControl.SelectedTab = Me.TabPageIniFile
            Case sender Is ToolStripMenuItemExtendedRTFTest
                Me.TabControl.SelectedTab = Me.TabPageExtRTF
            Case sender Is ToolStripMenuItemFileSearchTest
                Me.TabControl.SelectedTab = Me.TabPageFileSearch
        End Select

    End Sub

End Class
