' *************************************************************************************************
' Shape.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace ShapeControl

    ''' <summary>
    ''' Steuerelement zum Darstellen einer Linie, eines Rechtecks oder einer Ellipse.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Darstellen einer Linie, eines Rechtecks oder einer Ellipse.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(ShapeControl.Shape), "Shape.bmp")>
    Public Class Shape

        Inherits Control

#Region "interne Eigenschftsvariablen"

        Private ReadOnly components As IContainer
        Private _ShapeModus As ShapeModes
        Private _LineWidth As Single
        Private _LineColor As Color
        Private _FillColor As Color
        Private _DiagonalLineModus As DiagonalLineModes

#End Region

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.ShapeControl.Shape"/>. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            'Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent()
            'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            InitializeVariables()
            InitializeStyles()
        End Sub

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Legt die anzuzeigende Form fest oder gibt diese zurück.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt die anzuzeigende Form fest oder gibt diese zurück.")>
        Public Property ShapeModus() As ShapeModes
            Get
                Return _ShapeModus
            End Get
            Set(value As ShapeModes)
                _ShapeModus = value
                RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Breite der Linie oder Rahmenlinie fest oder gibt diese zurück.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt die Breite der Linie oder Rahmenlinie fest oder gibt diese zurück.")>
        Public Property LineWidth() As Single
            Get
                Return _LineWidth
            End Get
            Set(value As Single)
                _LineWidth = value
                RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Farbe der Linie oder Rahmenlinie fest oder gibt diese zurück.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt die Farbe der Linie oder Rahmenlinie fest oder gibt diese zurück.")>
        Public Property LineColor() As Color
            Get
                Return _LineColor
            End Get
            Set(value As Color)
                _LineColor = value
                RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Füllfarbe für die Form fest oder gibt diese zurück.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt die Füllfarbe für die Form fest oder gibt diese zurück.")>
        Public Property FillColor() As Color
            Get
                Return _FillColor
            End Get
            Set(value As Color)
                _FillColor = value
                RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt fest ob eine diagonale Linie von links oben nach rechts unten oder
        ''' umgekehrt verläuft oder gibt dieses zurück.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt fest ob eine diagonale Linie von links oben nach rechts unten oder umgekehrt verläuft oder gibt dieses zurück.")>
        Public Property DiagonalLineModus() As DiagonalLineModes
            Get
                Return _DiagonalLineModus
            End Get
            Set(value As DiagonalLineModes)
                _DiagonalLineModus = value
                RecreateHandle()
            End Set
        End Property

#End Region

#Region "überschriebene Eigenschften"

        ''' <summary>
        ''' Legt spezielle Parameter für das ShapeControl fest
        ''' </summary>
        ''' <remarks>
        ''' https://stackoverflow.com/questions/511320/transparent-control-backgrounds-on-a-vb-net-gradient-filled-form
        ''' </remarks>
        Protected Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                'WS EX TRANSPARENT aktivieren
                'https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property

#End Region

#Region "ausgeblendete Eigenschaften"

        ''' <summary>
        ''' ausgeblendet da nicht relevant
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
        ''' ausgeblendet da nicht relevant
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
        ''' ausgeblendet da nicht relevant
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
        ''' ausgeblendet da nicht relevant
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ForeColor As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property RightToLeft As RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

