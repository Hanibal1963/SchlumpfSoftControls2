' *************************************************************************************************
' 
' TransparentLabel.vb
' Copyright (c) 2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.
'
' weitere Infos:
' <Browsable> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.browsableattribute?view=netframework-4.7.2
' <Category> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.categoryattribute?view=netframework-4.7.2
' <Description> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.descriptionattribute?view=netframework-4.7.2
'
' *************************************************************************************************

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports SchlumpfSoft.Controls.Attribute

Namespace TransparentLabelControl

    ''' <summary>
    ''' Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(TransparentLabelControl.TransparentLabel), "TransparentLabel.bmp")>
    Public Class TransparentLabel

        Inherits Label

        Private components As IContainer

        Public Sub New()
            'Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent()
            'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            InitializeStyles()
        End Sub

#Region "ausgeblendete Eigenschaften"

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As Color)
                MyBase.BackColor = value
            End Set
        End Property

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage As Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImageLayout As ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overloads Property FlatStyle As FlatStyle
            Get
                Return MyBase.FlatStyle
            End Get
            Set(value As FlatStyle)
                MyBase.FlatStyle = value
            End Set
        End Property

#End Region

        ' Hiermit wird die Möglichkeit der Transparenz aktiviert
        Protected Overrides ReadOnly Property CreateParams As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                ' WS EX TRANSPARENT aktivieren (https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles)
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property

        Private Sub InitializeStyles()
            SetStyle(ControlStyles.Opaque, True)
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, False)
        End Sub

        ' Das Steuerelement überschreibt den Löschvorgang zum Bereinigen der Komponentenliste.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        ' Die folgende Prozedur ist für den Komponenten-Designer erforderlich.<br/>
        ' Sie kann mit dem Komponenten-Designer geändert werden.
        ' Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            components = New Container()
        End Sub

    End Class

End Namespace