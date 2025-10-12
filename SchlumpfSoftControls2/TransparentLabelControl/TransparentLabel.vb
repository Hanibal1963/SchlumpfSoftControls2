' *************************************************************************************************
' TransparentLabel.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
'Imports SchlumpfSoft.Controls.Attribute

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

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="TransparentLabel"/>-Klasse.
        ''' </summary>
        Public Sub New()
            'Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent()
            'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            InitializeStyles()
        End Sub

#Region "ausgeblendete Eigenschaften"

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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

        ''' <summary>
        ''' Gibt die Erstellungsparameter für das Steuerelement zurück und aktiviert die Transparenz.
        ''' </summary>
        ''' <returns>Die angepassten <see cref="CreateParams"/> mit aktiviertem WS_EX_TRANSPARENT-Stil.</returns>
        Protected Overrides ReadOnly Property CreateParams As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                ' WS EX TRANSPARENT aktivieren (https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles)
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property

        ''' <summary>
        ''' Gibt die Erstellungsparameter für das Steuerelement zurück und aktiviert die Transparenz.
        ''' </summary>
        Private Sub InitializeStyles()
            SetStyle(ControlStyles.Opaque, True)
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, False)
        End Sub

        ''' <summary>
        ''' Bereinigt die von der <see cref="TransparentLabel"/> verwendeten Ressourcen.
        ''' </summary>
        ''' <param name="disposing">Gibt an, ob verwaltete Ressourcen freigegeben werden sollen.</param>
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

        ''' <summary>
        ''' Initialisiert die Komponenten des Steuerelements.
        ''' </summary>
        ''' <remarks>
        ''' <para>Die folgende Prozedur ist für den Komponenten-Designer
        ''' erforderlich.</para>
        ''' <para>Sie kann mit dem Komponenten-Designer geändert werden.</para>
        ''' <para>Das Bearbeiten mit dem Code-Editor ist nicht möglich.</para>
        ''' </remarks>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            components = New Container()
        End Sub

    End Class

End Namespace