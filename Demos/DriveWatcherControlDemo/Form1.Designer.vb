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
        Me.Label_Result = New System.Windows.Forms.Label()
        Me.Label_Info = New System.Windows.Forms.Label()
        Me.DriveWatcher1 = New SchlumpfSoft.Controls.DriveWatcherControl.DriveWatcher(Me.components)
        Me.SuspendLayout()
        '
        'Label_Result
        '
        Me.Label_Result.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Result.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_Result.Location = New System.Drawing.Point(12, 58)
        Me.Label_Result.Name = "Label_Result"
        Me.Label_Result.Size = New System.Drawing.Size(372, 133)
        Me.Label_Result.TabIndex = 6
        '
        'Label_Info
        '
        Me.Label_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Info.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_Info.Location = New System.Drawing.Point(12, 9)
        Me.Label_Info.Name = "Label_Info"
        Me.Label_Info.Size = New System.Drawing.Size(372, 37)
        Me.Label_Info.TabIndex = 5
        Me.Label_Info.Text = "Lege eine CD ein, stecke einen USB-Stick oder ein externes Laufwerk an" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "oder füge" &
    " ein virtuelles Laufwerk hinzu und beobachte die Reaktion."
        '
        'DriveWatcher1
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 205)
        Me.Controls.Add(Me.Label_Result)
        Me.Controls.Add(Me.Label_Info)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DriveWatcher - Control - Demo"
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents Label_Result As Label
    Private WithEvents Label_Info As Label
    Private WithEvents DriveWatcher1 As SchlumpfSoft.Controls.DriveWatcherControl.DriveWatcher
End Class
