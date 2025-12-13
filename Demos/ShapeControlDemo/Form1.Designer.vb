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
        Dim Label_FillColor As System.Windows.Forms.Label
        Dim Label_LineColor As System.Windows.Forms.Label
        Dim Label_LineWidth As System.Windows.Forms.Label
        Dim Label_LineModus As System.Windows.Forms.Label
        Dim Label_ShapeModus As System.Windows.Forms.Label
        Dim PictureBox1 As System.Windows.Forms.PictureBox
        Me.Shape1 = New SchlumpfSoft.Controls.ShapeControl.Shape()
        Me.Button_FillColor = New System.Windows.Forms.Button()
        Me.Button_LineColor = New System.Windows.Forms.Button()
        Me.NumericUpDown_LineWidth = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox_LineStart = New System.Windows.Forms.ComboBox()
        Me.ComboBox_ShapeModus = New System.Windows.Forms.ComboBox()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Label_FillColor = New System.Windows.Forms.Label()
        Label_LineColor = New System.Windows.Forms.Label()
        Label_LineWidth = New System.Windows.Forms.Label()
        Label_LineModus = New System.Windows.Forms.Label()
        Label_ShapeModus = New System.Windows.Forms.Label()
        PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_LineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_FillColor
        '
        Label_FillColor.AutoSize = True
        Label_FillColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_FillColor.Location = New System.Drawing.Point(297, 165)
        Label_FillColor.Name = "Label_FillColor"
        Label_FillColor.Size = New System.Drawing.Size(157, 13)
        Label_FillColor.TabIndex = 45
        Label_FillColor.Text = "Füllfarbe Rechteck oder Ellipse:"
        '
        'Label_LineColor
        '
        Label_LineColor.AutoSize = True
        Label_LineColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_LineColor.Location = New System.Drawing.Point(297, 130)
        Label_LineColor.Name = "Label_LineColor"
        Label_LineColor.Size = New System.Drawing.Size(129, 13)
        Label_LineColor.TabIndex = 44
        Label_LineColor.Text = "Farbe Linie oder Rahmen:"
        '
        'Label_LineWidth
        '
        Label_LineWidth.AutoSize = True
        Label_LineWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_LineWidth.Location = New System.Drawing.Point(297, 95)
        Label_LineWidth.Name = "Label_LineWidth"
        Label_LineWidth.Size = New System.Drawing.Size(129, 13)
        Label_LineWidth.TabIndex = 42
        Label_LineWidth.Text = "Breite Linie oder Rahmen:"
        '
        'Label_LineModus
        '
        Label_LineModus.AutoSize = True
        Label_LineModus.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_LineModus.Location = New System.Drawing.Point(297, 59)
        Label_LineModus.Name = "Label_LineModus"
        Label_LineModus.Size = New System.Drawing.Size(133, 13)
        Label_LineModus.TabIndex = 41
        Label_LineModus.Text = "Startpunkt diagonale Linie:"
        '
        'Label_ShapeModus
        '
        Label_ShapeModus.AutoSize = True
        Label_ShapeModus.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_ShapeModus.Location = New System.Drawing.Point(297, 22)
        Label_ShapeModus.Name = "Label_ShapeModus"
        Label_ShapeModus.Size = New System.Drawing.Size(102, 13)
        Label_ShapeModus.TabIndex = 40
        Label_ShapeModus.Text = "anzuzeigende Form:"
        '
        'PictureBox1
        '
        PictureBox1.Image = Global.ShapeControlDemo.My.Resources.Resources.Papa_Schlumpf_08
        PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        PictureBox1.Location = New System.Drawing.Point(12, 12)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(201, 197)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 37
        PictureBox1.TabStop = False
        '
        'Shape1
        '
        Me.Shape1.DiagonalLineModus = SchlumpfSoft.Controls.ShapeControl.Shape.DiagonalLineModes.TopLeftToBottomRight
        Me.Shape1.FillColor = System.Drawing.Color.Gray
        Me.Shape1.LineColor = System.Drawing.Color.Black
        Me.Shape1.LineWidth = 2.0!
        Me.Shape1.Location = New System.Drawing.Point(122, 78)
        Me.Shape1.Name = "Shape1"
        Me.Shape1.ShapeModus = SchlumpfSoft.Controls.ShapeControl.Shape.ShapeModes.HorizontalLine
        Me.Shape1.Size = New System.Drawing.Size(154, 147)
        Me.Shape1.TabIndex = 48
        '
        'Button_FillColor
        '
        Me.Button_FillColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button_FillColor.Location = New System.Drawing.Point(458, 165)
        Me.Button_FillColor.Name = "Button_FillColor"
        Me.Button_FillColor.Size = New System.Drawing.Size(128, 20)
        Me.Button_FillColor.TabIndex = 47
        Me.Button_FillColor.Text = "Farbe wählen"
        Me.Button_FillColor.UseVisualStyleBackColor = True
        '
        'Button_LineColor
        '
        Me.Button_LineColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button_LineColor.Location = New System.Drawing.Point(458, 130)
        Me.Button_LineColor.Name = "Button_LineColor"
        Me.Button_LineColor.Size = New System.Drawing.Size(128, 20)
        Me.Button_LineColor.TabIndex = 46
        Me.Button_LineColor.Text = "Farbe wählen"
        Me.Button_LineColor.UseVisualStyleBackColor = True
        '
        'NumericUpDown_LineWidth
        '
        Me.NumericUpDown_LineWidth.Location = New System.Drawing.Point(458, 93)
        Me.NumericUpDown_LineWidth.Name = "NumericUpDown_LineWidth"
        Me.NumericUpDown_LineWidth.Size = New System.Drawing.Size(128, 20)
        Me.NumericUpDown_LineWidth.TabIndex = 43
        Me.NumericUpDown_LineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ComboBox_LineStart
        '
        Me.ComboBox_LineStart.FormattingEnabled = True
        Me.ComboBox_LineStart.Items.AddRange(New Object() {"links oben nach rechts unten", "links unten nach rechts oben"})
        Me.ComboBox_LineStart.Location = New System.Drawing.Point(458, 56)
        Me.ComboBox_LineStart.Name = "ComboBox_LineStart"
        Me.ComboBox_LineStart.Size = New System.Drawing.Size(128, 21)
        Me.ComboBox_LineStart.TabIndex = 39
        '
        'ComboBox_ShapeModus
        '
        Me.ComboBox_ShapeModus.FormattingEnabled = True
        Me.ComboBox_ShapeModus.Items.AddRange(New Object() {"Horizontale Linie", "Vertikale Linie", "Diagonale Linie", "Rechteck", "gefülltes Rechteck", "Ellipse", "gefüllte Ellipse"})
        Me.ComboBox_ShapeModus.Location = New System.Drawing.Point(458, 19)
        Me.ComboBox_ShapeModus.Name = "ComboBox_ShapeModus"
        Me.ComboBox_ShapeModus.Size = New System.Drawing.Size(128, 21)
        Me.ComboBox_ShapeModus.TabIndex = 38
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(599, 259)
        Me.Controls.Add(Me.Shape1)
        Me.Controls.Add(Me.Button_FillColor)
        Me.Controls.Add(Me.Button_LineColor)
        Me.Controls.Add(Label_FillColor)
        Me.Controls.Add(Label_LineColor)
        Me.Controls.Add(Me.NumericUpDown_LineWidth)
        Me.Controls.Add(Label_LineWidth)
        Me.Controls.Add(Label_LineModus)
        Me.Controls.Add(Label_ShapeModus)
        Me.Controls.Add(Me.ComboBox_LineStart)
        Me.Controls.Add(Me.ComboBox_ShapeModus)
        Me.Controls.Add(PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Shape Control Demo"
        CType(PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_LineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents Shape1 As SchlumpfSoft.Controls.ShapeControl.Shape
    Private WithEvents Button_FillColor As Button
    Private WithEvents Button_LineColor As Button
    Private WithEvents NumericUpDown_LineWidth As NumericUpDown
    Private WithEvents ComboBox_LineStart As ComboBox
    Private WithEvents ComboBox_ShapeModus As ComboBox
    Private WithEvents ColorDialog1 As ColorDialog
End Class
