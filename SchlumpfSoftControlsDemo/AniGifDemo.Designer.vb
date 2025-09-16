<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AniGifDemo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AniGifDemo))
        Me.AniGif = New SchlumpfSoft.Controls.AniGifControl.AniGif()
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'AniGif
        '
        Me.AniGif.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AniGif.AutoPlay = False
        Me.AniGif.CustomDisplaySpeed = False
        Me.AniGif.FramesPerSecond = New Decimal(New Integer() {10, 0, 0, 0})
        Me.AniGif.Gif = CType(resources.GetObject("AniGif.Gif"), System.Drawing.Bitmap)
        Me.AniGif.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.SizeMode.Normal
        Me.AniGif.Location = New System.Drawing.Point(3, 3)
        Me.AniGif.Name = "AniGif"
        Me.AniGif.Size = New System.Drawing.Size(239, 174)
        Me.AniGif.TabIndex = 0
        Me.AniGif.ZoomFactor = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 2
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.48956!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.51044!))
        Me.TableLayoutPanel.Controls.Add(Me.AniGif, 0, 0)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 2
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(527, 361)
        Me.TableLayoutPanel.TabIndex = 1
        '
        'AniGifDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.Name = "AniGifDemo"
        Me.Size = New System.Drawing.Size(527, 361)
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents AniGif As SchlumpfSoft.Controls.AniGifControl.AniGif
    Private WithEvents TableLayoutPanel As TableLayoutPanel
End Class
