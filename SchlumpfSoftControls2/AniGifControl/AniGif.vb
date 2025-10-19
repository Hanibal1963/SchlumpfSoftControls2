' *************************************************************************************************
' AniGif.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace AniGifControl

    ''' <summary>
    ''' Control zum anzeigen von animierten Grafiken.
    ''' </summary>
    <ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum Anzeigen von animierten Grafiken.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(AniGifControl.AniGif), "AniGifControl.AniGif.bmp")>
    Public Class AniGif

        Inherits System.Windows.Forms.UserControl

        Implements System.IDisposable

        Private WithEvents Timer As System.Windows.Forms.Timer
        Private components As System.ComponentModel.IContainer
        Private disposedValue As Boolean
        Private ReadOnly _AnimationHandler As System.EventHandler = AddressOf OnNextFrame ' Gemeinsamer Handler für ImageAnimator zum Stoppen/Neu-Registrieren

#Region "Ereignisdefinitionen"

        ''' <summary>
        ''' Wird ausgelöst wenn die Grafik nicht animiert werden kann.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Wird ausgelöst wenn die Grafik nicht animiert werden kann.")>
        Public Event NoAnimation(sender As Object, e As System.EventArgs)

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
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt fest ob die Animation sofort nach dem laden gestartet wird.")>
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
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die animierte Gif-Grafik zurück oder legt diese fest.")>
        Public Property Gif() As System.Drawing.Bitmap
            Get
                Return _Gif
            End Get
            Set(value As System.Drawing.Bitmap)
                SetGifImage(value)
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Anzeigemodus (Skalierung/Ausrichtung) der GIF-Grafik zurück oder legt
        ''' ihn fest.
        ''' </summary>
        ''' <remarks>
        ''' <b>Verhalten je Modus:</b> <para>- <see cref="SizeMode.Normal"/>: Das Bild wird
        ''' unverändert an Position (0,0) gezeichnet. Ist das Bild größer als das Control,
        ''' wird es abgeschnitten.</para>
        ''' <para>- <see cref="SizeMode.CenterImage"/>: Das Bild wird unverändert zentriert
        ''' gezeichnet. Es kann abgeschnitten werden, wenn es größer als das Control
        ''' ist.</para>
        ''' <para>- <see cref="SizeMode.Zoom"/>: Das Bild wird proportional skaliert und
        ''' zentriert. Die Skalierung richtet sich nach <see cref="ZoomFactor"/> (1–100%).
        ''' Die Berechnung orientiert sich an der Controlgröße, das Seitenverhältnis bleibt
        ''' erhalten.</para>
        ''' <para>- <see cref="SizeMode.Fill"/>: Das Bild wird proportional so skaliert,
        ''' dass das Control vollständig gefüllt wird. Dadurch kann das Bild an einer Seite
        ''' zugeschnitten werden; es wird zentriert gezeichnet.</para>
        ''' <b> Rendering:</b> <para>Für <see cref="SizeMode.Zoom"/> und <see
        ''' cref="SizeMode.Fill"/> werden hochwertige Interpolations- und
        ''' Glättungseinstellungen (HighQualityBicubic, HighQuality) verwendet, um die
        ''' Bildqualität bei der Skalierung zu verbessern.</para>
        '''  Ablauf/Seiteneffekte: <para>Beim Setzen dieser Eigenschaft wird das Control
        ''' über <see cref="System.Windows.Forms.Control.Invalidate"/> neu gezeichnet. Die
        ''' Bilddaten werden nicht verändert; nur die Darstellung ändert sich. Es wird kein
        ''' eigener Ereignis-Callback ausgelöst.</para>
        ''' <b> Hinweise:</b> <para>- Der Standardwert wird in <c>InitializeValues</c> gesetzt.</para>
        ''' <para>- <see cref="ZoomFactor"/> wirkt ausschließlich, wenn der Modus <see
        ''' cref="SizeMode.Zoom"/> aktiv ist.</para>
        ''' <para>- Die Eigenschaft ist im Designer sichtbar (Kategorie "Behavior").</para>
        ''' </remarks>
        ''' <value>
        ''' Einer der <see cref="SizeMode"/>-Werte: <list type="bullet">
        '''  <item>
        '''   <description><see cref="SizeMode.Normal"/>: Bild in Originalgröße, oben
        ''' links.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="SizeMode.CenterImage"/>: Bild in Originalgröße,
        ''' zentriert.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="SizeMode.Zoom"/>: Proportionale Skalierung nach <see
        ''' cref="ZoomFactor"/>, zentriert.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="SizeMode.Fill"/>: Proportionale Skalierung, Control
        ''' wird vollständig gefüllt (ggf. Zuschnitt), zentriert.</description>
        '''  </item>
        ''' </list>
        '''  Standardwert ist <see cref="SizeMode.Normal"/>.
        ''' </value>
        ''' <example>
        ''' Beispiel: <code language="vb"><![CDATA[
        '''  ' Bild proportional zoomen und auf 75% skalieren
        '''  AniGif1.GifSizeMode = SizeMode.Zoom
        '''  AniGif1.ZoomFactor = 75D
        '''  
        '''  ' Bild zentriert in Originalgröße anzeigen
        '''  AniGif1.GifSizeMode = SizeMode.CenterImage
        '''  
        '''  ' Bild das Control ausfüllen lassen (möglicher Zuschnitt)
        '''  AniGif1.GifSizeMode = SizeMode.Fill
        '''  ]]></code>
        ''' </example>
        ''' <seealso cref="ZoomFactor"/>
        ''' <seealso cref="Gif"/>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.")>
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
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt fest ob die benutzerdefinierte Anzeigegeschwindigkeit oder die in der Datei festgelegte Geschwindigkeit benutzt wird.")>
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
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt die benutzerdefinierte Anzeigegeschwindigkeit in Bildern/Sekunde fest wenn CustomDisplaySpeed auf True festgelegt ist.")>
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
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt den Zoomfaktor fest wenn GifSizeMode auf Zoom festgelegt ist.")>
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

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property MaximumSize As System.Drawing.Size
            Get
                Return MyBase.MaximumSize
            End Get
            Set(value As System.Drawing.Size)
                MyBase.MaximumSize = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property MinimumSize As System.Drawing.Size
            Get
                Return MyBase.MinimumSize
            End Get
            Set(value As System.Drawing.Size)
                MyBase.MinimumSize = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overloads Property Padding As System.Windows.Forms.Padding
            Get
                Return MyBase.Padding
            End Get
            Set(value As System.Windows.Forms.Padding)
                MyBase.Padding = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property RightToLeft() As System.Windows.Forms.RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As System.Windows.Forms.RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
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
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
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
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property AutoScrollOffset As System.Drawing.Point
            Get
                Return MyBase.AutoScrollOffset
            End Get
            Set(value As System.Drawing.Point)
                MyBase.AutoScrollOffset = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
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
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage() As System.Drawing.Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As System.Drawing.Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImageLayout() As System.Windows.Forms.ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As System.Windows.Forms.ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property ContextMenuStrip() As System.Windows.Forms.ContextMenuStrip
            Get
                Return MyBase.ContextMenuStrip
            End Get
            Set(value As System.Windows.Forms.ContextMenuStrip)
                MyBase.ContextMenuStrip = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Dock() As System.Windows.Forms.DockStyle
            Get
                Return MyBase.Dock
            End Get
            Set(value As System.Windows.Forms.DockStyle)
                MyBase.Dock = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Font() As System.Drawing.Font
            Get
                Return MyBase.Font
            End Get
            Set(value As System.Drawing.Font)
                MyBase.Font = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property ForeColor() As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.ForeColor = value
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see
        ''' cref="SchlumpfSoft.Controls.AniGifControl.AniGif"/>.
        ''' </summary>
        Public Sub New()
            InitializeComponent()
            InitializeValues() 'Standardwerte laden
        End Sub

        ''' <summary>
        ''' Initialisiert die Komponenten des Steuerelements.
        ''' </summary>
        ''' <remarks>
        ''' Designer-generierter Code.
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
                System.Drawing.ImageAnimator.StopAnimate(_Gif, _AnimationHandler)
            End If
            ' Nur animieren wenn AutoPlay aktiv ist
            If Not DesignMode AndAlso _Autoplay AndAlso _Gif IsNot Nothing AndAlso System.Drawing.ImageAnimator.CanAnimate(_Gif) Then
                System.Drawing.ImageAnimator.Animate(_Gif, _AnimationHandler)
            End If
        End Sub

        ''' <summary>
        ''' Zeichnet das aktuelle Frame des GIFs unter Berücksichtigung der Skalierung.
        ''' </summary>
        ''' <param name="e">Zeicheninformationen.</param>
        ''' <remarks>
        ''' Aktualisiert die Animation bei automatischer Geschwindigkeitssteuerung.
        ''' </remarks>
        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            If _Gif Is Nothing Then Return ' Null-Schutz

            Dim g As System.Drawing.Graphics = e.Graphics ' Variable für Zeichenfläche
            Dim rectstartsize As System.Drawing.Size = GetRectStartSize(_GifSizeMode, Me, _Gif, _ZoomFactor / 100) ' Größe der Zeichenfläche berechnen
            Dim rectstartpoint As System.Drawing.Point = GetRectStartPoint(_GifSizeMode, Me, _Gif, rectstartsize) 'Startpunkt der Zeichenfläche berechnen

            ' Qualitätsverbesserung nur bei Skalierung
            If _GifSizeMode = SizeMode.Zoom OrElse _GifSizeMode = SizeMode.Fill Then
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            End If

            g.DrawImage(_Gif, New System.Drawing.Rectangle(rectstartpoint, rectstartsize)) ' Zeichenfläche festlegen und Bild zeichnen
            If Not DesignMode And _Autoplay And Not _CustomDisplaySpeed Then ' Bild animieren wenn AutoPlay aktiv und Benutzerdefinierte Geschwindigkeit deaktiviert
                System.Drawing.ImageAnimator.UpdateFrames() ' im Bild gespeicherte Geschwindigkeit verwenden
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
                        System.Drawing.ImageAnimator.StopAnimate(_Gif, _AnimationHandler)
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
        Public Overloads Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

#Region "interne Ereignisbehandlungen"

        ''' <summary>
        ''' Reagiert auf den Wechsel des GIF-Bildes und initialisiert Animationsparameter.
        ''' </summary>
        Private Sub AniGif_GifChange() Handles Me.GifChanged
            If System.Drawing.ImageAnimator.CanAnimate(_Gif) = False And _Autoplay = True Then 'überprüfen ob das Bild animiert werden kann wenn Autoplay auf True gesetzt ist
                Timer.Stop() 'Timer stoppen und Anzahl der Frames auf 0 setzen (für nicht animiertes bild)
                _MaxFrame = 0
                RaiseEvent NoAnimation(Me, System.EventArgs.Empty) ' Ereignis auslösen
            Else 'Werte für Benutzerdefinierte Geschwindigkeit speichern
                _Dimension = New System.Drawing.Imaging.FrameDimension(_Gif.FrameDimensionsList(0))
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
        Private Sub OnNextFrame(o As Object, e As System.EventArgs)
            If AutoPlay AndAlso Not DesignMode Then
                Invalidate() 'neu zeichnen
            End If
        End Sub

        ''' <summary>
        ''' Timer-Tick zur manuellen Frame-Steuerung bei benutzerdefinierter Geschwindigkeit.
        ''' </summary>
        ''' <param name="sender">Timer.</param>
        ''' <param name="e">Ereignisdaten.</param>
        Private Sub Timer_Tick(sender As Object, e As System.EventArgs) Handles Timer.Tick
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
        Private Sub SetGifImage(value As System.Drawing.Bitmap)
            ' Vorherige Animation stoppen bevor Bild entsorgt wird
            If _Gif IsNot Nothing Then
                System.Drawing.ImageAnimator.StopAnimate(_Gif, _AnimationHandler)
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
        Private Function GetRectStartSize(Mode As SizeMode, Control As AniGif, Gif As System.Drawing.Bitmap, Zoom As Decimal) As System.Drawing.Size
            If Gif Is Nothing Then Return System.Drawing.Size.Empty ' Null-Schutz
            Select Case Mode
                Case SizeMode.Normal
                    ' Bild wird in Originalgröße angezeigt (keine Skalierung)
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
                Case SizeMode.CenterImage
                    ' Bild wird ebenfalls in Originalgröße angezeigt (zentriert, aber Größe bleibt gleich)
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
                Case SizeMode.Zoom
                    ' Bild wird proportional zum Zoomfaktor skaliert
                    If Gif.Size.Width < Gif.Size.Height Then
                        Return New System.Drawing.Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width) * Zoom), CInt(Control.Height * Zoom))
                    Else
                        ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen und mit Zoom multiplizieren
                        Return New System.Drawing.Size(CInt(Control.Width * Zoom), CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width) * Zoom))
                    End If
                Case SizeMode.Fill
                    ' Bild wird so skaliert, dass es das Control vollständig ausfüllt (Seitenverhältnis bleibt erhalten)
                    If Gif.Size.Width < Gif.Size.Height Then
                        ' Bild ist höher als breit
                        ' Höhe des Controls als Basis, Breite proportional berechnen
                        Return New System.Drawing.Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width)), Control.Height)
                    Else
                        ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen
                        Return New System.Drawing.Size(Control.Width, CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width)))
                    End If
                Case Else
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
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
        Private Function GetRectStartPoint(Mode As SizeMode, Control As AniGif, Gif As System.Drawing.Bitmap, RectStartSize As System.Drawing.Size) As System.Drawing.Point
            ' Bestimmt den Startpunkt (linke obere Ecke) für das Zeichnen des Bildes
            Select Case Mode
                Case SizeMode.Normal
                    ' Bild wird in Originalgröße oben links gezeichnet
                    Return New System.Drawing.Point(0, 0)
                Case SizeMode.CenterImage
                    ' Bild wird in Originalgröße zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und Bild-Höhe
                    Return New System.Drawing.Point(CInt((Control.Width - Gif.Size.Width) / 2), CInt((Control.Height - Gif.Size.Height) / 2))
                Case SizeMode.Zoom
                    ' Bild wird skaliert (gezoomt) und zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und skalierter Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und skalierter Bild-Höhe
                    Return New System.Drawing.Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
                Case SizeMode.Fill
                    ' Bild wird so skaliert, dass es das Control ausfüllt und zentriert gezeichnet
                    ' X- und Y-Position wie bei Zoom
                    Return New System.Drawing.Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
                Case Else
                    Return New System.Drawing.Point(0, 0) ' Fallback
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
                If _Gif IsNot Nothing Then System.Drawing.ImageAnimator.StopAnimate(_Gif, _AnimationHandler)
                Timer.Stop()
            End If
        End Sub

    End Class

End Namespace
