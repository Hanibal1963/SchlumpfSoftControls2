<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WizardDemo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardDemo))
        Me.Wizard1 = New SchlumpfSoft.Controls.WizardControl.Wizard()
        Me.PageWelcome1 = New SchlumpfSoft.Controls.WizardControl.PageWelcome()
        Me.PageStandard1 = New SchlumpfSoft.Controls.WizardControl.PageStandard()
        Me.PageCustom1 = New SchlumpfSoft.Controls.WizardControl.PageCustom()
        Me.PageFinish1 = New SchlumpfSoft.Controls.WizardControl.PageFinish()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Wizard1.SuspendLayout()
        Me.PageWelcome1.SuspendLayout()
        Me.PageStandard1.SuspendLayout()
        Me.PageCustom1.SuspendLayout()
        Me.PageFinish1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Wizard1
        '
        Me.Wizard1.Controls.Add(Me.PageFinish1)
        Me.Wizard1.Controls.Add(Me.PageCustom1)
        Me.Wizard1.Controls.Add(Me.PageStandard1)
        Me.Wizard1.Controls.Add(Me.PageWelcome1)
        Me.Wizard1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.HeaderTitleFont = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Wizard1.ImageHeader = CType(resources.GetObject("Wizard1.ImageHeader"), System.Drawing.Image)
        Me.Wizard1.ImageWelcome = CType(resources.GetObject("Wizard1.ImageWelcome"), System.Drawing.Image)
        Me.Wizard1.Location = New System.Drawing.Point(0, 0)
        Me.Wizard1.Name = "Wizard1"
        Me.Wizard1.Pages.AddRange(New SchlumpfSoft.Controls.WizardControl.WizardPage() {Me.PageWelcome1, Me.PageStandard1, Me.PageCustom1, Me.PageFinish1})
        Me.Wizard1.Size = New System.Drawing.Size(565, 332)
        Me.Wizard1.TabIndex = 0
        Me.Wizard1.WelcomeFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.WelcomeTitleFont = New System.Drawing.Font("Microsoft Sans Serif", 18.25!, System.Drawing.FontStyle.Bold)
        '
        'PageWelcome1
        '
        Me.PageWelcome1.Controls.Add(Me.Label2)
        Me.PageWelcome1.Description = "Beschreibung des Assistenten"
        Me.PageWelcome1.Location = New System.Drawing.Point(0, 0)
        Me.PageWelcome1.Name = "PageWelcome1"
        Me.PageWelcome1.Size = New System.Drawing.Size(565, 284)
        Me.PageWelcome1.TabIndex = 10
        Me.PageWelcome1.Title = "Willkommen zum Wizard Demo"
        '
        'PageStandard1
        '
        Me.PageStandard1.Controls.Add(Me.Label1)
        Me.PageStandard1.Description = "Eine Standardseite."
        Me.PageStandard1.Location = New System.Drawing.Point(0, 0)
        Me.PageStandard1.Name = "PageStandard1"
        Me.PageStandard1.Size = New System.Drawing.Size(565, 284)
        Me.PageStandard1.TabIndex = 11
        Me.PageStandard1.Title = "Standardseite"
        '
        'PageCustom1
        '
        Me.PageCustom1.Controls.Add(Me.Label3)
        Me.PageCustom1.Description = "Frei gestaltbae Seite"
        Me.PageCustom1.Location = New System.Drawing.Point(0, 0)
        Me.PageCustom1.Name = "PageCustom1"
        Me.PageCustom1.Size = New System.Drawing.Size(565, 284)
        Me.PageCustom1.TabIndex = 12
        Me.PageCustom1.Title = "Leere Seite"
        '
        'PageFinish1
        '
        Me.PageFinish1.Controls.Add(Me.Label4)
        Me.PageFinish1.Description = "Die letzte Sete des Assistenten"
        Me.PageFinish1.Location = New System.Drawing.Point(0, 0)
        Me.PageFinish1.Name = "PageFinish1"
        Me.PageFinish1.Size = New System.Drawing.Size(565, 284)
        Me.PageFinish1.TabIndex = 13
        Me.PageFinish1.Title = "Abschlussseite"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(49, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(281, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Hier können benutzerdefinierte Controls eingefügt werden."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(196, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(281, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Hier können benutzerdefinierte Controls eingefügt werden."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(142, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(281, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Hier können benutzerdefinierte Controls eingefügt werden."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(215, 134)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(281, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Hier können benutzerdefinierte Controls eingefügt werden."
        '
        'WizardDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Wizard1)
        Me.Name = "WizardDemo"
        Me.Size = New System.Drawing.Size(565, 332)
        Me.Wizard1.ResumeLayout(False)
        Me.PageWelcome1.ResumeLayout(False)
        Me.PageWelcome1.PerformLayout()
        Me.PageStandard1.ResumeLayout(False)
        Me.PageStandard1.PerformLayout()
        Me.PageCustom1.ResumeLayout(False)
        Me.PageCustom1.PerformLayout()
        Me.PageFinish1.ResumeLayout(False)
        Me.PageFinish1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents Wizard1 As SchlumpfSoft.Controls.WizardControl.Wizard
    Friend WithEvents PageStandard1 As SchlumpfSoft.Controls.WizardControl.PageStandard
    Friend WithEvents PageWelcome1 As SchlumpfSoft.Controls.WizardControl.PageWelcome
    Friend WithEvents PageCustom1 As SchlumpfSoft.Controls.WizardControl.PageCustom
    Friend WithEvents PageFinish1 As SchlumpfSoft.Controls.WizardControl.PageFinish
    Private WithEvents Label4 As Label
    Private WithEvents Label3 As Label
    Private WithEvents Label1 As Label
    Private WithEvents Label2 As Label
End Class
