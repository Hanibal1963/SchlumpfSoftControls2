' *************************************************************************************************
' Shape.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ShapeControl

    ''' <summary>
    ''' Steuerelement zum Darstellen einer Linie, eines Rechtecks oder einer Ellipse.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Steuerelement zum Darstellen einer Linie, eines Rechtecks oder einer Ellipse.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(ShapeControl.Shape), "Shape.bmp")>
    Public Class Shape : Inherits System.Windows.Forms.Control

#Region "Variablen"

        ''' <summary>
        ''' Container für Komponenten, die von diesem Control verwendet werden.
        ''' </summary>
        Private ReadOnly components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Speichert den aktuell gesetzten Modus (Formtyp), der gezeichnet werden soll.
        ''' </summary>
        Private _ShapeModus As ShapeModes

        ''' <summary>
        ''' Speichert die Linienbreite für Linie oder Rahmen.
        ''' </summary>
        Private _LineWidth As Single

        ''' <summary>
        ''' Speichert die Farbe der Linie oder Rahmenlinie.
        ''' </summary>
        Private _LineColor As System.Drawing.Color

        ''' <summary>
        ''' Speichert die Füllfarbe für Rechteck oder Ellipse, sofern gefüllte Formen gewählt wurden.
        ''' </summary>
        Private _FillColor As System.Drawing.Color

        ''' <summary>
        ''' Speichert die Richtung der diagonalen Linie.
        ''' </summary>
        Private _DiagonalLineModus As DiagonalLineModes

#End Region

