

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.Design


Namespace WizardControl

    ''' <summary>
    ''' Definiert eine Seite des Controls
    ''' </summary>
    <ToolboxItem(False)>
    Public Class WizardPage

        Inherits Panel

        Private _Style As PageStyle = PageStyle.Standard
        Private _Title As String = String.Empty
        Private _Description As String = String.Empty

        <Category("Design")>
        <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
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

        <DefaultValue("")>
        <Category("Design")>
        <Description("Ruft den Titel der Assistentenseite ab oder legt diesen fest.")>
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

        <DefaultValue("")>
        <Category("Design")>
        <Description("Ruft die Beschreibung der Assistentenseite ab oder legt diese fest.")>
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

            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            Me.SetStyle(ControlStyles.DoubleBuffer, True)
            Me.SetStyle(ControlStyles.ResizeRedraw, True)
            Me.SetStyle(ControlStyles.UserPaint, True)

        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)

            MyBase.OnPaint(e)

            If Me._Style = PageStyle.Custom Then Return

            Dim clientRectangle = MyBase.ClientRectangle
            Dim empty = Rectangle.Empty
            Dim empty2 = Rectangle.Empty
            Dim empty3 = Rectangle.Empty
            Dim genericDefault = StringFormat.GenericDefault

            genericDefault.LineAlignment = StringAlignment.Near
            genericDefault.Alignment = StringAlignment.Near
            genericDefault.Trimming = StringTrimming.EllipsisCharacter

            Select Case Me.Style

                Case PageStyle.Standard

                    clientRectangle.Height = 64
                    ControlPaint.DrawBorder3D(
                        e.Graphics,
                        clientRectangle,
                        Border3DStyle.Etched,
                        Border3DSide.Bottom)

                    clientRectangle.Height -= SystemInformation.Border3DSize.Height
                    e.Graphics.FillRectangle(
                        SystemBrushes.Window,
                        clientRectangle)

                    Dim num2 As Integer = CInt(Math.Floor(8.0))

                    empty.Location = New Point(Me.Width - 48 - num2, num2)
                    empty.Size = New Size(48, 48)

                    Dim image2 As Image = Nothing
                    Dim font3 = MyBase.Font
                    Dim font4 = MyBase.Font

                    If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Wizard Then

                        Dim wizard2 As Wizard = CType(Me.Parent, Wizard)
                        image2 = wizard2.ImageHeader
                        If image2 Is Nothing Then
                            empty.Size = New Size(0, 0)
                        End If
                        font3 = wizard2.HeaderFont
                        font4 = wizard2.HeaderTitleFont
                    End If

                    If image2 Is Nothing Then
                        ControlPaint.DrawFocusRectangle(e.Graphics, empty)
                    Else
                        e.Graphics.DrawImage(image2, empty)
                    End If

                    Dim num3 As Integer = CInt(
                        Math.Ceiling(
                        e.Graphics.MeasureString(
                        Me._Title,
                        font4,
                        0,
                        genericDefault).Height))

                    empty2.Location = New Point(8, 8)
                    empty2.Size = New Size(empty.Left - 8, num3)
                    empty3.Location = empty2.Location
                    empty3.Y += num3 + 4
                    empty3.Size = New Size(
                        empty2.Width,
                        64 - empty3.Y)

                    e.Graphics.DrawString(
                        Me._Title,
                        font4,
                        SystemBrushes.WindowText,
                        empty2,
                        genericDefault)

                    e.Graphics.DrawString(
                        Me._Description,
                        font3,
                        SystemBrushes.WindowText,
                        empty3,
                        genericDefault)

                    Exit Select

                Case PageStyle.Welcome, PageStyle.Finish

                    e.Graphics.FillRectangle(SystemBrushes.Window, clientRectangle)
                    empty.Location = Point.Empty
                    empty.Size = New Size(164, Me.Height)

                    Dim image As Image = Nothing
                    Dim font = MyBase.Font
                    Dim font2 = MyBase.Font

                    If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Wizard Then
                        Dim wizard As Wizard = CType(Me.Parent, Wizard)
                        image = wizard.ImageWelcome
                        font = wizard.WelcomeFont
                        font2 = wizard.WelcomeTitleFont
                    End If

                    If image Is Nothing Then
                        ControlPaint.DrawFocusRectangle(e.Graphics, empty)
                    Else
                        e.Graphics.DrawImage(image, empty)
                    End If

                    empty2.Location = New Point(172, 8)
                    empty2.Width = Me.Width - empty2.Left - 8

                    Dim num As Integer = CInt(
                        Math.Ceiling(
                        e.Graphics.MeasureString(
                        Me._Title,
                        font2,
                        empty2.Width,
                        genericDefault).Height))

                    empty3.Location = empty2.Location
                    empty3.Y += num + 8
                    empty3.Size = New Size(
                        Me.Width - empty3.Left - 8,
                        Me.Height - empty3.Y)

                    e.Graphics.DrawString(
                        Me._Title,
                        font2,
                        SystemBrushes.WindowText,
                        empty2,
                        genericDefault)

                    e.Graphics.DrawString(
                        Me._Description,
                        font,
                        SystemBrushes.WindowText,
                        empty3,
                        genericDefault)

                    Exit Select

            End Select

        End Sub

    End Class

    ''' <summary>
    ''' Definiert die Willkommenseite
    ''' </summary>
    <ToolboxItem(False)>
    Public Class PageWelcome

        Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Welcome

        <DefaultValue(PageStyle.Welcome)>
        <Category("Design")>
        <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
        Public Overrides Property Style As PageStyle
            Get
                Return Me._Style
            End Get
            Set(value As PageStyle)
                Me._Style = value
            End Set
        End Property

    End Class

    ''' <summary>
    ''' Definiert eine Standardseite
    ''' </summary>
    <ToolboxItem(False)>
    Public Class PageStandard

        Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Standard

        <DefaultValue(PageStyle.Standard)>
        <Category("Design")>
        <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
        Public Overrides Property Style As PageStyle
            Get
                Return Me._Style
            End Get
            Set(value As PageStyle)
                Me._Style = value
            End Set
        End Property

    End Class

    ''' <summary>
    ''' Definiert eine Benutzerdefinierte Seite
    ''' </summary>
    <ToolboxItem(False)>
    Public Class PageCustom

        Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Custom

        <DefaultValue(PageStyle.Custom)>
        <Category("Design")>
        <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
        Public Overrides Property Style As PageStyle
            Get
                Return Me._Style
            End Get
            Set(value As PageStyle)
                Me._Style = value
            End Set
        End Property

    End Class

    ''' <summary>
    ''' Definiert die Abschlußseite
    ''' </summary>
    <ToolboxItem(False)>
    Public Class PageFinish

        Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Finish

        <DefaultValue(PageStyle.Finish)>
        <Category("Design")>
        <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
        Public Overrides Property Style As PageStyle
            Get
                Return Me._Style
            End Get
            Set(value As PageStyle)
                Me._Style = value
            End Set
        End Property

    End Class

    ''' <summary>
    ''' Designer für die Seiten des Controls
    ''' </summary>
    Friend Class PageDesigner

        Inherits ParentControlDesigner

        Public Overrides ReadOnly Property SelectionRules As SelectionRules
            Get
                Return SelectionRules.Locked Or SelectionRules.Visible
            End Get
        End Property

    End Class

    ''' <summary>
    ''' Definiert die Auflistung der Seiten des Assistenten
    ''' </summary>
    Public Class PagesCollection

        Inherits CollectionBase

        Private ReadOnly _Owner As Wizard = Nothing

        Default Public Property Item(Index As Integer) As WizardPage
            Get
                Return CType(Me.List(Index), WizardPage)
            End Get
            Set(value As WizardPage)
                Me.List(Index) = value
            End Set
        End Property

        Friend Sub New(Owner As Wizard)

            Me._Owner = Owner

        End Sub

        Public Function Add(Page As WizardPage) As Integer

            Return Me.List.Add(Page)

        End Function

        Public Sub AddRange(Pages As WizardPage())

            For Each value As WizardPage In Pages
                Dim unused = Me.Add(value)
            Next

        End Sub

        Public Function IndexOf(Page As WizardPage) As Integer

            Return Me.List.IndexOf(Page)

        End Function

        Public Sub Insert(Index As Integer, Page As WizardPage)

            Me.List.Insert(Index, Page)

        End Sub

        Public Sub Remove(Page As WizardPage)

            Me.List.Remove(Page)

        End Sub

        Public Function Contains(Page As WizardPage) As Boolean

            Return Me.List.Contains(Page)

        End Function

        Protected Overrides Sub OnInsertComplete(Index As Integer, Value As Object)

            MyBase.OnInsertComplete(Index, Value)
            Me._Owner.SelectedIndex = Index

        End Sub

        <System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Style",
            "IDE0045:In bedingten Ausdruck konvertieren",
            Justification:="<Ausstehend>")>
        Protected Overrides Sub OnRemoveComplete(Index As Integer, Value As Object)

            MyBase.OnRemoveComplete(Index, Value)
            If Me._Owner.SelectedIndex = Index Then
                If Index < Me.InnerList.Count Then
                    Me._Owner.SelectedIndex = Index
                Else
                    Me._Owner.SelectedIndex = Me.InnerList.Count - 1
                End If
            End If

        End Sub

    End Class

    ''' <summary>
    ''' Dient zum anzeigen der Seitenstile im Seitendesigner
    ''' </summary>
    Friend Class PagesCollectionEditor

        Inherits CollectionEditor

        Private ReadOnly _PageTypes As Type()

        Public Sub New(PageType As Type)

            MyBase.New(PageType)
            Me._PageTypes = New Type(3) {
                GetType(PageWelcome),
                GetType(PageStandard),
                GetType(PageCustom),
                GetType(PageFinish)}

        End Sub

        Protected Overrides Function CreateNewItemTypes() As Type()

            Return Me._PageTypes

        End Function

    End Class

    ''' <summary>
    ''' Auflistung der verfügbaren Seitenstile
    ''' </summary>
    Public Enum PageStyle

        ''' <summary>
        ''' Stellt eine Standardseite dar.
        ''' </summary>
        Standard = 0

        ''' <summary>
        ''' Stellt die Willkommensseite dar.
        ''' </summary>
        Welcome = 1

        ''' <summary>
        ''' Stellt die Abschlußseite dar.
        ''' </summary>
        Finish = 2

        ''' <summary>
        ''' Stellt eine Benutzerdefinierte Seite dar.
        ''' </summary>
        Custom = 3

    End Enum


End Namespace