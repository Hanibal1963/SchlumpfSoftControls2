<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotifyFormDemo
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
        Me.Label_ShowTime = New System.Windows.Forms.Label()
        Me.Label_Message = New System.Windows.Forms.Label()
        Me.Label_Title = New System.Windows.Forms.Label()
        Me.Label_Style = New System.Windows.Forms.Label()
        Me.Label_Design = New System.Windows.Forms.Label()
        Me.Button_Show = New System.Windows.Forms.Button()
        Me.NumericUpDown_ShowTime = New System.Windows.Forms.NumericUpDown()
        Me.TextBox_Message = New System.Windows.Forms.TextBox()
        Me.TextBox_Title = New System.Windows.Forms.TextBox()
        Me.ComboBox_Style = New System.Windows.Forms.ComboBox()
        Me.ComboBox_Design = New System.Windows.Forms.ComboBox()
        Me.NotifyForm1 = New SchlumpfSoft.Controls.NotifyFormControl.NotifyForm()
        CType(Me.NumericUpDown_ShowTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_ShowTime
        '
        Me.Label_ShowTime.AutoSize = True
        Me.Label_ShowTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_ShowTime.Location = New System.Drawing.Point(21, 194)
        Me.Label_ShowTime.Name = "Label_ShowTime"
        Me.Label_ShowTime.Size = New System.Drawing.Size(64, 13)
        Me.Label_ShowTime.TabIndex = 32
        Me.Label_ShowTime.Text = "Anzeigezeit:"
        '
        'Label_Message
        '
        Me.Label_Message.AutoSize = True
        Me.Label_Message.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_Message.Location = New System.Drawing.Point(21, 117)
        Me.Label_Message.Name = "Label_Message"
        Me.Label_Message.Size = New System.Drawing.Size(77, 13)
        Me.Label_Message.TabIndex = 31
        Me.Label_Message.Text = "Mitteilungstext:"
        '
        'Label_Title
        '
        Me.Label_Title.AutoSize = True
        Me.Label_Title.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_Title.Location = New System.Drawing.Point(21, 84)
        Me.Label_Title.Name = "Label_Title"
        Me.Label_Title.Size = New System.Drawing.Size(61, 13)
        Me.Label_Title.TabIndex = 30
        Me.Label_Title.Text = "Fenstertitel:"
        '
        'Label_Style
        '
        Me.Label_Style.AutoSize = True
        Me.Label_Style.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_Style.Location = New System.Drawing.Point(21, 54)
        Me.Label_Style.Name = "Label_Style"
        Me.Label_Style.Size = New System.Drawing.Size(33, 13)
        Me.Label_Style.TabIndex = 29
        Me.Label_Style.Text = "Style:"
        '
        'Label_Design
        '
        Me.Label_Design.AutoSize = True
        Me.Label_Design.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label_Design.Location = New System.Drawing.Point(21, 25)
        Me.Label_Design.Name = "Label_Design"
        Me.Label_Design.Size = New System.Drawing.Size(43, 13)
        Me.Label_Design.TabIndex = 28
        Me.Label_Design.Text = "Design:"
        '
        'Button_Show
        '
        Me.Button_Show.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Show.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button_Show.Location = New System.Drawing.Point(164, 228)
        Me.Button_Show.Name = "Button_Show"
        Me.Button_Show.Size = New System.Drawing.Size(102, 23)
        Me.Button_Show.TabIndex = 27
        Me.Button_Show.Text = "Fenster anzeigen"
        Me.Button_Show.UseVisualStyleBackColor = True
        '
        'NumericUpDown_ShowTime
        '
        Me.NumericUpDown_ShowTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown_ShowTime.Location = New System.Drawing.Point(206, 192)
        Me.NumericUpDown_ShowTime.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_ShowTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_ShowTime.Name = "NumericUpDown_ShowTime"
        Me.NumericUpDown_ShowTime.Size = New System.Drawing.Size(60, 20)
        Me.NumericUpDown_ShowTime.TabIndex = 26
        Me.NumericUpDown_ShowTime.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TextBox_Message
        '
        Me.TextBox_Message.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox_Message.Location = New System.Drawing.Point(104, 115)
        Me.TextBox_Message.Multiline = True
        Me.TextBox_Message.Name = "TextBox_Message"
        Me.TextBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Message.Size = New System.Drawing.Size(162, 71)
        Me.TextBox_Message.TabIndex = 25
        Me.TextBox_Message.WordWrap = False
        '
        'TextBox_Title
        '
        Me.TextBox_Title.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox_Title.Location = New System.Drawing.Point(106, 82)
        Me.TextBox_Title.Name = "TextBox_Title"
        Me.TextBox_Title.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Title.Size = New System.Drawing.Size(160, 20)
        Me.TextBox_Title.TabIndex = 24
        Me.TextBox_Title.WordWrap = False
        '
        'ComboBox_Style
        '
        Me.ComboBox_Style.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Style.FormattingEnabled = True
        Me.ComboBox_Style.Items.AddRange(New Object() {"Info", "Frage", "Fehler", "Hinweis"})
        Me.ComboBox_Style.Location = New System.Drawing.Point(106, 51)
        Me.ComboBox_Style.Name = "ComboBox_Style"
        Me.ComboBox_Style.Size = New System.Drawing.Size(160, 21)
        Me.ComboBox_Style.TabIndex = 23
        '
        'ComboBox_Design
        '
        Me.ComboBox_Design.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_Design.FormattingEnabled = True
        Me.ComboBox_Design.Items.AddRange(New Object() {"Hell", "Farbig", "Dunkel"})
        Me.ComboBox_Design.Location = New System.Drawing.Point(106, 22)
        Me.ComboBox_Design.Name = "ComboBox_Design"
        Me.ComboBox_Design.Size = New System.Drawing.Size(160, 21)
        Me.ComboBox_Design.TabIndex = 22
        '
        'NotifyForm1
        '
        Me.NotifyForm1.Design = SchlumpfSoft.Controls.NotifyFormControl.NotifyFormDesign.Bright
        Me.NotifyForm1.Message = "Mitteilung"
        Me.NotifyForm1.ShowTime = 5000
        Me.NotifyForm1.Style = SchlumpfSoft.Controls.NotifyFormControl.NotifyFormStyle.Information
        Me.NotifyForm1.Title = "Titel"
        '
        'NotifyFormDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label_ShowTime)
        Me.Controls.Add(Me.Label_Message)
        Me.Controls.Add(Me.Label_Title)
        Me.Controls.Add(Me.Label_Style)
        Me.Controls.Add(Me.Label_Design)
        Me.Controls.Add(Me.Button_Show)
        Me.Controls.Add(Me.NumericUpDown_ShowTime)
        Me.Controls.Add(Me.TextBox_Message)
        Me.Controls.Add(Me.TextBox_Title)
        Me.Controls.Add(Me.ComboBox_Style)
        Me.Controls.Add(Me.ComboBox_Design)
        Me.Name = "NotifyFormDemo"
        Me.Size = New System.Drawing.Size(292, 272)
        CType(Me.NumericUpDown_ShowTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents Label_ShowTime As Label
    Private WithEvents Label_Message As Label
    Private WithEvents Label_Title As Label
    Private WithEvents Label_Style As Label
    Private WithEvents Label_Design As Label
    Private WithEvents Button_Show As Button
    Private WithEvents NumericUpDown_ShowTime As NumericUpDown
    Private WithEvents TextBox_Message As TextBox
    Private WithEvents TextBox_Title As TextBox
    Private WithEvents ComboBox_Style As ComboBox
    Private WithEvents ComboBox_Design As ComboBox
    Private WithEvents NotifyForm1 As SchlumpfSoft.Controls.NotifyFormControl.NotifyForm
End Class
