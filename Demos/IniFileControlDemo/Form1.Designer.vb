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
        Me.Panel = New System.Windows.Forms.Panel()
        Me.CheckBoxAutoSave = New System.Windows.Forms.CheckBox()
        Me.ContentView = New SchlumpfSoft.Controls.IniFileControl.ContentView()
        Me.EntryValueEdit = New SchlumpfSoft.Controls.IniFileControl.EntryValueEdit()
        Me.FileCommentEdit = New SchlumpfSoft.Controls.IniFileControl.CommentEdit()
        Me.EntryListEdit = New SchlumpfSoft.Controls.IniFileControl.ListEdit()
        Me.SectionsListEdit = New SchlumpfSoft.Controls.IniFileControl.ListEdit()
        Me.SectionCommentEdit = New SchlumpfSoft.Controls.IniFileControl.CommentEdit()
        Me.IniFile = New SchlumpfSoft.Controls.IniFileControl.IniFile()
        Me.Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel
        '
        Me.Panel.AutoScroll = True
        Me.Panel.Controls.Add(Me.CheckBoxAutoSave)
        Me.Panel.Controls.Add(Me.ContentView)
        Me.Panel.Controls.Add(Me.EntryValueEdit)
        Me.Panel.Controls.Add(Me.FileCommentEdit)
        Me.Panel.Controls.Add(Me.EntryListEdit)
        Me.Panel.Controls.Add(Me.SectionsListEdit)
        Me.Panel.Controls.Add(Me.SectionCommentEdit)
        Me.Panel.Location = New System.Drawing.Point(12, 12)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(938, 363)
        Me.Panel.TabIndex = 7
        '
        'CheckBoxAutoSave
        '
        Me.CheckBoxAutoSave.AutoSize = True
        Me.CheckBoxAutoSave.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxAutoSave.Location = New System.Drawing.Point(3, 330)
        Me.CheckBoxAutoSave.Name = "CheckBoxAutoSave"
        Me.CheckBoxAutoSave.Size = New System.Drawing.Size(135, 17)
        Me.CheckBoxAutoSave.TabIndex = 6
        Me.CheckBoxAutoSave.Text = "automatisch speichern:"
        Me.CheckBoxAutoSave.UseVisualStyleBackColor = True
        '
        'ContentView
        '
        Me.ContentView.Lines = Nothing
        Me.ContentView.Location = New System.Drawing.Point(3, 3)
        Me.ContentView.Name = "ContentView"
        Me.ContentView.Size = New System.Drawing.Size(247, 188)
        Me.ContentView.TabIndex = 0
        Me.ContentView.TitelText = "Dateiinhalt"
        '
        'EntryValueEdit
        '
        Me.EntryValueEdit.Location = New System.Drawing.Point(600, 203)
        Me.EntryValueEdit.Name = "EntryValueEdit"
        Me.EntryValueEdit.SelectedEntry = Nothing
        Me.EntryValueEdit.SelectedSection = ""
        Me.EntryValueEdit.Size = New System.Drawing.Size(327, 112)
        Me.EntryValueEdit.TabIndex = 5
        Me.EntryValueEdit.TitelText = "Eintragswert"
        Me.EntryValueEdit.Value = ""
        '
        'FileCommentEdit
        '
        Me.FileCommentEdit.Comment = New String() {""}
        Me.FileCommentEdit.Location = New System.Drawing.Point(3, 197)
        Me.FileCommentEdit.Name = "FileCommentEdit"
        Me.FileCommentEdit.SectionName = Nothing
        Me.FileCommentEdit.Size = New System.Drawing.Size(247, 118)
        Me.FileCommentEdit.TabIndex = 1
        Me.FileCommentEdit.TitelText = "Dateikommentar"
        '
        'EntryListEdit
        '
        Me.EntryListEdit.ListItems = New String() {""}
        Me.EntryListEdit.Location = New System.Drawing.Point(600, 12)
        Me.EntryListEdit.Name = "EntryListEdit"
        Me.EntryListEdit.SelectedSection = ""
        Me.EntryListEdit.Size = New System.Drawing.Size(327, 179)
        Me.EntryListEdit.TabIndex = 4
        Me.EntryListEdit.TitelText = "Einträge"
        '
        'SectionsListEdit
        '
        Me.SectionsListEdit.ListItems = New String() {""}
        Me.SectionsListEdit.Location = New System.Drawing.Point(267, 3)
        Me.SectionsListEdit.Name = "SectionsListEdit"
        Me.SectionsListEdit.SelectedSection = ""
        Me.SectionsListEdit.Size = New System.Drawing.Size(327, 188)
        Me.SectionsListEdit.TabIndex = 2
        Me.SectionsListEdit.TitelText = "Abschnitte"
        '
        'SectionCommentEdit
        '
        Me.SectionCommentEdit.Comment = New String() {""}
        Me.SectionCommentEdit.Location = New System.Drawing.Point(267, 203)
        Me.SectionCommentEdit.Name = "SectionCommentEdit"
        Me.SectionCommentEdit.SectionName = Nothing
        Me.SectionCommentEdit.Size = New System.Drawing.Size(327, 112)
        Me.SectionCommentEdit.TabIndex = 3
        Me.SectionCommentEdit.TitelText = "Abschnittskommentar"
        '
        'IniFile
        '
        Me.IniFile.AutoSave = False
        Me.IniFile.CommentPrefix = Global.Microsoft.VisualBasic.ChrW(59)
        Me.IniFile.FileName = "neue Datei.ini"
        Me.IniFile.FilePath = ""
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 398)
        Me.Controls.Add(Me.Panel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IniFile Control Demo"
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents Panel As Panel
    Private WithEvents CheckBoxAutoSave As CheckBox
    Private WithEvents ContentView As SchlumpfSoft.Controls.IniFileControl.ContentView
    Private WithEvents EntryValueEdit As SchlumpfSoft.Controls.IniFileControl.EntryValueEdit
    Private WithEvents FileCommentEdit As SchlumpfSoft.Controls.IniFileControl.CommentEdit
    Private WithEvents EntryListEdit As SchlumpfSoft.Controls.IniFileControl.ListEdit
    Private WithEvents SectionsListEdit As SchlumpfSoft.Controls.IniFileControl.ListEdit
    Private WithEvents SectionCommentEdit As SchlumpfSoft.Controls.IniFileControl.CommentEdit
    Private WithEvents IniFile As SchlumpfSoft.Controls.IniFileControl.IniFile
End Class
