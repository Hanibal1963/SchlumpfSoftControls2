' *************************************************************************************************
' 
' AniGif.vb
' Copyright (c)2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' Control zum Anzeigen von animierten Grafiken.
'
' weitere Infos:
'
' <Browsable> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.browsableattribute?view=netframework-4.7.2
' <Category> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.categoryattribute?view=netframework-4.7.2
' <Description> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.descriptionattribute?view=netframework-4.7.2
'
' *************************************************************************************************

Namespace AniGifControl

    ''' <summary>
    ''' Control zum anzeigen von animierten Grafiken.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls 2", False)>
    <System.ComponentModel.Description("Control zum Anzeigen von animierten Grafiken.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(AniGif), "AniGif.bmp")>
    Public Class AniGif

        Inherits System.Windows.Forms.UserControl

        Implements System.IDisposable

        Private WithEvents Timer As System.Windows.Forms.Timer
        Private components As System.ComponentModel.IContainer

        Private _Gif As System.Drawing.Bitmap ' Das aktuell geladene GIF-Bild
        Private _GifSizeMode As SizeMode ' Gibt an, wie das GIF im Steuerelement skaliert/angezeigt wird (z.B. gestreckt, zentriert)
        Private _CustomDisplaySpeed As Boolean ' Bestimmt, ob eine benutzerdefinierte Anzeigegeschwindigkeit verwendet wird
        Private _FramesPerSecond As Decimal ' Bildwiederholrate (Frames pro Sekunde) für die Animation
        Private _Dimension As System.Drawing.Imaging.FrameDimension ' Die Dimension (z.B. Zeit) der animierten Frames im GIF
        Private _Frame As Integer ' Der aktuelle Frame-Index, der angezeigt wird
        Private _MaxFrame As Integer ' Die maximale Anzahl der Frames im GIF
        Private _Autoplay As Boolean ' Gibt an, ob die Animation automatisch abgespielt wird
        Private _ZoomFactor As Decimal ' Zoomfaktor für die Anzeige des GIFs

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
                _Autoplay = value
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
                _Gif = If(value, My.Resources.AniGif_Standard) 'Standardanimation verwenden wenn keine Auswahl erfolgte
                RaiseEvent GifChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.")>
        Public Property GifSizeMode() As SizeMode
            Get
                Return _GifSizeMode
            End Get
            Set(value As SizeMode)
                _GifSizeMode = value
                Invalidate()
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
                _CustomDisplaySpeed = value
                RaiseEvent CustomDisplaySpeedChanged()
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
                Timer.Interval = CInt(1000 / _FramesPerSecond)
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
                _ZoomFactor = CheckZoomFactorValue(value)
                Invalidate() 'neu zeichnen
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

        Public Sub New()
            InitializeComponent()
            InitializeValues() 'Standardwerte laden
        End Sub

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AniGif))
            Me.Timer = New System.Windows.Forms.Timer(Me.components)
            Me.SuspendLayout()
            '
            'Timer
            '
            Me.Timer.Interval = 200
            '
            'AniGif
            '
            resources.ApplyResources(Me, "$this")
            Me.Name = "AniGif"
            Me.ResumeLayout(False)

        End Sub

        Protected Overloads Overrides Sub InitLayout()
            MyBase.InitLayout()
            ' Animation starten wenn nicht im Desigmodus und AutoPlay auf True
            If Not DesignMode And System.Drawing.ImageAnimator.CanAnimate(_Gif) Then
                System.Drawing.ImageAnimator.Animate(_Gif, AddressOf OnNextFrame)
            End If
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            ' Variable für Zeichenfläche
            Dim g As System.Drawing.Graphics = e.Graphics
            ' Größe der Zeichenfläche berechnen
            Dim rectstartsize As System.Drawing.Size = GetRectStartSize(_GifSizeMode, Me, _Gif, _ZoomFactor / 100)
            'Startpunkt der Zeichenfläche berechnen
            Dim rectstartpoint As System.Drawing.Point = GetRectStartPoint(_GifSizeMode, Me, _Gif, rectstartsize)
            ' Zeichenfläche festlegen und Bild zeichnen
            g.DrawImage(_Gif, New System.Drawing.Rectangle(rectstartpoint, rectstartsize))
            ' Bild animieren wenn AutoPlay aktiv und Benutzerdefinierte Geschwindigkeit deaktiviert
            If Not DesignMode And _Autoplay And Not _CustomDisplaySpeed Then
                ' im Bild gespeicherte Geschwindigkeit verwenden
                System.Drawing.ImageAnimator.UpdateFrames()
            End If
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If Timer IsNot Nothing Then
                    Timer.Dispose()
                End If
                If _Gif IsNot Nothing Then
                    _Gif.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "interne Ereignisbehandlungen"

        ' Wird ausgeführt wenn das Bild gewechselt wurde.
        Private Sub AniGif_GifChange() Handles Me.GifChanged
            'überprüfen ob das Bild animiert werden kann wenn Autoplay auf True gesetzt ist
            If System.Drawing.ImageAnimator.CanAnimate(_Gif) = False And _Autoplay = True Then
                'Timer stoppen und Anzahl der Frames auf 0 setzen (für nicht animiertes bild)
                Timer.Stop()
                _MaxFrame = 0
                RaiseEvent NoAnimation(Me, System.EventArgs.Empty) ' Ereignis auslösen
            Else
                'Werte für Benutzerdefinierte Geschwindigkeit speichern
                _Dimension = New System.Drawing.Imaging.FrameDimension(_Gif.FrameDimensionsList(0))
                _MaxFrame = _Gif.GetFrameCount(_Dimension) - 1
                _Frame = 0
                If _CustomDisplaySpeed Then Timer.Start() ' Timer starten
            End If
            Invalidate() ' neu zeichnen
            InitLayout() ' Animation starten
        End Sub

        ' Wird ausgeführt wenn die benutzerdefinierte Anzeigegeschwindigkeit ein oder ausgeschaltet wurde
        Private Sub AniGif_CustomDisplaySpeedChanged() Handles Me.CustomDisplaySpeedChanged
            Timer.Enabled = _CustomDisplaySpeed
            If Timer.Enabled Then
                Timer.Start()
            Else
                Timer.Stop()
            End If
        End Sub

        ' Wird ausgeführt wenn das nächste Teilbild angezeigt werden soll.
        Private Sub OnNextFrame(o As Object, e As System.EventArgs)
            If AutoPlay AndAlso Not DesignMode Then
                Invalidate() 'neu zeichnen
            End If
        End Sub

        ' wird ausgeführt wenn die Anzeigezeit abgelaufen ist.
        Private Sub Tick(sender As Object, e As System.EventArgs) Handles Timer.Tick
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

        ' Setzt die Standardwerte für die wichtigsten Variablen der Ani Gif Control.
        Private Sub InitializeValues()
            _Gif = My.Resources.AniGif_Standard ' Standard-GIF aus den Ressourcen laden
            _Autoplay = False                   ' Animation startet nicht automatisch
            _GifSizeMode = SizeMode.Normal      ' GIF wird in Originalgröße angezeigt
            _CustomDisplaySpeed = False         ' Keine benutzerdefinierte Geschwindigkeit
            _FramesPerSecond = 10               ' Standard: 10 Bilder pro Sekunde
            _ZoomFactor = 50                    ' Standard-Zoomfaktor: 50%

        End Sub

        ' Prüft den Wert für Bilder/Sekunde
        Private Function CheckFramesPerSecondValue(Frames As Decimal) As Decimal
            ' Überprüft, ob der FPS-Wert im zulässigen Bereich liegt (1 bis 50)
            Select Case Frames
                Case Is < 1 ' Wenn der Wert kleiner als 1 ist, auf Mindestwert 1 setzen
                    Return 1
                Case Is > 50 ' Wenn der Wert größer als 50 ist, auf Höchstwert 50 setzen
                    Return 50
                Case Else ' Ansonsten den übergebenen Wert zurückgeben
                    Return Frames
            End Select
        End Function

        ' Prüft, ob der übergebene Zoomfaktor im gültigen Bereich (1-100) liegt.
        ' Werte kleiner als 1 werden auf 1 gesetzt, Werte größer als 100 auf 100.
        ' Gibt den korrigierten Zoomfaktor im Bereich 1 bis 100 zurück.
        Private Function CheckZoomFactorValue(ZoomFactor As Decimal) As Decimal
            Select Case ZoomFactor
                Case Is < 0 ' Wenn der Zoomfaktor kleiner als 0 ist, auf Mindestwert 1 setzen
                    Return 1
                Case Is > 100 ' Wenn der Zoomfaktor größer als 100 ist, auf Höchstwert 100 setzen
                    Return 100
                Case Else ' Ansonsten den übergebenen Wert zurückgeben
                    Return ZoomFactor
            End Select
        End Function

        ' Bildgröße in Abhängikeit vom Zeichenodus berechnen.
        Private Function GetRectStartSize(Mode As SizeMode, Control As AniGif, Gif As System.Drawing.Bitmap, Zoom As Decimal) As System.Drawing.Size
            Select Case Mode
                Case SizeMode.Normal ' Bild wird in Originalgröße angezeigt (keine Skalierung)
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
                Case SizeMode.CenterImage ' Bild wird ebenfalls in Originalgröße angezeigt (zentriert, aber Größe bleibt gleich)
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
                Case SizeMode.Zoom ' Bild wird proportional zum Zoomfaktor skaliert
                    If Gif.Size.Width < Gif.Size.Height Then ' Bild ist höher als breit
                        ' Höhe des Controls als Basis, Breite proportional berechnen und mit Zoom multiplizieren
                        Return New System.Drawing.Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width) * Zoom), CInt(Control.Height * Zoom))
                    Else ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen und mit Zoom multiplizieren
                        Return New System.Drawing.Size(CInt(Control.Width * Zoom), CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width) * Zoom))
                    End If
                Case SizeMode.Fill ' Bild wird so skaliert, dass es das Control vollständig ausfüllt (Seitenverhältnis bleibt erhalten)
                    If Gif.Size.Width < Gif.Size.Height Then ' Bild ist höher als breit
                        ' Höhe des Controls als Basis, Breite proportional berechnen
                        Return New System.Drawing.Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width)), Control.Height)
                    Else ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen
                        Return New System.Drawing.Size(Control.Width, CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width)))
                    End If
            End Select
        End Function

        ' Startpunkt der Zeichenfläche in Abhängikeit vom Zeichenodus berechnen.
        Private Function GetRectStartPoint(Mode As SizeMode, Control As AniGif, Gif As System.Drawing.Bitmap, RectStartSize As System.Drawing.Size) As System.Drawing.Point
            ' Bestimmt den Startpunkt (linke obere Ecke) für das Zeichnen des Bildes
            Select Case Mode
                Case SizeMode.Normal ' Bild wird in Originalgröße oben links gezeichnet
                    Return New System.Drawing.Point(0, 0)
                Case SizeMode.CenterImage ' Bild wird in Originalgröße zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und Bild-Höhe
                    Return New System.Drawing.Point(CInt((Control.Width - Gif.Size.Width) / 2), CInt((Control.Height - Gif.Size.Height) / 2))
                Case SizeMode.Zoom ' Bild wird skaliert (gezoomt) und zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und skalierter Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und skalierter Bild-Höhe
                    Return New System.Drawing.Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
                Case SizeMode.Fill ' Bild wird so skaliert, dass es das Control ausfüllt und zentriert gezeichnet
                    ' X- und Y-Position wie bei Zoom
                    Return New System.Drawing.Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
            End Select
        End Function

#End Region

    End Class

End Namespace
