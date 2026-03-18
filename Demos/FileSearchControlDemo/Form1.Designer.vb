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
        Me.components = New System.ComponentModel.Container()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.ButtonStop = New System.Windows.Forms.Button()
        Me.ButtonStart = New System.Windows.Forms.Button()
        Me.CheckBoxSearchInSubFolders = New System.Windows.Forms.CheckBox()
        Me.TextBoxSearchPattern = New System.Windows.Forms.TextBox()
        Me.LabelSearchPattern = New System.Windows.Forms.Label()
        Me.ButtonSelectFolder = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.LabelSearchPath = New System.Windows.Forms.Label()
        Me.FileSearch1 = New SchlumpfSoft.Controls.FileSearchControl.FileSearch(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.SuspendLayout()
        '
        'LabelStatus
        '
        Me.LabelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelStatus.Location = New System.Drawing.Point(23, 452)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(698, 20)
        Me.LabelStatus.TabIndex = 27
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Location = New System.Drawing.Point(23, 96)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox3.Size = New System.Drawing.Size(698, 342)
        Me.TextBox3.TabIndex = 26
        Me.TextBox3.WordWrap = False
        '
        'ButtonStop
        '
        Me.ButtonStop.Location = New System.Drawing.Point(611, 52)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Size = New System.Drawing.Size(110, 23)
        Me.ButtonStop.TabIndex = 25
        Me.ButtonStop.Text = "Stop"
        Me.ButtonStop.UseVisualStyleBackColor = True
        '
        'ButtonStart
        '
        Me.ButtonStart.Location = New System.Drawing.Point(478, 51)
        Me.ButtonStart.Name = "ButtonStart"
        Me.ButtonStart.Size = New System.Drawing.Size(112, 25)
        Me.ButtonStart.TabIndex = 24
        Me.ButtonStart.Text = "Start"
        Me.ButtonStart.UseVisualStyleBackColor = True
        '
        'CheckBoxSearchInSubFolders
        '
        Me.CheckBoxSearchInSubFolders.AutoSize = True
        Me.CheckBoxSearchInSubFolders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxSearchInSubFolders.Location = New System.Drawing.Point(254, 52)
        Me.CheckBoxSearchInSubFolders.Name = "CheckBoxSearchInSubFolders"
        Me.CheckBoxSearchInSubFolders.Size = New System.Drawing.Size(148, 17)
        Me.CheckBoxSearchInSubFolders.TabIndex = 23
        Me.CheckBoxSearchInSubFolders.Text = "Unterordner einschliessen"
        Me.CheckBoxSearchInSubFolders.UseVisualStyleBackColor = True
        '
        'TextBoxSearchPattern
        '
        Me.TextBoxSearchPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxSearchPattern.Location = New System.Drawing.Point(97, 51)
        Me.TextBoxSearchPattern.Name = "TextBoxSearchPattern"
        Me.TextBoxSearchPattern.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxSearchPattern.Size = New System.Drawing.Size(135, 20)
        Me.TextBoxSearchPattern.TabIndex = 22
        Me.TextBoxSearchPattern.WordWrap = False
        '
        'LabelSearchPattern
        '
        Me.LabelSearchPattern.AutoSize = True
        Me.LabelSearchPattern.Location = New System.Drawing.Point(20, 53)
        Me.LabelSearchPattern.Name = "LabelSearchPattern"
        Me.LabelSearchPattern.Size = New System.Drawing.Size(68, 13)
        Me.LabelSearchPattern.TabIndex = 21
        Me.LabelSearchPattern.Text = "Suche nach:"
        '
        'ButtonSelectFolder
        '
        Me.ButtonSelectFolder.Location = New System.Drawing.Point(613, 17)
        Me.ButtonSelectFolder.Name = "ButtonSelectFolder"
        Me.ButtonSelectFolder.Size = New System.Drawing.Size(108, 22)
        Me.ButtonSelectFolder.TabIndex = 20
        Me.ButtonSelectFolder.Text = "Ordner wählen ..."
        Me.ButtonSelectFolder.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(97, 17)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(510, 20)
        Me.TextBox1.TabIndex = 19
        Me.TextBox1.WordWrap = False
        '
        'LabelSearchPath
        '
        Me.LabelSearchPath.AutoSize = True
        Me.LabelSearchPath.Location = New System.Drawing.Point(20, 19)
        Me.LabelSearchPath.Name = "LabelSearchPath"
        Me.LabelSearchPath.Size = New System.Drawing.Size(53, 13)
        Me.LabelSearchPath.TabIndex = 18
        Me.LabelSearchPath.Text = "Startpfad:"
        '
        'FileSearch1
        '
        Me.FileSearch1.SearchInSubfolders = False
        Me.FileSearch1.SearchPattern = "*.*"
        Me.FileSearch1.StartPath = ""
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Ordner wählen"
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 493)
        Me.Controls.Add(Me.LabelStatus)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.ButtonStop)
        Me.Controls.Add(Me.ButtonStart)
        Me.Controls.Add(Me.CheckBoxSearchInSubFolders)
        Me.Controls.Add(Me.TextBoxSearchPattern)
        Me.Controls.Add(Me.LabelSearchPattern)
        Me.Controls.Add(Me.ButtonSelectFolder)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.LabelSearchPath)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Text = "FileSearch Control Demo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents LabelStatus As Label
    Private WithEvents TextBox3 As TextBox
    Private WithEvents ButtonStop As Button
    Private WithEvents ButtonStart As Button
    Private WithEvents CheckBoxSearchInSubFolders As CheckBox
    Private WithEvents TextBoxSearchPattern As TextBox
    Private WithEvents LabelSearchPattern As Label
    Private WithEvents ButtonSelectFolder As Button
    Private WithEvents TextBox1 As TextBox
    Private WithEvents LabelSearchPath As Label
    Private WithEvents FileSearch1 As SchlumpfSoft.Controls.FileSearchControl.FileSearch
    Private WithEvents FolderBrowserDialog1 As FolderBrowserDialog
End Class
