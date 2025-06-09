<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SevenSegmentTest
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.SingleDigit1 = New SchlumpfSoft.Controls.SevenSegmentControl.SingleDigit()
        Me.MultiDigit1 = New SchlumpfSoft.Controls.SevenSegmentControl.MultiDigit()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(16, 109)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(37, 20)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox1.WordWrap = False
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Location = New System.Drawing.Point(16, 188)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox2.Size = New System.Drawing.Size(209, 20)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox2.WordWrap = False
        '
        'SingleDigit1
        '
        Me.SingleDigit1.ColonActive = False
        Me.SingleDigit1.CustomBitPattern = 0
        Me.SingleDigit1.DecimalPointActive = False
        Me.SingleDigit1.DigitValue = Nothing
        Me.SingleDigit1.InactiveColor = System.Drawing.Color.DarkGray
        Me.SingleDigit1.ItalicFactor = -0.1!
        Me.SingleDigit1.Location = New System.Drawing.Point(16, 135)
        Me.SingleDigit1.Name = "SingleDigit1"
        Me.SingleDigit1.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
        Me.SingleDigit1.SegmentWidth = 10
        Me.SingleDigit1.ShowColon = False
        Me.SingleDigit1.ShowDecimalPoint = True
        Me.SingleDigit1.Size = New System.Drawing.Size(37, 44)
        Me.SingleDigit1.TabIndex = 4
        Me.SingleDigit1.TabStop = False
        '
        'MultiDigit1
        '
        Me.MultiDigit1.DigitCount = 4
        Me.MultiDigit1.DigitPadding = New System.Windows.Forms.Padding(10, 4, 10, 4)
        Me.MultiDigit1.InactiveColor = System.Drawing.Color.DarkGray
        Me.MultiDigit1.ItalicFactor = -0.1!
        Me.MultiDigit1.Location = New System.Drawing.Point(16, 214)
        Me.MultiDigit1.Name = "MultiDigit1"
        Me.MultiDigit1.SegmentWidth = 10
        Me.MultiDigit1.ShowDecimalPoint = True
        Me.MultiDigit1.Size = New System.Drawing.Size(209, 39)
        Me.MultiDigit1.TabIndex = 5
        Me.MultiDigit1.TabStop = False
        Me.MultiDigit1.Value = Nothing
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(16, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(209, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Segmentfarbe für aktives Segment ..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(16, 42)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(209, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Segmentfarbe für inaktives Segment ..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(16, 71)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(209, 23)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "Hintergrundfarbe der Anzeige ..."
        Me.Button3.UseVisualStyleBackColor = True
        '
        'SevenSegmentTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.MultiDigit1)
        Me.Controls.Add(Me.SingleDigit1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "SevenSegmentTest"
        Me.Size = New System.Drawing.Size(246, 275)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents TextBox1 As TextBox
    Private WithEvents TextBox2 As TextBox
    Private WithEvents SingleDigit1 As SchlumpfSoft.Controls.SevenSegmentControl.SingleDigit
    Private WithEvents MultiDigit1 As SchlumpfSoft.Controls.SevenSegmentControl.MultiDigit
    Private WithEvents Button1 As Button
    Private WithEvents Button2 As Button
    Private WithEvents Button3 As Button
    Private WithEvents ColorDialog1 As ColorDialog
End Class
