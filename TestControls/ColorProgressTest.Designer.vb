<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ColorProgressTest
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
        Me.ColorProgressBar1 = New SchlumpfSoft.Controls.ColorProgressBarControl.ColorProgressBar()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColorProgressBar1
        '
        Me.ColorProgressBar1.BackColor = System.Drawing.Color.Black
        Me.ColorProgressBar1.BarColor = System.Drawing.Color.Blue
        Me.ColorProgressBar1.BorderColor = System.Drawing.Color.Black
        Me.ColorProgressBar1.EmptyColor = System.Drawing.Color.LightGray
        Me.ColorProgressBar1.Location = New System.Drawing.Point(21, 210)
        Me.ColorProgressBar1.Name = "ColorProgressBar1"
        Me.ColorProgressBar1.Padding = New System.Windows.Forms.Padding(1)
        Me.ColorProgressBar1.Size = New System.Drawing.Size(214, 18)
        Me.ColorProgressBar1.TabIndex = 0
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(184, 17)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(51, 20)
        Me.NumericUpDown1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Wert des ProgressBar:"
        '
        'CheckBox1
        '
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.Location = New System.Drawing.Point(21, 43)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(214, 21)
        Me.CheckBox1.TabIndex = 3
        Me.CheckBox1.Text = "Glanz anzeigen:"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox2.Location = New System.Drawing.Point(21, 70)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(214, 18)
        Me.CheckBox2.TabIndex = 4
        Me.CheckBox2.Text = "Rahmen anzeigen:"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(21, 99)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(214, 22)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Rahmenfarbe ändern ..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(21, 132)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(214, 22)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Balkenfarbe ändern ..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(21, 169)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(214, 22)
        Me.Button3.TabIndex = 7
        Me.Button3.Text = "Hintergrundfarbe ändern ..."
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ColorProgressTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.ColorProgressBar1)
        Me.Name = "ColorProgressTest"
        Me.Size = New System.Drawing.Size(253, 247)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents ColorProgressBar1 As SchlumpfSoft.Controls.ColorProgressBarControl.ColorProgressBar
    Private WithEvents NumericUpDown1 As NumericUpDown
    Private WithEvents Label1 As Label
    Private WithEvents CheckBox1 As CheckBox
    Private WithEvents CheckBox2 As CheckBox
    Private WithEvents ColorDialog1 As ColorDialog
    Private WithEvents Button1 As Button
    Private WithEvents Button2 As Button
    Private WithEvents Button3 As Button
End Class