#Region "Aufzählungen"

        ''' <summary>
        ''' Legt fest in welcher Richtung die diagonale Linie gezeichnet wird
        ''' </summary>
        Public Enum DiagonalLineModes

            ''' <summary>
            ''' von links oben nach rechts unten
            ''' </summary>
            TopLeftToBottomRight = 0

            ''' <summary>
            ''' von links unten nach rechts oben
            ''' </summary>
            BottomLeftToTopRight = 1

        End Enum

        ''' <summary>
        ''' Legt fest welche Form gezeichnet wird
        ''' </summary>
        Public Enum ShapeModes

            ''' <summary>
            ''' Horizontale Linie
            ''' </summary>
            HorizontalLine = 0

            ''' <summary>
            ''' Vertikale Linie
            ''' </summary>
            VerticalLine = 1

            ''' <summary>
            ''' diagonale Linie
            ''' </summary>
            DiagonalLine = 2

            ''' <summary>
            ''' Rechteck
            ''' </summary>
            Rectangle = 3

            ''' <summary>
            ''' gefülltes Rechteck
            ''' </summary>
            FilledRectangle = 4

            ''' <summary>
            ''' Kreis oder Ellipse
            ''' </summary>
            Ellipse = 5

            ''' <summary>
            ''' gefüllter Kreis oder gefüllte Ellipse
            ''' </summary>
            FilledEllipse = 6

        End Enum


#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Legt die anzuzeigende Form fest oder gibt diese zurück.
        ''' </summary>
        ''' <value>Ein Wert aus <see cref="ShapeModes"/> der die Form bestimmt.</value>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die anzuzeigende Form fest oder gibt diese zurück.")>
        Public Property ShapeModus() As ShapeModes
            Get
                Return Me._ShapeModus
            End Get
            Set(value As ShapeModes)
                Me._ShapeModus = value
                Me.RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Breite der Linie oder Rahmenlinie fest oder gibt diese zurück.
        ''' </summary>
        ''' <value>Die Breite der Linie in Pixeln.</value>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Breite der Linie oder Rahmenlinie fest oder gibt diese zurück.")>
        Public Property LineWidth() As Single
            Get
                Return Me._LineWidth
            End Get
            Set(value As Single)
                Me._LineWidth = value
                Me.RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Farbe der Linie oder Rahmenlinie fest oder gibt diese zurück.
        ''' </summary>
        ''' <value>Eine <see cref="System.Drawing.Color"/> Instanz für die Linienfarbe.</value>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Farbe der Linie oder Rahmenlinie fest oder gibt diese zurück.")>
        Public Property LineColor() As System.Drawing.Color
            Get
                Return Me._LineColor
            End Get
            Set(value As System.Drawing.Color)
                Me._LineColor = value
                Me.RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Füllfarbe für die Form fest oder gibt diese zurück.
        ''' </summary>
        ''' <value>Eine <see cref="System.Drawing.Color"/> Instanz für die Füllung.</value>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Füllfarbe für die Form fest oder gibt diese zurück.")>
        Public Property FillColor() As System.Drawing.Color
            Get
                Return Me._FillColor
            End Get
            Set(value As System.Drawing.Color)
                Me._FillColor = value
                Me.RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt fest ob eine diagonale Linie von links oben nach rechts unten oder
        ''' umgekehrt verläuft oder gibt dieses zurück.
        ''' </summary>
        ''' <value>Ein Wert aus <see cref="DiagonalLineModes"/> zur Bestimmung der Richtung.</value>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt fest ob eine diagonale Linie von links oben nach rechts unten oder umgekehrt verläuft oder gibt dieses zurück.")>
        Public Property DiagonalLineModus() As DiagonalLineModes
            Get
                Return Me._DiagonalLineModus
            End Get
            Set(value As DiagonalLineModes)
                Me._DiagonalLineModus = value
                Me.RecreateHandle()
            End Set
        End Property

        ''' <summary>
        ''' Legt spezielle Parameter für das ShapeControl fest.
        ''' </summary>
        ''' <returns>Ein <see cref="System.Windows.Forms.CreateParams"/> Objekt mit erweiterten Stil-Flags.</returns>
        ''' <remarks>
        ''' https://stackoverflow.com/questions/511320/transparent-control-backgrounds-on-a-vb-net-gradient-filled-form
        ''' </remarks>
        Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
            Get
                Dim cp As System.Windows.Forms.CreateParams = MyBase.CreateParams
                'WS EX TRANSPARENT aktivieren
                'https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        ''' <value>Die Hintergrundfarbe des Controls.</value>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackColor As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.BackColor = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        ''' <value>Das Hintergrundbild des Controls.</value>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage As System.Drawing.Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As System.Drawing.Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        ''' <value>Das Layout des Hintergrundbildes.</value>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImageLayout As System.Windows.Forms.ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As System.Windows.Forms.ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        ''' <value>Die Schriftart für das Control.</value>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Font As System.Drawing.Font
            Get
                Return MyBase.Font
            End Get
            Set(value As System.Drawing.Font)
                MyBase.Font = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        ''' <value>Die Vordergrundfarbe des Controls.</value>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property ForeColor As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        ''' <value>Die Schreibrichtung.</value>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property RightToLeft As System.Windows.Forms.RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As System.Windows.Forms.RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        ''' <value>Der Text des Controls.</value>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.ShapeControl.Shape"/>. 
        ''' </summary>
        ''' <remarks>Richtet Standardwerte und Zeichenstile ein.</remarks>
        Public Sub New()
            'Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            Me.InitializeVariables()
            Me.InitializeStyles()
        End Sub

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Initialisiert die Standardwerte für das ShapeControl.
        ''' </summary>
        Private Sub InitializeVariables()
            Me._ShapeModus = ShapeModes.HorizontalLine 'Horizontale Linie
            Me._DiagonalLineModus = DiagonalLineModes.TopLeftToBottomRight 'diagonale Linie von links oben nach rechts unten
            Me._LineColor = System.Drawing.Color.Black 'schwarze Linie für Linie und Rahmenlinie bei Ellipse und Rechteck
            Me._LineWidth = 2 'Breite der Linie
            Me._FillColor = System.Drawing.Color.Gray 'Füllfarbe für Ellipse und Rechteck
        End Sub

        ''' <summary>
        ''' Initialisiert die Styles für das ShapeControl.
        ''' </summary>
        Private Sub InitializeStyles()
            Me.SetStyle(System.Windows.Forms.ControlStyles.Opaque, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, False)
        End Sub

        ''' <summary>
        ''' <para>Die folgende Prozedur ist für den Komponenten-Designer erforderlich.</para>
        ''' <para>Sie kann mit dem Komponenten-Designer geändert werden.</para>
        ''' </summary>
        ''' <remarks>
        ''' <para>Hinweis:</para>
        ''' <para>Das Bearbeiten mit dem Code-Editor ist nicht möglich.</para>
        ''' </remarks>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.ResumeLayout(False)
        End Sub

