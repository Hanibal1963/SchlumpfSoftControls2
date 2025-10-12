<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DriveWatcherDemo
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
        Me.components = New System.ComponentModel.Container()
        Me.DriveWatcher1 = New SchlumpfSoft.Controls.DriveWatcherControl.DriveWatcher(Me.components)
        Me.Label_Result = New System.Windows.Forms.Label()
        Me.Label_Info = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label_Result
        '
        Me.Label_Result.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Result.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_Result.Location = New System.Drawing.Point(13, 62)
        Me.Label_Result.Name = "Label_Result"
        Me.Label_Result.Size = New System.Drawing.Size(365, 133)
        Me.Label_Result.TabIndex = 4
        '
        'Label_Info
        '
        Me.Label_Info.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Info.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_Info.Location = New System.Drawing.Point(13, 13)
        Me.Label_Info.Name = "Label_Info"
        Me.Label_Info.Size = New System.Drawing.Size(365, 37)
        Me.Label_Info.TabIndex = 3
        Me.Label_Info.Text = "Lege eine CD ein, stecke einen USB-Stick oder ein externes Laufwerk an" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "oder füge" &
    " ein virtuelles Laufwerk hinzu und beobachte die Reaktion."
        '
        'DriveWatcherDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label_Result)
        Me.Controls.Add(Me.Label_Info)
        Me.Name = "DriveWatcherDemo"
        Me.Size = New System.Drawing.Size(388, 208)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents DriveWatcher1 As SchlumpfSoft.Controls.DriveWatcherControl.DriveWatcher
    Private WithEvents Label_Result As Label
    Private WithEvents Label_Info As Label
End Class
