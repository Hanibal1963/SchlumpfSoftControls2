<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ColorProgressBarDemo
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
        Me.NumericUpDownProgressValue = New System.Windows.Forms.NumericUpDown()
        Me.ButtonBorderColor = New System.Windows.Forms.Button()
        Me.ButtonEmptyColor = New System.Windows.Forms.Button()
        Me.ButtonBarColor = New System.Windows.Forms.Button()
        Me.CheckBoxShowGliss = New System.Windows.Forms.CheckBox()
        Me.CheckBoxShowBorder = New System.Windows.Forms.CheckBox()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        CType(Me.NumericUpDownProgressValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColorProgressBar1
        '
        Me.ColorProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ColorProgressBar1.BackColor = System.Drawing.Color.Black
        Me.ColorProgressBar1.BarColor = System.Drawing.Color.Blue
        Me.ColorProgressBar1.BorderColor = System.Drawing.Color.Black
        Me.ColorProgressBar1.EmptyColor = System.Drawing.Color.LightGray
        Me.ColorProgressBar1.Location = New System.Drawing.Point(15, 109)
        Me.ColorProgressBar1.Name = "ColorProgressBar1"
        Me.ColorProgressBar1.Padding = New System.Windows.Forms.Padding(1)
        Me.ColorProgressBar1.Size = New System.Drawing.Size(276, 24)
        Me.ColorProgressBar1.TabIndex = 0
        '
        'NumericUpDownProgressValue
        '
        Me.NumericUpDownProgressValue.Location = New System.Drawing.Point(81, 80)
        Me.NumericUpDownProgressValue.Name = "NumericUpDownProgressValue"
        Me.NumericUpDownProgressValue.Size = New System.Drawing.Size(57, 20)
        Me.NumericUpDownProgressValue.TabIndex = 6
        '
        'ButtonBorderColor
        '
        Me.ButtonBorderColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonBorderColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonBorderColor.Location = New System.Drawing.Point(155, 80)
        Me.ButtonBorderColor.Name = "ButtonBorderColor"
        Me.ButtonBorderColor.Size = New System.Drawing.Size(136, 23)
        Me.ButtonBorderColor.TabIndex = 7
        Me.ButtonBorderColor.Text = "Rahmenfarbe ändern"
        Me.ButtonBorderColor.UseVisualStyleBackColor = True
        '
        'ButtonEmptyColor
        '
        Me.ButtonEmptyColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEmptyColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonEmptyColor.Location = New System.Drawing.Point(155, 21)
        Me.ButtonEmptyColor.Name = "ButtonEmptyColor"
        Me.ButtonEmptyColor.Size = New System.Drawing.Size(136, 23)
        Me.ButtonEmptyColor.TabIndex = 8
        Me.ButtonEmptyColor.Text = "Hintergrundfarbe ändern"
        Me.ButtonEmptyColor.UseVisualStyleBackColor = True
        '
        'ButtonBarColor
        '
        Me.ButtonBarColor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonBarColor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonBarColor.Location = New System.Drawing.Point(155, 51)
        Me.ButtonBarColor.Name = "ButtonBarColor"
        Me.ButtonBarColor.Size = New System.Drawing.Size(136, 23)
        Me.ButtonBarColor.TabIndex = 9
        Me.ButtonBarColor.Text = "Balkenfarbe ändern"
        Me.ButtonBarColor.UseVisualStyleBackColor = True
        '
        'CheckBoxShowGliss
        '
        Me.CheckBoxShowGliss.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxShowGliss.Checked = True
        Me.CheckBoxShowGliss.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxShowGliss.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CheckBoxShowGliss.Location = New System.Drawing.Point(15, 50)
        Me.CheckBoxShowGliss.Name = "CheckBoxShowGliss"
        Me.CheckBoxShowGliss.Size = New System.Drawing.Size(123, 24)
        Me.CheckBoxShowGliss.TabIndex = 10
        Me.CheckBoxShowGliss.Text = "Glanz anzeigen"
        Me.CheckBoxShowGliss.UseVisualStyleBackColor = True
        '
        'CheckBoxShowBorder
        '
        Me.CheckBoxShowBorder.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxShowBorder.Checked = True
        Me.CheckBoxShowBorder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxShowBorder.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CheckBoxShowBorder.Location = New System.Drawing.Point(15, 20)
        Me.CheckBoxShowBorder.Name = "CheckBoxShowBorder"
        Me.CheckBoxShowBorder.Size = New System.Drawing.Size(123, 24)
        Me.CheckBoxShowBorder.TabIndex = 11
        Me.CheckBoxShowBorder.Text = "Ramen anzeigen"
        Me.CheckBoxShowBorder.UseVisualStyleBackColor = True
        '
        'ColorProgressBarDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.NumericUpDownProgressValue)
        Me.Controls.Add(Me.ButtonBorderColor)
        Me.Controls.Add(Me.ButtonEmptyColor)
        Me.Controls.Add(Me.ButtonBarColor)
        Me.Controls.Add(Me.CheckBoxShowGliss)
        Me.Controls.Add(Me.CheckBoxShowBorder)
        Me.Controls.Add(Me.ColorProgressBar1)
        Me.Name = "ColorProgressBarDemo"
        Me.Size = New System.Drawing.Size(310, 165)
        CType(Me.NumericUpDownProgressValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents ColorProgressBar1 As SchlumpfSoft.Controls.ColorProgressBarControl.ColorProgressBar
    Private WithEvents NumericUpDownProgressValue As NumericUpDown
    Private WithEvents ButtonBorderColor As Button
    Private WithEvents ButtonEmptyColor As Button
    Private WithEvents ButtonBarColor As Button
    Private WithEvents CheckBoxShowGliss As CheckBox
    Private WithEvents CheckBoxShowBorder As CheckBox
    Private WithEvents ColorDialog1 As ColorDialog
End Class
