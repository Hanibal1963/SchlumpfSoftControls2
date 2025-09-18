<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AniGifDemo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AniGifDemo))
        Me.AniGif = New SchlumpfSoft.Controls.AniGifControl.AniGif()
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.AniGif.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.SizeMode.Normal
        Me.AniGif.Location = New System.Drawing.Point(3, 3)
        Me.AniGif.Name = "AniGif"
        Me.TableLayoutPanel.SetRowSpan(Me.AniGif, 5)
        Me.AniGif.Size = New System.Drawing.Size(240, 156)
        Me.AniGif.TabIndex = 0
        Me.AniGif.ZoomFactor = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 3
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
        Me.TableLayoutPanel.Controls.Add(Me.AniGif, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.Label1, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.NumericUpDown1, 2, 3)
        Me.TableLayoutPanel.Controls.Add(Me.NumericUpDown2, 2, 4)
        Me.TableLayoutPanel.Controls.Add(Me.CheckBox1, 2, 1)
        Me.TableLayoutPanel.Controls.Add(Me.CheckBox2, 2, 2)
        Me.TableLayoutPanel.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.Label3, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.Label4, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.Label5, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.ComboBox1, 2, 0)
        Me.TableLayoutPanel.Controls.Add(Me.Button1, 0, 5)
        Me.TableLayoutPanel.Controls.Add(Me.Button2, 1, 5)
        Me.TableLayoutPanel.Controls.Add(Me.Label6, 0, 6)
        Me.TableLayoutPanel.Controls.Add(Me.Button3, 0, 7)
        Me.TableLayoutPanel.Controls.Add(Me.Button4, 1, 7)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 8
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel.Size = New System.Drawing.Size(562, 261)
        Me.TableLayoutPanel.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(249, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Anzeigemodus"
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(249, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(194, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Autostart"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(249, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(173, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Benutzerdefinierte Geschwindigkeit"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(449, 31)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(449, 59)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox2.TabIndex = 5
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(449, 87)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDown1.TabIndex = 6
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(449, 115)
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDown2.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(249, 84)
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
        Me.Label5.Location = New System.Drawing.Point(249, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(194, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Zoomfaktor"
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(449, 3)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(110, 21)
        Me.ComboBox1.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel.SetColumnSpan(Me.Label6, 3)
        Me.Label6.Location = New System.Drawing.Point(3, 192)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(556, 19)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Standardanimation"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(3, 165)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(111, 24)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "< zurück"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(249, 165)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(111, 24)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "vorwärts >"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(3, 223)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(111, 23)
        Me.Button3.TabIndex = 14
        Me.Button3.Text = "Start"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(249, 223)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(111, 23)
        Me.Button4.TabIndex = 15
        Me.Button4.Text = "Stop"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'AniGifDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.Name = "AniGifDemo"
        Me.Size = New System.Drawing.Size(562, 261)
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents AniGif As SchlumpfSoft.Controls.AniGifControl.AniGif
    Private WithEvents TableLayoutPanel As TableLayoutPanel
    Private WithEvents Label1 As Label
    Private WithEvents NumericUpDown1 As NumericUpDown
    Private WithEvents NumericUpDown2 As NumericUpDown
    Private WithEvents CheckBox1 As CheckBox
    Private WithEvents CheckBox2 As CheckBox
    Private WithEvents Label2 As Label
    Private WithEvents Label3 As Label
    Private WithEvents Label4 As Label
    Private WithEvents Label5 As Label
    Private WithEvents ComboBox1 As ComboBox
    Private WithEvents Label6 As Label
    Private WithEvents Button1 As Button
    Private WithEvents Button2 As Button
    Private WithEvents Button3 As Button
    Private WithEvents Button4 As Button
End Class
