<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShapeDemo
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
        Label_FillColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label_FillColor.AutoSize = True
        Label_FillColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_FillColor.Location = New System.Drawing.Point(306, 172)
        Label_FillColor.Name = "Label_FillColor"
        Label_FillColor.Size = New System.Drawing.Size(157, 13)
        Label_FillColor.TabIndex = 33
        Label_FillColor.Text = "Füllfarbe Rechteck oder Ellipse:"
        '
        'Label_LineColor
        '
        Label_LineColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label_LineColor.AutoSize = True
        Label_LineColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_LineColor.Location = New System.Drawing.Point(306, 137)
        Label_LineColor.Name = "Label_LineColor"
        Label_LineColor.Size = New System.Drawing.Size(129, 13)
        Label_LineColor.TabIndex = 32
        Label_LineColor.Text = "Farbe Linie oder Rahmen:"
        '
        'Label_LineWidth
        '
        Label_LineWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label_LineWidth.AutoSize = True
        Label_LineWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_LineWidth.Location = New System.Drawing.Point(306, 102)
        Label_LineWidth.Name = "Label_LineWidth"
        Label_LineWidth.Size = New System.Drawing.Size(129, 13)
        Label_LineWidth.TabIndex = 30
        Label_LineWidth.Text = "Breite Linie oder Rahmen:"
        '
        'Label_LineModus
        '
        Label_LineModus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label_LineModus.AutoSize = True
        Label_LineModus.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_LineModus.Location = New System.Drawing.Point(306, 66)
        Label_LineModus.Name = "Label_LineModus"
        Label_LineModus.Size = New System.Drawing.Size(133, 13)
        Label_LineModus.TabIndex = 29
        Label_LineModus.Text = "Startpunkt diagonale Linie:"
        '
        'Label_ShapeModus
        '
        Label_ShapeModus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label_ShapeModus.AutoSize = True
        Label_ShapeModus.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label_ShapeModus.Location = New System.Drawing.Point(306, 29)
        Label_ShapeModus.Name = "Label_ShapeModus"
        Label_ShapeModus.Size = New System.Drawing.Size(102, 13)
        Label_ShapeModus.TabIndex = 28
        Label_ShapeModus.Text = "anzuzeigende Form:"
        '
        'PictureBox1
        '
        PictureBox1.Image = My.Resources.Resources.Papa_Schlumpf_08
        PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        PictureBox1.Location = New System.Drawing.Point(19, 19)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(201, 197)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 25
        PictureBox1.TabStop = False
        '
        'Shape1
        '
        Me.Shape1.DiagonalLineModus = SchlumpfSoft.Controls.ShapeControl.DiagonalLineModes.TopLeftToBottomRight
        Me.Shape1.FillColor = System.Drawing.Color.Gray
        Me.Shape1.LineColor = System.Drawing.Color.Black
        Me.Shape1.LineWidth = 2.0!
        Me.Shape1.Location = New System.Drawing.Point(129, 85)
        Me.Shape1.Name = "Shape1"
        Me.Shape1.ShapeModus = SchlumpfSoft.Controls.ShapeControl.ShapeModes.HorizontalLine
        Me.Shape1.Size = New System.Drawing.Size(154, 147)
        Me.Shape1.TabIndex = 36
        '
        'Button_FillColor
        '
        Me.Button_FillColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_FillColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button_FillColor.Location = New System.Drawing.Point(467, 172)
        Me.Button_FillColor.Name = "Button_FillColor"
        Me.Button_FillColor.Size = New System.Drawing.Size(128, 20)
        Me.Button_FillColor.TabIndex = 35
        Me.Button_FillColor.Text = "Farbe wählen"
        Me.Button_FillColor.UseVisualStyleBackColor = True
        '
        'Button_LineColor
        '
        Me.Button_LineColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_LineColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button_LineColor.Location = New System.Drawing.Point(467, 137)
        Me.Button_LineColor.Name = "Button_LineColor"
        Me.Button_LineColor.Size = New System.Drawing.Size(128, 20)
        Me.Button_LineColor.TabIndex = 34
        Me.Button_LineColor.Text = "Farbe wählen"
        Me.Button_LineColor.UseVisualStyleBackColor = True
        '
        'NumericUpDown_LineWidth
        '
        Me.NumericUpDown_LineWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_LineWidth.Location = New System.Drawing.Point(467, 100)
        Me.NumericUpDown_LineWidth.Name = "NumericUpDown_LineWidth"
        Me.NumericUpDown_LineWidth.Size = New System.Drawing.Size(128, 20)
        Me.NumericUpDown_LineWidth.TabIndex = 31
        Me.NumericUpDown_LineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ComboBox_LineStart
        '
        Me.ComboBox_LineStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_LineStart.FormattingEnabled = True
        Me.ComboBox_LineStart.Items.AddRange(New Object() {"links oben nach rechts unten", "links unten nach rechts oben"})
        Me.ComboBox_LineStart.Location = New System.Drawing.Point(467, 63)
        Me.ComboBox_LineStart.Name = "ComboBox_LineStart"
        Me.ComboBox_LineStart.Size = New System.Drawing.Size(128, 21)
        Me.ComboBox_LineStart.TabIndex = 27
        '
        'ComboBox_ShapeModus
        '
        Me.ComboBox_ShapeModus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_ShapeModus.FormattingEnabled = True
        Me.ComboBox_ShapeModus.Items.AddRange(New Object() {"Horizontale Linie", "Vertikale Linie", "Diagonale Linie", "Rechteck", "gefülltes Rechteck", "Ellipse", "gefüllte Ellipse"})
        Me.ComboBox_ShapeModus.Location = New System.Drawing.Point(467, 26)
        Me.ComboBox_ShapeModus.Name = "ComboBox_ShapeModus"
        Me.ComboBox_ShapeModus.Size = New System.Drawing.Size(128, 21)
        Me.ComboBox_ShapeModus.TabIndex = 26
        '
        'ShapeDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
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
        Me.Name = "ShapeDemo"
        Me.Size = New System.Drawing.Size(618, 255)
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
