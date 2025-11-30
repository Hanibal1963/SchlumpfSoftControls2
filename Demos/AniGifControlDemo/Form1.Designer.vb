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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.AniGif = New SchlumpfSoft.Controls.AniGifControl.AniGif()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NumericUpDownFramesPerSecound = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownZoomFactor = New System.Windows.Forms.NumericUpDown()
        Me.CheckBoxAutoplay = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCustomDisplaySpeed = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxAnsicht = New System.Windows.Forms.ComboBox()
        Me.ButtonBack = New System.Windows.Forms.Button()
        Me.ButtonForward = New System.Windows.Forms.Button()
        Me.LabelAni = New System.Windows.Forms.Label()
        Me.ButtonStart = New System.Windows.Forms.Button()
        Me.ButtonStop = New System.Windows.Forms.Button()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.NumericUpDownFramesPerSecound, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownZoomFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 3
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116.0!))
        Me.TableLayoutPanel.Controls.Add(Me.AniGif, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.Label1, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.NumericUpDownFramesPerSecound, 2, 3)
        Me.TableLayoutPanel.Controls.Add(Me.NumericUpDownZoomFactor, 2, 4)
        Me.TableLayoutPanel.Controls.Add(Me.CheckBoxAutoplay, 2, 1)
        Me.TableLayoutPanel.Controls.Add(Me.CheckBoxCustomDisplaySpeed, 2, 2)
        Me.TableLayoutPanel.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.Label3, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.Label4, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.Label5, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.ComboBoxAnsicht, 2, 0)
        Me.TableLayoutPanel.Controls.Add(Me.ButtonBack, 0, 5)
        Me.TableLayoutPanel.Controls.Add(Me.ButtonForward, 1, 5)
        Me.TableLayoutPanel.Controls.Add(Me.LabelAni, 0, 6)
        Me.TableLayoutPanel.Controls.Add(Me.ButtonStart, 0, 7)
        Me.TableLayoutPanel.Controls.Add(Me.ButtonStop, 1, 7)
        Me.TableLayoutPanel.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 8
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel.Size = New System.Drawing.Size(629, 250)
        Me.TableLayoutPanel.TabIndex = 2
        '
        'AniGif
        '
        Me.AniGif.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AniGif.AutoPlay = False
        Me.AniGif.CustomDisplaySpeed = False
        Me.AniGif.FramesPerSecond = New Decimal(New Integer() {10, 0, 0, 0})
        Me.AniGif.Gif = CType(resources.GetObject("AniGif.Gif"), System.Drawing.Bitmap)
        Me.AniGif.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.AniGif.ImageSizeMode.Normal
        Me.AniGif.Location = New System.Drawing.Point(3, 3)
        Me.AniGif.Name = "AniGif"
        Me.TableLayoutPanel.SetRowSpan(Me.AniGif, 5)
        Me.AniGif.Size = New System.Drawing.Size(307, 158)
        Me.AniGif.TabIndex = 0
        Me.AniGif.ZoomFactor = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(316, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Anzeigemodus"
        '
        'NumericUpDownFramesPerSecound
        '
        Me.NumericUpDownFramesPerSecound.Location = New System.Drawing.Point(516, 87)
        Me.NumericUpDownFramesPerSecound.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NumericUpDownFramesPerSecound.Name = "NumericUpDownFramesPerSecound"
        Me.NumericUpDownFramesPerSecound.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDownFramesPerSecound.TabIndex = 6
        '
        'NumericUpDownZoomFactor
        '
        Me.NumericUpDownZoomFactor.Location = New System.Drawing.Point(516, 115)
        Me.NumericUpDownZoomFactor.Name = "NumericUpDownZoomFactor"
        Me.NumericUpDownZoomFactor.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDownZoomFactor.TabIndex = 7
        '
        'CheckBoxAutoplay
        '
        Me.CheckBoxAutoplay.AutoSize = True
        Me.CheckBoxAutoplay.Location = New System.Drawing.Point(516, 31)
        Me.CheckBoxAutoplay.Name = "CheckBoxAutoplay"
        Me.CheckBoxAutoplay.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxAutoplay.TabIndex = 4
        Me.CheckBoxAutoplay.UseVisualStyleBackColor = True
        '
        'CheckBoxCustomDisplaySpeed
        '
        Me.CheckBoxCustomDisplaySpeed.AutoSize = True
        Me.CheckBoxCustomDisplaySpeed.Location = New System.Drawing.Point(516, 59)
        Me.CheckBoxCustomDisplaySpeed.Name = "CheckBoxCustomDisplaySpeed"
        Me.CheckBoxCustomDisplaySpeed.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxCustomDisplaySpeed.TabIndex = 5
        Me.CheckBoxCustomDisplaySpeed.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(316, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(194, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Autostart"
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(316, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(194, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Benutzerdefinierte Geschwindigkeit"
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(316, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(194, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Geschwindikeit"
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(316, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(194, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Zoomfaktor"
        '
        'ComboBoxAnsicht
        '
        Me.ComboBoxAnsicht.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxAnsicht.FormattingEnabled = True
        Me.ComboBoxAnsicht.Items.AddRange(New Object() {"Normal", "Zentriert", "Zoom", "Fill"})
        Me.ComboBoxAnsicht.Location = New System.Drawing.Point(516, 3)
        Me.ComboBoxAnsicht.Name = "ComboBoxAnsicht"
        Me.ComboBoxAnsicht.Size = New System.Drawing.Size(110, 21)
        Me.ComboBoxAnsicht.TabIndex = 10
        '
        'ButtonBack
        '
        Me.ButtonBack.Location = New System.Drawing.Point(3, 167)
        Me.ButtonBack.Name = "ButtonBack"
        Me.ButtonBack.Size = New System.Drawing.Size(111, 22)
        Me.ButtonBack.TabIndex = 12
        Me.ButtonBack.Text = "< zurück"
        Me.ButtonBack.UseVisualStyleBackColor = True
        '
        'ButtonForward
        '
        Me.ButtonForward.Location = New System.Drawing.Point(316, 167)
        Me.ButtonForward.Name = "ButtonForward"
        Me.ButtonForward.Size = New System.Drawing.Size(111, 22)
        Me.ButtonForward.TabIndex = 13
        Me.ButtonForward.Text = "vorwärts >"
        Me.ButtonForward.UseVisualStyleBackColor = True
        '
        'LabelAni
        '
        Me.LabelAni.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelAni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel.SetColumnSpan(Me.LabelAni, 3)
        Me.LabelAni.Location = New System.Drawing.Point(3, 192)
        Me.LabelAni.Name = "LabelAni"
        Me.LabelAni.Size = New System.Drawing.Size(623, 19)
        Me.LabelAni.TabIndex = 11
        Me.LabelAni.Text = "Standardanimation"
        '
        'ButtonStart
        '
        Me.ButtonStart.Location = New System.Drawing.Point(3, 223)
        Me.ButtonStart.Name = "ButtonStart"
        Me.ButtonStart.Size = New System.Drawing.Size(111, 23)
        Me.ButtonStart.TabIndex = 14
        Me.ButtonStart.Text = "Start"
        Me.ButtonStart.UseVisualStyleBackColor = True
        '
        'ButtonStop
        '
        Me.ButtonStop.Location = New System.Drawing.Point(316, 223)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Size = New System.Drawing.Size(111, 23)
        Me.ButtonStop.TabIndex = 15
        Me.ButtonStop.Text = "Stop"
        Me.ButtonStop.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 272)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AniGif - Control - Demo"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.NumericUpDownFramesPerSecound, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownZoomFactor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents TableLayoutPanel As TableLayoutPanel
    Private WithEvents AniGif As SchlumpfSoft.Controls.AniGifControl.AniGif
    Private WithEvents Label1 As Label
    Private WithEvents NumericUpDownFramesPerSecound As NumericUpDown
    Private WithEvents NumericUpDownZoomFactor As NumericUpDown
    Private WithEvents CheckBoxAutoplay As CheckBox
    Private WithEvents CheckBoxCustomDisplaySpeed As CheckBox
    Private WithEvents Label2 As Label
    Private WithEvents Label3 As Label
    Private WithEvents Label4 As Label
    Private WithEvents Label5 As Label
    Private WithEvents ComboBoxAnsicht As ComboBox
    Private WithEvents ButtonBack As Button
    Private WithEvents ButtonForward As Button
    Private WithEvents LabelAni As Label
    Private WithEvents ButtonStart As Button
    Private WithEvents ButtonStop As Button
End Class
