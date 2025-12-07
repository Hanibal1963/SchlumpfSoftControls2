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
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
        Me.RTFTB = New SchlumpfSoft.Controls.ExtendedRTFControl.ExtendedRTF()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonFontFormat = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonBold = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonItalic = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonUnderline = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonStrikeout = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFontSizeUp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFontSizeDown = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonForeColor = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonBackColor = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonToggleBullets = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonDelFormat = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonLeftIndentUp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonLeftIndentDown = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonTextLeft = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonTextCenter = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonTextRight = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.RTFTB)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(619, 377)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(619, 406)
        Me.ToolStripContainer1.TabIndex = 1
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        '
        'RTFTB
        '
        Me.RTFTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RTFTB.Location = New System.Drawing.Point(0, 0)
        Me.RTFTB.Name = "RTFTB"
        Me.RTFTB.SelectionBold = False
        Me.RTFTB.SelectionFontSize = 8.25!
        Me.RTFTB.SelectionForeColor = System.Drawing.Color.Black
        Me.RTFTB.SelectionItalic = False
        Me.RTFTB.SelectionLeftIndent = 0
        Me.RTFTB.SelectionStrikeout = False
        Me.RTFTB.SelectionUnderline = False
        Me.RTFTB.Size = New System.Drawing.Size(619, 377)
        Me.RTFTB.TabIndex = 0
        Me.RTFTB.Text = "Beispielttext" & Global.Microsoft.VisualBasic.ChrW(10) & "Beispielttext" & Global.Microsoft.VisualBasic.ChrW(10) & "Beispielttext" & Global.Microsoft.VisualBasic.ChrW(10) & "Beispielttext" & Global.Microsoft.VisualBasic.ChrW(10) & "Beispielttext" & Global.Microsoft.VisualBasic.ChrW(10) & "Beispieltte" &
    "xt" & Global.Microsoft.VisualBasic.ChrW(10) & "Beispielttext" & Global.Microsoft.VisualBasic.ChrW(10) & "Beispielttext"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonFontFormat, Me.ToolStripButtonBold, Me.ToolStripButtonItalic, Me.ToolStripButtonUnderline, Me.ToolStripButtonStrikeout, Me.ToolStripButtonFontSizeUp, Me.ToolStripButtonFontSizeDown, Me.ToolStripButtonForeColor, Me.ToolStripButtonBackColor, Me.ToolStripButtonToggleBullets, Me.ToolStripButtonDelFormat, Me.ToolStripButtonLeftIndentUp, Me.ToolStripButtonLeftIndentDown, Me.ToolStripButtonTextLeft, Me.ToolStripButtonTextCenter, Me.ToolStripButtonTextRight})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(428, 29)
        Me.ToolStrip1.TabIndex = 0
        '
        'ToolStripButtonFontFormat
        '
        Me.ToolStripButtonFontFormat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFontFormat.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Fontformat
        Me.ToolStripButtonFontFormat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonFontFormat.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFontFormat.Name = "ToolStripButtonFontFormat"
        Me.ToolStripButtonFontFormat.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonFontFormat.Text = "ToolStripButtonFontFormat"
        Me.ToolStripButtonFontFormat.ToolTipText = "Schriftart ..."
        '
        'ToolStripButtonBold
        '
        Me.ToolStripButtonBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonBold.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Bold
        Me.ToolStripButtonBold.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonBold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonBold.Name = "ToolStripButtonBold"
        Me.ToolStripButtonBold.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonBold.Text = "ToolStripButtonBold"
        Me.ToolStripButtonBold.ToolTipText = "Fett umschalten"
        '
        'ToolStripButtonItalic
        '
        Me.ToolStripButtonItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonItalic.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Italic
        Me.ToolStripButtonItalic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonItalic.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonItalic.Name = "ToolStripButtonItalic"
        Me.ToolStripButtonItalic.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonItalic.Text = "ToolStripButtonItalic"
        Me.ToolStripButtonItalic.ToolTipText = "Kursiv umschalten"
        '
        'ToolStripButtonUnderline
        '
        Me.ToolStripButtonUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonUnderline.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Underline
        Me.ToolStripButtonUnderline.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonUnderline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonUnderline.Name = "ToolStripButtonUnderline"
        Me.ToolStripButtonUnderline.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonUnderline.Text = "ToolStripButtonUnderline"
        Me.ToolStripButtonUnderline.ToolTipText = "Unterstrichen umschalten"
        '
        'ToolStripButtonStrikeout
        '
        Me.ToolStripButtonStrikeout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonStrikeout.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Strikeout
        Me.ToolStripButtonStrikeout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonStrikeout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonStrikeout.Name = "ToolStripButtonStrikeout"
        Me.ToolStripButtonStrikeout.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonStrikeout.Text = "ToolStripButtonStrikeout"
        Me.ToolStripButtonStrikeout.ToolTipText = "Durchgestrichen umschalten"
        '
        'ToolStripButtonFontSizeUp
        '
        Me.ToolStripButtonFontSizeUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFontSizeUp.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Fontup
        Me.ToolStripButtonFontSizeUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonFontSizeUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFontSizeUp.Name = "ToolStripButtonFontSizeUp"
        Me.ToolStripButtonFontSizeUp.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonFontSizeUp.Text = "ToolStripButtonFontSizeUp"
        Me.ToolStripButtonFontSizeUp.ToolTipText = "Schrift vergrössern"
        '
        'ToolStripButtonFontSizeDown
        '
        Me.ToolStripButtonFontSizeDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFontSizeDown.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Fontdown
        Me.ToolStripButtonFontSizeDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonFontSizeDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFontSizeDown.Name = "ToolStripButtonFontSizeDown"
        Me.ToolStripButtonFontSizeDown.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonFontSizeDown.Text = "ToolStripButtonFontSizeDown"
        Me.ToolStripButtonFontSizeDown.ToolTipText = "Schrift verkleinern"
        '
        'ToolStripButtonForeColor
        '
        Me.ToolStripButtonForeColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonForeColor.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Forecolor
        Me.ToolStripButtonForeColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonForeColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonForeColor.Name = "ToolStripButtonForeColor"
        Me.ToolStripButtonForeColor.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonForeColor.Text = "ToolStripButtonForeColor"
        Me.ToolStripButtonForeColor.ToolTipText = "Schriftfarbe ..."
        '
        'ToolStripButtonBackColor
        '
        Me.ToolStripButtonBackColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonBackColor.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Backcolor
        Me.ToolStripButtonBackColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonBackColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonBackColor.Name = "ToolStripButtonBackColor"
        Me.ToolStripButtonBackColor.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonBackColor.Text = "ToolStripButtonBackColor"
        Me.ToolStripButtonBackColor.ToolTipText = "Hintergrundfarbe ..."
        '
        'ToolStripButtonToggleBullets
        '
        Me.ToolStripButtonToggleBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonToggleBullets.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Togglebullets
        Me.ToolStripButtonToggleBullets.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonToggleBullets.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonToggleBullets.Name = "ToolStripButtonToggleBullets"
        Me.ToolStripButtonToggleBullets.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonToggleBullets.Text = "ToolStripButtonToggleBullets"
        Me.ToolStripButtonToggleBullets.ToolTipText = "Aufzählung umschalten"
        '
        'ToolStripButtonDelFormat
        '
        Me.ToolStripButtonDelFormat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonDelFormat.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Delformat
        Me.ToolStripButtonDelFormat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonDelFormat.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonDelFormat.Name = "ToolStripButtonDelFormat"
        Me.ToolStripButtonDelFormat.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonDelFormat.Text = "ToolStripButtonDelFormat"
        Me.ToolStripButtonDelFormat.ToolTipText = "Formatierung löschen"
        '
        'ToolStripButtonLeftIndentUp
        '
        Me.ToolStripButtonLeftIndentUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonLeftIndentUp.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.LeftIndentUp
        Me.ToolStripButtonLeftIndentUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonLeftIndentUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonLeftIndentUp.Name = "ToolStripButtonLeftIndentUp"
        Me.ToolStripButtonLeftIndentUp.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonLeftIndentUp.Text = "ToolStripButtonLeftIndentUp"
        Me.ToolStripButtonLeftIndentUp.ToolTipText = "Einzug vergrössern"
        '
        'ToolStripButtonLeftIndentDown
        '
        Me.ToolStripButtonLeftIndentDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonLeftIndentDown.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.LeftIndentDown
        Me.ToolStripButtonLeftIndentDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonLeftIndentDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonLeftIndentDown.Name = "ToolStripButtonLeftIndentDown"
        Me.ToolStripButtonLeftIndentDown.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonLeftIndentDown.Text = "ToolStripButtonLeftIndentDown"
        Me.ToolStripButtonLeftIndentDown.ToolTipText = "Einzug verkleinern"
        '
        'ToolStripButtonTextLeft
        '
        Me.ToolStripButtonTextLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonTextLeft.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Textleft
        Me.ToolStripButtonTextLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonTextLeft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonTextLeft.Name = "ToolStripButtonTextLeft"
        Me.ToolStripButtonTextLeft.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonTextLeft.Text = "ToolStripButtonTextLeft"
        Me.ToolStripButtonTextLeft.ToolTipText = "Text linksbündig"
        '
        'ToolStripButtonTextCenter
        '
        Me.ToolStripButtonTextCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonTextCenter.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Textcenter
        Me.ToolStripButtonTextCenter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonTextCenter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonTextCenter.Name = "ToolStripButtonTextCenter"
        Me.ToolStripButtonTextCenter.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonTextCenter.Text = "ToolStripButtonTextCenter"
        Me.ToolStripButtonTextCenter.ToolTipText = "Text zentriert"
        '
        'ToolStripButtonTextRight
        '
        Me.ToolStripButtonTextRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonTextRight.Image = Global.ExtendedRTFControlDemo.My.Resources.Resources.Textright
        Me.ToolStripButtonTextRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButtonTextRight.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonTextRight.Name = "ToolStripButtonTextRight"
        Me.ToolStripButtonTextRight.Size = New System.Drawing.Size(26, 26)
        Me.ToolStripButtonTextRight.Text = "ToolStripButtonTextRight"
        Me.ToolStripButtonTextRight.ToolTipText = "Text rechtsbündig"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(619, 406)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ExtendedRTFControl Demo"
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents ToolStripContainer1 As ToolStripContainer
    Private WithEvents RTFTB As SchlumpfSoft.Controls.ExtendedRTFControl.ExtendedRTF
    Private WithEvents ToolStrip1 As ToolStrip
    Private WithEvents ToolStripButtonFontFormat As ToolStripButton
    Private WithEvents ToolStripButtonBold As ToolStripButton
    Private WithEvents ToolStripButtonItalic As ToolStripButton
    Private WithEvents ToolStripButtonUnderline As ToolStripButton
    Private WithEvents ToolStripButtonStrikeout As ToolStripButton
    Private WithEvents ToolStripButtonFontSizeUp As ToolStripButton
    Private WithEvents ToolStripButtonFontSizeDown As ToolStripButton
    Private WithEvents ToolStripButtonForeColor As ToolStripButton
    Private WithEvents ToolStripButtonBackColor As ToolStripButton
    Private WithEvents ToolStripButtonToggleBullets As ToolStripButton
    Private WithEvents ToolStripButtonDelFormat As ToolStripButton
    Private WithEvents ToolStripButtonLeftIndentUp As ToolStripButton
    Private WithEvents ToolStripButtonLeftIndentDown As ToolStripButton
    Private WithEvents ToolStripButtonTextLeft As ToolStripButton
    Private WithEvents ToolStripButtonTextCenter As ToolStripButton
    Private WithEvents ToolStripButtonTextRight As ToolStripButton
End Class
