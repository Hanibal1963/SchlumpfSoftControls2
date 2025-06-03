<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AniGifTest
    Inherits System.Windows.Forms.UserControl

    'UserControl1 überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AniGifTest))
        Me.AniGif1 = New SchlumpfSoft.Controls.AniGifControl.AniGif()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxBorderStyle = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxSizeMode = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericUpDownFramesPerSecund = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownZoomfaktor = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBoxAutostart = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCustomDisplaySpeed = New System.Windows.Forms.CheckBox()
        CType(Me.NumericUpDownFramesPerSecund, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownZoomfaktor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AniGif1
        '
        Me.AniGif1.AutoPlay = False
        Me.AniGif1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AniGif1.CustomDisplaySpeed = False
        Me.AniGif1.FramesPerSecond = New Decimal(New Integer() {10, 0, 0, 0})
        Me.AniGif1.Gif = CType(resources.GetObject("AniGif1.Gif"), System.Drawing.Bitmap)
        Me.AniGif1.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.SizeMode.Normal
        Me.AniGif1.Location = New System.Drawing.Point(19, 23)
        Me.AniGif1.Name = "AniGif1"
        Me.AniGif1.Size = New System.Drawing.Size(181, 157)
        Me.AniGif1.TabIndex = 0
        Me.AniGif1.ZoomFactor = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(223, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Rahmenstil:"
        '
        'ComboBoxBorderStyle
        '
        Me.ComboBoxBorderStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBoxBorderStyle.FormattingEnabled = True
        Me.ComboBoxBorderStyle.Items.AddRange(New Object() {"kein", "einfach", "3D"})
        Me.ComboBoxBorderStyle.Location = New System.Drawing.Point(317, 24)
        Me.ComboBoxBorderStyle.Name = "ComboBoxBorderStyle"
        Me.ComboBoxBorderStyle.Size = New System.Drawing.Size(178, 21)
        Me.ComboBoxBorderStyle.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(223, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Anzeigeart:"
        '
        'ComboBoxSizeMode
        '
        Me.ComboBoxSizeMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBoxSizeMode.FormattingEnabled = True
        Me.ComboBoxSizeMode.Items.AddRange(New Object() {"normal", "zeintriert", "gezoomt", "ausgefüllt"})
        Me.ComboBoxSizeMode.Location = New System.Drawing.Point(317, 53)
        Me.ComboBoxSizeMode.Name = "ComboBoxSizeMode"
        Me.ComboBoxSizeMode.Size = New System.Drawing.Size(178, 21)
        Me.ComboBoxSizeMode.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(223, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(211, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "benutzerdefinierte Anzeigegeschwindigkeit:"
        '
        'NumericUpDownFramesPerSecund
        '
        Me.NumericUpDownFramesPerSecund.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDownFramesPerSecund.Location = New System.Drawing.Point(440, 84)
        Me.NumericUpDownFramesPerSecund.Name = "NumericUpDownFramesPerSecund"
        Me.NumericUpDownFramesPerSecund.Size = New System.Drawing.Size(55, 16)
        Me.NumericUpDownFramesPerSecund.TabIndex = 6
        '
        'NumericUpDownZoomfaktor
        '
        Me.NumericUpDownZoomfaktor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDownZoomfaktor.Location = New System.Drawing.Point(440, 106)
        Me.NumericUpDownZoomfaktor.Name = "NumericUpDownZoomfaktor"
        Me.NumericUpDownZoomfaktor.Size = New System.Drawing.Size(55, 16)
        Me.NumericUpDownZoomfaktor.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(223, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Zoomfaktor:"
        '
        'CheckBoxAutostart
        '
        Me.CheckBoxAutostart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxAutostart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CheckBoxAutostart.Location = New System.Drawing.Point(226, 156)
        Me.CheckBoxAutostart.Name = "CheckBoxAutostart"
        Me.CheckBoxAutostart.Size = New System.Drawing.Size(269, 24)
        Me.CheckBoxAutostart.TabIndex = 9
        Me.CheckBoxAutostart.Text = "automatisch starten:"
        Me.CheckBoxAutostart.UseVisualStyleBackColor = True
        '
        'CheckBoxCustomDisplaySpeed
        '
        Me.CheckBoxCustomDisplaySpeed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxCustomDisplaySpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CheckBoxCustomDisplaySpeed.Location = New System.Drawing.Point(226, 128)
        Me.CheckBoxCustomDisplaySpeed.Name = "CheckBoxCustomDisplaySpeed"
        Me.CheckBoxCustomDisplaySpeed.Size = New System.Drawing.Size(269, 24)
        Me.CheckBoxCustomDisplaySpeed.TabIndex = 10
        Me.CheckBoxCustomDisplaySpeed.Text = "benutzerdefinierte Geschwindigkeit benutzen:"
        Me.CheckBoxCustomDisplaySpeed.UseVisualStyleBackColor = True
        '
        'AniGifTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckBoxCustomDisplaySpeed)
        Me.Controls.Add(Me.CheckBoxAutostart)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.NumericUpDownZoomfaktor)
        Me.Controls.Add(Me.NumericUpDownFramesPerSecund)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBoxSizeMode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxBorderStyle)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.AniGif1)
        Me.Name = "AniGifTest"
        Me.Size = New System.Drawing.Size(511, 200)
        CType(Me.NumericUpDownFramesPerSecund, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownZoomfaktor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents AniGif1 As SchlumpfSoft.Controls.AniGifControl.AniGif
    Private WithEvents Label1 As Label
    Private WithEvents ComboBoxBorderStyle As ComboBox
    Private WithEvents Label2 As Label
    Private WithEvents ComboBoxSizeMode As ComboBox
    Private WithEvents Label3 As Label
    Private WithEvents NumericUpDownFramesPerSecund As NumericUpDown
    Private WithEvents NumericUpDownZoomfaktor As NumericUpDown
    Private WithEvents Label4 As Label
    Private WithEvents CheckBoxAutostart As CheckBox
    Private WithEvents CheckBoxCustomDisplaySpeed As CheckBox
End Class
