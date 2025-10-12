' *************************************************************************************************
' AniGif.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

#Region "Importe"

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
'Imports SchlumpfSoft.Controls.Attribute
Imports System.Drawing.Drawing2D ' Für bessere Interpolationsqualität bei Skalierung

#End Region

Namespace AniGifControl

    ''' <summary>
    ''' Control zum anzeigen von animierten Grafiken.
    ''' </summary>
    <ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <Description("Control zum Anzeigen von animierten Grafiken.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(AniGifControl.AniGif), "AniGifControl.AniGif.bmp")>
    Public Class AniGif

        Inherits UserControl

        Implements IDisposable

        Private WithEvents Timer As Timer
        Private components As IContainer
        Private disposedValue As Boolean

#Region "Interne Eigenschaftsvariablen"

        Private _Gif As Bitmap ' Das aktuell geladene GIF-Bild
        Private _GifSizeMode As SizeMode ' Gibt an, wie das GIF im Steuerelement skaliert/angezeigt wird (z.B. gestreckt, zentriert)
        Private _CustomDisplaySpeed As Boolean ' Bestimmt, ob eine benutzerdefinierte Anzeigegeschwindigkeit verwendet wird
        Private _FramesPerSecond As Decimal ' Bildwiederholrate (Frames pro Sekunde) für die Animation
        Private _Dimension As Imaging.FrameDimension ' Die Dimension (z.B. Zeit) der animierten Frames im GIF
        Private _Frame As Integer ' Der aktuelle Frame-Index, der angezeigt wird
        Private _MaxFrame As Integer ' Die maximale Anzahl der Frames im GIF
        Private _Autoplay As Boolean ' Gibt an, ob die Animation automatisch abgespielt wird
        Private _ZoomFactor As Decimal ' Zoomfaktor für die Anzeige des GIFs
        Private ReadOnly _AnimationHandler As EventHandler = AddressOf OnNextFrame ' Gemeinsamer Handler für ImageAnimator zum Stoppen/Neu-Registrieren

#End Region

#Region "Ereignisdefinitionen"

        ''' <summary>
        ''' Wird ausgelöst wenn die Grafik nicht animiert werden kann.
        ''' </summary>
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Wird ausgelöst wenn die Grafik nicht animiert werden kann.")>
        Public Event NoAnimation(sender As Object, e As EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn sich das Bild geändert hat.
        ''' </summary>
        Private Event GifChanged()

        ''' <summary>
        ''' Wird ausgelöst wenn sich die Anzeigegeschwindigkeit geändert hat.
        ''' </summary>
        Private Event CustomDisplaySpeedChanged()

        ''' <summary>
        ''' Wird ausgelöst wenn sich die Frames pro Sekunde geändert haben.
        ''' </summary>
        Private Event FramesPerSecondChanged()

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Legt fest ob die Animation sofort nach dem laden gestartet wird.
        ''' </summary>
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Legt fest ob die Animation sofort nach dem laden gestartet wird.")>
        Public Property AutoPlay() As Boolean
            Get
                Return _Autoplay
            End Get
            Set(value As Boolean)
                _Autoplay = value ' AutoPlay setzen und Animation starten/stoppen
                InitLayout()
            End Set
        End Property

        ''' <summary>
        ''' Gibt die animierte Gif-Grafik zurück oder legt diese fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt die animierte Gif-Grafik zurück oder legt diese fest.")>
        Public Property Gif() As Bitmap
            Get
                Return _Gif
            End Get
            Set(value As Bitmap)
                SetGifImage(value)
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.")>
        Public Property GifSizeMode() As SizeMode
            Get
                Return _GifSizeMode
            End Get
            Set(value As SizeMode)
                SetGifSizeMode(value)
            End Set
        End Property

        ''' <summary>
        ''' Legt fest ob die benutzerdefinierte Anzeigegeschwindigkeit oder <br/> 
        ''' die in der Datei festgelegte Geschwindigkeit benutzt wird.
        ''' </summary>
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Legt fest ob die benutzerdefinierte Anzeigegeschwindigkeit oder die in der Datei festgelegte Geschwindigkeit benutzt wird.")>
        Public Property CustomDisplaySpeed As Boolean
            Get
                Return _CustomDisplaySpeed
            End Get
            Set(value As Boolean)
                SetCustomDisplaySpeed(value)
            End Set
        End Property

        ''' <summary>
        ''' Legt die benutzerdefinierte Anzeigegeschwindigkeit in Bildern/Sekunde fest.
        ''' </summary>
        ''' <remarks>
        ''' Bewirkt nur eine Änderung wenn <seealso cref="CustomDisplaySpeed"/> auf True festgelegt ist.
        ''' </remarks>
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Legt die benutzerdefinierte Anzeigegeschwindigkeit in Bildern/Sekunde fest wenn CustomDisplaySpeed auf True festgelegt ist.")>
        Public Property FramesPerSecond As Decimal
            Get
                Return _FramesPerSecond
            End Get
            Set(value As Decimal)
                _FramesPerSecond = CheckFramesPerSecondValue(value)
                RaiseEvent FramesPerSecondChanged()
            End Set
        End Property

        ''' <summary>
        ''' Legt den Zoomfaktor fest. 
        ''' </summary>
        ''' <remarks>
        ''' Bewirkt nur eine Änderung wenn <seealso cref="GifSizeMode"/> auf <seealso cref="SizeMode.Zoom"/> festgelegt ist.
        ''' </remarks>
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Legt den Zoomfaktor fest wenn GifSizeMode auf Zoom festgelegt ist.")>
        Public Property ZoomFactor As Decimal
            Get
                Return _ZoomFactor
            End Get
            Set(value As Decimal)
                SetZoomFactor(value)
            End Set
        End Property

#End Region

#Region "ausgeblendete Eigenschaften"

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property MaximumSize As Size
            Get
                Return MyBase.MaximumSize
            End Get
            Set(value As Size)
                MyBase.MaximumSize = value
            End Set
        End Property

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property MinimumSize As Size
            Get
                Return MyBase.MinimumSize
            End Get
            Set(value As Size)
                MyBase.MinimumSize = value
            End Set
        End Property

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overloads Property Padding As Padding
            Get
                Return MyBase.Padding
            End Get
            Set(value As Padding)
                MyBase.Padding = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property RightToLeft() As RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property AllowDrop() As Boolean
            Get
                Return MyBase.AllowDrop
            End Get
            Set(value As Boolean)
                MyBase.AllowDrop = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property AutoScrollOffset As Point
            Get
                Return MyBase.AutoScrollOffset
            End Get
            Set(value As Point)
                MyBase.AutoScrollOffset = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property AutoSize As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage() As Image
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
        Public Overrides Property BackgroundImageLayout() As ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ContextMenuStrip() As ContextMenuStrip
            Get
                Return MyBase.ContextMenuStrip
            End Get
            Set(value As ContextMenuStrip)
                MyBase.ContextMenuStrip = value
            End Set
        End Property

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Dock() As DockStyle
            Get
                Return MyBase.Dock
            End Get
            Set(value As DockStyle)
                MyBase.Dock = value
            End Set
        End Property

        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Font() As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

#End Region

        Public Sub New()
            InitializeComponent()
            InitializeValues() 'Standardwerte laden
        End Sub

        ''' <summary>
        ''' Initialisiert die Komponenten des Steuerelements.
        ''' </summary>
        ''' <remarks>
        ''' Designer-generierter Code, erweitert um Dokumentationskommentar.
        ''' </remarks>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.Timer = New System.Windows.Forms.Timer(Me.components)
            Me.SuspendLayout()
            '
            'Timer
            '
            Me.Timer.Interval = 200
            '
            'AniGif
            '
            Me.DoubleBuffered = True
            Me.Name = "AniGif"
            Me.ResumeLayout(False)
        End Sub

        ''' <summary>
        ''' Führt Layout-Initialisierung durch und startet ggf. die GIF-Animation.
        ''' </summary>
        ''' <remarks>
        ''' Stoppt vorher eine bestehende Animation um Mehrfach-Registrierungen zu vermeiden.
        ''' </remarks>
        Protected Overloads Overrides Sub InitLayout()
            MyBase.InitLayout()
            ' Alte Animation stoppen um Mehrfach-Registrierungen zu vermeiden
            If _Gif IsNot Nothing Then
                ImageAnimator.StopAnimate(_Gif, _animationHandler)
            End If
            ' Nur animieren wenn AutoPlay aktiv ist
            If Not DesignMode AndAlso _Autoplay AndAlso _Gif IsNot Nothing AndAlso ImageAnimator.CanAnimate(_Gif) Then
                ImageAnimator.Animate(_Gif, _animationHandler)
            End If
        End Sub

        ''' <summary>
        ''' Zeichnet das aktuelle Frame des GIFs unter Berücksichtigung der Skalierung.
        ''' </summary>
        ''' <param name="e">Zeicheninformationen.</param>
        ''' <remarks>
        ''' Aktualisiert die Animation bei automatischer Geschwindigkeitssteuerung.
        ''' </remarks>
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            If _Gif Is Nothing Then Return ' Null-Schutz

            Dim g As Graphics = e.Graphics ' Variable für Zeichenfläche
            Dim rectstartsize As Size = GetRectStartSize(_GifSizeMode, Me, _Gif, _ZoomFactor / 100) ' Größe der Zeichenfläche berechnen
            Dim rectstartpoint As Point = GetRectStartPoint(_GifSizeMode, Me, _Gif, rectstartsize) 'Startpunkt der Zeichenfläche berechnen

            ' Qualitätsverbesserung nur bei Skalierung
            If _GifSizeMode = SizeMode.Zoom OrElse _GifSizeMode = SizeMode.Fill Then
                g.InterpolationMode = InterpolationMode.HighQualityBicubic
                g.PixelOffsetMode = PixelOffsetMode.HighQuality
                g.SmoothingMode = SmoothingMode.HighQuality
            End If

            g.DrawImage(_Gif, New Rectangle(rectstartpoint, rectstartsize)) ' Zeichenfläche festlegen und Bild zeichnen
            If Not DesignMode And _Autoplay And Not _CustomDisplaySpeed Then ' Bild animieren wenn AutoPlay aktiv und Benutzerdefinierte Geschwindigkeit deaktiviert
                ImageAnimator.UpdateFrames() ' im Bild gespeicherte Geschwindigkeit verwenden
            End If
        End Sub

        ''' <summary>
        ''' Gibt Ressourcen frei und stoppt ggf. laufende Animationen.
        ''' </summary>
        ''' <param name="disposing">True um verwaltete Ressourcen freizugeben.</param>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' Animation stoppen bevor Bild entsorgt wird
                    If _Gif IsNot Nothing Then
                        ImageAnimator.StopAnimate(_Gif, _animationHandler)
                    End If
                    components?.Dispose()
                    Timer?.Dispose()
                    _Gif?.Dispose()
                End If
                disposedValue = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        ''' <summary>
        ''' IDisposable Support
        ''' </summary>
        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

#Region "interne Ereignisbehandlungen"

        ''' <summary>
        ''' Reagiert auf den Wechsel des GIF-Bildes und initialisiert Animationsparameter.
        ''' </summary>
        Private Sub AniGif_GifChange() Handles Me.GifChanged
            If ImageAnimator.CanAnimate(_Gif) = False And _Autoplay = True Then 'überprüfen ob das Bild animiert werden kann wenn Autoplay auf True gesetzt ist
                Timer.Stop() 'Timer stoppen und Anzahl der Frames auf 0 setzen (für nicht animiertes bild)
                _MaxFrame = 0
                RaiseEvent NoAnimation(Me, EventArgs.Empty) ' Ereignis auslösen
            Else 'Werte für Benutzerdefinierte Geschwindigkeit speichern
                _Dimension = New Imaging.FrameDimension(_Gif.FrameDimensionsList(0))
                _MaxFrame = _Gif.GetFrameCount(_Dimension) - 1
                _Frame = 0
                If _CustomDisplaySpeed Then
                    Timer.Interval = CInt(1000 / _FramesPerSecond) ' Intervall sofort setzen
                    Timer.Start() ' Timer starten
                End If
            End If
            Invalidate() ' neu zeichnen
            InitLayout() ' Animation starten
        End Sub

        ''' <summary>
        ''' Aktiviert oder deaktiviert die benutzerdefinierte Animationsgeschwindigkeit.
        ''' </summary>
        Private Sub AniGif_CustomDisplaySpeedChanged() Handles Me.CustomDisplaySpeedChanged
            ' Intervall direkt setzen und Timer entsprechend Zustand starten/stoppen
            If _CustomDisplaySpeed Then
                Timer.Interval = CInt(1000 / _FramesPerSecond)
                Timer.Start()
            Else
                Timer.Stop()
            End If
        End Sub

        ''' <summary>
        ''' Reagiert auf Änderung der Frames-pro-Sekunde Einstellung.
        ''' </summary>
        Private Sub AniGif_FramesPerSecondChanged() Handles Me.FramesPerSecondChanged
            If _FramesPerSecond < 1D Then _FramesPerSecond = 1D ' Sicherheitsprüfung
            If Timer.Enabled Then
                Timer.Stop() ' Timer stoppen um die Intervalle zu aktualisieren
                Timer.Interval = CInt(1000 / _FramesPerSecond)
                Timer.Start() ' Timer neu starten
            Else
                Timer.Interval = CInt(1000 / _FramesPerSecond) ' Nur das Intervall aktualisieren wenn der Timer nicht läuft
            End If
        End Sub

        ''' <summary>
        ''' Wird vom ImageAnimator bei jedem anstehenden Frame aufgerufen.
        ''' </summary>
        ''' <param name="o">Auslösendes Objekt.</param>
        ''' <param name="e">Ereignisdaten.</param>
        Private Sub OnNextFrame(o As Object, e As EventArgs)
            If AutoPlay AndAlso Not DesignMode Then
                Invalidate() 'neu zeichnen
            End If
        End Sub

        ''' <summary>
        ''' Timer-Tick zur manuellen Frame-Steuerung bei benutzerdefinierter Geschwindigkeit.
        ''' </summary>
        ''' <param name="sender">Timer.</param>
        ''' <param name="e">Ereignisdaten.</param>
        Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
            'Bild animieren wenn AutoPlay und Benutzerdefinierte Geschwindigkeit aktiv
            If Not DesignMode AndAlso AutoPlay Then
                If _MaxFrame = 0 Then Exit Sub ' wenn Frames = 0 ist das Bild nicht animiert -> Ende
                If _Frame > _MaxFrame Then _Frame = 0 ' Bildzähler zurücksetzen wenn maximale Anzahl überschritten
                Dim unused = _Gif.SelectActiveFrame(_Dimension, _Frame) ' nächstes Bild auswählen
                _Frame += 1 ' Bildzähler weiterschalten
                Invalidate() ' neu zeichnen
            End If
        End Sub

#End Region

#Region "Interne Hilfsfunktionen"

        ''' <summary>
        ''' Setzt die Standardwerte für die wichtigsten Variablen der Ani Gif Control.
        ''' </summary>
        Private Sub InitializeValues()
            _Gif = My.Resources.Standard ' Standard-GIF aus den Ressourcen laden
            _Autoplay = False                   ' Animation startet nicht automatisch
            _GifSizeMode = SizeMode.Normal      ' GIF wird in Originalgröße angezeigt
            _CustomDisplaySpeed = False         ' Keine benutzerdefinierte Geschwindigkeit
            _FramesPerSecond = 10               ' Standard: 10 Bilder pro Sekunde
            _ZoomFactor = 50                    ' Standard-Zoomfaktor: 50%
        End Sub

        ''' <summary>
        ''' Setzt eine neue Animation.
        ''' </summary>
        ''' <param name="value">Neues GIF-Bitmap oder Nothing für Standard.</param>
        Private Sub SetGifImage(value As Bitmap)
            ' Vorherige Animation stoppen bevor Bild entsorgt wird
            If _Gif IsNot Nothing Then
                ImageAnimator.StopAnimate(_Gif, _animationHandler)
                _Gif.Dispose() ' Vorhandenes Bild freigeben
            End If
            _Gif = If(value, My.Resources.Standard) 'Standardanimation verwenden wenn keine Auswahl erfolgte
            RaiseEvent GifChanged()
        End Sub

        ''' <summary>
        ''' Setzt die Anzeigeart der Animation.
        ''' </summary>
        ''' <param name="value">Neuer SizeMode.</param>
        Private Sub SetGifSizeMode(value As SizeMode)
            _GifSizeMode = value
            Invalidate()
        End Sub

        ''' <summary>
        ''' Setzt die benutzerdefinierte Anzeigegeschwindigkeit (aktiv/inaktiv).
        ''' </summary>
        ''' <param name="value">True zur Aktivierung.</param>
        Private Sub SetCustomDisplaySpeed(value As Boolean)
            _CustomDisplaySpeed = value
            RaiseEvent CustomDisplaySpeedChanged()
        End Sub

        ''' <summary>
        ''' Setzt den Zoomfaktor (wird geprüft und begrenzt).
        ''' </summary>
        ''' <param name="value">Neuer Zoomfaktor (1-100).</param>
        Private Sub SetZoomFactor(value As Decimal)
            _ZoomFactor = CheckZoomFactorValue(value)
            Invalidate() 'neu zeichnen
        End Sub

        ''' <summary>
        ''' Prüft und korrigiert den FPS-Wert in den Bereich 1..50.
        ''' </summary>
        ''' <param name="Frames">Angeforderte Frames pro Sekunde.</param>
        ''' <returns>Korrigierter Wert.</returns>
        Private Function CheckFramesPerSecondValue(Frames As Decimal) As Decimal
            ' Überprüft, ob der FPS-Wert im zulässigen Bereich liegt (1 bis 50)
            Select Case Frames
                Case Is < 1 : Return 1' Wenn der Wert kleiner als 1 ist, auf Mindestwert 1 setzen
                Case Is > 50 : Return 50 ' Wenn der Wert größer als 50 ist, auf Höchstwert 50 setzen
                Case Else : Return Frames ' Ansonsten den übergebenen Wert zurückgeben
            End Select
        End Function

        ''' <summary>
        ''' Prüft, ob der übergebene Zoomfaktor im gültigen Bereich (1-100) liegt.
        ''' </summary>
        ''' <param name="ZoomFactor">Angeforderter Zoomfaktor.</param>
        ''' <returns>Gibt den korrigierten Zoomfaktor im Bereich 1 bis 100 zurück.</returns>
        Private Function CheckZoomFactorValue(ZoomFactor As Decimal) As Decimal
            Select Case ZoomFactor
                Case Is < 1 : Return 1' Wenn der Zoomfaktor kleiner als 1 ist, auf Mindestwert 1 setzen
                Case Is > 100 : Return 100 ' Wenn der Zoomfaktor größer als 100 ist, auf Höchstwert 100 setzen
                Case Else : Return ZoomFactor ' Ansonsten den übergebenen Wert zurückgeben
            End Select
        End Function

        ''' <summary>
        ''' Berechnet die Zielgröße des zu zeichnenden Bildes abhängig vom Modus.
        ''' </summary>
        ''' <param name="Mode">Aktueller SizeMode.</param>
        ''' <param name="Control">Referenz auf das Control.</param>
        ''' <param name="Gif">Aktuelles GIF.</param>
        ''' <param name="Zoom">Zoomfaktor (0-1) bei Zoom-Modus.</param>
        ''' <returns>Berechnete Größe.</returns>
        Private Function GetRectStartSize(Mode As SizeMode, Control As AniGif, Gif As Bitmap, Zoom As Decimal) As Size
            If Gif Is Nothing Then Return Size.Empty ' Null-Schutz
            Select Case Mode
                Case SizeMode.Normal
                    ' Bild wird in Originalgröße angezeigt (keine Skalierung)
                    Return New Size(Gif.Size.Width, Gif.Size.Height)
                Case SizeMode.CenterImage
                    ' Bild wird ebenfalls in Originalgröße angezeigt (zentriert, aber Größe bleibt gleich)
                    Return New Size(Gif.Size.Width, Gif.Size.Height)
                Case SizeMode.Zoom
                    ' Bild wird proportional zum Zoomfaktor skaliert
                    If Gif.Size.Width < Gif.Size.Height Then
                        Return New Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width) * Zoom), CInt(Control.Height * Zoom))
                    Else
                        ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen und mit Zoom multiplizieren
                        Return New Size(CInt(Control.Width * Zoom), CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width) * Zoom))
                    End If
                Case SizeMode.Fill
                    ' Bild wird so skaliert, dass es das Control vollständig ausfüllt (Seitenverhältnis bleibt erhalten)
                    If Gif.Size.Width < Gif.Size.Height Then
                        ' Bild ist höher als breit
                        ' Höhe des Controls als Basis, Breite proportional berechnen
                        Return New Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width)), Control.Height)
                    Else
                        ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen
                        Return New Size(Control.Width, CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width)))
                    End If
                Case Else
                    Return New Size(Gif.Size.Width, Gif.Size.Height)
            End Select
        End Function

        ''' <summary>
        ''' Berechnet den Startpunkt (linke obere Ecke) für das Zeichnen des Bildes.
        ''' </summary>
        ''' <param name="Mode">Aktueller SizeMode.</param>
        ''' <param name="Control">Referenz auf das Control.</param>
        ''' <param name="Gif">Aktuelles GIF.</param>
        ''' <param name="RectStartSize">Berechnete Zielgröße.</param>
        ''' <returns>Zeichenursprung.</returns>
        Private Function GetRectStartPoint(Mode As SizeMode, Control As AniGif, Gif As Bitmap, RectStartSize As Size) As Point
            ' Bestimmt den Startpunkt (linke obere Ecke) für das Zeichnen des Bildes
            Select Case Mode
                Case SizeMode.Normal
                    ' Bild wird in Originalgröße oben links gezeichnet
                    Return New Point(0, 0)
                Case SizeMode.CenterImage
                    ' Bild wird in Originalgröße zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und Bild-Höhe
                    Return New Point(CInt((Control.Width - Gif.Size.Width) / 2), CInt((Control.Height - Gif.Size.Height) / 2))
                Case SizeMode.Zoom
                    ' Bild wird skaliert (gezoomt) und zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und skalierter Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und skalierter Bild-Höhe
                    Return New Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
                Case SizeMode.Fill
                    ' Bild wird so skaliert, dass es das Control ausfüllt und zentriert gezeichnet
                    ' X- und Y-Position wie bei Zoom
                    Return New Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
                Case Else
                    Return New Point(0, 0) ' Fallback
            End Select
        End Function

#End Region

        ''' <summary>
        ''' Startet die Animation (falls noch nicht aktiv).
        ''' </summary>
        Public Sub StartAnimation()
            If Not _Autoplay Then
                _Autoplay = True
                InitLayout()
            End If
        End Sub

        ''' <summary>
        ''' Stoppt die Animation und beendet Timer sowie ImageAnimator.
        ''' </summary>
        Public Sub StopAnimation()
            If _Autoplay Then
                _Autoplay = False
                If _Gif IsNot Nothing Then ImageAnimator.StopAnimate(_Gif, _animationHandler)
                Timer.Stop()
            End If
        End Sub

    End Class

End Namespace