#End Region

        ''' <summary>
        ''' zeichnet das ShapeControl neu
        ''' </summary>
        ''' <param name="e">Ein <see cref="System.Windows.Forms.PaintEventArgs"/>, das die
        ''' Ereignisdaten enthält.
        ''' </param>
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim g As Graphics = CreateGraphics()
            Select Case _ShapeModus
                Case ShapeModes.HorizontalLine  ' horizontale Linie zeichnen (mittig im Rahmen des Controls)
                    g.DrawLine(New Pen(_LineColor, _LineWidth), 0, CInt(Height / 2), Width, CInt(Height / 2))
                Case ShapeModes.VerticalLine ' vertikale Linie zeichnen (mittig im Rahmen des Controls)
                    g.DrawLine(New Pen(_LineColor, _LineWidth), CInt(Width / 2), 0, CInt(Width / 2), Height)
                Case ShapeModes.DiagonalLine ' diagonale Linie zeichnen
                    Select Case _DiagonalLineModus ' von links unten nach rechts oben
                        Case DiagonalLineModes.BottomLeftToTopRight
                            g.DrawLine(New Pen(_LineColor, _LineWidth), 0, Height, Width, 0)
                        Case DiagonalLineModes.TopLeftToBottomRight   ' von links oben nach rechts unten
                            g.DrawLine(New Pen(_LineColor, _LineWidth), 0, 0, Width, Height)
                    End Select
                Case ShapeModes.Rectangle ' einfaches Rechteck zeichnen
                    g.DrawRectangle(New Pen(_LineColor, _LineWidth), _LineWidth / 2, _LineWidth / 2, Width - _LineWidth, Height - _LineWidth)
                Case ShapeModes.FilledRectangle  ' einfaches Rechteck zeichnen und ausfüllen
                    g.DrawRectangle(New Pen(_LineColor, _LineWidth), _LineWidth / 2, _LineWidth / 2, Width - _LineWidth, Height - _LineWidth)
                    g.FillRectangle(New SolidBrush(_FillColor), _LineWidth, _LineWidth, Width - (2 * _LineWidth), Height - (2 * _LineWidth))
                Case ShapeModes.Ellipse ' einfache Ellipse zeichnen
                    g.DrawEllipse(New Pen(_LineColor, _LineWidth), _LineWidth / 2, _LineWidth / 2, Width - _LineWidth, Height - _LineWidth)
                Case ShapeModes.FilledEllipse ' einfache Ellipe zeichnen und ausfüllen
                    g.DrawEllipse(New Pen(_LineColor, _LineWidth), _LineWidth / 2, _LineWidth / 2, Width - _LineWidth, Height - _LineWidth)
                    g.FillEllipse(New SolidBrush(_FillColor), _LineWidth, _LineWidth, Width - (2 * _LineWidth), Height - (2 * _LineWidth))
            End Select
        End Sub

        ''' <summary>
        ''' Gibt die vom <see cref="SchlumpfSoft.Controls.ShapeControl.Shape"/> verwendeten
        ''' nicht verwalteten Ressourcen frei und gibt optional die verwalteten Ressourcen
        ''' frei.
        ''' </summary>
        ''' <param name="disposing"><para>Wenn auf <see langword="true"/> gesetzt, dann
        ''' werden verwaltete und nicht verwaltete Ressourcen freigegeben</para>
        ''' <para>andernfalls nur nicht verwaltete Ressourcen.</para></param>
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        ''' <summary>
        ''' Initialisiert die Standardwerte für das ShapeControl
        ''' </summary>
        Private Sub InitializeVariables()
            _ShapeModus = ShapeModes.HorizontalLine 'Horizontale Linie
            _DiagonalLineModus = DiagonalLineModes.TopLeftToBottomRight 'diagonale Linie von links oben nach rechts unten
            _LineColor = Color.Black 'schwarze Linie für Linie und Rahmenlinie bei Ellipse und Rechteck
            _LineWidth = 2 'Breite der Linie
            _FillColor = Color.Gray 'Füllfarbe für Ellipse und Rechteck
        End Sub

        ''' <summary>
        ''' Initialisiert die Styles für das ShapeControl
        ''' </summary>
        Private Sub InitializeStyles()
            SetStyle(ControlStyles.Opaque, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, False)
        End Sub

        ''' <summary>
        ''' <para>Die folgende Prozedur ist für den Komponenten-Designer
        ''' erforderlich.</para>
        ''' <para>Sie kann mit dem Komponenten-Designer geändert werden.</para>
        ''' </summary>
        ''' <remarks>
        ''' <para>Hinweis:</para>
        ''' <para>Das Bearbeiten mit dem Code-Editor ist nicht möglich.</para>
        ''' </remarks>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            SuspendLayout()
            ResumeLayout(False)
        End Sub

    End Class

End Namespace