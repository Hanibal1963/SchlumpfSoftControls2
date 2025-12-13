' *************************************************************************************************
' WizardPage.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Definiert eine Seite des Controls
    ''' </summary>
    <System.ComponentModel.ToolboxItem(False)>
    Public Class WizardPage : Inherits System.Windows.Forms.Panel

        Private _Style As PageStyle = PageStyle.Standard
        Private _Title As String = String.Empty
        Private _Description As String = String.Empty

        <System.ComponentModel.Category("Design")>
        <System.ComponentModel.Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
        Public Overridable Property Style As PageStyle
            Get
                Return Me._Style
            End Get
            Set(value As PageStyle)
                If Me._Style = value Then
                    Return
                End If
                Me._Style = value
                If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Wizard Then
                    Dim wizard As Wizard = CType(Me.Parent, Wizard)
                    If wizard.SelectedPage Is Me Then
                        wizard.SelectedPage = Me
                    End If
                Else
                    Me.Invalidate()
                End If
            End Set
        End Property

        <System.ComponentModel.DefaultValue("")>
        <System.ComponentModel.Category("Design")>
        <System.ComponentModel.Description("Ruft den Titel der Assistentenseite ab oder legt diesen fest.")>
        Public Overridable Property Title As String
            Get
                Return Me._Title
            End Get
            Set(value As String)
                If Equals(value, Nothing) Then
                    value = String.Empty
                End If
                If Not Equals(Me._Title, value) Then
                    Me._Title = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        <System.ComponentModel.DefaultValue("")>
        <System.ComponentModel.Category("Design")>
        <System.ComponentModel.Description("Ruft die Beschreibung der Assistentenseite ab oder legt diese fest.")>
        Public Overridable Property Description As String
            Get
                Return Me._Description
            End Get
            Set(value As String)
                If Equals(value, Nothing) Then
                    value = String.Empty
                End If
                If Not Equals(Me._Description, value) Then
                    Me._Description = value
                    Me.Invalidate()
                End If
            End Set
        End Property

        Public Sub New()
            Me.InitializeStyles()
        End Sub

        Private Sub InitializeStyles()
            Me.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, True)
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            If Me._Style = PageStyle.Custom Then Return
            Dim clientRectangle = MyBase.ClientRectangle
            Dim empty = System.Drawing.Rectangle.Empty
            Dim empty2 = System.Drawing.Rectangle.Empty
            Dim empty3 = System.Drawing.Rectangle.Empty
            Dim genericDefault = System.Drawing.StringFormat.GenericDefault
            genericDefault.LineAlignment = System.Drawing.StringAlignment.Near
            genericDefault.Alignment = System.Drawing.StringAlignment.Near
            genericDefault.Trimming = System.Drawing.StringTrimming.EllipsisCharacter
            Select Case Me.Style
                Case PageStyle.Standard
                    clientRectangle.Height = 64
                    System.Windows.Forms.ControlPaint.DrawBorder3D(e.Graphics, clientRectangle, System.Windows.Forms.Border3DStyle.Etched, System.Windows.Forms.Border3DSide.Bottom)
                    clientRectangle.Height -= System.Windows.Forms.SystemInformation.Border3DSize.Height
                    e.Graphics.FillRectangle(System.Drawing.SystemBrushes.Window, clientRectangle)
                    Dim num2 As Integer = CInt(System.Math.Floor(8.0))
                    empty.Location = New System.Drawing.Point(Me.Width - 48 - num2, num2)
                    empty.Size = New System.Drawing.Size(48, 48)
                    Dim image2 As System.Drawing.Image = Nothing
                    Dim font3 = MyBase.Font
                    Dim font4 = MyBase.Font
                    If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Wizard Then
                        Dim wizard2 As Wizard = CType(Me.Parent, Wizard)
                        image2 = wizard2.ImageHeader
                        If image2 Is Nothing Then
                            empty.Size = New System.Drawing.Size(0, 0)
                        End If
                        font3 = wizard2.HeaderFont
                        font4 = wizard2.HeaderTitleFont
                    End If
                    If image2 Is Nothing Then
                        System.Windows.Forms.ControlPaint.DrawFocusRectangle(e.Graphics, empty)
                    Else
                        e.Graphics.DrawImage(image2, empty)
                    End If
                    Dim num3 As Integer = CInt(System.Math.Ceiling(e.Graphics.MeasureString(Me._Title, font4, 0, genericDefault).Height))
                    empty2.Location = New System.Drawing.Point(8, 8)
                    empty2.Size = New System.Drawing.Size(empty.Left - 8, num3)
                    empty3.Location = empty2.Location
                    empty3.Y += num3 + 4
                    empty3.Size = New System.Drawing.Size(empty2.Width, 64 - empty3.Y)
                    e.Graphics.DrawString(Me._Title, font4, System.Drawing.SystemBrushes.WindowText, empty2, genericDefault)
                    e.Graphics.DrawString(Me._Description, font3, System.Drawing.SystemBrushes.WindowText, empty3, genericDefault)
                    Exit Select
                Case PageStyle.Welcome, PageStyle.Finish
                    e.Graphics.FillRectangle(System.Drawing.SystemBrushes.Window, clientRectangle)
                    empty.Location = System.Drawing.Point.Empty
                    empty.Size = New System.Drawing.Size(164, Me.Height)
                    Dim image As System.Drawing.Image = Nothing
                    Dim font = MyBase.Font
                    Dim font2 = MyBase.Font
                    If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Wizard Then
                        Dim wizard As Wizard = CType(Me.Parent, Wizard)
                        image = wizard.ImageWelcome
                        font = wizard.WelcomeFont
                        font2 = wizard.WelcomeTitleFont
                    End If
                    If image Is Nothing Then
                        System.Windows.Forms.ControlPaint.DrawFocusRectangle(e.Graphics, empty)
                    Else
                        e.Graphics.DrawImage(image, empty)
                    End If
                    empty2.Location = New System.Drawing.Point(172, 8)
                    empty2.Width = Me.Width - empty2.Left - 8
                    Dim num As Integer = CInt(System.Math.Ceiling(e.Graphics.MeasureString(Me._Title, font2, empty2.Width, genericDefault).Height))
                    empty3.Location = empty2.Location
                    empty3.Y += num + 8
                    empty3.Size = New System.Drawing.Size(Me.Width - empty3.Left - 8, Me.Height - empty3.Y)
                    e.Graphics.DrawString(Me._Title, font2, System.Drawing.SystemBrushes.WindowText, empty2, genericDefault)
                    e.Graphics.DrawString(Me._Description, font, System.Drawing.SystemBrushes.WindowText, empty3, genericDefault)
                    Exit Select
            End Select
        End Sub

    End Class



End Namespace