#End Region

#Region "überschriebene Methoden"

        ''' <summary>
        ''' zeichnet das ShapeControl neu.
        ''' </summary>
        ''' <param name="e">Ein <see cref="System.Windows.Forms.PaintEventArgs"/>, das die Ereignisdaten enthält.</param>
        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            Dim g As System.Drawing.Graphics = Me.CreateGraphics()
            Select Case Me._ShapeModus
                Case ShapeModes.HorizontalLine  ' horizontale Linie zeichnen (mittig im Rahmen des Controls)
                    g.DrawLine(New System.Drawing.Pen(Me._LineColor, Me._LineWidth), 0, CInt(Me.Height / 2), Me.Width, CInt(Me.Height / 2))
                Case ShapeModes.VerticalLine ' vertikale Linie zeichnen (mittig im Rahmen des Controls)
                    g.DrawLine(New System.Drawing.Pen(Me._LineColor, Me._LineWidth), CInt(Me.Width / 2), 0, CInt(Me.Width / 2), Me.Height)
                Case ShapeModes.DiagonalLine ' diagonale Linie zeichnen
                    Select Case Me._DiagonalLineModus ' von links unten nach rechts oben
                        Case DiagonalLineModes.BottomLeftToTopRight
                            g.DrawLine(New System.Drawing.Pen(Me._LineColor, Me._LineWidth), 0, Me.Height, Me.Width, 0)
                        Case DiagonalLineModes.TopLeftToBottomRight   ' von links oben nach rechts unten
                            g.DrawLine(New System.Drawing.Pen(Me._LineColor, Me._LineWidth), 0, 0, Me.Width, Me.Height)
                    End Select
                Case ShapeModes.Rectangle ' einfaches Rechteck zeichnen
                    g.DrawRectangle(New System.Drawing.Pen(Me._LineColor, Me._LineWidth), Me._LineWidth / 2, Me._LineWidth / 2, Me.Width - Me._LineWidth, Me.Height - Me._LineWidth)
                Case ShapeModes.FilledRectangle  ' einfaches Rechteck zeichnen und ausfüllen
                    g.DrawRectangle(New System.Drawing.Pen(Me._LineColor, Me._LineWidth), Me._LineWidth / 2, Me._LineWidth / 2, Me.Width - Me._LineWidth, Me.Height - Me._LineWidth)
                    g.FillRectangle(New System.Drawing.SolidBrush(Me._FillColor), Me._LineWidth, Me._LineWidth, Me.Width - (2 * Me._LineWidth), Me.Height - (2 * Me._LineWidth))
                Case ShapeModes.Ellipse ' einfache Ellipse zeichnen
                    g.DrawEllipse(New System.Drawing.Pen(Me._LineColor, Me._LineWidth), Me._LineWidth / 2, Me._LineWidth / 2, Me.Width - Me._LineWidth, Me.Height - Me._LineWidth)
                Case ShapeModes.FilledEllipse ' einfache Ellipe zeichnen und ausfüllen
                    g.DrawEllipse(New System.Drawing.Pen(Me._LineColor, Me._LineWidth), Me._LineWidth / 2, Me._LineWidth / 2, Me.Width - Me._LineWidth, Me.Height - Me._LineWidth)
                    g.FillEllipse(New System.Drawing.SolidBrush(Me._FillColor), Me._LineWidth, Me._LineWidth, Me.Width - (2 * Me._LineWidth), Me.Height - (2 * Me._LineWidth))
            End Select
        End Sub

        ''' <summary>
        ''' Gibt die vom <see cref="SchlumpfSoft.Controls.ShapeControl.Shape"/> verwendeten nicht verwalteten Ressourcen frei und gibt optional die verwalteten Ressourcen frei.
        ''' </summary>
        ''' <param name="disposing">Wenn auf <see langword="true"/> gesetzt, dann werden verwaltete und nicht verwaltete Ressourcen freigegeben, andernfalls nur nicht verwaltete Ressourcen.</param>
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso Me.components IsNot Nothing Then
                    Me.components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

#End Region

    End Class

End Namespace