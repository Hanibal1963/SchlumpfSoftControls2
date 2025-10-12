<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FileSearchDemo
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
        Me.components = New System.ComponentModel.Container()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FileSearch1 = New SchlumpfSoft.Controls.FileSearchControl.FileSearch(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(611, 52)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(110, 23)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "Stop"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(478, 51)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(112, 25)
        Me.Button2.TabIndex = 14
        Me.Button2.Text = "Start"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.Location = New System.Drawing.Point(254, 52)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(148, 17)
        Me.CheckBox1.TabIndex = 13
        Me.CheckBox1.Text = "Unterordner einschliessen"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Location = New System.Drawing.Point(97, 51)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox2.Size = New System.Drawing.Size(135, 20)
        Me.TextBox2.TabIndex = 12
        Me.TextBox2.WordWrap = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Suche nach:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(613, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 22)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Ordner wählen ..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(97, 17)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(510, 20)
        Me.TextBox1.TabIndex = 9
        Me.TextBox1.WordWrap = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Startpfad:"
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
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Location = New System.Drawing.Point(23, 96)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox3.Size = New System.Drawing.Size(698, 342)
        Me.TextBox3.TabIndex = 16
        Me.TextBox3.WordWrap = False
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(23, 452)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(698, 20)
        Me.Label3.TabIndex = 17
        '
        'FilesearchDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FilesearchDemo"
        Me.Size = New System.Drawing.Size(738, 496)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents Button3 As Button
    Private WithEvents Button2 As Button
    Private WithEvents CheckBox1 As CheckBox
    Private WithEvents TextBox2 As TextBox
    Private WithEvents Label2 As Label
    Private WithEvents Button1 As Button
    Private WithEvents TextBox1 As TextBox
    Private WithEvents Label1 As Label
    Private WithEvents FileSearch1 As SchlumpfSoft.Controls.FileSearchControl.FileSearch
    Private WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Private WithEvents TextBox3 As TextBox
    Private WithEvents Label3 As Label
End Class
