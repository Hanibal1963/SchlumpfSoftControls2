<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim SplitContainer1 As System.Windows.Forms.SplitContainer
        Dim SplitContainer2 As System.Windows.Forms.SplitContainer
        Me.ExplorerTreeView1 = New SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView()
        Me.TextBox = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ListView = New System.Windows.Forms.ListView()
        Me.ButtonSearchPath = New System.Windows.Forms.Button()
        Me.LabelPath = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        SplitContainer1 = New System.Windows.Forms.SplitContainer()
        SplitContainer2 = New System.Windows.Forms.SplitContainer()
        CType(SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        CType(SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.Panel2.SuspendLayout()
        SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        SplitContainer1.Location = New System.Drawing.Point(12, 12)
        SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        SplitContainer1.Panel1.Controls.Add(Me.ExplorerTreeView1)
        '
        'SplitContainer1.Panel2
        '
        SplitContainer1.Panel2.Controls.Add(SplitContainer2)
        SplitContainer1.Size = New System.Drawing.Size(639, 437)
        SplitContainer1.SplitterDistance = 190
        SplitContainer1.TabIndex = 2
        '
        'ExplorerTreeView1
        '
        Me.ExplorerTreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExplorerTreeView1.Indent = 19
        Me.ExplorerTreeView1.ItemHeight = 16
        Me.ExplorerTreeView1.LineColor = System.Drawing.Color.Black
        Me.ExplorerTreeView1.Location = New System.Drawing.Point(0, 0)
        Me.ExplorerTreeView1.Name = "ExplorerTreeView1"
        Me.ExplorerTreeView1.ShowLines = True
        Me.ExplorerTreeView1.ShowPlusMinus = True
        Me.ExplorerTreeView1.ShowRootLines = True
        Me.ExplorerTreeView1.Size = New System.Drawing.Size(190, 437)
        Me.ExplorerTreeView1.TabIndex = 0
        '
        'SplitContainer2
        '
        SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        SplitContainer2.Location = New System.Drawing.Point(0, 0)
        SplitContainer2.Name = "SplitContainer2"
        SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        SplitContainer2.Panel1.Controls.Add(Me.TextBox)
        '
        'SplitContainer2.Panel2
        '
        SplitContainer2.Panel2.Controls.Add(Me.TableLayoutPanel)
        SplitContainer2.Size = New System.Drawing.Size(445, 437)
        SplitContainer2.SplitterDistance = 118
        SplitContainer2.TabIndex = 0
        '
        'TextBox
        '
        Me.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox.Location = New System.Drawing.Point(0, 0)
        Me.TextBox.Multiline = True
        Me.TextBox.Name = "TextBox"
        Me.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox.Size = New System.Drawing.Size(445, 118)
        Me.TextBox.TabIndex = 0
        Me.TextBox.WordWrap = False
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 2
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel.Controls.Add(Me.ListView, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.ButtonSearchPath, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.LabelPath, 0, 1)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 2
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(445, 315)
        Me.TableLayoutPanel.TabIndex = 1
        '
        'ListView
        '
        Me.TableLayoutPanel.SetColumnSpan(Me.ListView, 2)
        Me.ListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView.HideSelection = False
        Me.ListView.Location = New System.Drawing.Point(3, 3)
        Me.ListView.MultiSelect = False
        Me.ListView.Name = "ListView"
        Me.ListView.Size = New System.Drawing.Size(439, 276)
        Me.ListView.TabIndex = 0
        Me.ListView.UseCompatibleStateImageBehavior = False
        Me.ListView.View = System.Windows.Forms.View.List
        '
        'ButtonSearchPath
        '
        Me.ButtonSearchPath.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonSearchPath.Location = New System.Drawing.Point(343, 285)
        Me.ButtonSearchPath.Name = "ButtonSearchPath"
        Me.ButtonSearchPath.Size = New System.Drawing.Size(99, 27)
        Me.ButtonSearchPath.TabIndex = 1
        Me.ButtonSearchPath.Text = "Pfad suchen ..."
        Me.ButtonSearchPath.UseVisualStyleBackColor = True
        '
        'LabelPath
        '
        Me.LabelPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPath.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelPath.Location = New System.Drawing.Point(3, 287)
        Me.LabelPath.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelPath.Name = "LabelPath"
        Me.LabelPath.Size = New System.Drawing.Size(334, 20)
        Me.LabelPath.TabIndex = 2
        Me.LabelPath.Text = " "
        Me.LabelPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 461)
        Me.Controls.Add(SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ExplorerTreeView Demo"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        SplitContainer2.Panel1.PerformLayout()
        SplitContainer2.Panel2.ResumeLayout(False)
        CType(SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents ExplorerTreeView1 As SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView
    Private WithEvents TextBox As TextBox
    Private WithEvents TableLayoutPanel As TableLayoutPanel
    Private WithEvents ListView As ListView
    Private WithEvents ButtonSearchPath As Button
    Private WithEvents LabelPath As Label
    Private WithEvents FolderBrowserDialog1 As FolderBrowserDialog
End Class
