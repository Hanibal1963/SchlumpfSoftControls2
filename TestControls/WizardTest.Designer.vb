<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WizardTest
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardTest))
        Me.Wizard1 = New SchlumpfSoft.Controls.WizardControl.Wizard()
        Me.PageWelcome1 = New SchlumpfSoft.Controls.WizardControl.PageWelcome()
        Me.PageStandard1 = New SchlumpfSoft.Controls.WizardControl.PageStandard()
        Me.PageCustom1 = New SchlumpfSoft.Controls.WizardControl.PageCustom()
        Me.PageFinish1 = New SchlumpfSoft.Controls.WizardControl.PageFinish()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Wizard1.SuspendLayout()
        Me.PageWelcome1.SuspendLayout()
        Me.PageStandard1.SuspendLayout()
        Me.PageCustom1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Wizard1
        '
        Me.Wizard1.Controls.Add(Me.PageFinish1)
        Me.Wizard1.Controls.Add(Me.PageCustom1)
        Me.Wizard1.Controls.Add(Me.PageStandard1)
        Me.Wizard1.Controls.Add(Me.PageWelcome1)
        Me.Wizard1.Dock = System.Windows.Forms.DockStyle.None
        Me.Wizard1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.HeaderTitleFont = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Wizard1.ImageHeader = CType(resources.GetObject("Wizard1.ImageHeader"), System.Drawing.Image)
        Me.Wizard1.ImageWelcome = CType(resources.GetObject("Wizard1.ImageWelcome"), System.Drawing.Image)
        Me.Wizard1.Location = New System.Drawing.Point(38, 56)
        Me.Wizard1.Name = "Wizard1"
        Me.Wizard1.Pages.AddRange(New SchlumpfSoft.Controls.WizardControl.WizardPage() {Me.PageWelcome1, Me.PageStandard1, Me.PageCustom1, Me.PageFinish1})
        Me.Wizard1.Size = New System.Drawing.Size(389, 249)
        Me.Wizard1.TabIndex = 0
        Me.Wizard1.WelcomeFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.WelcomeTitleFont = New System.Drawing.Font("Microsoft Sans Serif", 18.25!, System.Drawing.FontStyle.Bold)
        '
        'PageWelcome1
        '
        Me.PageWelcome1.Controls.Add(Me.Label1)
        Me.PageWelcome1.Description = "Seitenbeschreibung"
        Me.PageWelcome1.Location = New System.Drawing.Point(0, 0)
        Me.PageWelcome1.Name = "PageWelcome1"
        Me.PageWelcome1.Size = New System.Drawing.Size(389, 201)
        Me.PageWelcome1.TabIndex = 10
        '
        'PageStandard1
        '
        Me.PageStandard1.Controls.Add(Me.Label2)
        Me.PageStandard1.Description = "Seitenbeschreibung"
        Me.PageStandard1.Location = New System.Drawing.Point(0, 0)
        Me.PageStandard1.Name = "PageStandard1"
        Me.PageStandard1.Size = New System.Drawing.Size(389, 201)
        Me.PageStandard1.TabIndex = 11
        '
        'PageCustom1
        '
        Me.PageCustom1.Controls.Add(Me.Label3)
        Me.PageCustom1.Description = "Seitenbeschreibung"
        Me.PageCustom1.Location = New System.Drawing.Point(0, 0)
        Me.PageCustom1.Name = "PageCustom1"
        Me.PageCustom1.Size = New System.Drawing.Size(389, 201)
        Me.PageCustom1.TabIndex = 12
        '
        'PageFinish1
        '
        Me.PageFinish1.Description = "Seitenbeschreibung"
        Me.PageFinish1.Location = New System.Drawing.Point(0, 0)
        Me.PageFinish1.Name = "PageFinish1"
        Me.PageFinish1.Size = New System.Drawing.Size(389, 201)
        Me.PageFinish1.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(197, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Willkommen"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(66, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Standardseite"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(64, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Leere Seite"
        '
        'WizardTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Wizard1)
        Me.Name = "WizardTest"
        Me.Size = New System.Drawing.Size(507, 401)
        Me.Wizard1.ResumeLayout(False)
        Me.PageWelcome1.ResumeLayout(False)
        Me.PageWelcome1.PerformLayout()
        Me.PageStandard1.ResumeLayout(False)
        Me.PageStandard1.PerformLayout()
        Me.PageCustom1.ResumeLayout(False)
        Me.PageCustom1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents Wizard1 As SchlumpfSoft.Controls.WizardControl.Wizard
    Friend WithEvents PageWelcome1 As SchlumpfSoft.Controls.WizardControl.PageWelcome
    Private WithEvents Label1 As Label
    Friend WithEvents PageStandard1 As SchlumpfSoft.Controls.WizardControl.PageStandard
    Friend WithEvents PageCustom1 As SchlumpfSoft.Controls.WizardControl.PageCustom
    Friend WithEvents PageFinish1 As SchlumpfSoft.Controls.WizardControl.PageFinish
    Private WithEvents Label2 As Label
    Private WithEvents Label3 As Label
End Class
