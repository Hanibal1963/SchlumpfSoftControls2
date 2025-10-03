<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IniFileDemo
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
        Me.IniFile1 = New SchlumpfSoft.Controls.IniFileControl.IniFile()
        Me.ContentView1 = New SchlumpfSoft.Controls.IniFileControl.ContentView()
        Me.CommentEdit1 = New SchlumpfSoft.Controls.IniFileControl.CommentEdit()
        Me.EntryValueEdit1 = New SchlumpfSoft.Controls.IniFileControl.EntryValueEdit()
        Me.ListEdit1 = New SchlumpfSoft.Controls.IniFileControl.ListEdit()
        Me.SuspendLayout()
        '
        'IniFile1
        '
        Me.IniFile1.AutoSave = False
        Me.IniFile1.CommentPrefix = Global.Microsoft.VisualBasic.ChrW(59)
        Me.IniFile1.FileName = "neue Datei.ini"
        Me.IniFile1.FilePath = ""
        '
        'ContentView1
        '
        Me.ContentView1.Lines = Nothing
        Me.ContentView1.Location = New System.Drawing.Point(22, 40)
        Me.ContentView1.Name = "ContentView1"
        Me.ContentView1.Size = New System.Drawing.Size(118, 159)
        Me.ContentView1.TabIndex = 0
        Me.ContentView1.TitelText = "ContentView"
        '
        'CommentEdit1
        '
        Me.CommentEdit1.Comment = New String() {""}
        Me.CommentEdit1.Location = New System.Drawing.Point(179, 56)
        Me.CommentEdit1.Name = "CommentEdit1"
        Me.CommentEdit1.SectionName = Nothing
        Me.CommentEdit1.Size = New System.Drawing.Size(226, 106)
        Me.CommentEdit1.TabIndex = 1
        Me.CommentEdit1.TitelText = "CommentEdit"
        '
        'EntryValueEdit1
        '
        Me.EntryValueEdit1.Location = New System.Drawing.Point(179, 206)
        Me.EntryValueEdit1.Name = "EntryValueEdit1"
        Me.EntryValueEdit1.SelectedEntry = Nothing
        Me.EntryValueEdit1.SelectedSection = ""
        Me.EntryValueEdit1.Size = New System.Drawing.Size(194, 84)
        Me.EntryValueEdit1.TabIndex = 2
        Me.EntryValueEdit1.TitelText = "EntryValueEdit"
        Me.EntryValueEdit1.Value = ""
        '
        'ListEdit1
        '
        Me.ListEdit1.ListItems = New String() {""}
        Me.ListEdit1.Location = New System.Drawing.Point(39, 321)
        Me.ListEdit1.Name = "ListEdit1"
        Me.ListEdit1.SelectedSection = ""
        Me.ListEdit1.Size = New System.Drawing.Size(353, 102)
        Me.ListEdit1.TabIndex = 3
        Me.ListEdit1.TitelText = "ListEdit"
        '
        'IniFileDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ListEdit1)
        Me.Controls.Add(Me.EntryValueEdit1)
        Me.Controls.Add(Me.CommentEdit1)
        Me.Controls.Add(Me.ContentView1)
        Me.Name = "IniFileDemo"
        Me.Size = New System.Drawing.Size(849, 523)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents IniFile1 As SchlumpfSoft.Controls.IniFileControl.IniFile
    Private WithEvents ContentView1 As SchlumpfSoft.Controls.IniFileControl.ContentView
    Private WithEvents CommentEdit1 As SchlumpfSoft.Controls.IniFileControl.CommentEdit
    Private WithEvents EntryValueEdit1 As SchlumpfSoft.Controls.IniFileControl.EntryValueEdit
    Private WithEvents ListEdit1 As SchlumpfSoft.Controls.IniFileControl.ListEdit
End Class
