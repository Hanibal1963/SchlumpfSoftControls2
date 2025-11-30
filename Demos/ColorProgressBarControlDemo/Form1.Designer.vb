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
        Me.NumericUpDownProgressValue = New System.Windows.Forms.NumericUpDown()
        Me.ButtonBorderColor = New System.Windows.Forms.Button()
        Me.ButtonEmptyColor = New System.Windows.Forms.Button()
        Me.ButtonBarColor = New System.Windows.Forms.Button()
        Me.CheckBoxShowGliss = New System.Windows.Forms.CheckBox()
        Me.CheckBoxShowBorder = New System.Windows.Forms.CheckBox()
        Me.ColorProgressBar1 = New SchlumpfSoft.Controls.ColorProgressBarControl.ColorProgressBar()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.NumericUpDownProgressValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumericUpDownProgressValue
        '
        Me.NumericUpDownProgressValue.Location = New System.Drawing.Point(83, 75)
        Me.NumericUpDownProgressValue.Name = "NumericUpDownProgressValue"
        Me.NumericUpDownProgressValue.Size = New System.Drawing.Size(52, 20)
        Me.NumericUpDownProgressValue.TabIndex = 13
        '
        'ButtonBorderColor
        '
        Me.ButtonBorderColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonBorderColor.Location = New System.Drawing.Point(158, 75)
        Me.ButtonBorderColor.Name = "ButtonBorderColor"
        Me.ButtonBorderColor.Size = New System.Drawing.Size(136, 23)
        Me.ButtonBorderColor.TabIndex = 14
        Me.ButtonBorderColor.Text = "Rahmenfarbe ändern"
        Me.ButtonBorderColor.UseVisualStyleBackColor = True
        '
        'ButtonEmptyColor
        '
        Me.ButtonEmptyColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonEmptyColor.Location = New System.Drawing.Point(158, 16)
        Me.ButtonEmptyColor.Name = "ButtonEmptyColor"
        Me.ButtonEmptyColor.Size = New System.Drawing.Size(136, 23)
        Me.ButtonEmptyColor.TabIndex = 15
        Me.ButtonEmptyColor.Text = "Hintergrundfarbe ändern"
        Me.ButtonEmptyColor.UseVisualStyleBackColor = True
        '
        'ButtonBarColor
        '
        Me.ButtonBarColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonBarColor.Location = New System.Drawing.Point(158, 46)
        Me.ButtonBarColor.Name = "ButtonBarColor"
        Me.ButtonBarColor.Size = New System.Drawing.Size(136, 23)
        Me.ButtonBarColor.TabIndex = 16
        Me.ButtonBarColor.Text = "Balkenfarbe ändern"
        Me.ButtonBarColor.UseVisualStyleBackColor = True
        '
        'CheckBoxShowGliss
        '
        Me.CheckBoxShowGliss.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxShowGliss.Checked = True
        Me.CheckBoxShowGliss.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxShowGliss.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CheckBoxShowGliss.Location = New System.Drawing.Point(12, 45)
        Me.CheckBoxShowGliss.Name = "CheckBoxShowGliss"
        Me.CheckBoxShowGliss.Size = New System.Drawing.Size(123, 24)
        Me.CheckBoxShowGliss.TabIndex = 17
        Me.CheckBoxShowGliss.Text = "Glanz anzeigen"
        Me.CheckBoxShowGliss.UseVisualStyleBackColor = True
        '
        'CheckBoxShowBorder
        '
        Me.CheckBoxShowBorder.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxShowBorder.Checked = True
        Me.CheckBoxShowBorder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxShowBorder.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CheckBoxShowBorder.Location = New System.Drawing.Point(12, 15)
        Me.CheckBoxShowBorder.Name = "CheckBoxShowBorder"
        Me.CheckBoxShowBorder.Size = New System.Drawing.Size(123, 24)
        Me.CheckBoxShowBorder.TabIndex = 18
        Me.CheckBoxShowBorder.Text = "Ramen anzeigen"
        Me.CheckBoxShowBorder.UseVisualStyleBackColor = True
        '
        'ColorProgressBar1
        '
        Me.ColorProgressBar1.BackColor = System.Drawing.Color.Black
        Me.ColorProgressBar1.BarColor = System.Drawing.Color.Blue
        Me.ColorProgressBar1.BorderColor = System.Drawing.Color.Black
        Me.ColorProgressBar1.EmptyColor = System.Drawing.Color.LightGray
        Me.ColorProgressBar1.Location = New System.Drawing.Point(12, 104)
        Me.ColorProgressBar1.Name = "ColorProgressBar1"
        Me.ColorProgressBar1.Padding = New System.Windows.Forms.Padding(1)
        Me.ColorProgressBar1.Size = New System.Drawing.Size(282, 24)
        Me.ColorProgressBar1.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Wert:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(309, 143)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumericUpDownProgressValue)
        Me.Controls.Add(Me.ButtonBorderColor)
        Me.Controls.Add(Me.ButtonEmptyColor)
        Me.Controls.Add(Me.ButtonBarColor)
        Me.Controls.Add(Me.CheckBoxShowGliss)
        Me.Controls.Add(Me.CheckBoxShowBorder)
        Me.Controls.Add(Me.ColorProgressBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Text = "ColorProgressBat - Control - Demo"
        CType(Me.NumericUpDownProgressValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents NumericUpDownProgressValue As NumericUpDown
    Private WithEvents ButtonBorderColor As Button
    Private WithEvents ButtonEmptyColor As Button
    Private WithEvents ButtonBarColor As Button
    Private WithEvents CheckBoxShowGliss As CheckBox
    Private WithEvents CheckBoxShowBorder As CheckBox
    Private WithEvents ColorProgressBar1 As SchlumpfSoft.Controls.ColorProgressBarControl.ColorProgressBar
    Private WithEvents ColorDialog1 As ColorDialog
    Private WithEvents Label1 As Label
End Class
