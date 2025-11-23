<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SevenSegmentDemo
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxMulti = New System.Windows.Forms.TextBox()
        Me.TextBoxSingle = New System.Windows.Forms.TextBox()
        Me.MultiDigit1 = New SchlumpfSoft.Controls.SevenSegmentControl.MultiDigit()
        Me.SingleDigit1 = New SchlumpfSoft.Controls.SevenSegmentControl.SingleDigit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(19, 133)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(151, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Zeichen eingeben (maximal 8):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(19, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Zeichen eingeben:"
        '
        'TextBoxMulti
        '
        Me.TextBoxMulti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxMulti.Location = New System.Drawing.Point(188, 131)
        Me.TextBoxMulti.MaxLength = 7
        Me.TextBoxMulti.Name = "TextBoxMulti"
        Me.TextBoxMulti.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxMulti.Size = New System.Drawing.Size(109, 20)
        Me.TextBoxMulti.TabIndex = 12
        Me.TextBoxMulti.WordWrap = False
        '
        'TextBoxSingle
        '
        Me.TextBoxSingle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxSingle.Location = New System.Drawing.Point(122, 17)
        Me.TextBoxSingle.MaxLength = 1
        Me.TextBoxSingle.Name = "TextBoxSingle"
        Me.TextBoxSingle.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxSingle.Size = New System.Drawing.Size(48, 20)
        Me.TextBoxSingle.TabIndex = 11
        Me.TextBoxSingle.WordWrap = False
        '
        'MultiDigit1
        '
        Me.MultiDigit1.DigitCount = 6
        Me.MultiDigit1.DigitPadding = New System.Windows.Forms.Padding(10, 4, 10, 4)
        Me.MultiDigit1.InactiveColor = System.Drawing.Color.DarkGray
        Me.MultiDigit1.ItalicFactor = -0.1!
        Me.MultiDigit1.Location = New System.Drawing.Point(22, 170)
        Me.MultiDigit1.Name = "MultiDigit1"
        Me.MultiDigit1.SegmentWidth = 10
        Me.MultiDigit1.ShowDecimalPoint = True
        Me.MultiDigit1.Size = New System.Drawing.Size(275, 44)
        Me.MultiDigit1.TabIndex = 15
        Me.MultiDigit1.TabStop = False
        Me.MultiDigit1.Value = Nothing
        '
        'SingleDigit1
        '
        Me.SingleDigit1.ColonActive = False
        Me.SingleDigit1.CustomBitPattern = 0
        Me.SingleDigit1.DecimalPointActive = False
        Me.SingleDigit1.DigitValue = Nothing
        Me.SingleDigit1.InactiveColor = System.Drawing.Color.DarkGray
        Me.SingleDigit1.ItalicFactor = -0.1!
        Me.SingleDigit1.Location = New System.Drawing.Point(22, 49)
        Me.SingleDigit1.Name = "SingleDigit1"
        Me.SingleDigit1.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
        Me.SingleDigit1.SegmentWidth = 10
        Me.SingleDigit1.ShowColon = False
        Me.SingleDigit1.ShowDecimalPoint = True
        Me.SingleDigit1.Size = New System.Drawing.Size(40, 50)
        Me.SingleDigit1.TabIndex = 16
        Me.SingleDigit1.TabStop = False
        '
        'SevenSegmentDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SingleDigit1)
        Me.Controls.Add(Me.MultiDigit1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxMulti)
        Me.Controls.Add(Me.TextBoxSingle)
        Me.Name = "SevenSegmentDemo"
        Me.Size = New System.Drawing.Size(341, 253)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents TextBoxMulti As System.Windows.Forms.TextBox
    Private WithEvents TextBoxSingle As System.Windows.Forms.TextBox
    Friend WithEvents MultiDigit1 As SchlumpfSoft.Controls.SevenSegmentControl.MultiDigit
    Friend WithEvents SingleDigit1 As SchlumpfSoft.Controls.SevenSegmentControl.SingleDigit
End Class
