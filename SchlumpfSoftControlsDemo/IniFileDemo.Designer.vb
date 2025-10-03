<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IniFileDemo
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
        Me.IniFile1 = New SchlumpfSoft.Controls.IniFileControl.IniFile()
        Me.SuspendLayout()
        '
        'IniFile1
        '
        Me.IniFile1.AutoSave = False
        Me.IniFile1.CommentPrefix = Global.Microsoft.VisualBasic.ChrW(59)
        Me.IniFile1.FileName = "neue Datei.ini"
        Me.IniFile1.FilePath = ""
        '
        'IniFileDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "IniFileDemo"
        Me.Size = New System.Drawing.Size(849, 523)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents IniFile1 As SchlumpfSoft.Controls.IniFileControl.IniFile
End Class
